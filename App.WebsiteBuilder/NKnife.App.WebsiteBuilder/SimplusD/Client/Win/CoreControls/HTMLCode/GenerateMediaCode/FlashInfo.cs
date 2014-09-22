using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;
using System.Drawing;

namespace Jeelu.SimplusD.Client.Win
{
    public class FlashInfo
    {
        private int width, height, version, frameCount, fileLength;
        private float frameRate;
        private bool isCompressed;

        public FlashInfo(string filename)
        {
            if (!File.Exists(filename))
                throw new FileNotFoundException(filename);
            FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            BinaryReader reader = new BinaryReader(stream);
            try
            {
                if (stream.Length < 8)
                    throw new InvalidDataException("文件不是 Flash 文件格式");
                string flashMark = new string(reader.ReadChars(3));
                if (flashMark != "FWS" && flashMark != "CWS")
                    throw new InvalidDataException("文件不是 Flash 文件格式");
                isCompressed = flashMark == "CWS";
                version = Convert.ToInt32(reader.ReadByte());
                fileLength = reader.ReadInt32();
                byte[] dataPart = new byte[stream.Length - 8];
                reader.Read(dataPart, 0, dataPart.Length);
                MemoryStream dataStream = new MemoryStream(dataPart);
                try
                {
                    if (isCompressed)
                    {
                        ManagedZLib.ManagedZLib.Initialize();
                        ManagedZLib.CompressionStream zlibStream = new ManagedZLib.CompressionStream(dataStream, ManagedZLib.CompressionOptions.Decompress);
                        BinaryReader zlibReader = new BinaryReader(zlibStream);
                        MemoryStream memoryStreamDecompressed = null;
                        try
                        {
                            byte[] decompressedPart = new byte[fileLength - 8];
                            zlibReader.Read(decompressedPart, 0, decompressedPart.Length);
                            memoryStreamDecompressed = new MemoryStream(decompressedPart);
                            ProcessCompressedPart(memoryStreamDecompressed);
                        }
                        finally
                        {
                            if (memoryStreamDecompressed != null)
                                memoryStreamDecompressed.Close();
                            zlibReader.Close();
                            zlibStream.Close();
                            ManagedZLib.ManagedZLib.Terminate();
                        }
                    }
                    else
                        ProcessCompressedPart(dataStream);
                }
                finally
                {
                    dataStream.Close();
                }
            }
            finally
            {
                reader.Close();
                stream.Close();
            }
        }

        private void ProcessCompressedPart(MemoryStream stream)
        {
            BinaryReader reader = new BinaryReader(stream);
            try
            {
                byte[] rect;
                int nbits, totalBits, totalBytes;
                nbits = reader.ReadByte() >> 3;
                totalBits = nbits * 4 + 5;
                totalBytes = totalBits / 8;
                if (totalBits % 8 != 0)
                    totalBytes++;
                reader.BaseStream.Seek(-1, SeekOrigin.Current);
                rect = reader.ReadBytes(totalBytes);
                frameRate = float.Parse(string.Format("{1}.{0}", reader.ReadByte(), reader.ReadByte()));
                frameCount = Convert.ToInt32(reader.ReadInt16());
                BitArray bits = new BitArray(rect);
                bool[] reversedBits = new bool[bits.Length];
                for (int i = 0; i < totalBytes; i++)
                {
                    int count = 7;
                    for (int j = 8 * i; j < 8 * (i + 1); j++)
                    {
                        reversedBits[j + count] = bits[j];
                        count -= 2;
                    }
                }
                bits = new BitArray(reversedBits);
                StringBuilder sbField = new StringBuilder(bits.Length);
                for (int i = 0; i < bits.Length; i++)
                    sbField.Append(bits[i] ? "1" : "0");
                string result = sbField.ToString();
                string widthBinary = result.Substring(nbits + 5, nbits);
                string heightBinary = result.Substring(3 * nbits + 5, nbits);
                width = Convert.ToInt32(FlashInfo.BinaryToInt64(widthBinary) / 20);
                height = Convert.ToInt32(FlashInfo.BinaryToInt64(heightBinary) / 20);
            }
            finally
            {
                reader.Close();
            }
        }

        private static long BinaryToInt64(string binaryString)
        {
            if (string.IsNullOrEmpty(binaryString))
                throw new ArgumentNullException();
            long result = 0;
            for (int i = 0; i < binaryString.Length; i++)
            {
                result = result * 2;
                if (binaryString[i] == '1')
                    result++;
            }
            return result;
        }

        public int Width
        {
            get
            {
                return this.width;
            }
        }

        public int Height
        {
            get
            {
                return this.height;
            }
        }

        public int FileLength
        {
            get
            {
                return this.fileLength;
            }
        }

        public int Version
        {
            get
            {
                return this.version;
            }
        }

        public float FrameRate
        {
            get
            {
                return this.frameRate;
            }
        }

        public int FrameCount
        {
            get
            {
                return this.frameCount;
            }
        }

        public bool IsCompressed
        {
            get
            {
                return this.isCompressed;
            }
        }
    }

}
