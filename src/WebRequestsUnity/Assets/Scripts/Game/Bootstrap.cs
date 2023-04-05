using Services.Factory;
using Services.Logger;
using Services.WebRequest;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Bootstrap : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private Transform _textContainer;
        [SerializeField] private GameObject _textPrefab;

        [SerializeField] private Button _refreshButton;

        private IWebRequestsService _webRequestsService;
    
        private void Awake()
        {
            _refreshButton.onClick.AddListener(RefreshData);
        }

        private void Start()
        {
            SetUpServices();
        }

        private void SetUpServices()
        {
            ILoggerService loggerService = new LoggerService();

            IUIFactory uiFactory = new UIFactory(_textContainer, _textPrefab);

            _webRequestsService = new WebRequestsService(uiFactory, loggerService, this);

            RefreshData();
        }

        private void RefreshData()
        {
            _webRequestsService.GetRequest(Constants.SERVER_URL);
        }

        private void OnDestroy()
        {
            _refreshButton.onClick.RemoveListener(RefreshData);
        }
    }
}