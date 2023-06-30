public class Speed : BaseStatts
{

    public override void Upgrade(WeaponStats stats)
    {
        if (Bank.Instance.CoinsCount >= GetCurrentCost())
        {
            stats.AddSpeed(GetCurrentValue());
            IncreaseIndex();
            SaveStat("SpeedIndex", GetCurrentIndex());
            if (isMaxLevel())
                return;
            SetCost();
            SetValue();
            SetLevel();
        }
    }
}
