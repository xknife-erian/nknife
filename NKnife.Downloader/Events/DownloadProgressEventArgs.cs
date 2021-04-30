namespace NKnife.Downloader.Events
{
    /// <summary>
    /// Download progress changed event arguments
    /// </summary>
    public class DownloadProgressChangedEventArgs
    {
        /// <summary>
        /// Creates instance of the class
        /// </summary>
        /// <param name="progress">Current download progress</param>
        /// <param name="speed">Current download speed</param>
        public DownloadProgressChangedEventArgs(int progress, int speed)
        {
            this.Progress = progress;
            this.Speed = speed;
        }
        /// <summary>
        /// Gets the current progress
        /// </summary>
        public int Progress { get; }

        /// <summary>
        /// Gets the current speed
        /// </summary>
        public int Speed { get; }
    }
}
