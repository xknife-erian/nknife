using System;
using System.Collections.Generic;
using System.Threading;
using NKnife.Upgrade4Github.Util.Download.Enums;
using NKnife.Upgrade4Github.Util.Download.Events;
using NKnife.Upgrade4Github.Util.Download.Interfaces;
using NKnife.Upgrade4Github.Util.Download.Util;

namespace NKnife.Upgrade4Github.Util.Download
{
    /// <summary>
    ///     Provides methods to create and process download queue
    /// </summary>
    public class DownloadQueue : IQueue, IDisposable
    {
        #region Variables

        private HttpDownloader _downloader;
        private readonly List<QueueElement> _elements;
        private QueueElement _currentElement;
        private int _progress;
        private bool _queuePaused, _startEventRaised;

        /// <summary>
        ///     Occurs when queue element's progress is changed
        /// </summary>
        public event EventHandler QueueProgressChanged;

        /// <summary>
        ///     Occurs when the queue is completely completed
        /// </summary>
        public event EventHandler QueueCompleted;

        /// <summary>
        ///     Occurs when the queue element is completed
        /// </summary>
        public event QueueElementCompletedEventHandler QueueElementCompleted;

        /// <summary>
        ///     Occurs when the queue has been started
        /// </summary>
        public event EventHandler QueueElementStartedDownloading;

        #endregion

        #region Constructor + Destructor

        /// <summary>
        ///     Creates a queue and initializes resources
        /// </summary>
        public DownloadQueue()
        {
            _downloader = null;
            _elements = new List<QueueElement>();
            _queuePaused = true;
            CurrentDownloadSpeed = 0;
        }

        /// <summary>
        ///     Destructor for the object
        /// </summary>
        ~DownloadQueue()
        {
            Cancel();
        }

        #endregion

        #region Events

        private void Downloader_ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            _progress = e.Progress;
            CurrentProgress = _progress;
            CurrentDownloadSpeed = e.Speed;
        }

        private void Downloader_Completed(object sender, EventArgs e)
        {
            QueueElementCompleted?.Invoke(this, new QueueElementCompletedEventArgs(CurrentIndex, _currentElement));
            for (var i = 0; i < _elements.Count; i++)
            {
                if (!_elements[i].Equals(_currentElement))
                    continue;
                _elements[i] = new QueueElement
                {
                    Id = _elements[i].Id,
                    Url = _elements[i].Url,
                    Destination = _elements[i].Destination,
                    Completed = true
                };
                break;
            }

            CreateNextDownload();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the number of elements in the queue
        /// </summary>
        public int QueueLength => _elements.Count;

        /// <summary>
        ///     Gets the index number of the element that is currently processing
        /// </summary>
        public int CurrentIndex
        {
            get
            {
                for (var i = 0; i < _elements.Count; i++)
                    if (!_elements[i].Completed)
                        return i;
                return -1;
            }
        }

        /// <summary>
        ///     Gets the download progress of the current download process
        /// </summary>
        public int CurrentProgress
        {
            get => _progress;
            private set
            {
                _progress = value;
                if (QueueProgressChanged != null && CurrentIndex >= 0 && !_queuePaused)
                    QueueProgressChanged(this, EventArgs.Empty);
                if (QueueElementStartedDownloading != null && _progress > 0 && !_startEventRaised)
                {
                    QueueElementStartedDownloading(this, EventArgs.Empty);
                    _startEventRaised = true;
                }
            }
        }

        /// <summary>
        ///     Gets the download speed of the current download progress
        /// </summary>
        public int CurrentDownloadSpeed { get; private set; }

        /// <summary>
        ///     Gets the Range header value of the current download
        /// </summary>
        public bool CurrentAcceptRange => _downloader.AcceptRange;

        /// <summary>
        ///     Gets the State of the current download
        /// </summary>
        public DownloadState State => _downloader.State;

        #endregion

        #region Methods

        /// <summary>
        ///     Adds new download elements into the queue
        /// </summary>
        /// <param name="url">The URL source that contains the object will be downloaded</param>
        /// <param name="destPath">The destination path to save the downloaded file</param>
        public void Add(string url, string destPath)
        {
            _elements.Add(new QueueElement
            {
                Id = Guid.NewGuid().ToString(),
                Url = url,
                Destination = destPath
            });
        }

        /// <summary>
        ///     Deletes the queue element at the given index
        /// </summary>
        /// <param name="index">The index of the element that will be deleted</param>
        public void Delete(int index)
        {
            if (_elements[index].Equals(_currentElement) && _downloader != null)
            {
                _downloader.Cancel();
                _currentElement = new QueueElement {Url = ""};
            }

            _elements.RemoveAt(index);
            if (!_queuePaused)
                CreateNextDownload();
        }

        /// <summary>
        ///     Deletes all elements in the queue
        /// </summary>
        public void Clear()
        {
            Cancel();
        }

        /// <summary>
        ///     Starts the queue async
        /// </summary>
        public void StartAsync()
        {
            CreateNextDownload();
        }

        /// <summary>
        ///     Stops and deletes all elements in the queue
        /// </summary>
        public void Cancel()
        {
            _downloader?.Cancel();
            Thread.Sleep(100);
            _elements.Clear();
            _queuePaused = true;
        }

        /// <summary>
        ///     The queue process resumes
        /// </summary>
        public async void ResumeAsync()
        {
            if (_currentElement.Url == "")
            {
                CreateNextDownload();
                return;
            }

            await _downloader.ResumeAsync();
            _queuePaused = false;
        }

        /// <summary>
        ///     The queue process pauses
        /// </summary>
        public void Pause()
        {
            _downloader.Pause();
            _queuePaused = true;
        }

        /// <summary>
        ///     Removes all resources used
        /// </summary>
        public void Dispose()
        {
            Cancel();
        }

        #endregion

        #region FromGithub Methods

        private async void CreateNextDownload()
        {
            var elt = GetFirstNotCompletedElement();
            if (string.IsNullOrEmpty(elt.Url))
                return;
            _downloader = new HttpDownloader(elt.Url, elt.Destination);
            _downloader.DownloadCompleted += Downloader_Completed;
            _downloader.DownloadProgressChanged += Downloader_ProgressChanged;
            await _downloader.StartAsync();
            _currentElement = elt;
            _queuePaused = false;
            _startEventRaised = false;
        }

        private QueueElement GetFirstNotCompletedElement()
        {
            foreach (var t in _elements)
            {
                if (!t.Completed)
                    return t;
            }

            QueueCompleted?.Invoke(this, new EventArgs());
            return new QueueElement();
        }

        #endregion
    }
}