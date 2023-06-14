using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Parametr
{
    public float baseValue;
    public int currentLevel;
    public float additionValue;
    public int goldForUpgrade;
    public float Value()
    {
        return baseValue + currentLevel * additionValue;
    }
}

[Serializable]
public class EnergyParametr : Parametr
{
    public List<int> expFromLevels = new();

    public new float Value()
    {
        return expFromLevels[currentLevel];
    }
}

[Serializable]
public class PlayerStatsData
{
    public Parametr maxHealth;
    public EnergyParametr maxExperience;

    public List<int> expFromLevels = new();
    public int hlam;
    public Parametr damage;
    public Parametr attackSpeed;
    public Parametr armor;
    public Parametr regeneration;
    public List<PlayerSettings> levelSettings = new();

    public float currentHealth;
    public float currentExperience;
    public int currentGolds;
    public int currentHlam;
    public int currentLevel;
    public int currentCycle;
}

public class PlayerStats : MonoBehaviour
{
    [SerializeField] public Parametr maxHealth;
    [SerializeField] private EnergyParametr maxExperience;
    
    [SerializeField] private List<int> expFromLevels = new ();
    [SerializeField] private int hlam = 0;
    [SerializeField] public Parametr damage;
    [SerializeField] public Parametr attackSpeed;
    [SerializeField] public Parametr armor;
    [SerializeField] private Parametr regeneration;
    [SerializeField] private List<PlayerSettings> baseSettings;

    [SerializeField] private float currentHealth;
    [SerializeField] private float currentExperience;
    [SerializeField] private int currentGolds;
    [SerializeField] private int currentHlam;
    [SerializeField] private int currentLevel = 0;

    private int currentCycle;
    private bool pause = false;

    private void Start()
    {
        GlobalEvents.ApplyGolds.AddListener(ApplyGolds);
        GlobalEvents.ApplyDamage.AddListener(ApplyDamage);
        GlobalEvents.ApplyExperience.AddListener(ApplyExperience);
        GlobalEvents.ApplyHlam.AddListener(ApplyHlam);
        GlobalEvents.BuyArmor.AddListener(BuyArmor);
        GlobalEvents.BuyHealth.AddListener(BuyHealth);
        GlobalEvents.BuyDamage.AddListener(BuyDamage);
        GlobalEvents.BuyAttackSpeed.AddListener(BuyAttackSpeed);
        GlobalEvents.DefaultSettingsLoaded.AddListener(LoadDefaultSettings);
        GlobalEvents.ChangeCycleIndex.AddListener(UpdateCycle);
        GlobalEvents.EvRewardedLevelRestart.AddListener(ApplyMaxHealth);
        GlobalEvents.Pause.AddListener(Pause);
        GlobalEvents.UnPause.AddListener(UnPause);

        StartCoroutine(Regeneration());
        DontDestroyOnLoad(gameObject);
    }

    private void ApplyMaxHealth()
    {
        ApplyDamage(-MaxHealth);
    }

    private void BuyHealth()
    {
        if (BuyParametr(maxHealth))
        {
            currentHealth = maxHealth.Value();
            GlobalEvents.UpdateUI.Invoke();
        }
    }

    private void BuyAttackSpeed()
    {
        if (BuyParametr(attackSpeed))
        {
            GlobalEvents.UpdateUI.Invoke();
        }
    }

    private void BuyArmor()
    {
        if (BuyParametr(armor))
        {
            GlobalEvents.UpdateUI.Invoke();
        }
    }

    private bool BuyParametr(Parametr parametr)
    {
        if (currentHlam >= (parametr.currentLevel + 1) * parametr.goldForUpgrade)
        {
            parametr.currentLevel += 1;
            currentHlam -= (parametr.currentLevel) * parametr.goldForUpgrade;
            return true;
        }
        return false;
    }

    private void BuyDamage()
    {
        if (BuyParametr(damage))
        {
            GlobalEvents.UpdateUI.Invoke();
        }
    }

    private void ApplyDamage(float damage)
    {
        currentHealth -= (damage - damage * Armor / 100);
        if (currentHealth > maxHealth.Value()) currentHealth = maxHealth.Value();
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            GlobalEvents.BattleTrainDie.Invoke(); 
        }
        GlobalEvents.UpdateUI.Invoke();
    }

    private void ApplyExperience(int exp)
    {
        currentExperience += exp;
        if (currentExperience >= maxExperience.Value())
        {
            currentExperience = maxExperience.Value();
            currentLevel++;
            if (currentLevel >= expFromLevels.Count)
                currentLevel--;
            maxExperience.currentLevel = currentLevel;
            GlobalEvents.NewExperienseLevel.Invoke(currentLevel);
        }
        GlobalEvents.UpdateUI.Invoke();
    }

    private void ApplyGolds(int coins)
    {
        currentGolds += coins;
        GlobalEvents.UpdateUI.Invoke();
    }

    private void ApplyHlam(int hlam)
    {
        currentHlam += hlam;
        GlobalEvents.UpdateUI.Invoke();
    }

    private void UpdateCycle(int cycle)
    {
        currentCycle = cycle;
    }

    private IEnumerator Regeneration() 
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (currentHealth < maxHealth.Value() && !pause)
            {
                currentHealth += regeneration.Value();
                if (currentHealth > maxHealth.Value()) currentHealth = maxHealth.Value();
                GlobalEvents.UpdateUI.Invoke();
            }
        }
    }

    public float Health 
    { 
        get { return currentHealth; }
    }
    public float Experience
    {
        get { return currentExperience; }
    }
    public int Golds
    {
        get { return currentGolds; }
    }
    public float MaxHealth
    {
        get { return maxHealth.Value();}
    }
    public float CostHealth
    {
        get { return (maxHealth.currentLevel + 1) * maxHealth.goldForUpgrade; }
    }
    public float HealthAddition
    {
        get { return maxHealth.additionValue; }
    }
    public float AttackAddition
    {
        get { return attackSpeed.additionValue; }
    }
    public float AttackSpeed
    {
        get { return attackSpeed.Value(); }
    }
    public float AttackSpeedCost
    {
        get { return (attackSpeed.currentLevel + 1) * attackSpeed.goldForUpgrade; }
    }
    public float DamageAddition
    {
        get { return damage.additionValue; }
    }
    public float DamageCost
    {
        get { return (damage.currentLevel + 1) * damage.goldForUpgrade; }
    }
    public float Armor
    {
        get { return armor.Value(); }
    }
    public float Damage
    {
        get { return damage.Value(); }
    }

    public int TargetUIExperience
    {
        get { return expFromLevels[currentLevel]; }
    }

    public float CurrentUIExperience
    {
        get 
        {
            return currentExperience;
        }
    }

    public int MinUIExperience
    {
        get 
        {
            if (currentLevel == 0) return 0;
            return expFromLevels[currentLevel - 1];
        }
    }

    public int Level
    {
        get { return currentLevel + 1; }
    }

    public int Hlam
    {
        get { return currentHlam; }
    }

    private void LoadDefaultSettings(ExcelSettings settings)
    {
        var goldFU = settings.playerSettings[1].goldForUpgrade;
        maxHealth.currentLevel = 0;
        maxHealth.baseValue = settings.playerSettings[0].health;
        maxHealth.additionValue = settings.playerSettings[1].health;
        maxHealth.goldForUpgrade = goldFU;

        damage.currentLevel = 0;
        damage.baseValue = settings.playerSettings[0].damage;
        damage.additionValue = settings.playerSettings[1].damage;
        damage.goldForUpgrade = goldFU;

        armor.currentLevel = 0;
        armor.baseValue = settings.playerSettings[0].armor;
        armor.additionValue = settings.playerSettings[1].armor;
        armor.goldForUpgrade = goldFU;

        attackSpeed.currentLevel = 0;
        attackSpeed.baseValue = settings.playerSettings[0].attackSpeed;
        attackSpeed.additionValue = settings.playerSettings[1].attackSpeed;
        attackSpeed.goldForUpgrade = goldFU;

        regeneration.currentLevel = 0;
        regeneration.baseValue = settings.playerSettings[0].regeneration;
        regeneration.additionValue = settings.playerSettings[1].regeneration;
        regeneration.goldForUpgrade = goldFU;

        maxExperience.currentLevel = 0;
        maxExperience.baseValue = expFromLevels[0];
        maxExperience.expFromLevels = expFromLevels;

        baseSettings = settings.playerSettings;

        currentExperience = 0;
        currentHealth = maxHealth.Value();

        GlobalEvents.UpdateUI.Invoke();
    }

    public PlayerStatsData GetCurrentSettings()
    {
        var pleayerStatsData = new PlayerStatsData
        {
            maxHealth = maxHealth,
            maxExperience = maxExperience,
            expFromLevels = expFromLevels,
            hlam = hlam,
            damage = damage,
            attackSpeed = attackSpeed,
            armor = armor,
            regeneration = regeneration,
            levelSettings = baseSettings,
            currentHealth = currentHealth,
            currentExperience = currentExperience,
            currentGolds = currentGolds,
            currentHlam = currentHlam,
            currentLevel = currentLevel,
            currentCycle = currentCycle,
        };
        return pleayerStatsData;
    }

    public void SetCurrentSettings(PlayerStatsData newSettings)
    {
        baseSettings = newSettings.levelSettings;
        expFromLevels = newSettings.expFromLevels;
        currentLevel = newSettings.currentLevel;
        maxHealth = newSettings.maxHealth;
        maxExperience = newSettings.maxExperience;
        hlam = newSettings.hlam;
        damage = newSettings.damage;
        attackSpeed = newSettings.attackSpeed;
        armor = newSettings.armor;
        regeneration = newSettings.regeneration;
        currentHealth = newSettings.currentHealth;
        currentExperience = newSettings.currentExperience;
        currentGolds = newSettings.currentGolds;
        currentHlam = newSettings.currentHlam;
        currentCycle = newSettings.currentCycle;
        GlobalEvents.UpdateUI.Invoke();
    }

    private void Pause()
    {
        pause = true;
    }

    private void UnPause()
    {
        pause = false;
    }
}
