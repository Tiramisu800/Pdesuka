using Pdesuka.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public static AdManager Instance;

    [SerializeField] private string _androidID;
    [SerializeField] private string _iOsID;
    [SerializeField] private string _adUnitId = "Interstitial_Android";
    [SerializeField] private bool _testMode = true;
    private string _gameId;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
#if UNITY_IOS
            _gameId = _iOsID;
#elif UNITY_ANDROID
            _gameId = _androidID;
#elif UNITY_EDITOR
            _gameId = _androidID;
#endif
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(_gameId, _testMode, this);
        }
    }

    public void OnInitializationComplete()
    {
        LoadAdd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    private void LoadAdd()
    {
        if (Advertisement.isInitialized)
        {
            Advertisement.Load(_adUnitId, this);
        }
    }

    public void ShowRewardedAd()
    {
        if (Advertisement.isInitialized)
        {
            Advertisement.Show(_adUnitId, this);
        }
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("Unity Ads Loaded" + placementId);
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Unity Ads Failed to Load: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Unity Ads Show Failure: {error.ToString()} - {message}");
        Time.timeScale = 1;
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("Unity Ads Show Start");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("Unity Ads Show Click");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        switch (showCompletionState)
        {
            case UnityAdsShowCompletionState.SKIPPED:
                break;
            case UnityAdsShowCompletionState.COMPLETED:
                break;
            case UnityAdsShowCompletionState.UNKNOWN:

                break;
            default:
                break;
        }
    }

    

}
