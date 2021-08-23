using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace NKnife.Win
{
    public sealed partial class Api
    {
        /// <summary>
        /// API播放Wav声音文件
        /// </summary>
        public class Sounder
        {
            //System.Media.SoundPlayer sp = new System.Media.SoundPlayer();

            [DllImport("winmm")]
            static extern bool PlaySound(string szSound, IntPtr hMod, PlaySoundFlags flags);

            [Flags]
            enum PlaySoundFlags : int
            {
                /// <summary>
                /// 同步播放声音
                /// </summary>
                SndSync = 0x0000,    /* play synchronously (default) */ //同步
                /// <summary>
                /// 异步播放声音
                /// </summary>
                SndAsync = 0x0001,    /* play asynchronously */ //异步
                SndNodefault = 0x0002,    /* silence (!default) if sound not found */
                SndMemory = 0x0004,    /* pszSound points to a memory file */
                SndLoop = 0x0008,    /* loop the sound until next sndPlaySound */
                SndNostop = 0x0010,    /* don't stop any currently playing sound */
                SndNowait = 0x00002000, /* don't wait if the driver is busy */
                SndAlias = 0x00010000, /* name is a registry alias */
                SndAliasId = 0x00110000, /* alias is a predefined ID */
                SndFilename = 0x00020000, /* name is file name */
                SndResource = 0x00040004    /* name is resource name or atom */
            }

            /// <summary>
            /// 同步播放指定的Wav文件
            /// </summary>
            /// <param name="wavFilename"></param>
            public static void SyncWavPlay(string wavFilename)
            {
                PlaySound(wavFilename, IntPtr.Zero, PlaySoundFlags.SndSync);
            }

            /// <summary>
            /// 停止同步播放Wav文件
            /// </summary>
            public static void SyncWavStop()
            {
                PlaySound(null, IntPtr.Zero, PlaySoundFlags.SndSync);
            }

            /// <summary>
            /// 异步播放指定的Wav文件
            /// </summary>
            /// <param name="wavFilename"></param>
            public static void AsyncWavPlay(string wavFilename)
            {
                PlaySound(wavFilename, IntPtr.Zero, PlaySoundFlags.SndAsync);
            }

            /// <summary>
            /// 停止异步播放Wav文件
            /// </summary>
            public void AsyncWavStop()
            {
                PlaySound(null, IntPtr.Zero, PlaySoundFlags.SndAsync);
            }
        }
    }
}