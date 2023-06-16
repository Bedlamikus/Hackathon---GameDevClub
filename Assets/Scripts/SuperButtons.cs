using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SuperButtons : MonoBehaviour
{
    [SerializeField] private float timeSuperAttack = 1f;
    [SerializeField] private float timeSuperAttackCoolDown = 3f;
    [SerializeField] private float multiplySuperAttackSpeed = 3f;
    [SerializeField] private Button buttonSuperAttack;
    [SerializeField] private Image imageSuperAttackFilled;

    [SerializeField] private float timeSuperRegen = 1f;
    [SerializeField] private float timeSuperRegenCoolDown = 3f;
    [SerializeField] private float multiplySuperRegenSpeed = 3f;
    [SerializeField] private Button buttonSuperRegen;
    [SerializeField] private Image imageSuperRegenFilled;

    private void Start()
    {
        buttonSuperAttack.onClick.AddListener(SuperAttackEvent);
        buttonSuperRegen.onClick.AddListener(SuperRegenEvent);
    }

    private void OnEnable()
    {
        StartCoroutine(FillImage(imageSuperAttackFilled, buttonSuperAttack, timeSuperAttackCoolDown));
        StartCoroutine(FillImage(imageSuperRegenFilled, buttonSuperRegen, timeSuperRegenCoolDown));
    }

    private void SuperAttackEvent()
    {
        GlobalEvents.SuperAttackSpeed.Invoke(timeSuperAttack, multiplySuperAttackSpeed);
        StartCoroutine(FillImage(imageSuperAttackFilled, buttonSuperAttack, timeSuperAttackCoolDown));
    }

    private void SuperRegenEvent()
    {
        GlobalEvents.SuperRegen.Invoke(timeSuperRegen, multiplySuperRegenSpeed);
        StartCoroutine(FillImage(imageSuperRegenFilled, buttonSuperRegen, timeSuperRegenCoolDown));
    }

    private IEnumerator FillImage(Image image, Button button, float timer)
    {
        image.gameObject.SetActive(true);
        button.interactable = false;
        float currentTimer = timer;
        while (currentTimer > 0)
        {
            image.fillAmount = Mathf.Lerp(0, 1, currentTimer / timer);
            currentTimer -= Time.deltaTime;
            yield return null;
        }
        image.gameObject.SetActive(false);
        button.interactable = true;
    }
}
