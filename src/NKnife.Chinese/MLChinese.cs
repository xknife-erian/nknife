using System;
using System.Collections.Generic;
using System.Text;
using LEIAO.Feature.Runtime.ViewModels.CompileInfoPane;
using Microsoft.ML;
using Microsoft.ML.Data;
using NKnife;
using NKnife.Util;
using NLog;
using RAY.Common.Flow.Runtime;
using RAY.Library.Utils;

namespace LEIAO.Samples.Runtime.Exp.Kit
{
    /*
     * 暂时未启用，本类被设置为不编译
     * lukan, 2024-8-24
     */

    /// <summary>
    /// 使用Microsoft.ML对中文文本进行一些简单操作
    /// </summary>
    public class MLChinese
    {
        static readonly Random s_random     = new Random(DateTime.Now.Microsecond);

        public static string GetRandomMessage()
        {
            var tempStepId = IdGenerator.Generate();

            var size    = s_random.Next(2, 15);
            var message = new StringBuilder();

            for (var j = 0; j < size; j++)
            {
                message.Append(Guid.NewGuid().ToString("N").ToUpper());
            }

            return message.ToString();
        }

        public static string GetRandomStepName()
        {
            var sb   = new StringBuilder("MockName: ");
            var len  = s_random.Next(4, 12);
            var name = RandomUtil.GenerateRandomChineseText(len);
            return name.ToString();
        }


        private static readonly string[] sentences = {
            "伴随经济的快速发展和人类生活水平的提高，肥胖、２型糖尿病、非酒精性脂肪肝等各种代谢功能紊乱疾病已成为我国重要的公共卫生问题。",
            "产热脂肪细胞含有丰富的线粒体以及解偶联蛋白，可通过消耗甘油三酯和葡萄糖并以非颤抖性产热的方式来维持哺乳动物核心体温。",
            "鉴于产热过程增加体内能量消耗，因此，激活产热脂肪成为防治糖脂代谢紊乱性疾病的重要研究方向。",
            "线粒体作为产热脂肪细胞中功能高度保守的细胞器，在产热激活和失活过程中出现代谢适应性的改变以响应产热能力的变化。",
            "而产热脂肪细胞中线粒体代谢适应的失调则可能造成产热能力损伤，甚至全身代谢紊乱。",
            "本文概述了近年来报道的产热脂肪细胞线粒体多方面的灵活重塑，包括线粒体动力学变化、磷脂和嵴的重塑以及线粒体活性氧水平和氧化应激等方面。",
            "全面地总结了相关的调控因子，最后对脂肪细胞产热能力的维持提供了深入的见解，展望了未来有关产热脂肪细胞线粒体的研究方向。",
            "以期为开发靶向新药以激活脂肪产热来对抗代谢性疾病提供理论参考。",
            "产热脂肪细胞，特别是棕色脂肪细胞和米色脂肪细胞，因其独特的产热特性，成为防治代谢性疾病的研究热点。",
            "BAT和米色脂肪细胞富含线粒体，可通过解偶联蛋白介导的非颤抖性产热来增加能量消耗，从而发挥抗肥胖和抗糖尿病的作用。",
            "线粒体作为产热脂肪细胞中功能高度保守的细胞器，在产热激活和失活过程中出现代谢适应性的改变以响应产热能力的变化。",
            "然而，产热脂肪细胞中线粒体代谢适应的失调则可能造成产热能力损伤，甚至全身代谢紊乱。",
            "近年来，越来越多的研究表明，线粒体代谢适应性调节在产热脂肪细胞的功能维持中起着至关重要的作用。",
            "线粒体作为产热脂肪细胞中功能高度保守的细胞器，在产热激活和失活过程中出现代谢适应性的改变以响应产热能力的变化。",
            "而产热脂肪细胞中线粒体代谢适应的失调则可能造成产热能力损伤，甚至全身代谢紊乱。",
            "近年来，越来越多的研究表明，线粒体代谢适应性调节在产热脂肪细胞的功能维持中起着至关重要的作用。",
            "本文概述了近年来报道的产热脂肪细胞线粒体多方面的灵活重塑，包括线粒体动力学变化、磷脂和嵴的重塑以及线粒体活性氧水平和氧化应激等方面。",
            "全面地总结了相关的调控因子，最后对脂肪细胞产热能力的维持提供了深入的见解，展望了未来有关产热脂肪细胞线粒体的研究方向。",
            "以期为开发靶向新药以激活脂肪产热来对抗代谢性疾病提供理论参考。",
        };

        public static string GenerateRandomChineseSentence(int length)
        {
            var mlContext = new MLContext();

            var trainingData = new List<TextData>();
            foreach (var sentence in sentences)
            {
                trainingData.Add(new TextData { Text = sentence });
            }

            var trainingDataView = mlContext.Data.LoadFromEnumerable(trainingData);

            var textPipeline = mlContext.Transforms.Text.FeaturizeText("Features", nameof(TextData.Text));

            var textTransformer = textPipeline.Fit(trainingDataView);
            var transformedData = textTransformer.Transform(trainingDataView);

            var predictionEngine = mlContext.Model.CreatePredictionEngine<TextData, TextPrediction>(textTransformer);

            var generatedText = new List<string>();

            for (int i = 0; i < length; i++)
            {
                var randomText = trainingData[s_random.Next(trainingData.Count)].Text;
                var prediction = predictionEngine.Predict(new TextData { Text = randomText });
                generatedText.Add(MutateSentence(prediction.Text));
            }

            var result = string.Join(" ", generatedText);

            return result;
        }

        private static string MutateSentence(string sentence)
        {
            var words = sentence.Split(' ');
            if (words.Length > 1)
            {
                var index = s_random.Next(words.Length);
                words[index] = words[s_random.Next(words.Length)];
            }
            return string.Join(" ", words);
        }

        public class TextData
        {
            public string Text { get; set; }
        }

        public class TextPrediction
        {
            [ColumnName("Text")]
            public string Text { get; set; }
        }
    }
}

