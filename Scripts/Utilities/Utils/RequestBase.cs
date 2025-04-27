namespace MiraiGameFoundation.Scripts.Utilities.Utils
{
    using System;
    using System.Text;
    using Cysharp.Threading.Tasks;
    using UnityEngine.Networking;

    public class RequestBase
    {
        protected UnityWebRequest request;

        public RequestBase(string endPoin) { this.request = UnityWebRequest.Get(endPoin); }

        public RequestBase(string endPoin, string postData, RequestType type = RequestType.POST)
        {
            if (string.IsNullOrEmpty(postData)) return;
            this.request = new UnityWebRequest(endPoin, type.ToString());
            var bodyRaw = Encoding.UTF8.GetBytes(postData);
            this.request.uploadHandler   = new UploadHandlerRaw(bodyRaw);
            this.request.downloadHandler = new DownloadHandlerBuffer();
        }

        public RequestBase SetHeader(string name, string value)
        {
            this.request.SetRequestHeader(name, value);

            return this;
        }

        public async UniTask<RequestBase> Send(Action<RequestBase> onDone = null, Action<RequestBase> onFail = null)
        {
            this.request.SetRequestHeader("Content-Type", "application/json");
            this.request.SetRequestHeader("Accept",       "*/*");
            this.request.SetRequestHeader("Connection",   "keep-alive");

            this.request.SendWebRequest();

            await UniTask.WaitUntil(() => this.request.result != UnityWebRequest.Result.InProgress);

            if (this.request.result == UnityWebRequest.Result.Success)
            {
                if (onDone == null) return this;
                try
                {
                    onDone(this);
                }
                catch (Exception e)
                {
                }
            }
            else
            {
                if (onFail == null) return this;
                try
                {
                    onFail(this);
                }
                catch (Exception e)
                {
                }
            }

            return this;
        }

        public string GetResHeader(string key) { return this.request.GetResponseHeader(key); }

        public bool IsDone => this.request.isDone;

        public float Progress => this.request.downloadProgress;

        public string AccessToken => this.GetResHeader("x-access-token");

        public string Response     => this.request.downloadHandler.text;
        public byte[] ResponseData => this.request.downloadHandler.data;

        public long ResponseCode => this.request.responseCode;

        public enum RequestType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}