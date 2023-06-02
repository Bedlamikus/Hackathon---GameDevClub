using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIProgressBarUpdate : MonoBehaviour
{
    [SerializeField] private TMP_Text maxHealth;
    [SerializeField] private TMP_Text health;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text maxExperience;
    [SerializeField] private TMP_Text experience;
    [SerializeField] private TMP_Text level;
    [SerializeField] private Slider experienceSlider;

    private PlayerStats player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerStats>();
        GlobalEvents.UpdateUI.AddListener(UpdateHealthBar);
        GlobalEvents.UpdateUI.AddListener(UpdateExperience);
    }

    private void UpdateHealthBar()
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
    private void OnEnable()
    {
        GlobalEvents.UpdateUI.Invoke();
    }
}
