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

    private void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        GlobalEvents.UpdateUI.AddListener(UpdateUI);
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (Player == null) return;
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
        maxHealth.text = Player.MaxHealth.ToString();
        health.text = ((int)Player.Health).ToString();
        healthSlider.value = Player.Health / Player.MaxHealth;
    }
    private void UpdateExperience()
    {
        maxExperience.text = Player.TargetUIExperience.ToString();
        experience.text = Player.CurrentUIExperience.ToString();
        level.text = level.text.Substring(0,4) + Player.Level.ToString();
        experienceSlider.minValue = Player.MinUIExperience;
        experienceSlider.maxValue = Player.TargetUIExperience;
        experienceSlider.value = Player.CurrentUIExperience;
    }
    private void UpdateCoins()
    {
        golds.text = Player.Golds.ToString();
    }
    private void UpdateHlam()
    {
        hlam.text = Player.Hlam.ToString();
    }
    private void BuyHealth()
    {
        Buy(
            Player.maxHealth,
            buyHealth,
            buyHealthButtonText,
            buyHealthButtonNoActiveText,
            healthButton,
            healthButtonNoActive);
    }
    private void BuyAttackSpeed()
    {
        Buy(
            Player.attackSpeed, 
            buyAttackSpeed,
            buyAttackSpeedButtonText,
            buyAttackSpeedButtonNoActiveText,
            attackSpeedButton,
            attackSpeedButtonNoActive);
    }
    private void BuyDamage()
    {
        Buy(
            Player.damage,
            buyDamage,
            buyDamageButtonText,
            buyDamageButtonNoActiveText,
            damageButton,
            damageButtonNoActive);
    }
    private void BuyArmor()
    {
        Buy(
            Player.armor,
            buyArmor,
            buyArmorButtonText,
            buyArmorButtonNoActiveText,
            armorButton,
            armorButtonNoActive);
    }

    private void Buy(Parametr parametr, TMP_Text currentPriseAndNext, TMP_Text howCost, TMP_Text howCostUnActive, GameObject buyButton, GameObject unActiveButton)
    {
        var cost = (parametr.currentLevel + 1) * parametr.goldForUpgrade;
        if (cost > Player.Hlam)
        {
            buyButton.SetActive(false);
            unActiveButton.SetActive(true);
        }
        else
        {
            buyButton.SetActive(true);
            unActiveButton.SetActive(false);
        }
        currentPriseAndNext.text = parametr.Value().ToString();// + "+(" + parametr.additionValue.ToString() + ")";
        howCost.text = cost.ToString();
        howCostUnActive.text = cost.ToString();
    }

    private PlayerStats Player
    {
        get 
        {
            if (player == null)
                player = FindObjectOfType<PlayerStats>();
            return player;
        }
    }
}
