using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpdateUIStats : MonoBehaviour
{
    [SerializeField] private TMP_Text buyHealth;
    [SerializeField] private TMP_Text buyHealthButtonText;
    [SerializeField] private TMP_Text buyHealthButtonNoActiveText;
    [SerializeField] private GameObject healthButton;
    [SerializeField] private GameObject healthButtonNoActive;

    [SerializeField] private TMP_Text buyAttackSpeed;
    [SerializeField] private TMP_Text buyAttackSpeedButtonText;
    [SerializeField] private TMP_Text buyAttackSpeedButtonNoActiveText;
    [SerializeField] private GameObject attackSpeedButton;
    [SerializeField] private GameObject attackSpeedButtonNoActive;

    [SerializeField] private TMP_Text buyDamage;
    [SerializeField] private TMP_Text buyDamageButtonText;
    [SerializeField] private TMP_Text buyDamageButtonNoActiveText;
    [SerializeField] private GameObject damageButton;
    [SerializeField] private GameObject damageButtonNoActive;

    [SerializeField] private TMP_Text maxHealth;
    [SerializeField] private TMP_Text health;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text maxExperience;
    [SerializeField] private TMP_Text experience;
    [SerializeField] private TMP_Text level;
    [SerializeField] private Slider experienceSlider;
    [SerializeField] private TMP_Text golds;
    [SerializeField] private TMP_Text hlam;

    private PlayerStats player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerStats>();
        GlobalEvents.UpdateUI.AddListener(UpdateHealth);
        GlobalEvents.UpdateUI.AddListener(UpdateExperience);
        GlobalEvents.UpdateUI.AddListener(UpdateCoins);
        GlobalEvents.UpdateUI.AddListener(UpdateHlam);
        GlobalEvents.UpdateUI.AddListener(BuyDamage);
    }

    private void UpdateHealth()
    {
        maxHealth.text = player.MaxHealth.ToString();
        BuyHealth();
        //buyHealthButtonText.text = player.CostHealth.ToString();
        health.text = ((int)player.Health).ToString();
        healthSlider.value = player.Health / player.MaxHealth;
    }

    private void BuyHealth()
    {
        buyHealth.text = player.MaxHealth.ToString() + "+(" + player.HealthAddition.ToString() + ")";
        buyHealthButtonText.text = player.CostHealth.ToString();
        buyHealthButtonNoActiveText.text = player.CostHealth.ToString();
        if (player.CostHealth > player.Hlam)
        {
            healthButton.SetActive(false);
            healthButtonNoActive.SetActive(true);
        }
        else
        {
            healthButton.SetActive(true);
            healthButtonNoActive.SetActive(false);
        }
    }

    private void UpdateExperience()
    {
        maxExperience.text = player.TargetUIExperience.ToString();
        experience.text = player.CurrentUIExperience.ToString();
        level.text = "LVL " + player.Level.ToString();
        experienceSlider.minValue = player.MinUIExperience;
        experienceSlider.maxValue = player.TargetUIExperience;
        experienceSlider.value = player.CurrentUIExperience;
    }

    private void UpdateCoins()
    {
        golds.text = player.Golds.ToString();
    }

    private void UpdateHlam()
    {
        hlam.text = player.Hlam.ToString();
    }

    private void BuyAttack()
    {
        buyAttackSpeed.text = (player.AttackSpeed).ToString() + "+(" + player.AttackAddition.ToString() + ")";
        buyAttackSpeedButtonText.text = player.AttackSpeedCost.ToString();
        buyAttackSpeedButtonNoActiveText.text = player.AttackSpeedCost.ToString();
        if (player.AttackSpeedCost > player.Hlam)
        { 
            attackSpeedButton.SetActive(false);
            attackSpeedButtonNoActive.SetActive(true);
        }
        else
        {
            attackSpeedButton.SetActive(true);
            attackSpeedButtonNoActive.SetActive(false);
        }
    }

    private void BuyDamage()
    {
        buyDamage.text = (player.Damage).ToString() + "+(" + player.DamageAddition.ToString() + ")";
        buyDamageButtonText.text = player.DamageCost.ToString();
        buyDamageButtonNoActiveText.text = player.DamageCost.ToString();
        if (player.DamageCost > player.Hlam)
        {
            damageButton.SetActive(false);
            damageButtonNoActive.SetActive(true);
        }
        else
        {
            damageButton.SetActive(true);
            damageButtonNoActive.SetActive(false);
        }
    }

}
