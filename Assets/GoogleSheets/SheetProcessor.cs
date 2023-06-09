using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;

public class SheetProcessor : MonoBehaviour
{
    //CycleSettings
    private const int cNum = 1;
    private const int xCoord = 3;
    private const int yCoord = 4;
    private const int cEnemyType = 5;
    private const int cCountEnemy = 6;
    private const int cPauseBeforeSpawn = 7;
    private const int cCooldDownBeetwenSpawns = 8;
    private const int eEnemyType = 0;
    private const int eHealth = 1;
    private const int eAttack = 2;
    private const int eSpeedMultiplier = 3;
    private const int eScale = 4;
    private const int eCoolDownAttack = 5;

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
            int id = ParseInt(cells[cNum]);
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
                battlePoint.enemies = new List<EnemiesSpawnerByCycle>();

                //read enemies
                i++;
                string[] enemySells = rows[i].Split(_cellSeporator);
                while (enemySells[0] == "" && enemySells[2] == "" && i < rows.Length)
                {
                    var enemiesSettings = new EnemiesSpawnerByCycle
                    {
                        type = enemySells[cEnemyType],
                        count = ParseInt(enemySells[cCountEnemy]),
                        coolDownBeetwenSpawns = ParseFloat(enemySells[cCooldDownBeetwenSpawns]),
                        pauseBeforeSpawn = ParseFloat(enemySells[cPauseBeforeSpawn]),
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
        }
        return data;
    }

    public List<EnemiesSettings> LoadEnemiesSettings(string cvsRawData)
    {
        char lineEnding = GetPlatformSpecificLineEnd();
        string[] rows = Convert(cvsRawData).Split(lineEnding);
        int dataStartRawIndex = 1;
        List<EnemiesSettings> data = new();

        for (int i = dataStartRawIndex; i < rows.Length; i += 2)
        {
            string[] cells = rows[i].Split(_cellSeporator);
            var settings = new EnemiesSettings
            {
                enemyType = cells[eEnemyType],
                health = ParseInt(cells[eHealth]),
                damage = ParseFloat(cells[eAttack]),
                speedMultiplier = ParseFloat(cells[eSpeedMultiplier]),
                scale = ParseFloat(cells[eScale]),
                coolDownAttack = ParseFloat(cells[eCoolDownAttack]),
            };
            string[] additionCells = rows[i+1].Split(_cellSeporator);
            settings.additionHealth = ParseInt(additionCells[eHealth]);
            settings.additionDamage = ParseFloat(additionCells[eAttack]);
            settings.additionSpeedMultiplier = ParseFloat(additionCells[eSpeedMultiplier]);
            settings.additionScale = ParseFloat(additionCells[eScale]);
            settings.additionCoolDownAttack = ParseFloat(additionCells[eCoolDownAttack]);
            data.Add(settings);
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
