using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public List<Level> levels = new List<Level>();
    private int currentLevel = 0;

    public void StartLevel(int index)
    {
        SceneManager.LoadScene(index + 1);
    }
    public bool IsLevelOpened(int index)
    {
        if (levels[index].enabled) return true;
        return false;
    }
}

