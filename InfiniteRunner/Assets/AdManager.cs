using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdManager : MonoBehaviour
{
    public static AdManager instance;

    private const string MaxSdkKey = "31yfPvKGT3LgIuCPEKHzWRmSV5xPWbGnB8PWGTbfRnOXHktU2QIar3kGgxMrR-xS84tMM_aiVDF2MHaiwMSV__";
    private const string InterstitialAdUnitId = "264b4fc9e9791988";
    private const string RewardedAdUnitId = "ENTER_REWARD_AD_UNIT_ID_HERE";
    private const string RewardedInterstitialAdUnitId = "ENTER_REWARD_INTER_AD_UNIT_ID_HERE";
    private const string BannerAdUnitId = "ENTER_BANNER_AD_UNIT_ID_HERE";
    private const string MRecAdUnitId = "ENTER_MREC_AD_UNIT_ID_HERE";
    int retryAttempt;
    void Start()
    {
        MaxSdkCallbacks.OnSdkInitializedEvent += sdkConfiguration =>
        {
            // AppLovin SDK is initialized, configure and start loading ads.
            Debug.Log("MAX SDK Initialized");

            InitializeInterstitialAds();
            /*InitializeRewardedAds();
            InitializeRewardedInterstitialAds();
            InitializeBannerAds();
            InitializeMRecAds();*/
            MaxSdk.ShowMediationDebugger();
        };

        MaxSdk.SetSdkKey(MaxSdkKey);
        MaxSdk.InitializeSdk();
    }
    //Plays an interstitial ad
    public void PlayInterstitialAd()
    {
        if (MaxSdk.IsInterstitialReady(InterstitialAdUnitId))
        {
            MaxSdk.ShowInterstitial(InterstitialAdUnitId);
        }
    }

    #region Interstitial Ad Methods
    public void InitializeInterstitialAds()
    {
        // Attach callback
        MaxSdkCallbacks.OnInterstitialLoadedEvent += OnInterstitialLoadedEvent;
        MaxSdkCallbacks.OnInterstitialLoadFailedEvent += OnInterstitialFailedEvent;
        MaxSdkCallbacks.OnInterstitialAdFailedToDisplayEvent += InterstitialFailedToDisplayEvent;
        MaxSdkCallbacks.OnInterstitialHiddenEvent += OnInterstitialDismissedEvent;

        // Load the first interstitial
        LoadInterstitial();
    }

    private void LoadInterstitial()
    {
        MaxSdk.LoadInterstitial(InterstitialAdUnitId);
    }

    private void OnInterstitialLoadedEvent(string adUnitId)
    {
        // Interstitial ad is ready to be shown. MaxSdk.IsInterstitialReady(adUnitId) will now return 'true'

        // Reset retry attempt
        retryAttempt = 0;
    }

    private void OnInterstitialFailedEvent(string adUnitId, int errorCode)
    {
        // Interstitial ad failed to load 
        // We recommend retrying with exponentially higher delays up to a maximum delay (in this case 64 seconds)

        retryAttempt++;
        double retryDelay = Math.Pow(2, Math.Min(6, retryAttempt));

        Invoke("LoadInterstitial", (float)retryDelay);
    }
    private void InterstitialFailedToDisplayEvent(string adUnitId, int errorCode)
    {
        // Interstitial ad failed to display. We recommend loading the next ad
        LoadInterstitial();
    }

    private void OnInterstitialDismissedEvent(string adUnitId)
    {
        // Interstitial ad is hidden. Pre-load the next ad
        LoadInterstitial();
    }
    #endregion
}
