using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class UISettings : MonoBehaviour
{
    [SerializeField] private Button sound;
    [SerializeField] private TMP_Text sound_BTN_text;
    [SerializeField] private TMP_Text sound_BTN_before_text;

    [SerializeField] private Button close;

    private PlayerStats settings;

    private void Start()
    {
        sound.onClick.AddListener(Mute);
        close.onClick.AddListener(CloseSettings);
    }

    public void Preload()
    {
        sound_BTN_text.text = sound_BTN_before_text.text + " on";
        if (Settings.Mute)
        {
            sound_BTN_text.text = sound_BTN_before_text.text + " off";
        }
    }

    private void Mute()
    {
        if (Settings.Mute)
        {
            sound_BTN_text.text = sound_BTN_before_text.text + " on";
            GlobalEvents.UnMute.Invoke();
        }
        else
        {
            sound_BTN_text.text = sound_BTN_before_text.text + " off";
            GlobalEvents.Mute.Invoke();
        }
    }

    private void CloseSettings()
    {
        GlobalEvents.SaveCurrentSettings.Invoke();
        gameObject.SetActive(false);
    }

    private PlayerStats Settings
    {
        get
        {
            if (settings == null)
            {
                settings = FindObjectOfType<PlayerStats>();
            }
            return settings;
        }
    }
}
