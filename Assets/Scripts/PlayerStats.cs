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
    virtual public float Value()
    {
        return baseValue + currentLevel * additionValue;
    }
}

[Serializable]
public class EnergyParametr : Parametr
{
    public List<int> expFromLevels = new();

    override public float Value()
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
    [SerializeField] private Parametr maxHealth;
    [SerializeField] private EnergyParametr maxExperience;
    
    [SerializeField] private List<int> expFromLevels = new ();
    [SerializeField] private int hlam = 0;
    [SerializeField] private Parametr damage;
    [SerializeField] private Parametr attackSpeed;
    [SerializeField] private Parametr armor;
    [SerializeField] private Parametr regeneration;
    [SerializeField] private List<PlayerSettings> baseSettings;

    [SerializeField] private float currentHealth;
    [SerializeField] private float currentExperience;
    [SerializeField] private int currentGolds;
    [SerializeField] private int currentHlam;
    [SerializeField] private int currentLevel = 0;

    private int currentCycle;

    private void Start()
    {
        GlobalEvents.ApplyGolds.AddListener(ApplyGolds);
        GlobalEvents.ApplyDamage.AddListener(ApplyDamage);
        GlobalEvents.ApplyExperience.AddListener(ApplyExperience);
        GlobalEvents.ApplyHlam.AddListener(ApplyHlam);
        GlobalEvents.BuyHealth.AddListener(BuyHealth);
        GlobalEvents.BuyAttack.AddListener(BuyAttack);
        GlobalEvents.DefaultSettingsLoaded.AddListener(LoadDefaultSettings);
        GlobalEvents.ChangeCycleIndex.AddListener(UpdateCycle);
        StartCoroutine(Regeneration());
    }

    private void BuyHealth()
    {
        if (currentHlam >= (maxHealth.currentLevel + 1) * maxHealth.goldForUpgrade)
        {
            maxHealth.currentLevel += 1;
            currentHealth = maxHealth.Value();
            currentHlam -= (maxHealth.currentLevel) * maxHealth.goldForUpgrade;
            GlobalEvents.UpdateUI.Invoke();
        }
    }

    private void BuyAttack()
    {
        if (currentHlam >= (attackSpeed.currentLevel + 1) * attackSpeed.goldForUpgrade)
        {
            attackSpeed.currentLevel += 1;
            currentHlam -= (attackSpeed.currentLevel) * attackSpeed.goldForUpgrade;
            GlobalEvents.UpdateUI.Invoke();
        }
    }

    private void ApplyDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth > maxHealth.Value()) currentHealth = maxHealth.Value();
        if (currentHealth <= 0) GlobalEvents.BattleTrainDie.Invoke();
        GlobalEvents.UpdateUI.Invoke();
    }

    private void ApplyExperience(int exp)
    {
        currentExperience += exp;
        if (currentExperience >= maxExperience.Value())
        {
            currentExperience = maxExperience.Value();
            currentLevel += 1;
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
            if (currentHealth < maxHealth.Value())
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

    public float Armor
    {
        get { return armor.Value(); }
    }

    public float Damage
    {
        get { return damage.Value(); }
    }

    public float AttackSpeed
    {
        get { return attackSpeed.Value(); }
    }

    public float AttackCost
    {
        get { return (attackSpeed.currentLevel + 1) * attackSpeed.goldForUpgrade; }
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
}
