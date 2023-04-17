using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    //Setup variables, inital values.

    //Unique gameID (android/iOS), reference from unity monetization dashboard.
    private string _androidGameId = "5248647";
    private string _iOSGameId = "5248646";
    [SerializeField] private bool _testMode = true;
    [SerializeField] private Button _showAdButton;
    private string _gameId;
    private string _androidAdUnitId = "Rewarded_Android";
    private string _iOSAdUnitId = "Rewarded_iOS";
    private string _adUnitId = null;
    private UI_Timer TimerUI; 

    private void Awake()
    {
        //Grab TimerUI reference from GameManager
        TimerUI = GameManager.GetTimerUI_Static();

        //Set _gameId and _adUnitId based on platform. Will remain null if wrong platform/non editor.
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
        //Set ad button to non interactable
        _showAdButton.interactable = false;

        //Load rewarded ad if initialized else initalize ads (loads rewarded ads after initalizing)
        if (Advertisement.isInitialized)
        {
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
    }

    public void LoadRewardedAd()
    {
        Advertisement.Load(_adUnitId, this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        //Configure the button to call the ShowRewardedAd() method on click
        _showAdButton.onClick.AddListener(ShowRewardedAd);
        //Set ad button to interactable
        _showAdButton.interactable = true;
    }

    public void ShowRewardedAd() //OnClick
    {
        //Set ad button to non interactable
        _showAdButton.interactable = false;
        //Then show the ad
        Advertisement.Show(_adUnitId, this);
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {placementId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log("OnUnityAdsShowFailure");
        //Call OnAdFinished method to return to game without receiving reward.
        OnAdFinished();
    }

    public void OnUnityAdsShowStart(string placementId)
    {
    }

    public void OnUnityAdsShowClick(string placementId)
    {
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("OnUnityAdsShowComplete " + showCompletionState);
        //If adUnitId is android/iOS adUnitId and Ad has completed sucessfully then:
        if ((placementId.Equals(_androidAdUnitId) || placementId.Equals(_iOSAdUnitId)) && UnityAdsShowCompletionState.COMPLETED.Equals(showCompletionState))
        {
            //Set total time allowed to 3 minutes as reward for completing ad.
            TimerUI.totalTimeAllowed = 180f;
            OnAdFinished();
        }
    }

    void OnDestroy()
    {
        //Clean up the button listeners:
        _showAdButton.onClick.RemoveAllListeners();
    }

    private void OnAdFinished()
    {
        //Set used extra life to true, hide continue box and resume game.
        TimerUI.usedExtraLife = true;
        GameManager.GetContinueBox_Static().SetActive(false);
        GameManager.ResumeGame();
    }
}