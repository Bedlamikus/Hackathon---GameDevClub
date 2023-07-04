using UnityEngine;
using UnityEngine.UI;

namespace YG.Example
{
    public class SaverTest : MonoBehaviour
    {
        private PlayerStats playerStats;

        private void OnEnable() => YandexGame.GetDataEvent += GetLoad;
        private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;

        private void Awake()
        {
            if (YandexGame.SDKEnabled)
                GetLoad();
            GlobalEvents.SaveCurrentSettings.AddListener(Save);
        }


        public void Save()
        {
            YandexGame._savesData.playerStatsData = playerStats.GetCurrentJsonSettings();
            YandexGame.SaveProgress();
        }

        public void Load() => YandexGame.LoadProgress();

        public void GetLoad()
        {
            print("try load settings in SaverTest");
            GlobalEvents.SettingsLoaded.Invoke(YandexGame.Instance.savesData().playerStatsData);
            if (YandexGame._savesData.isFirstSession) MetricEvents.Instance.FirstStartGame();
        }
    }
}