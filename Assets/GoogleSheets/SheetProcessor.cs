using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SheetProcessor : MonoBehaviour
{
    //CycleSettings
    private const int num = 1;
    private const int xCoord = 3;
    private const int yCoord = 4;
    private const int enemyType = 5;
    private const int countEnemy = 6;
    private const int pauseBeforeSpawn = 7;
    private const int cooldDownBeetwenSpawns = 8;
    private const int coolDownAttack = 9;
    private const int health = 10;
    private const int attack = 11;

    //PlayerSettings
    private const int pGoldForUpgrade = 0;
    private const int pDamage = 1;
    private const int pAttackSpeed = 2;
    private const int pArmor = 3;
    private const int pHP = 4;
    private const int pRegeneration = 5;

    private const char _cellSeporator = ',';

    public ExcelSettings LoadCycleSettings(string cvsRawData)
    {
        char lineEnding = GetPlatformSpecificLineEnd();
        string[] rows = Convert(cvsRawData).Split(lineEnding);
        int dataStartRawIndex = 1;
        ExcelSettings data = new ();
        data.cycleSettings = new List<CycleSettings>();

        for (int i = dataStartRawIndex; i < rows.Length - 1; i++)
        {
            string[] cells = rows[i].Split(_cellSeporator);
            int id = ParseInt(cells[num]);
            data.cycleSettings.Add(new CycleSettings()
            {
                num = id,
                battlePoints = new List<BattlePoint>()
            });


            //read battlePoints
            i++;
            string[] battlePoints = rows[i].Split(_cellSeporator);
            while (battlePoints[2] == "BattlePoint" && i < rows.Length)
            {
                var battlePoint = new BattlePoint();
                battlePoint.position = new Vector2Int(ParseInt(battlePoints[xCoord]), ParseInt(battlePoints[yCoord]));
                battlePoint.enemies = new List<EnemiesSettings>();

                //read enemies
                i++;
                string[] enemySells = rows[i].Split(_cellSeporator);
                while (enemySells[0] == "" && enemySells[2] == "" && i < rows.Length)
                {
                    var enemiesSettings = new EnemiesSettings
                    {
                        type = enemySells[enemyType],
                        count = ParseInt(enemySells[countEnemy]),
                        coolDownAttack = ParseFloat(enemySells[coolDownAttack]),
                        health = ParseInt(enemySells[health]),
                        coolDownBeetwenSpawns = ParseFloat(enemySells[cooldDownBeetwenSpawns]),
                        damage = ParseFloat(enemySells[attack]),
                        pauseBeforeSpawn = ParseFloat(enemySells[pauseBeforeSpawn])
                    };
                    battlePoint.enemies.Add(enemiesSettings);
                    i++;
                    if (i < rows.Length)
                        enemySells = rows[i].Split(_cellSeporator);
                }
                data.cycleSettings[data.cycleSettings.Count - 1].battlePoints.Add(battlePoint);
                if (i < rows.Length)
                    battlePoints = rows[i].Split(_cellSeporator);
            }
            i--;
        }
        return data;

    }

    public List<PlayerSettings> LoadPlayerSettings(string cvsRawData)
    {
        char lineEnding = GetPlatformSpecificLineEnd();
        string[] rows = Convert(cvsRawData).Split(lineEnding);
        int dataStartRawIndex = 1;
        List<PlayerSettings> data = new();

        for (int i = dataStartRawIndex; i < rows.Length; i++)
        {
            string[] cells = rows[i].Split(_cellSeporator);
            var settings = new PlayerSettings
            {
                damage = ParseFloat(cells[pDamage]),
                attackSpeed = ParseFloat(cells[pAttackSpeed]),
                armor = ParseFloat(cells[pArmor]),
                health = ParseInt(cells[pHP]),
                regeneration = ParseFloat(cells[pRegeneration]),
                goldForUpgrade = ParseInt(cells[pGoldForUpgrade]),
            };
            data.Add(settings);
            print(data.Count);
        }
        return data;
    }


    private int ParseInt(string s)
    {
        int result = -1;
        if (!int.TryParse(s, System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.GetCultureInfo("en-US"), out result))
        {
            Debug.Log("Can't parse int, wrong text");
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

    //0,6,"0,6",1,10,"0,3"
    private string Convert(string s)
    {
        string result = "";
        for (int i = 0; i < s.Length;  i++)
        {
            if (s[i] == '"')
            {
                i++;
                while (s[i] != '"')
                {
                    if (s[i] == ',') result += '.';
                    else result += s[i];
                    i++;
                }
            }
            else
            {
                result += s[i];
            }
        }
        return result;
    }
}
