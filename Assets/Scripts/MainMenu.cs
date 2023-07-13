using System;
using System.Collections;
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
    public int currentCycle;
}

public class MainMenu : MonoBehaviour
{
    public UIScrollLevels scrollLevels;
    public List<Level> levels;
    private int currentLevel = 0;

    private void OnEnable()
    {
        YandexGame.GetDataEvent += Load;
    }
    private void OnDisable()
    {
        YandexGame.GetDataEvent -= Load;
    }
    public void StartLevel(int index)
    {
        currentLevel = index;
        Save();
        YandexGame.Instance._SaveProgress();
        SceneManager.LoadScene(index + 1);
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
    }
    public void Load()
    {
        var data = YandexGame.Instance.savesData();
        if (data.levels == null) return;
        levels = data.levels;
        currentLevel = data.currentLevel;
    }
}

