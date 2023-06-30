
public class Health : BaseStatts
{
    public override void Upgrade(WeaponStats stats)
    {
        if (Bank.Instance.CoinsCount >= GetCurrentCost())
        {
            stats.AddHealth(GetCurrentValue());
            IncreaseIndex();
            SaveStat("HealthIndex", GetCurrentIndex());
            if (isMaxLevel())
                return;
            SetCost();
            SetValue();
            SetLevel();
        }
    }

}
