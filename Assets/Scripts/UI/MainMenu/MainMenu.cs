using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

[Serializable]
public class Level
{
    public bool enabled;
    public string name;
    public bool ended;
    public bool startAgane;
    public int currentCycle;
}

public class MainMenu : MonoBehaviour
{
    [SerializeField] private List<Level> levels;
    private int currentLevel = 0;
    [SerializeField] private UILevelListMainMenu levelItems;
    [SerializeField] private GameObject levelEndedPanel;

    private void Start()
    {
        GlobalEvents.StartLevelButton.AddListener(StartCurrentLevel);
    }

    public void StartLevel(int index)
    {
        currentLevel = index;
        if (levels[index].ended && !levels[index].startAgane)
        {
            levelEndedPanel.SetActive(true);
            return;
        }
        Save();
        SceneManager.LoadScene(index + 1);
    }
    public void StartCurrentLevelAgain()
    {
        levels[currentLevel].startAgane = true;
        Save();
        SceneManager.LoadScene(currentLevel + 1);
    }

    public void StartCurrentLevel()
    {
        StartLevel(levelItems.level);
    }

    public bool IsLevelOpened(int index)
    {
        if (levels[index].enabled) return true;
        return false;
    }
    public void Save()
    {
        var data = YandexGame.Instance.savesData();
        data.levels = levels;
        data.currentLevel = currentLevel;
        YandexGame.Instance._SaveProgress();
    }

    public void Load(string _)
    {
        var data = YandexGame.Instance.savesData();
        if (data.levels == null)
        {
            CheckUILevels();
            return;
        }
        levels = data.levels;
        currentLevel = data.currentLevel;
        CheckUILevels();
    }

    private void CheckUILevels()
    {
        for (int i = 0; i < levels.Count; i++)
        {
            if (levels[i].enabled)
            {
                levelItems.ShowButtonStartLevel(i);
            }
        }
    }

    private void Awake()
    {
        GlobalEvents.SettingsLoaded.AddListener(Load);
    }
}

