public  class Damage : BaseStatts
{
    public override void Upgrade(WeaponStats stats)
    {
        if(Bank.Instance.CoinsCount >= GetCurrentCost())
        {
            stats.AddDamage(GetCurrentValue());
            IncreaseIndex();
            SaveStat("DamageIndex", GetCurrentIndex());
            if (isMaxLevel())
                return;
            SetCost();
            SetValue();
            SetLevel();
        }
    }

}
