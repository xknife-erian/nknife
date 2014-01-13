
//http://blog.csdn.net/21aspnet/archive/2007/03/23/1539211.aspx

//原创  C#语音朗读 收藏
//电脑朗读”(英文)一个很好的触发点，通过它可以实现电子小说阅读、英文听力测试、英文单词学习...
//    下面的Speech已对MSTTS作了简单封装。
 
//1.安装好MSTTS（如果你有装金山词霸，系统就已经安装了,在C:\windows\speech\下）,可以在winntspeech中打到vtxtauto.tlb文件； 没有的话，就要装TTS和SAPI在金山的碟上有这两个文件！  
   
//  TTS：Microsoft   Text-To-Speech   Engine     (全文朗读引擎)  
//  SAPI:Microsoft   Speech   API                             （语音API）

//2.用.Net SDK自带的tlbimp工具把vtxtauto.tlb转换成.dll格式:
//  tlbimp vtxtauto.tlb /silent /namespace:mstts /out:mstts.dll
//  这时的mstts.dll已成为.net framework运行库的一个类。
//3.编写一个封装vtxtauto的简单类:Speech .

//using System;
//using mstts;  //MSTTS名称空间
//namespace Bedlang
//{      //定义名称空间
//    public class Speech
//    {
//        private VTxtAuto VTxtAutoEx;
//        public Speech()
//        {
//            VTxtAutoEx = new VTxtAuto();
//            VTxtAutoEx.Register(" ", " "); //注册COM组件   
//        }
//        public void Speak(String text)
//        {
//            VTxtAutoEx.Speak(text, 0);   //发音
//        }
//    }
//}