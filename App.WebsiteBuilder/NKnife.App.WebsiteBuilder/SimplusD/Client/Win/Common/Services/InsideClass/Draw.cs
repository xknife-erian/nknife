using System.IO;
using System.Drawing;
using System.Diagnostics;

namespace Jeelu.SimplusD.Client.Win
{
    static public partial class Service
    {
        static public class Draw
        {
            static private Image _imgSignExcalmatory;
            static private Image _imgSignQuestion;
            /// <summary>
            /// 给图片打标记。目前根据SignType的不同可以打两种：感叹号、问号
            /// </summary>
            static public void DrawSign(Image image, SignType type)
            {
                Image signImg = null;
                Debug.Assert(image != null);

                ///获取打标记的标记图片
                switch (type)
                {
                    case SignType.ExcalmatoryPoint:
                        {
                            if (_imgSignExcalmatory == null)
                            {
                                string strSignImg = System.IO.Path.Combine(PathService.SoftwarePath, @"Image\Expand\ExcalmatoryPoint.png");
                                _imgSignExcalmatory = Image.FromFile(strSignImg);
                               // _imgSignExcalmatory = ResourceService.GetResourceImage("sd.img.ExcalmatoryPoint");
                            }
                            signImg = _imgSignExcalmatory;
                            break;
                        }
                    case SignType.QuestionPoint:
                        {
                            if (_imgSignQuestion == null)
                            {
                                string strSignImg = System.IO.Path.Combine(PathService.SoftwarePath, @"Image\Expand\QuestionPoint.png");
                                _imgSignQuestion = Image.FromFile(strSignImg);
                                //_imgSignQuestion = ResourceService.GetResourceImage("sd.img.QuestionPoint");
                            }
                            signImg = _imgSignQuestion;
                            break;
                        }
                    default:
                        Debug.Assert(false);
                        break;
                }

                ///将标记图片Draw上去
                Graphics g = Graphics.FromImage(image);
                g.DrawImage(signImg, image.Width - signImg.Width, image.Height - signImg.Height,signImg.Width,signImg.Height);
                g.Flush();
                g.Dispose();
            }
            static public string GetSignKey(string srcKey, SignType type)
            {
                return srcKey + "&" + type;
            }

            /// <summary>
            /// 根据原图片返回一个带有服务器状态的图标
            /// </summary>
            static public Image DrawServerSign(Image image, ServerState state)
            {
                Image signImg = null;
                Debug.Assert(image != null);

                const int space = 0;
                const int serverSignWidth = 6;

                ///创建一个将要返回的图片
                Bitmap resultImg = new Bitmap(serverSignWidth + image.Width + space, image.Height);

                ///将两个图片都画在将要返回的图片上
                Graphics g = Graphics.FromImage(resultImg);

                ///先画原本的图标
                g.DrawImage(image, serverSignWidth + space, 0,image.Width,image.Height);

                ///打标记
                if (state != ServerState.None)
                {
                    string signImgPath = System.IO.Path.Combine(PathService.SoftwarePath,
                        string.Format(@"Image\ServerState\{0}.png", state));
                    signImg = Image.FromFile(signImgPath);
                    g.DrawImage(signImg, 0, (image.Height-signImg.Height)/2,signImg.Width,signImg.Height);
                }
                g.Flush();
                g.Dispose();

                return resultImg;
            }

            static public string GetServerSignKey(string srcKey, ServerState state)
            {
                return state + "&" + srcKey;
            }
        }
    }
}
