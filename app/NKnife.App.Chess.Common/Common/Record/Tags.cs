using System.Collections;
using System.Collections.Generic;
using System.Text;
using NKnife.Chesses.Common.Base;
using NKnife.Chesses.Common.Interface;

namespace NKnife.Chesses.Common.Record
{
    /* 标签对部分
     * 
     * 标签对部分由一系列空的或更多的“标签对”组成。而标签对则由左右方括号、象征符号、字符串
     * 符号组成。象征符号是标签的名字，而字符串符号就是如之一起的标签值，它们的表示法都有一
     * 定标准。在一个标签对部分里不能出现同一个标签名多于一次以上。标签名是分大小写的，所有
     * 用于档案储存的标签名都是以大写字母开头。
     * 
     * 七个标签项
     * 这是最基本七个标签项目，实际上输入时，顺序不一定这样严格，而且还可能根据需要进行扩展
     * 和增加。
        1) Event 比赛名。比如：
            [Event "FIDE World Championship"] 　//国际棋联世界锦标赛
            [Event "Casual Game"]　 　　　　　　//即兴比赛
            [Event "?"] 　　　　　　　　　　　　//比赛名不详
        2) Site 比赛地点。比如：
            [Site "New York City, NY USA"] 　   //美国纽约
            [Site "Beijing, China"] 　　　　    //中国北京
        3) Date 该局开始时日期，使用当地时间。比如：
            [Date "2001.01.01"] 　              //一看就懂了吧！
            [Date "1993.??.??"] 　              //1993年但月、日不详
        4) Round 该局在比赛中的轮次。比如：
            [Round "1"] 
            [Round "3.1"] 　                    //第3大轮的第1小轮 ，以“.”分开；
            [Round "?"] 　　                    //轮次不详
        5) White 白方棋手名字。有一定的书写规则，不同民族的人的姓名不好一概而论，但至少看懂
     *     是不难的。电脑棋手则在名字后加上版本信息。比如：
            [White "Tal, Mikhail N."]　         //米哈依尔·N·塔尔
            [White "Kasparov, Garry"] 　        //加里·卡斯帕罗夫
        6) Black 黑方棋手名字，与白方的规则没有区别。
        7) Result 该局结果。比如：
            [Result "0-1"]　 　　               //黑胜
            [Result "1-0"] 　　　               //白胜
            [Result "1/2-1/2"] 　               //和棋
            [Result "*"]　 　　　               //可能还在进行，可能该局作废，可能其它原因
    */

    /* 常见标签对
        [Event "CHN-RUS Summit Men"]//赛事名，中国-俄罗斯最高对抗赛男子组
        [EventDate "2001.09.07"] 　 //赛事开始日期
        [Site "Shanghai"]　         //地点，上海
        [Date "2001.09.10"] 　      //该局日期
        [Round "4"] 　              //第4轮
        [White "Ye Jiangchuan"] 　  //白方 叶江川
        [Black "Dreev, Alexey"] 　  //黑方 A·德里耶夫
        [Result "1-0"] 　           //对局结果白胜
        [Opening "Caro-Kann: classical, 6.h4"]　//开局名，卡罗-卡恩防御：经典变例，6.h4
        [ECO "B19"] 　      //ECO开局编号为B19
        [NIC "CK.11"] 　    //"New In Chess"编号
        [WhiteElo "2677"] 　//白方国际ELO等级分
        [BlackElo "2690"] 　//黑方国际ELO等级分
        [PlyCount "75"] 　  //双方共走了75步(不是回合)
     */

    /// <summary>
    /// 棋局记录的标签对部分
    /// </summary>
    public class Tags : Definition, IChessItem
    {
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in this._Definitions)
            {
                sb.AppendFormat("[{0} \"{1}\"]", item.Key, item.Value).AppendLine();
            }
            return sb.ToString();
        }

        #region IItem

        public string ItemType => "Tags";

        public string Value => this.ToString();

        #endregion

    }

    /* 补充标签项
        　
　　        补充标签项不是必须的，实际上来自不同地方的PGN棋谱往往不一样。
        　
        4-1 棋手相关信息
            1) WhiteTitle 白方头衔，例如FM、IM、GM 
            2) WhiteElo 白方国际等级分，指国际棋联的ELO等级分
            3) WhiteNA 白方email或其它网上地址
            4) WhiteType 白方类型，human指人类；program指软件(电脑)
　　        黑方写法雷同；如果这些补充信息欠奉，则用“-”表示。
        　
        4-2 赛事相关信息
            1) EventDate 赛事开始日子，与标签对里的Date不同，这是指整个赛事的开始日
            2) EventSponsor 赛事赞助者
            3) Section 区域，比如公开还是保留
            4) Stage 阶段，比如预赛还是决赛
            5) Board 台数，也就是表示团体赛或车轮战中的第几台(盘)
            6) Time 以“小时：分钟：秒钟”表示该局开始的当地时间
        　
        4-3 开局信息
            1) Opening 以字符串表示传统的开局名称
            2) Variation 变例名，以后将有提炼
            3) SubVariation 分支变例名
            4) ECO “开局百科”(Encyclopedia of Chess Openings)里定义的开局编号
            5) NIC 著名的“New in Chess”数据库里定义的开局编号
        　
        4-4 时限
　　        以TimeControl作为标签名，有6种不同表示，举例：
            1) [TimeControl "?"] 　     //时限不知道
            2) [TimeControl "-"] 　     //无限时
            3) [TimeControl "40/9000"]  //9000秒内(即2个半小时)走满40步
            4) [TimeControl "300"] 　   //每方300秒包时制对局，也就是5分钟快棋
            5) [TimeControl "4500+60"]  //用于“加时制”对局，这里是4500秒(90分钟)基础时限，然后每走一步往加60秒
            6) [TimeControl "*180"]     //每步限时的“沙漏制”时限，这里星号后面的数字是秒数，即每一步都要在180秒之内走完
　　        其实还可以定义额外的时限表示法。
        　
        4-5 开始局面
　　        默认的开始局面，当然就是对局最初的原始局面。不过假如对局规定是从某一局面开始的，就会用到如下标签项。
            1) SetUp 如果数值是1，表示该局开始局面是“摆”出来的
            2) FEN 以“福斯夫-爱德华兹记号法”(Forsyth-Edwards Notation) 表示开始局面，关于FEN的表示法，下一篇再来解释
        　
        4-6 对局结论
　　        以Termination标签名表示Result标签项没有能披露的额外信息
            1) [Termination "abandoned"]　 　　　 //该局放弃
            2) [Termination "adjudication"] 　　　//结果由第三方宣判
            3) [Termination "death"] 　　　　　　 //哦……
            4) [Termination "emergency"]　 　　　 //出现无法预料的情况
            5) [Termination "normal"]　 　　　　　//常规结束
            6) [Termination "rules infraction"] 　//失利方违规
            7) [Termination "time forfeit"]　 　　//失利方超时
            8) [Termination "unterminated"]　 　　//没有结束
        　
        4-7 其它
　　        这是不好归入以上各类的标签项。
            1) Annotator 评注者(们)的名字
            2) Mode 这是下该局的方式，比如OTB代表棋盘上，PM代表通过书面邮件，EM代表通过电子邮件，ICS指在网上站点下的，
               TC代表通过通常的长途电讯
            3) PlyCount 表示该局的步数，严格来说是指“半”步数
     */
} 
