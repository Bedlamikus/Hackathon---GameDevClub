using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SheetProcessor : MonoBehaviour
{
    private const int num = 1;
    private const int xCoord = 2;
    private const int yCoord = 3;
    private const int zCoord = 4;
    private const int enemyType = 5;
    private const int countEnemy = 6;
    private const int coolDown = 7;

    private const char _cellSeporator = ',';
    private const char _inCellSeporator = ';';

    public ExcelSettings ProcessData(string cvsRawData)
    {
        char lineEnding = GetPlatformSpecificLineEnd();
        string[] rows = cvsRawData.Split(lineEnding);
        int dataStartRawIndex = 1;
        ExcelSettings data = new ExcelSettings();
        data.settings = new List<CycleSettings>();

        for (int i = dataStartRawIndex; i < rows.Length - 1; i++)
        {
            string[] cells = rows[i].Split(_cellSeporator);
            int id = ParseInt(cells[num]);
            Vector3 position = new Vector3(ParseFloat(cells[xCoord]), ParseFloat(cells[yCoord]), ParseFloat(cells[zCoord]));

            data.settings.Add(new CycleSettings()
            {
                num = id,
                position = position,
                enemies = new List<EnemiesSettings>()
            });
            string[] enemySells = rows[i+1].Split(_cellSeporator);
            while (enemySells[0] == "" && i+1 < rows.Length)
            {
                i++;
                var enemiesSettings = new EnemiesSettings();
                enemiesSettings.type = enemySells[enemyType];
                enemiesSettings.count = ParseInt(enemySells[countEnemy]);
                enemiesSettings.cooldown = ParseFloat(enemySells[coolDown]);
                data.settings[data.settings.Count - 1].enemies.Add(enemiesSettings);
                enemySells = rows[i].Split(_cellSeporator);
            }
            i--;
        }
        return data;

    }
    private int ParseInt(string s)
    {
        int result = -1;
        if (!int.TryParse(s, System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.GetCultureInfo("en-US"), out result))
        {
            Debug.Log("Can't parse int, wrong text");
            print(s);
        }

        return result;
    }
    
    private float ParseFloat(string s)
    {
        float result = -1;
        if (!float.TryParse(s, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.GetCultureInfo("en-US"), out result))
        {
            Debug.Log("Can't pars float,wrong text ");
        }

        return result;
    }
    
    private char GetPlatformSpecificLineEnd()
    {
        char lineEnding = '\n';
#if UNITY_IOS
        lineEnding = '\r';
#endif
        return lineEnding;
    }
}
