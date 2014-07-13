﻿using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NKnife.Chinese.Ime.Pinyin
{
    public class DefaultSyllableCollection : ISyllableCollection
    {
        private static readonly ReadOnlyCollection<string> _SyllableList;

        public int Check(string pinyin)
        {
            if (_SyllableList.IndexOf(pinyin) >= 0)
                return 0;
            if(IsSubSyllable(pinyin))
                return 1;
            return 2;
        }

        /// <summary>
        /// 检查如果不是一个完整的合法音节，那么是否至少一个合法音节的前缀
        /// </summary>
        /// <param name="pinyin"></param>
        /// <returns></returns>
        private bool IsSubSyllable(string pinyin)
        {
            foreach (var syllable in _SyllableList)
            {
                if (pinyin.Length >= syllable.Length)
                    continue;
                if (Compare(syllable, pinyin))
                    return true;
            }
            return false;
        }

        private bool Compare(string left, string right)
        {
            for (int i = 0; i < right.Length; i++)
            {
                if(!(left[i].Equals(right[i])))
                {
                    return false;
                }
            }
            return true;
        }

        static DefaultSyllableCollection()
        {
            _SyllableList = InitializeCollection();
        }

        public static ReadOnlyCollection<string> InitializeCollection()
        {
            var list = new List<string>
            {
                "a",
                "ba",
                "bo",
                "bi",
                "bu",
                "bai",
                "bei",
                "bao",
                "biao",
                "bie",
                "ban",
                "bian",
                "ben",
                "bin",
                "bang",
                "beng",
                "bing",
                "pa",
                "po",
                "pi",
                "pu",
                "pai",
                "pei",
                "pao",
                "piao",
                "pou",
                "pe",
                "pan",
                "pian",
                "pen",
                "pin",
                "pang",
                "peng",
                "ping",
                "ma",
                "mo",
                "me",
                "mi",
                "mu",
                "mai",
                "mei",
                "mao",
                "miao",
                "mou",
                "miu",
                "mie",
                "man",
                "mian",
                "men",
                "min",
                "mang",
                "meng",
                "ming",
                "fa",
                "fo",
                "fu",
                "fei",
                "fiao",
                "fou",
                "fan",
                "fen",
                "fang",
                "feng",
                "da",
                "de",
                "di",
                "du",
                "duo",
                "dai",
                "dei",
                "dui",
                "dao",
                "diao",
                "dou",
                "diu",
                "die",
                "dan",
                "dian",
                "duan",
                "dun",
                "dang",
                "deng",
                "ding",
                "dong",
                "ta",
                "te",
                "ti",
                "tu",
                "tuo",
                "tai",
                "tei",
                "tui",
                "tao",
                "tiao",
                "tou",
                "tie",
                "tan",
                "tian",
                "tuan",
                "tun",
                "tang",
                "teng",
                "ting",
                "tong",
                "na",
                "ne",
                "ni",
                "nu",
                "nuo",
                "nv",
                "nai",
                "nei",
                "nao",
                "niao",
                "nou",
                "niu",
                "nie",
                "nve",
                "nan",
                "nian",
                "nuan",
                "nen",
                "nin",
                "nang",
                "niang",
                "neng",
                "ning",
                "nong",
                "la",
                "le",
                "li",
                "lia",
                "lu",
                "luo",
                "lv",
                "lai",
                "lei",
                "lao",
                "liao",
                "lou",
                "lie",
                "lve",
                "lan",
                "lian",
                "luan",
                "lin",
                "lun",
                "lang",
                "liang",
                "leng",
                "ling",
                "long",
                "ga",
                "ge",
                "gu",
                "gua",
                "guo",
                "gai",
                "guai",
                "gei",
                "gui",
                "gao",
                "gou",
                "gan",
                "guan",
                "gen",
                "gun",
                "gang",
                "guang",
                "geng",
                "gong",
                "ka",
                "ke",
                "ku",
                "kua",
                "kuo",
                "kai",
                "kuai",
                "kui",
                "kao",
                "kou",
                "kan",
                "kuan",
                "ken",
                "kun",
                "kang",
                "kuang",
                "keng",
                "kong",
                "ha",
                "he",
                "hu",
                "hua",
                "huo",
                "hai",
                "huai",
                "hei",
                "hui",
                "hao",
                "hou",
                "han",
                "huan",
                "hen",
                "hun",
                "hang",
                "huang",
                "heng",
                "hong",
                "ji",
                "jia",
                "ju",
                "jiao",
                "jiu",
                "jie",
                "jue",
                "jian",
                "juan",
                "jin",
                "jun",
                "jiang",
                "jing",
                "jong",
                "qi",
                "qia",
                "qu",
                "qiao",
                "qiu",
                "qie",
                "que",
                "qian",
                "quan",
                "qin",
                "qun",
                "qiang",
                "qing",
                "qong",
                "xi",
                "xia",
                "xu",
                "xiao",
                "xiu",
                "xie",
                "xue",
                "xian",
                "xuan",
                "xin",
                "xun",
                "xiang",
                "xing",
                "xong",
                "zha",
                "zhe",
                "zhi",
                "zhu",
                "zhua",
                "zhuo",
                "zhai",
                "zhuai",
                "zhui",
                "zhao",
                "zhou",
                "zhan",
                "zhuan",
                "zhen",
                "zhun",
                "zhang",
                "zhuang",
                "zheng",
                "zhong",
                "cha",
                "che",
                "chi",
                "chu",
                "chuo",
                "chai",
                "chuai",
                "chui",
                "chao",
                "chou",
                "chan",
                "chuan",
                "chen",
                "chun",
                "chang",
                "chuang",
                "cheng",
                "chong",
                "sha",
                "she",
                "shi",
                "shu",
                "shua",
                "shuo",
                "shai",
                "shuai",
                "shei",
                "shui",
                "shao",
                "shou",
                "shan",
                "shuan",
                "shen",
                "shun",
                "shang",
                "shuang",
                "sheng",
                "re",
                "ri",
                "ru",
                "ruo",
                "rui",
                "rao",
                "rou",
                "ran",
                "ruan",
                "ren",
                "run",
                "rang",
                "reng",
                "rong",
                "za",
                "ze",
                "zi",
                "zu",
                "zuo",
                "zai",
                "zei",
                "zui",
                "zao",
                "zou",
                "zan",
                "zuan",
                "zen",
                "zun",
                "zang",
                "zeng",
                "zong",
                "ca",
                "ce",
                "ci",
                "cu",
                "cuo",
                "cai",
                "cui",
                "cao",
                "cou",
                "can",
                "cuan",
                "cen",
                "cun",
                "cang",
                "ceng",
                "cong",
                "sa",
                "se",
                "si",
                "su",
                "suo",
                "sai",
                "sui",
                "sao",
                "sou",
                "san",
                "suan",
                "sen",
                "sun",
                "sang",
                "seng",
                "song",
                "ya",
                "yi",
                "yu",
                "yao",
                "you",
                "ye",
                "yue",
                "yan",
                "yuan",
                "yin",
                "yun",
                "yang",
                "yong",
                "wa",
                "wu",
                "wai",
                "wei",
                "er",
                "wan",
                "wen",
                "wang",
                "weng"
            };
            list.TrimExcess();
            return list.AsReadOnly();
        }
    }
}