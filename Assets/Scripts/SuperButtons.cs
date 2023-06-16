using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SuperButtons : MonoBehaviour
{
    [SerializeField] private Button SuperAttack;
    [SerializeField] private Image SuperAttackFilledImage;

    private void Start()
    {
        SuperAttack.onClick.AddListener(SuperAttackEvent);
    }

    private void SuperAttackEvent()
    {
        GlobalEvents.SuperAttackSpeed.Invoke(3f);
        StartCoroutine(FillImage(SuperAttackFilledImage, 3f));
    }

    private IEnumerator FillImage(Image image, float timer)
    {
        image.gameObject.SetActive(true);
        SuperAttack.interactable = false;
        float currentTimer = timer;
        while (currentTimer > 0)
        {
            image.fillAmount = Mathf.Lerp(0, 1, currentTimer / timer);
            currentTimer -= Time.deltaTime;
            yield return null;
        }
        image.gameObject.SetActive(false);
        SuperAttack.interactable = true;
    }
}
