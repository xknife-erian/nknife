﻿using System;
using System.IO;
using System.Text;

namespace NKnife.Wrapper.Mp3
{
    /// <summary>
    ///     获取MP3文件的ID3 V1版本的TAG信息的类
    /// </summary>
    public class Mp3TagId3V1
    {
        /// <summary>
        ///     流派分类，共有148种，只列举了前21种，应用前应补充
        /// </summary>
        private readonly string[] _genreArray =
        {
            "Blues", "Classic Rock", "Country", "Dance", "Disco", "Funk", "Grunge", "Hip-Hop",
            "Jazz", "Metal", "New Age", "Oldies", "Other", "Pop", "R&B", "Rap", "Reggae", "Rock", "Techno",
            "Industrial", "Alternative"
        };

        private readonly string _album = string.Empty;
        private readonly string _artist = string.Empty;
        private readonly string _comment = string.Empty;
        private readonly string _genre;
        private readonly string _publishYear = string.Empty;

        private readonly string _title = string.Empty;

        /// <summary>
        /// </summary>
        /// <param name="mp3FilePath">MP3文件的完整路径</param>
        public Mp3TagId3V1(string mp3FilePath)
        {
            var tagBody = new byte[128];

            if (!File.Exists(mp3FilePath))
                throw new FileNotFoundException("指定的MP3文件不存在！", mp3FilePath);

            //读取MP3文件的最后128个字节的内容
            using (var fs = new FileStream(mp3FilePath, FileMode.Open, FileAccess.Read))
            {
                fs.Seek(-128, SeekOrigin.End);
                fs.Read(tagBody, 0, 128);
                fs.Close();
            }

            //取TAG段的前三个字节
            string tagFlag = Encoding.Default.GetString(tagBody, 0, 3);

            //如果没有TAG信息
            if (!"TAG".Equals(tagFlag, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new InvalidDataException("指定的MP3文件没有TAG信息！");
            }

            //按照MP3 ID3 V1 的tag定义，依次读取相关的信息
            _title = Encoding.Default.GetString(tagBody, 3, 30).TrimEnd();
            _artist = Encoding.Default.GetString(tagBody, 33, 30).TrimEnd();
            _album = Encoding.Default.GetString(tagBody, 62, 30).TrimEnd();
            _publishYear = Encoding.Default.GetString(tagBody, 93, 4).TrimEnd();
            _comment = Encoding.Default.GetString(tagBody, 97, 30);
            Int16 g = tagBody[127];
            _genre = g >= _genreArray.Length ? "未知" : _genreArray[g];
        }

        /// <summary>
        ///     标题
        /// </summary>
        public string Title
        {
            get { return _title; }
        }

        /// <summary>
        ///     艺术家，演唱者
        /// </summary>
        public string Artist
        {
            get { return _artist; }
        }

        /// <summary>
        ///     所属专辑
        /// </summary>
        public string Album
        {
            get { return _album; }
        }

        /// <summary>
        ///     发行年份
        /// </summary>
        public string PublishYear
        {
            get { return _publishYear; }
        }

        /// <summary>
        ///     备注、说明
        /// </summary>
        public string Comment
        {
            get
            {
                if (_comment.Length == 30)
                {
                    //如果是 ID3 V1.1的版本，那么comment只占前28个byte，第30个byte存放音轨信息
                    if (TagVersion(_comment)) return _comment.Substring(0, 28).TrimEnd();
                }
                return _comment.TrimEnd();
            }
        }

        /// <summary>
        ///     音轨
        /// </summary>
        public string Track
        {
            get
            {
                if (_comment.Length == 30)
                {
                    //如果是 ID3 V1.1的版本，读取音轨信息
                    if (TagVersion(_comment)) return ((int) _comment[29]).ToString();
                }

                return string.Empty;
            }
        }

        /// <summary>
        ///     流派
        /// </summary>
        public string Genre
        {
            get { return _genre; }
        }

        /// <summary>
        ///     判断MP3的TAG信息的版本，是V1.0 还是 V1.1
        /// </summary>
        /// <param name="comment"></param>
        /// <returns>true表示是 1.1，false表示是 1.0</returns>
        private bool TagVersion(string comment)
        {
            if (comment[28].Equals('\0') && comment[29] > 0 || comment[28] == 32 && comment[29] != 32)
                return true;
            return false;
        }
    }
}