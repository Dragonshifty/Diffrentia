using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsAreGo : MonoBehaviour
{
    InterstitialAd interstitialAd;

    void Awake()
    {
        interstitialAd = InterstitialAd.Instance;
    }
    void Start()
    {
        if (interstitialAd.ShowAdsOrNot()) interstitialAd.ShowAd();
    }

}
