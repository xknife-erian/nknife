using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Module.Chess
{
    /// <summary>
    /// 一个棋局的Reader监听接口，面向<see>ChessPGNReader</see>，
    /// 监听ChessPGNReader开始解析时发生的一系列事件。
    /// </summary>
    public interface IPGNReaderEvents
    {
        /// <summary>
        /// 当一个新棋局开始时
        /// </summary>
        /// <param name="iParser"></param>
        void NewGame(IPGNReader iParser);
        /// <summary>
        /// 当头部信息解析完毕时
        /// </summary>
        /// <param name="iParser"></param>
        void ExitHeader(IPGNReader iParser);
        /// <summary>
        /// 当解析到一个变招时
        /// </summary>
        /// <param name="iParser"></param>
        void EnterVariation(IPGNReader iParser);
        /// <summary>
        /// 当一个变招解析完成时
        /// </summary>
        /// <param name="iParser"></param>
        void ExitVariation(IPGNReader iParser);
        /// <summary>
        /// 当解析开始时
        /// </summary>
        /// <param name="iParser"></param>
        void Starting(IPGNReader iParser);
        /// <summary>
        /// 当解析结束时
        /// </summary>
        /// <param name="iParser"></param>
        void Finished(IPGNReader iParser);
        /// <summary>
        /// 当解析Tag字段时
        /// </summary>
        /// <param name="iParser"></param>
        void TagParsed(IPGNReader iParser);
        /// <summary>
        /// 当解析Nag（Numeric Annotation Glyph）时
        /// </summary>
        /// <param name="iParser"></param>
        void NagParsed(IPGNReader iParser);
        /// <summary>
        /// 当解析到行棋时
        /// </summary>
        /// <param name="iParser"></param>
        void StepParsed(IPGNReader iParser);
        /// <summary>
        /// 当解析到一条评论时
        /// </summary>
        /// <param name="iParser"></param>
        void CommentParsed(IPGNReader iParser);
        /// <summary>
        /// 当解析到棋局结束时(1/2-1/2, 1-0, 0-1)
        /// </summary>
        /// <param name="iParser"></param>
        void EndMarker(IPGNReader iParser);
    }
}
