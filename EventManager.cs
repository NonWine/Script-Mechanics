using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public static class EventManager 
{
    public static Action onCardsPress;
    public static Action onResetZona;
    public static Action onUnlockMove;
    public static Action<int> onCoinCollect;
    public static Action onAddSpider;
    public static Action onSpiderUpgrade;
    public static Action onTutorial;
    public static void InvokeUnlockMove() => onUnlockMove?.Invoke();

    public static void InvokeTutorail() => onTutorial?.Invoke();

    public static void InvokeCollectCoinUP(int i) => onCoinCollect?.Invoke(i);

    public static void InvokeAddSpiderEvent() => onAddSpider?.Invoke();

    public static void SpiderUpgrade()
    {
        onSpiderUpgrade?.Invoke();
    }

    public static void LaunchUpgrade()
    {
        onCardsPress?.Invoke();
    }

    public static void InvokeResetZone()
    {
        onResetZona?.Invoke();
    }
}
