
using System.Collections.Generic;
using static UnityEngine.Experimental.Rendering.RayTracingAccelerationStructure;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // Ваши сохранения

        public ExcelSettings _defaultData;
        public string playerStatsData = "";
        public List<Level> levels;
        public int currentLevel;

        public SavesYG()
        {
            string[] rawCSVtext = new string[1];
            rawCSVtext[0] = "rev 001,num,,xCoord,yCoord,EnemyType,CountEnemy,PauseBeforeSpawn,CoolDownBetweenSpawns in sec\r\nCycle,1,,,,,,,\r\n,,BattlePoint,1,7,,,,\r\n,,,,,BaseZombie,12,0,1\r\n,,BattlePoint,4,3,,,,\r\n,,,,,BaseZombie,18,0,1\r\nCycle,2,,,,,,,\r\n,,BattlePoint,1,7,,,,\r\n,,,,,BaseZombie,15,0,\"0,5\"\r\n,,BattlePoint,4,2,,,,\r\n,,,,,FastZombie,10,0,0.9\r\n,,,,,BaseZombie,15,3,0.7\r\nCycle,3,,,,,,,\r\n,,BattlePoint,0,0,,,,\r\n,,,,,SlowZombie,10,0,1\r\n,,BattlePoint,0,0,,,,\r\n,,,,,BaseZombie,15,0,0.9\r\n,,,,,BaseZombie,15,4,0.7\r\nCycle,4,,,,,,,\r\n,,BattlePoint,0,0,,,,\r\n,,,,,FastZombie,18,0,\"0,5\"\r\n,,BattlePoint,0,0,,,,\r\n,,,,,BaseZombie,16,0,0.9\r\n,,,,,SlowZombie,14,7,1\r\nCycle,5,,,,,,,\r\n,,BattlePoint,0,0,,,,\r\n,,,,,SlowZombie,18,0,1\r\n,,BattlePoint,0,0,,,,\r\n,,,,,BaseZombie,20,0,0.9\r\n,,,,,BaseZombie,14,7,0.7\r\nCycle,6,,,,,,,\r\n,,BattlePoint,0,0,,,,\r\n,,,,,BaseZombie,20,0,\"0,5\"\r\n,,BattlePoint,0,0,,,,\r\n,,,,,SlowZombie,18,0,1\r\n,,,,,SlowZombie,25,7,1\r\nCycle,7,,,,,,,\r\n,,BattlePoint,0,0,,,,\r\n,,,,,BaseZombie,21,0,\"0,5\"\r\n,,BattlePoint,0,0,,,,\r\n,,,,,FastZombie,24,0,0.9\r\n,,,,,BaseZombie,27,7,0.7\r\nCycle,8,,,,,,,\r\n,,BattlePoint,0,0,,,,\r\n,,,,,SlowZombie,25,0,1\r\n,,BattlePoint,0,0,,,,\r\n,,,,,BaseZombie,25,0,0.9\r\n,,,,,BaseZombie,32,7,0.7\r\nCycle,9,,,,,,,\r\n,,BattlePoint,0,0,,,,\r\n,,,,,BaseZombie,25,0,\"0,5\"\r\n,,BattlePoint,0,0,,,,\r\n,,,,,FastZombie,30,0,0.9\r\n,,,,,SlowZombie,35,7,1\r\nCycle,10,,,,,,,\r\n,,BattlePoint,0,0,,,,\r\n,,,,,BaseZombie,32,0,\"0,5\"\r\n,,BattlePoint,0,0,,,,\r\n,,,,,SlowZombie,38,0,1\r\n,,,,,BaseZombie,40,7,0.7";
            _defaultData = SheetProcessor.LoadCycleSettings(rawCSVtext[0]);
            rawCSVtext[0] = "Gold for Upg,Damage,Atk speed,Armor,HP,regen,Range\r\n0,6,2,1,10,\"0,3\",6\r\n8,4,\"0,2\",\"0,5\",8,\"0,25\",\"0,25\"";
            _defaultData.playerSettings = SheetProcessor.LoadPlayerSettings(rawCSVtext[0]);
            rawCSVtext[0] = "NamePrefab,HP,Atk,Speed,Prefab size,CoolDown attack\r\nBaseZombie,5,2,1,1,1\r\naddition,3,\"1,5\",0,0,0\r\nFastZombie,5,3,2,\"0,7\",1\r\naddition,3,2,0,0,0\r\nSlowZombie,20,2,\"0,6\",\"1,4\",1\r\naddition,12,2,0,0,0";
            _defaultData.enemiesSettings = SheetProcessor.LoadEnemiesSettings(rawCSVtext[0]);
        }
    }
}
