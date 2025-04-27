namespace Utilities.Utils
{
    using Cysharp.Threading.Tasks;
    using GameFoundation.DI;
    using GameFoundation.Scripts.Utilities.LogService;
    using MiraiGameFoundation.Scripts.Utilities.Utils;
    using Newtonsoft.Json;
    using UnityEngine;

    public class ApiService : IInitializable
    {
        private const string ApiUrl = "http://localhost:11434/api/chat";
        private const string IpUrl  = "https://api.ipify.org?format=json";

        private readonly ILogService logger;

        public ApiService(ILogService logger) { this.logger = logger; }

        public async void Initialize()
        {
            this.logger.Log("Initializing API request...");

            var data = await this.GetIP();
            this.logger.Log("ip: "          + data.ip);
            this.logger.Log("Device Name: " + this.GetDeviceName);

            this.PostInfo();
        }

        private async void PostInfo()
        {
            var postData = new
            {
                model = "deepseek-r1:8b",
                messages = new[]
                {
                    new { role = "system", content = "you are a salty pirate" },
                    new { role = "user", content   = "hello, how are you" }
                },
                stream = false
            };

            var jsonBody = JsonConvert.SerializeObject(postData);

            var request = new RequestBase(ApiUrl, jsonBody, RequestBase.RequestType.POST);
            await request.Send(
                onDone: (req) => { this.logger.Log($"API Request Success: {req.Response}"); },
                onFail: (req) => { this.logger.LogWithColor($"API Request Failed: {req.ResponseCode}", Color.red); }
            );
        }

        private async UniTask<IpData> GetIP()
        {
            var ip      = new IpData();
            var request = new RequestBase(IpUrl);
            await request.Send(
                onDone: (req) =>
                {
                    this.logger.Log($"IP Request Success: {req.Response}");
                    ip = JsonUtility.FromJson<IpData>(req.Response);
                },
                onFail: (req) => { this.logger.LogWithColor($"IP Request Failed: {req.ResponseCode}", Color.red); }
            );

            return ip;
        }

        private string GetDeviceName => System.Environment.MachineName;
    }

    [System.Serializable]
    public class IpData
    {
        public string ip;
    }
}