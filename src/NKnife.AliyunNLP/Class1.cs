using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Http;
using Aliyun.Acs.Core.Profile;

namespace NKnife.AliyunNLP
{
    class Program
    {
        static void Main(string[] args)
        {
            // 设置您的AccessKey ID和Secret
            string accessKeyId     = "yourAccessKeyId";
            string accessKeySecret = "yourAccessKeySecret";

            // 创建DefaultAcsClient对象
            var profile = DefaultProfile.GetProfile("cn-hangzhou", accessKeyId, accessKeySecret);
            var client  = new DefaultAcsClient(profile);

            // 创建通用请求对象
            CommonRequest request = new CommonRequest
            {
                // 设置请求参数
                Method     = MethodType.POST,
                Domain     = "nlp-automl.aliyuncs.com",
                Version    = "2019-07-01",
                Action = "GenerateText"
            };

            // 添加请求参数
            request.AddBodyParameters("ModelId", "yourModelId");
            request.AddBodyParameters("Prompt", "生成一个中文句子");
            request.AddBodyParameters("MaxLength", "20");
            request.AddBodyParameters("NumResults", "1");

            try
            {
                // 发送请求并获取响应
                CommonResponse response = client.GetAcsResponse<CommonResponse>(null);//request);

                // 输出结果
                Console.WriteLine("Generated text: " + response.HttpResponse.Content); // 这里需要解析JSON来提取结果
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }
    }
}