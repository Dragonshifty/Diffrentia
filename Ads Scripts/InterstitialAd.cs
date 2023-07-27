using UnityEngine;
using UnityEngine.Advertisements;
 
public class InterstitialAd : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    private static InterstitialAd instance;
    public bool oneTimeAdSkip = false;
    private int adsCounter = 1;

    public static InterstitialAd Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InterstitialAd>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    instance = obj.AddComponent<InterstitialAd>();
                }
            }
            return instance;
        }
    }

    [SerializeField] string _androidAdUnitId = "Interstitial_Android";
    // [SerializeField] string _iOsAdUnitId = "Interstitial_iOS";
    string _adUnitId;
    // private bool isAdShowing;
 
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            _adUnitId = _androidAdUnitId;
        }
        else
        {
            Destroy(gameObject);
        }
    }
 
    // Load content to the Ad Unit:
    public void LoadAd()
    {
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }
 
    // Show the loaded content in the Ad Unit:
    public void ShowAd()
    {
        // Note that if the ad content wasn't previously loaded, this method will fail
        Debug.Log("Showing Ad: " + _adUnitId);
        if (!oneTimeAdSkip)
        {
            Advertisement.Show(_adUnitId, this);
        } else
        {
            oneTimeAdSkip = false;
            adsCounter = 3;
        }
        
    }
 
    // Implement Load Listener and Show Listener interface methods: 
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        // Optionally execute code if the Ad Unit successfully loads content.
    }
 
    public void OnUnityAdsFailedToLoad(string _adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {_adUnitId} - {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to load, such as attempting to try again.
        // isAdShowing = false;
    }
 
    public void OnUnityAdsShowFailure(string _adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {_adUnitId}: {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to show, such as loading another ad.
        // isAdShowing = false;
    }
 
    public void OnUnityAdsShowStart(string _adUnitId) 
    { 
        // isAdShowing = true;
    }
    public void OnUnityAdsShowClick(string _adUnitId) { }
    public void OnUnityAdsShowComplete(string _adUnitId, UnityAdsShowCompletionState showCompletionState) 
    { 
        // isAdShowing = false;
    }

    public bool ShowAdsOrNot()
    {
        adsCounter++;
        return adsCounter % 2 == 0 ? true : false;
    }

    public void LoadAdOrNot()
    {
        if (adsCounter != 1 && adsCounter % 2 != 0)
        {
            LoadAd();
        }
    }
}
