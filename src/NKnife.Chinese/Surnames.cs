using System;
using System.Collections.Generic;
using System.Linq;
using NKnife.Util;

namespace NKnife.Chinese
{
    public class Surnames
    {
        /*
         * https://www.gov.cn/xinwen/2021-02/08/content_5585906.htm
         * 《二〇二〇年全国姓名报告》发布
         */
        private static string[] s_chineseSurnames1To100 =
        {
            "王", "李", "张", "刘", "陈", "杨", "黄", "赵", "吴", "周",
            "徐", "孙", "马", "朱", "胡", "郭", "何", "林", "罗", "高",
            "郑", "梁", "谢", "宋", "唐", "许", "邓", "韩", "冯", "曹",
            "彭", "曾", "肖", "田", "董", "潘", "袁", "蔡", "余", "蒋",
            "叶", "于", "杜", "苏", "程", "魏", "丁", "吕", "卢", "任",
            "姚", "沈", "钟", "姜", "崔", "谭", "范", "陆", "汪", "廖",
            "石", "金", "韦", "贾", "夏", "付", "方", "白", "邹", "熊",
            "孟", "秦", "邱", "江", "尹", "薛", "闫", "段", "雷", "侯",
            "龙", "黎", "史", "陶", "贺", "毛", "郝", "顾", "龚", "邵",
            "万", "覃", "严", "武", "钱", "戴", "莫", "孔", "向", "常"
        };

        private static readonly string[] s_chineseSurnames101To200 =
        {
            "赖", "洪", "汤", "萧", "傅", "温", "康", "施", "文", "牛",
            "樊", "葛", "邢", "安", "齐", "易", "乔", "伍", "阎", "庞",
            "颜", "倪", "庄", "聂", "章", "鲁", "岳", "翟", "欧", "殷",
            "詹", "骆", "申", "耿", "关", "兰", "焦", "俞", "左", "柳",
            "甘", "祝", "包", "宁", "欧阳", "尚", "符", "舒", "阮", "柯",
            "纪", "梅", "童", "凌", "毕", "单", "季", "裴", "霍", "涂",
            "成", "苗", "谷", "盛", "曲", "翁", "冉", "蓝", "路", "游",
            "辛", "靳", "管", "柴", "蒙", "鲍", "华", "喻", "祁", "蒲",
            "房", "占", "屈", "饶", "解", "牟", "艾", "尤", "阳", "时",
            "穆", "农", "司", "卓", "古", "吉", "缪", "简", "车", "项"
        };

        private static readonly string[] s_chineseSurnames201To300 =
        {
            "连", "芦", "麦", "褚", "娄", "窦", "戚", "岑", "景", "党",
            "宫", "费", "卜", "冷", "晏", "席", "卫", "米", "柏", "宗",
            "瞿", "桂", "全", "佟", "应", "臧", "闵", "苟", "邬", "边",
            "卡", "姬", "师", "和", "仇", "栾", "隋", "商", "沙", "刁",
            "荣", "巫", "寇", "桑", "郎", "甄", "丛", "仲", "虞", "敖",
            "巩", "明", "佘", "池", "查", "邝", "麻", "苑", "迟", "官",
            "封", "谈", "匡", "鞠", "代", "惠", "荆", "乐", "翼", "郁",
            "胥", "南", "班", "储", "原", "栗", "燕", "楚", "鄢", "劳",
            "谌", "钮", "奚", "皮", "粟", "洗", "蔺", "楼", "盘", "满",
            "闻", "昌", "厉", "伊", "仝", "区", "郜", "候", "阚", "花"
        };

        private static readonly string[] s_chineseSurnames301To400 =
        {
            "权", "强", "帅", "习", "屠", "豆", "朴", "盖", "练", "廉",
            "禹", "井", "祖", "漆", "巴", "丰", "支", "卿", "国", "狄",
            "平", "计", "索", "宣", "晋", "相", "初", "门", "云", "容",
            "敬", "来", "扈", "显", "芮", "都", "普", "网", "浦", "戈",
            "伏", "鹿", "薄", "邸", "雍", "辜", "羊", "阿", "乌", "母",
            "求", "亓", "修", "部", "赫", "杭", "况", "那", "宿", "鲜",
            "印", "褪", "隆", "茹", "诸", "战", "慕", "危", "玉", "银",
            "亢", "嵇", "公", "哈", "湛", "宾", "戎", "勾", "茅", "利",
            "於", "丘", "居", "竺", "揭", "干", "但", "尉", "冶", "斯",
            "元", "束", "檀", "衣", "信", "展", "阴", "昝", "智", "上官"
        };

        private static readonly string[] s_chineseSurnames401To500 =
        {
            "幸", "奉", "植", "衡", "富", "尧", "闭", "由", "郗", "水",
            "融", "家", "松", "山", "贝", "凤", "荀", "訾", "裘", "濮",
            "宦", "逢", "寿", "弓", "贵", "能", "步", "滑", "锤", "岩",
            "弘", "充", "通", "广", "邴", "养", "子", "蓬", "糜", "變",
            "鄂", "暴", "双", "汲", "别", "终", "经", "韶", "从", "羿",
            "怀", "红", "西", "蔚", "达", "蓟", "邮", "宓", "隗", "后",
            "咸", "琥", "木", "才", "牧", "前", "巢", "毋", "沃", "没",
            "益", "堵", "慎", "库", "乜", "莘", "苍", "凡", "越", "扶",
            "暨", "酆", "法", "腾", "伯", "位", "须", "禄", "桓", "呼",
            "拉", "扎", "汝", "青", "藩", "长", "布", "贡", "湖", "刀"
        };

        private static readonly string[] s_chineseSurnames501To600 =
        {
            "令狐", "宇", "庾", "东", "次", "佴", "矫", "正", "多", "格",
            "宝", "加", "待", "海", "藏", "保", "庚", "旦", "诸葛", "赏",
            "巨", "延", "司徒", "自", "德", "尼", "渠", "过", "历", "雒",
            "铁", "轩", "年", "泮", "籍", "仁", "操", "字", "户", "刑",
            "旷", "黑", "良", "靖", "刷", "上", "虎", "台", "郏", "鱼",
            "泽", "其", "琚", "素", "蹇", "宛", "税", "畅", "侍", "招",
            "谯", "扬", "赛", "百", "生", "种", "娜", "玄", "买", "伦",
            "萨", "茆", "小", "续", "里", "纳", "么", "洛", "未", "度",
            "依", "旺", "营", "太", "㕁", "司马", "英", "拓", "大", "永",
            "要", "茶", "冒", "郇", "忻", "果", "化", "粱", "先", "嘎"
        };

        // 姓氏及其对应的权重
        private static readonly Dictionary<string, double> s_surnameWeightDict = new()
        {
            { "王", 7.21 }, { "李", 7.18 }, { "张", 6.78 }, { "刘", 5.12 }, { "杨", 4.51 },
            { "陈", 3.95 }, { "黄", 3.45 }, { "赵", 2.29 }, { "吴", 2.21 }, { "周", 2.12 },
            { "徐", 1.97 }, { "孙", 1.95 }, { "马", 1.94 }, { "朱", 1.93 }, { "胡", 1.92 },
            { "郭", 1.91 }, { "何", 1.90 }, { "林", 1.89 }, { "罗", 1.88 }, { "高", 1.87 },
        };

        private static readonly List<double> s_cumulativeWeights;

        static Surnames()
        {
            Initialize();
            s_cumulativeWeights = CalculateCumulativeWeights(s_surnameWeightDict);
        }

        private static void Initialize()
        {
            // 计算已有的姓氏权重总和（仅前20个）
            var totalWeightFirstArray = s_surnameWeightDict.Values.Sum();

            // 计算剩余部分的总权重
            var remainingTotalWeight = 100 - totalWeightFirstArray;

            // 包括前20个姓氏在内的总姓氏数量
            var totalSurnames = 600;

            // 计算线性递减的起始权重
            var linearStartWeight = 1.97;
            var linearEndWeight = 1.87;

            // 计算线性递减的步长
            var linearStep = (linearStartWeight - linearEndWeight) / 10;

            // 计算第21个姓氏的初始权重
            var initialWeight21st = linearEndWeight;

            // 计算未归一化的权重总和
            double unnormalizedSum = CalculateLinearSum(linearStartWeight, linearStep, 10) + initialWeight21st;

            // 从第22个姓氏开始计算权重总和
            double currentWeight = initialWeight21st;
            for (var x = 22; x <= totalSurnames; x++)
            {
                var weight = 1.87 * Math.Exp(-0.005403 * x) + 0.000001; // 使用新函数
                unnormalizedSum += weight;
            }

            // 计算归一化因子
            var normalizationFactor = remainingTotalWeight / unnormalizedSum;

            // 添加剩余的姓氏
            AddRemainingSurnamesWithDecay(normalizationFactor, initialWeight21st);
        }

        private static double CalculateLinearSum(double startWeight, double step, int count)
        {
            double sum = 0;
            for (int i = 0; i < count; i++)
            {
                sum += startWeight - i * step;
            }
            return sum;
        }

        private static void AddRemainingSurnamesWithDecay(double normalizationFactor, double initialWeight)
        {
            var currentIndex = 21;
            double currentWeight = initialWeight;

            // 添加第1个数组中第21个姓氏之后的姓氏
            for (var i = 20; i < s_chineseSurnames1To100.Length; i++, currentIndex++)
            {
                if (currentIndex <= 30) // 如果当前索引小于等于30，则使用线性递减
                {
                    var weight = normalizationFactor * (1.97 - (currentIndex - 21) * 0.01); // 使用线性递减
                    s_surnameWeightDict.Add(s_chineseSurnames1To100[i], weight);
                }
                else
                {
                    var weight = normalizationFactor * (1.87 * Math.Exp(-0.005403 * currentIndex) + 0.000001); // 使用新函数
                    s_surnameWeightDict.Add(s_chineseSurnames1To100[i], weight);
                }
            }

            // 添加剩余的四个数组中的姓氏
            for (var i = 0; i < s_chineseSurnames101To200.Length; i++, currentIndex++)
            {
                var weight = normalizationFactor * (1.87 * Math.Exp(-0.005403 * currentIndex) + 0.000001); // 使用新函数
                s_surnameWeightDict.Add(s_chineseSurnames101To200[i], weight);
            }

            for (var i = 0; i < s_chineseSurnames201To300.Length; i++, currentIndex++)
            {
                var weight = normalizationFactor * (1.87 * Math.Exp(-0.005403 * currentIndex) + 0.000001); // 使用新函数
                s_surnameWeightDict.Add(s_chineseSurnames201To300[i], weight);
            }

            for (var i = 0; i < s_chineseSurnames301To400.Length; i++, currentIndex++)
            {
                var weight = normalizationFactor * (1.87 * Math.Exp(-0.005403 * currentIndex) + 0.000001); // 使用新函数
                s_surnameWeightDict.Add(s_chineseSurnames301To400[i], weight);
            }

            for (var i = 0; i < s_chineseSurnames401To500.Length; i++, currentIndex++)
            {
                var weight = normalizationFactor * (1.87 * Math.Exp(-0.005403 * currentIndex) + 0.000001); // 使用新函数
                s_surnameWeightDict.Add(s_chineseSurnames401To500[i], weight);
            }

            for (var i = 0; i < s_chineseSurnames501To600.Length; i++, currentIndex++)
            {
                var weight = normalizationFactor * (1.87 * Math.Exp(-0.005403 * currentIndex) + 0.000001); // 使用新函数
                s_surnameWeightDict.Add(s_chineseSurnames501To600[i], weight);
            }
        }

        public string[] GenerateRandomSurnames(int count)
        {
            var result = new List<string>(count);

            /*
             经验法则:
               对于高度计算密集型的任务，如果每项任务的执行时间大于几毫秒，那么使用并行处理通常是有益的。
               如果每项任务的执行时间小于几百微秒，那么并行处理可能带来的开销会超过其带来的性能提升。
               通常，任务数量大约是可用核心数的2到4倍时，可以获得较好的并行效果。
             具体案例:
               如果预期生成的姓氏数量较大（比如几千到几百万），并且假设每项任务的执行时间足以克服并行化的开销，那么并行处理将是合适的。
               对于较小的数量（比如几十个），并行处理可能会因开销而变得不划算。
             */

            var random = RandomUtil.GetRandom();
            for (var i = 0; i < count; i++)
            {
                var rand = random.NextDouble() * s_cumulativeWeights.Last();

                // 使用二分查找找到对应的姓氏
                var index = BinarySearch(rand);
                result.Add(s_surnameWeightDict.ElementAt(index).Key);
            }

            return result.ToArray();
        }

        private static int BinarySearch(double target)
        {
            int left  = 0;
            int right = s_cumulativeWeights.Count - 1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;

                if (Math.Abs(s_cumulativeWeights[mid] - target) < 0.000001)
                {
                    return mid;
                }
                else if (s_cumulativeWeights[mid] < target)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }

            return left;
        }

        private static List<double> CalculateCumulativeWeights(Dictionary<string, double> surnameWeightDict)
        {
            var    cumulativeWeights = new List<double>();
            double cumulativeWeight  = 0.0;

            foreach (var surname in surnameWeightDict)
            {
                cumulativeWeight += surname.Value;
                cumulativeWeights.Add(cumulativeWeight);
            }

            return cumulativeWeights;
        }
    }
}