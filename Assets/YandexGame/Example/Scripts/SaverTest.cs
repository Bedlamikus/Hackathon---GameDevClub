using UnityEngine;
using UnityEngine.UI;

namespace YG.Example
{
    public class SaverTest : MonoBehaviour
    {
        private GoogleSheetLoader googleSheetLoader;

        private void OnEnable() => YandexGame.GetDataEvent += GetLoad;
        private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;

        private void Awake()
        {
            if (YandexGame.SDKEnabled)
                GetLoad();
        }

        public void Save()
        {
            YandexGame.savesData.money = int.Parse(integerText.text);
            YandexGame.savesData.newPlayerName = stringifyText.text.ToString();

            for (int i = 0; i < booleanArrayToggle.Length; i++)
                YandexGame.savesData.openLevels[i] = booleanArrayToggle[i].isOn;

            YandexGame.SaveProgress();
        }

        public void Load() => YandexGame.LoadProgress();

        public void GetLoad()
        {
            GlobalEvents.DefaultSettingsLoaded.Invoke(YandexGame.savesData._data);
            if (YandexGame.savesData.isFirstSession) MetricEvents.Instance.FirstStartGame();
        }
    }
}