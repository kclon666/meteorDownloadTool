using System;
using System.IO;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading.Tasks;

namespace MinecraftDownloader
{
    class Program
    {
        static async Task Main(string[] args)
        { 
            Console.WriteLine("2024/11/20/19.37.28我找萨比AI写的萨比C#程序(原python)\r\n致敬大佬,顶针  dingzhen-vape \r\nhttps://github.com/dingzhen-vape");
            Console.WriteLine(" ");
            Console.WriteLine("如果下载速度慢,请使用github的加速 or vpn");
            Console.WriteLine("请输入Minecraft版本号：");
            string version = Console.ReadLine();
            string WurstUrl = $"https://github.com/dingzhen-vape/WurstCN/releases/download/ATV/Wurst-ClientCN-MC{version}.jar";
            string MeteorUrl = $"https://github.com/dingzhen-vape/MeteorCN/releases/download/ATV/meteor-client-{version}.jar";

            Console.WriteLine("请输入客户端名称（Wurst输入1/Meteor输入2）：");
            string clientInput = Console.ReadLine();
            string clientName = clientInput == "1" ? "Wurst" : "Meteor";

            string url = clientInput == "1" ? WurstUrl : MeteorUrl;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("链接成功");
                        await SaveFileAsync(response.Content, clientName + "-ClientCN-MC" + version + ".jar");
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        Console.WriteLine("链接不存在，请检查版本号是否正确,或该版本尚未更新，敬请期待");
                    }
                    else
                    {
                        Console.WriteLine("无法获取数据，状态码: " + response.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("发生错误: " + ex.Message);
            }
        } 
        static async Task SaveFileAsync(HttpContent content, string fileName)
        {
            using (FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                await content.CopyToAsync(fileStream);
            }
            Console.WriteLine("文件已保存到: " + fileName);
        }
    }
}
