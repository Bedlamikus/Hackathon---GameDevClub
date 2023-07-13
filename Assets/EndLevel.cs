using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class EndLevel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Train>() == null) return;
        var data = YandexGame.Instance.savesData();
        data.levels[data.currentLevel].ended = true;
        data.currentLevel++;
        YandexGame.Instance._SaveProgress();
        SceneManager.LoadScene(0);
    }
}
