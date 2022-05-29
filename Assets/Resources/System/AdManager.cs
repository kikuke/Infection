using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour
{
    [SerializeField] string appId;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        MobileAds.Initialize(appId);
        gameObject.GetComponent<OffPageAdController>().RequestInterstitial();
        gameObject.GetComponent<RewardAdController>().RequestRewardedAd();
    }
}
