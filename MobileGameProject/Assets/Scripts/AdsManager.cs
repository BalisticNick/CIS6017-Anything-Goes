using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    private string _androidGameId = "5248647";
    private string _iOSGameId = "5248646";
    [SerializeField] bool _testMode = true;
    [SerializeField] Button _showAdButton;
    private string _gameId;
    private string _androidAdUnitId = "Rewarded_Android";
    private string _iOSAdUnitId = "Rewarded_iOS";
    private string _adUnitId = null;
    public static bool isReady;

    public static bool IsAdvertisementReady()
    {
        return isReady;
    }

    private void Awake()
    {
#if UNITY_IOS
            _gameId = _iOSGameId;
            _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _gameId = _androidGameId;
        _adUnitId = _androidAdUnitId;
#elif UNITY_EDITOR
            _gameId = _androidGameId; //Only for testing the functionality in the Editor
            _adUnitId = _androidAdUnitId;
#endif
        _showAdButton.interactable = false;
        if (Advertisement.isInitialized)
        {
            Debug.Log("Advertisement is Initialized");
            LoadRewardedAd();
        }
        else
        {
            InitializeAds();
        }
    }
    public void InitializeAds()
    {
        Advertisement.Initialize(_gameId, _testMode, this);
    }

    public void OnInitializationComplete()
    {
        LoadRewardedAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
        isReady = false;
    }

    public void LoadRewardedAd()
    {
#if UNITY_IOS
        Advertisement.Load(_adUnitId, this);
#elif UNITY_ANDROID
        Advertisement.Load(_adUnitId, this);
#elif UNITY_EDITOR
        Advertisement.Load(_adUnitId, this);
#endif
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        // Configure the button to call the ShowAd() method when clicked:
        _showAdButton.onClick.AddListener(ShowRewardedAd);
        // Enable the button for users to click:
        _showAdButton.interactable = true;
    }

    public void ShowRewardedAd() //onClick
    {
        // Disable the button:
        _showAdButton.interactable = false;
        // Then show the ad:
        Advertisement.Show(_adUnitId, this);
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {placementId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log("OnUnityAdsShowFailure");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("OnUnityAdsShowStart");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("OnUnityAdsShowClick");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("OnUnityAdsShowComplete " + showCompletionState);
        if ((placementId.Equals(_androidAdUnitId) || placementId.Equals(_iOSAdUnitId)) && UnityAdsShowCompletionState.COMPLETED.Equals(showCompletionState))
        {
            UI_Timer TimerUI = FindAnyObjectByType<UI_Timer>();
            TimerUI.totalTimeAllowed = 180f;
            TimerUI.usedExtraLife = true;
        }
    }

    void OnDestroy()
    {
        // Clean up the button listeners:
        _showAdButton.onClick.RemoveAllListeners();
    }
}