using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int maxExperience;
    
    [SerializeField] private List<int> expFromLevels = new List<int>();
    [SerializeField] private int hlam = 0;
    [SerializeField] private float damage = 6;
    [SerializeField] private float attackSpeed = 0.6f;
    [SerializeField] private float armor = 1.0f;
    [SerializeField] private float regeneration = 0.3f;
    [SerializeField] private List<PlayerSettings> levelSettings = new List<PlayerSettings>();

    private float currentHealth;
    private int currentExperience;
    private int currentGolds;
    private int currentHlam;
    private int currentLevel = 0;

    private void UpdateStats(int lvl)
    {
        maxHealth = levelSettings[lvl].health;
        damage = levelSettings[lvl].damage;
        attackSpeed = levelSettings[lvl].attackSpeed;
        armor = levelSettings[lvl].armor;
        regeneration = levelSettings[lvl].regeneration;
    }

    private void Start()
    {
        UpdateStats(0);
        maxExperience = expFromLevels[0];
        currentHealth = maxHealth;
        currentExperience = 0;
        GlobalEvents.ApplyGolds.AddListener(ApplyGolds);
        GlobalEvents.ApplyDamage.AddListener(ApplyDamage);
        GlobalEvents.ApplyExperience.AddListener(ApplyExperience);
        GlobalEvents.ApplyHlam.AddListener(ApplyHlam);
        GlobalEvents.UpdateUI.Invoke();
        GlobalEvents.BuyHealth.AddListener(BuyHealth);
        StartCoroutine(Regeneration());
        SaveStats();
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

    private IEnumerator Regeneration()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            currentHealth += regeneration;
            if (currentHealth < maxHealth) GlobalEvents.UpdateUI.Invoke();
            else currentHealth = maxHealth;
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

    private void SaveStats()
    {
        string expFromLevelsSettings = JsonUtility.ToJson(this);
        PlayerPrefs.SetString("levelSettings", expFromLevelsSettings);
        var t = new PlayerStats();
        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("levelSettings", expFromLevelsSettings), t);
        print(t.levelSettings[9].health);
    }
}
