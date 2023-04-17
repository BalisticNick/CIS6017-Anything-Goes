using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    private string _androidGameId = "5248647";
    private string _iOSGameId = "5248646";
    [SerializeField] bool _testMode = true;
    [SerializeField] private RewardedAdsButton rewardedAdsButton;
    private string _gameId;

    private void Awake()
    {
        Init();
    }

    private void OnEnable()
    {
        Init();
    }
    
    public void Init()
    {
        if (Advertisement.isInitialized)
        {
            Debug.Log("Advertisement is Initialized");
            rewardedAdsButton.LoadAd();
        }
        else
        {
            InitializeAds();
        }
    }

    public void InitializeAds()
    {
#if UNITY_IOS
            _gameId = _iOSGameId;
#elif UNITY_ANDROID
        _gameId = _androidGameId;
#elif UNITY_EDITOR
            _gameId = _androidGameId; //Only for testing the functionality in the Editor
#endif
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(_gameId, _testMode, this);
        }
    }


    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        rewardedAdsButton.LoadAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}