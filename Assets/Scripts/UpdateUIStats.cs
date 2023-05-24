using UnityEngine;
using TMPro;

public class UpdateUIStats : MonoBehaviour
{
    [SerializeField] private TMP_Text buyHealth;
    [SerializeField] private TMP_Text maxHealth;
    [SerializeField] private TMP_Text health;
    [SerializeField] private TMP_Text maxExperience;
    [SerializeField] private TMP_Text experience;
    [SerializeField] private TMP_Text level;
    [SerializeField] private TMP_Text golds;
    [SerializeField] private TMP_Text hlam;

    private PlayerStats player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerStats>();
        GlobalEvents.UpdateUI.AddListener(UpdateDamage);
        GlobalEvents.UpdateUI.AddListener(UpdateExperience);
        GlobalEvents.UpdateUI.AddListener(UpdateCoins);
        GlobalEvents.UpdateUI.AddListener(UpdateHlam);
    }

    private void UpdateDamage()
    {

        maxHealth.text = player.MaxHealth.ToString();
        buyHealth.text = maxHealth.text;
        health.text = ((int)player.Health).ToString();
    }

    private void UpdateExperience()
    {
        maxExperience.text = player.TargetUIExperience.ToString();
        experience.text = player.CurrentUIExperience.ToString();
        level.text = "LVL " + player.Level.ToString();
    }

    private void UpdateCoins()
    {
        golds.text = player.Golds.ToString();
    }

    private void UpdateHlam()
    {
        hlam.text = player.Hlam.ToString();
    }
}
