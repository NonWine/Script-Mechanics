using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MAXHelper;
using MadPixelAnalytics;
using UnityEngine.Events;

public class ADSManagerSDK : MonoBehaviour
{
    public static ADSManagerSDK Instance { get; private set; }
    [SerializeField] private GameObject _beforeDiedVideoPanel;
    [SerializeField] private GameObject _x2CoinsADPanel;
    public UnityAction<AdInfo> adInfoDead;
    public PanelsDeluxe panelsDeluxe;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Invoke(nameof(TurnOnBanner), 5f);
      //  AdsManager.Instance.OnAdShown += AdsAfterDead;
    }


    public void ShowAdsAfterWin()
    {
        AnalyticsManager.CustomEvent("video_ads_avaiable", new Dictionary<string, object>
        {{"ad_type","rewarded" },
         {"placement", "on_EndGame" },
         {"result","avaiable" },
         {"connection", 1}
        });
         
        //_x2CoinsADPanel.SetActive(true);
        AdsManager.Instance.OnAdAvailable += AdsAfterWin;

    }

    public void ShowADSVideoPanelAfterDead()
    {
        AnalyticsManager.CustomEvent("video_ads_avaiable", new Dictionary<string, object>
        {{"ad_type","rewarded" },
         {"placement", "on_deadPlayer" },
         {"result","avaiable" },
         {"connection", 1}
        });

        AdsManager.Instance.OnAdAvailable += AdsAfterDead;
        GameManager.Instance.GetGamePanel().SetActive(false);
        _beforeDiedVideoPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void AvailableChestADS()
    {
        AnalyticsManager.CustomEvent("video_ads_avaiable", new Dictionary<string, object>
        {{"ad_type","rewarded" },
         {"placement", "OpenedChest" },
         {"result","avaiable" },
         {"connection", 1}
        });
        AdsManager.Instance.OnAdAvailable += AdsOpenedChest;
    }


    public void AdsOpenedChest(AdInfo adInfo)
    {
        adInfo = new AdInfo("AfterOpenChest", AdsManager.EAdType.REWARDED, true, "giveAbilities");
        Debug.Log("adsAfterOpenChest");
    }

    private void AdsAfterDead(AdInfo adInfo)
    {
        adInfo = new AdInfo("AfterDead", AdsManager.EAdType.REWARDED, true, "respawn");
        Debug.Log("adsAfterDead");
    }

    private void AdsAfterWin(AdInfo adInfo)
    {
        adInfo = new AdInfo("AfterEndGame", AdsManager.EAdType.REWARDED, true, "X2RewardCoins");
        Debug.Log("adsAfterWin");
    }

    public void LaunchVideoAfterDead()
    {

        AnalyticsManager.CustomEvent("video_ads_started", new Dictionary<string, object>
        {{"ad_type","rewarded" },
         {"placement", "on_deadPlayer" },
         {"result","started" },
         {"connection", 1}
        });

        AdsManager.Instance.OnAdAvailable -= AdsAfterDead;
        AdsManager.Instance.OnAdStarted += AdsAfterDead;
        AdsManager.EResultCode eResult = AdsManager.ShowRewarded(this.gameObject, OnFinishAfterDead, "AfterDead");
        if (eResult != AdsManager.EResultCode.OK)
        {
            // здесь можно показать UI, что реклама не подгружена
        }

    }

    public void LaunchVideoAfterOpenedChest()
    {

        AnalyticsManager.CustomEvent("video_ads_started", new Dictionary<string, object>
        {{"ad_type","rewarded" },
         {"placement", "OpenedChest" },
         {"result","started" },
         {"connection", 1}
        });

        AdsManager.Instance.OnAdAvailable -= AdsOpenedChest;
        AdsManager.Instance.OnAdStarted += AdsOpenedChest;
        AdsManager.EResultCode eResult = AdsManager.ShowRewarded(this.gameObject, OnFinishAfterOpenedChest, "OpenedChest");
        if (eResult != AdsManager.EResultCode.OK)
        {
            // здесь можно показать UI, что реклама не подгружена
        }

    }


    public void LaunchVideoAfterWin()
    {

        AnalyticsManager.CustomEvent("video_ads_started", new Dictionary<string, object>
        {{"ad_type","rewarded" },
         {"placement", "on_EndGame" },
         {"result","started" },
         {"connection", 1}
        });

        AdsManager.Instance.OnAdAvailable -= AdsAfterWin;
        AdsManager.Instance.OnAdStarted += AdsAfterWin;
        AdsManager.EResultCode eResult = AdsManager.ShowRewarded(this.gameObject, OnFinishADSX2Coins, "AfterEndGame");
        if (eResult != AdsManager.EResultCode.OK)
        {
            // здесь можно показать UI, что реклама не подгружена
        }

    }

    private void OnFinishAfterDead(bool Success)
    {
        if (Success)
        {

            AnalyticsManager.CustomEvent("video_ads_watch", new Dictionary<string, object>
            { {"ad_type","rewarded" },
              {"placement", "on_deadPlayer" },
              {"result","watch" },
              {"connection", 1}
                                    });
            AdsManager.Instance.OnAdStarted -= AdsAfterDead;
            AdsManager.Instance.OnAdShown += AdsAfterDead;
            Time.timeScale = 1f;
            //  _beforeDiedVideoPanel.SetActive(false);
            GameManager.Instance.GetLosePanel().SetActive(false);
            Player.Instance.Respawn();
            Bank.Instance.AddCoins(0);
            GameManager.Instance.GetGamePanel().SetActive(true);
            AdsManager.Instance.OnAdShown -= AdsAfterDead;
            

        }
        else
        {

        }

    }

    private void OnFinishAfterOpenedChest(bool Success)
    {
        if (Success)
        {

            AnalyticsManager.CustomEvent("video_ads_watch", new Dictionary<string, object>
            { {"ad_type","rewarded" },
              {"placement", "OpenedChest" },
              {"result","watch" },
              {"connection", 1}
                                    });
            AdsManager.Instance.OnAdStarted -= AdsOpenedChest;
            AdsManager.Instance.OnAdShown += AdsOpenedChest;
            AdsManager.Instance.OnAdShown -= AdsOpenedChest;
            panelsDeluxe.GiveAllUpgrades();

        }
        else
        {

        }

    }



    private void OnFinishADSX2Coins(bool Success)
    {
        if (Success)
        {
            AnalyticsManager.CustomEvent("video_ads_watch", new Dictionary<string, object>
        {{"ad_type","rewarded" },
         {"placement", "on_EndGame" },
         {"result","watch" },
         {"connection", 1}
        });

            AdsManager.Instance.OnAdStarted -= AdsAfterWin;
            AdsManager.Instance.OnAdShown += AdsAfterWin;
            Time.timeScale = 1f;
          //  _x2CoinsADPanel.SetActive(false);
           // GameManager.Instance.GetGamePanel().SetActive(true);
            AdsManager.Instance.OnAdShown -= AdsAfterWin;
            GameManager.Instance.RestartGame(1000);
        }
        else
        {

        }
    }

    public void NoTHXButtonAfterDead()
    {
        AdsManager.Instance.OnAdAvailable -= AdsAfterDead;
        AdsManager.ShowInter("CancelRevive");
        GameManager.Instance.RestartGame(0);
    }

    public void NoTHXButtonX2Reward() 
    {
        AdsManager.Instance.OnAdAvailable -= AdsAfterWin;
        //   _x2CoinsADPanel.SetActive(false);
        GameManager.Instance.RestartGame(500);
    }


    private void TurnOnBanner()
    {
        AdsManager.ToggleBanner(true);
        AnalyticsManager.CustomEvent("video_ads_watch", new Dictionary<string, object>
        {{"ad_type","banner" },
         {"placement", "startGame" },
         {"result","watch" },
         {"connection", 1}
        });
    }
}
