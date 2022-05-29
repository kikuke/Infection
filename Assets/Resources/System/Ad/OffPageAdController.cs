using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class OffPageAdController : MonoBehaviour
{
    private InterstitialAd interstitial;
    
    [SerializeField] string unitId;

    public void RequestInterstitial()
    {
        Debug.Log("RequestInterstitial");
        interstitial = new InterstitialAd(unitId);

        // Called when an ad request has successfully loaded.
        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.interstitial.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        this.interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;

        AdRequest request = new AdRequest.Builder().Build();

        interstitial.LoadAd(request);
    }
    
    public void ShowInterstitial()
    {
        Debug.Log("ShowInterstitial");
        if (interstitial.IsLoaded())
            interstitial.Show();
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.Message);
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        Debug.Log("ClosedInterstitial");
        MonoBehaviour.print("HandleAdClosed event received");
        interstitial.Destroy();
        Application.Quit();
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }
}
