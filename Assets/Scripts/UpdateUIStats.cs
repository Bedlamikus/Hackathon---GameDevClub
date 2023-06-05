using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpdateUIStats : MonoBehaviour
{
    [SerializeField] private TMP_Text buyHealth;
    [SerializeField] private TMP_Text buyHealthButtonText;

    [SerializeField] private TMP_Text buyAttack;
    [SerializeField] private TMP_Text buyAttackButtonText;
    [SerializeField] private TMP_Text buyAttackButtonNoActiveText;
    [SerializeField] private GameObject attackButton;
    [SerializeField] private GameObject attackButtonNoActive;


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
        GlobalEvents.UpdateUI.AddListener(BuyAttack);
    }

    private void UpdateHealth()
    {
        maxHealth.text = player.MaxHealth.ToString();
        buyHealth.text = maxHealth.text + "+(" + player.HealthAddition.ToString() + ")";
        buyHealthButtonText.text = player.CostHealth.ToString();
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

    private void BuyAttack()
    {
        buyAttack.text = player.HealthAddition.ToString();
        buyAttackButtonText.text = player.AttackCost.ToString();
        buyAttackButtonNoActiveText.text = player.AttackCost.ToString();
        if (player.AttackCost > player.Hlam)
        { 
            attackButton.SetActive(false);
            attackButtonNoActive.SetActive(true);
        }
        else
        {
            attackButton.SetActive(true);
            attackButtonNoActive.SetActive(false);
        }

    }
}
