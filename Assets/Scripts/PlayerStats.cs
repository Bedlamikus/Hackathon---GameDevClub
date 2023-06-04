using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStatsData
{
    public int maxHealth;
    public int maxExperience;

    public List<int> expFromLevels = new();
    public int hlam;
    public float damage;
    public float attackSpeed;
    public float armor;
    public float regeneration;
    public List<PlayerSettings> levelSettings = new();

    public float currentHealth;
    public int currentExperience;
    public int currentGolds;
    public int currentHlam;
    public int currentLevel;
    public int currentCycle;
}

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int maxExperience;
    
    [SerializeField] private List<int> expFromLevels = new ();
    [SerializeField] private int hlam = 0;
    [SerializeField] private float damage = 6;
    [SerializeField] private float attackSpeed = 0.6f;
    [SerializeField] private float armor = 1.0f;
    [SerializeField] private float regeneration = 0.3f;
    [SerializeField] private List<PlayerSettings> levelSettings = new ();

    [SerializeField] private float currentHealth;
    [SerializeField] private int currentExperience;
    [SerializeField] private int currentGolds;
    [SerializeField] private int currentHlam;
    [SerializeField] private int currentLevel = 0;

    private int currentCycle;

    private void UpdateStats(int lvl)
    {
        if (levelSettings.Count <=0) return;
        maxHealth = levelSettings[lvl].health;
        currentHealth = maxHealth;
        damage = levelSettings[lvl].damage;
        attackSpeed = levelSettings[lvl].attackSpeed;
        armor = levelSettings[lvl].armor;
        regeneration = levelSettings[lvl].regeneration;
    }

    private void Start()
    {
        UpdateStats(0);
        maxExperience = expFromLevels[0];
        currentExperience = 0;
        GlobalEvents.ApplyGolds.AddListener(ApplyGolds);
        GlobalEvents.ApplyDamage.AddListener(ApplyDamage);
        GlobalEvents.ApplyExperience.AddListener(ApplyExperience);
        GlobalEvents.ApplyHlam.AddListener(ApplyHlam);
        GlobalEvents.BuyHealth.AddListener(BuyHealth);
        GlobalEvents.DefaultSettingsLoaded.AddListener(UpdateSettings);
        GlobalEvents.ChangeCycleIndex.AddListener(UpdateCycle);
        StartCoroutine(Regeneration());
    }

    private void BuyHealth()
    {
        if (currentHlam > 10)
        {
            maxHealth += 10;
            currentHealth = maxHealth;
            GlobalEvents.UpdateUI.Invoke();
        }
    }

    private void ApplyDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        if (currentHealth <= 0) GlobalEvents.BattleTrainDie.Invoke();
        GlobalEvents.UpdateUI.Invoke();
    }

    private void ApplyExperience(int exp)
    {
        currentExperience += exp;
        if (currentExperience >= maxExperience)
        {
            currentLevel += 1;
            currentExperience = maxExperience;
            maxExperience = expFromLevels[currentLevel];
            UpdateStats(currentLevel);
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
            if (currentHealth < maxHealth)
            {
                currentHealth += regeneration;
                if (currentHealth > maxHealth) currentHealth = maxHealth;
                GlobalEvents.UpdateUI.Invoke();
            }
        }
    }

    public float Health 
    { 
        get { return currentHealth; }
    }

    public int Experience
    {
        get { return currentExperience; }
    }

    public int Golds
    {
        get { return currentGolds; }
    }

    public int MaxHealth
    {
        get { return maxHealth; }
    }

    public int TargetUIExperience
    {
        get { return expFromLevels[currentLevel]; }
    }

    public int CurrentUIExperience
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
        get { return armor; }
        set { armor = value; }
    }

    public float Damage
    {
        get { return damage; }
    }

    public float AttackSpeed
    {
        get { return attackSpeed; }
    }

    private void UpdateSettings(ExcelSettings settings)
    {
        levelSettings = settings.playerSettings;
        maxExperience = expFromLevels[0];
        currentExperience = 0;
        UpdateStats(0);
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
            levelSettings = levelSettings,
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
        levelSettings = newSettings.levelSettings;
        expFromLevels = newSettings.expFromLevels;
        currentLevel = newSettings.currentLevel;
        UpdateStats(currentLevel);
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
