using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

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
    public bool mute;
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

    private float currentRegenerationTime;
    private bool superRegeneration = false;

    private float damageMultiply = 1f;

    private MetricEvents metric;

    private bool mute;

    private void Awake()
    {
        GlobalEvents.ApplyGolds.AddListener(ApplyGolds);
        GlobalEvents.ApplyDamage.AddListener(ApplyDamage);
        GlobalEvents.ApplyExperience.AddListener(ApplyExperience);
        GlobalEvents.ApplyHlam.AddListener(ApplyHlam);
        GlobalEvents.BuyArmor.AddListener(BuyArmor);
        GlobalEvents.BuyHealth.AddListener(BuyHealth);
        GlobalEvents.BuyDamage.AddListener(BuyDamage);
        GlobalEvents.BuyAttackSpeed.AddListener(BuyAttackSpeed);
        GlobalEvents.ChangeCycleIndex.AddListener(UpdateCycle);
        GlobalEvents.EvRewardedLevelRestart.AddListener(ApplyMaxHealth);
        GlobalEvents.Pause.AddListener(Pause);
        GlobalEvents.UnPause.AddListener(UnPause);
        GlobalEvents.SuperRegen.AddListener(SuperRegen);
        GlobalEvents.SuperDamage.AddListener(SuperDamage);
        GlobalEvents.Mute.AddListener(SoundOff);
        GlobalEvents.UnMute.AddListener(SoundOn);

        metric = FindObjectOfType<MetricEvents>();

        StartCoroutine(Regeneration());
    }

    private void OnEnable()
    {
        if (YandexGame.Instance.savesData().playerStatsData == "")
        {
            LoadDefaultSettings(YandexGame.Instance.savesData()._defaultData);
        }
        else
        {
            SetCurrentSettings(YandexGame.Instance.savesData().playerStatsData);
            print("PlayerStats: CurrentCycle = " + currentCycle);
        }
        GlobalEvents.UpdateUI.Invoke();
    }

    private void Start()
    {
        GlobalEvents.UpdateUI.Invoke();
        GlobalEvents.LoadDefaultSettings.AddListener(LoadDefaultSettingsFromYandex);
    }

    private void OnDisable()
    {
        YandexGame.Instance.savesData().playerStatsData = GetCurrentJsonSettings();
        YandexGame.Instance._SaveProgress();
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
            metric.ApplySpeedAttack();
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
            metric.ApplyDamage();
            GlobalEvents.UpdateUI.Invoke();
        }
    }

    private void ApplyDamage(float damage)
    {
        if (superRegeneration) return;
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
        currentRegenerationTime = 1f;
        float timer = 0;
        while (true)
        {
            timer += Time.deltaTime;
            if (timer > currentRegenerationTime && currentHealth < maxHealth.Value() && !pause)
            {
                timer = 0;
                currentHealth += regeneration.Value();
                if (currentHealth > maxHealth.Value()) currentHealth = maxHealth.Value();
                GlobalEvents.UpdateUI.Invoke();
            }
            yield return null;
        }
    }

    private void SuperRegen(float time, float multiply)
    {
        StartCoroutine(SuperRegenCoroutine(time, multiply));
    }

    IEnumerator SuperRegenCoroutine(float time, float multiply)
    {
        superRegeneration = true;
        currentRegenerationTime = 1/ multiply;
        float timer = 0f;
        while (timer <= time && currentHealth < maxHealth.Value() && !pause)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        currentRegenerationTime = 1f;
        superRegeneration = false;
    }

    private void SuperDamage(float time, float multiply)
    {
        StartCoroutine(SuperDamageCoroutine(time, multiply));
    }

    IEnumerator SuperDamageCoroutine(float time, float multiply)
    {
        damageMultiply = multiply;
        float timer = 0f;
        while (timer <= time && !pause)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        damageMultiply = 1f;
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
        get { return damage.Value() * damageMultiply; }
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

    public int CurrentCycle
    {
        get { return currentCycle; }
    }

    private void LoadDefaultSettingsFromYandex()
    {
        LoadDefaultSettings(YandexGame.Instance.savesData()._defaultData);
        YandexGame.Instance.savesData().playerStatsData = GetCurrentJsonSettings();
        YandexGame.Instance._SaveProgress();
    }

    public void LoadDefaultSettings(ExcelSettings settings)
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

        expFromLevels = new List<int>
        {
            30, 110, 210, 320, 460, 620, 830, 1080, 1380, 1740, 2160, 2640, 3210, 3850, 4580, 5410, 6330, 7360, 8510, 9770, 11160,
        };

        maxExperience.currentLevel = 0;
        maxExperience.baseValue = expFromLevels[0];
        maxExperience.expFromLevels = expFromLevels;

        baseSettings = settings.playerSettings;

        currentExperience = 0;
        currentLevel = 0;
        currentHlam = 0;
        currentGolds = 0;
        currentCycle = 0;
        currentHealth = maxHealth.Value();

        mute = false;

        GlobalEvents.UpdateUI.Invoke();
    }

    public string GetCurrentJsonSettings()
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
            mute = mute,
        };
        
        return JsonUtility.ToJson(pleayerStatsData);
    }

    public void SetCurrentSettings(string newSettings)
    {
        var settings = JsonUtility.FromJson<PlayerStatsData>(newSettings);
        baseSettings = settings.levelSettings;
        expFromLevels = settings.expFromLevels;
        currentLevel = settings.currentLevel;
        maxHealth = settings.maxHealth;
        maxExperience = settings.maxExperience;
        hlam = settings.hlam;
        damage = settings.damage;
        attackSpeed = settings.attackSpeed;
        armor = settings.armor;
        regeneration = settings.regeneration;
        currentHealth = settings.currentHealth;
        currentExperience = settings.currentExperience;
        currentGolds = settings.currentGolds;
        currentHlam = settings.currentHlam;
        currentCycle = settings.currentCycle;
        mute = settings.mute;
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

    public bool Mute
    {
        get
        {
            return mute;
        }
        set
        {
            mute = value;
        }
    }
    private void SoundOff()
    {
        mute = true;
    }
    private void SoundOn()
    {
        mute = false;
    }
}
