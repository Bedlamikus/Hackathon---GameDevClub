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

    [SerializeField] private TMP_Text buyArmor;
    [SerializeField] private TMP_Text buyArmorButtonText;
    [SerializeField] private TMP_Text buyArmorButtonNoActiveText;
    [SerializeField] private GameObject armorButton;
    [SerializeField] private GameObject armorButtonNoActive;

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
        GlobalEvents.UpdateUI.AddListener(UpdateUI);
    }

    private void UpdateUI()
    {
        UpdateCoins();
        UpdateHlam();
        UpdateHealth();
        UpdateExperience();
        BuyHealth();
        BuyAttackSpeed();
        BuyDamage();
        BuyArmor();
    }

    private void UpdateHealth()
    {
        maxHealth.text = player.MaxHealth.ToString();
        health.text = ((int)player.Health).ToString();
        healthSlider.value = player.Health / player.MaxHealth;
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
    private void BuyHealth()
    {
        Buy(
            player.maxHealth,
            buyHealth,
            buyHealthButtonText,
            buyHealthButtonNoActiveText,
            healthButton,
            healthButtonNoActive);
    }
    private void BuyAttackSpeed()
    {
        Buy(
            player.attackSpeed, 
            buyAttackSpeed,
            buyAttackSpeedButtonText,
            buyAttackSpeedButtonNoActiveText,
            attackSpeedButton,
            attackSpeedButtonNoActive);
    }
    private void BuyDamage()
    {
        Buy(
            player.damage,
            buyDamage,
            buyDamageButtonText,
            buyDamageButtonNoActiveText,
            damageButton,
            damageButtonNoActive);
    }
    private void BuyArmor()
    {
        Buy(
            player.armor,
            buyArmor,
            buyArmorButtonText,
            buyArmorButtonNoActiveText,
            armorButton,
            armorButtonNoActive);
    }

    private void Buy(Parametr parametr, TMP_Text currentPriseAndNext, TMP_Text howCost, TMP_Text howCostUnActive, GameObject buyButton, GameObject unActiveButton)
    {
        currentPriseAndNext.text = parametr.Value().ToString() + "+(" + parametr.additionValue.ToString() + ")";
        var cost = (parametr.currentLevel + 1) * parametr.goldForUpgrade;
        howCost.text = cost.ToString();
        howCostUnActive.text = player.DamageCost.ToString();
        if (cost > player.Hlam)
        {
            buyButton.SetActive(false);
            unActiveButton.SetActive(true);
        }
        else
        {
            buyButton.SetActive(true);
            unActiveButton.SetActive(false);
        }
    }
}
