using UnityEngine;
using UnityEngine.UI;

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

    private void RefreshData()
    {
        _webRequestsService.GetRequest("https://my-json-server.typicode.com/typicode/demo/posts");
    }

    private void Start()
    {
        ILoggerService loggerService = new LoggerService();

        IUIFactory uiFactory = new UIFactory(_textContainer, _textPrefab);
        
        _webRequestsService = new WebRequestsService(uiFactory, loggerService, this);

        RefreshData();
    }
}