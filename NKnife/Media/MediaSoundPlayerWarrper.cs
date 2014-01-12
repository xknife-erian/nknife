using System.Media;

namespace NKnife.Media
{
    /// <summary>
    /// 2012-06-21 最简单的实现，目前无实用价值，仅供测试
    /// 通过System.Media.SoundPlayer类实现，仅能播放wav文件
    /// </summary>
    public class MediaSoundPlayerWarrper
    {
        private readonly SoundPlayer _MusicPlayer;
        public MediaSoundPlayerWarrper()
        {
            _MusicPlayer = new SoundPlayer();
        }

        public void Play(string fileName)
        {
            _MusicPlayer.SoundLocation = fileName;
            _MusicPlayer.PlayLooping();
        }
    }
}
