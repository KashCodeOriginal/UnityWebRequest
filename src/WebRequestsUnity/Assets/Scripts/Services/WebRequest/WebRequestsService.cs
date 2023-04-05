using System;
using System.Collections;
using Game;
using Newtonsoft.Json;
using Services.Factory;
using Services.Logger;
using UnityEngine.Networking;

namespace Services.WebRequest
{
    public class WebRequestsService : IWebRequestsService
    {
        public WebRequestsService(IUIFactory uiFactory, ILoggerService loggerService, ICoroutineRunner coroutineRunner)
        {
            _uiFactory = uiFactory;
            _loggerService = loggerService;
            _coroutineRunner = coroutineRunner;
        }

        private readonly IUIFactory _uiFactory;
        private readonly ILoggerService _loggerService;
        private readonly ICoroutineRunner _coroutineRunner;

        public void GetRequest(string url)
        { 
            _coroutineRunner.StartCoroutine(GetWebRequest(url, Success));
        }

        private void Success(DB db)
        {
            _uiFactory.ClearText();

            var profileName = db.profile;

            foreach (var post in db.posts)
            {
                _uiFactory.CreateText($"{profileName.name}: posted {post.title}, with id: {post.id}");
            }

            foreach (var comment in db.comments)
            {
                _uiFactory.CreateText($"Post id: {comment.id}, with comment id: {comment.postId}, with text: {comment.body}");
            }
        }

        private IEnumerator GetWebRequest(string url, Action<DB> success)
        {
            var webRequest = UnityWebRequest.Get(url);
        
            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.InProgress:
                    _loggerService.PrintInfo(nameof(WebRequestsService), "WebRequest is processing");
                    break;
                case UnityWebRequest.Result.Success:
                    _loggerService.PrintInfo(nameof(WebRequestsService), "WebRequest get success");
                
                    var post = JsonConvert.DeserializeObject<DB>(webRequest.downloadHandler.text);

                    success?.Invoke(post);

                    break;
                case UnityWebRequest.Result.ConnectionError:
                    _loggerService.PrintWarning(nameof(WebRequestsService), "WebRequest connection error");
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    _loggerService.PrintWarning(nameof(WebRequestsService), "WebRequest protocol error");
                    break;
                case UnityWebRequest.Result.DataProcessingError:
                    _loggerService.PrintWarning(nameof(WebRequestsService), "WebRequest data processing error");
                    break;
                default:
                    _loggerService.PrintWarning(nameof(WebRequestsService), "Web request argument out of range");
                    break;
            }
        }
    }
}