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

    [SerializeField] private float timeSuperDamage = 3f;
    [SerializeField] private float timeSuperDamageCoolDown = 8f;
    [SerializeField] private float multiplySuperDamage = 1.25f;
    [SerializeField] private Button buttonSuperDamage;
    [SerializeField] private Image imageSuperDamageFilled;

    private void Start()
    {
        buttonSuperAttack.onClick.AddListener(SuperAttackEvent);
        buttonSuperRegen.onClick.AddListener(SuperRegenEvent);
        buttonSuperDamage.onClick.AddListener(SuperDamageEvent);
    }

    private void OnEnable()
    {
        StartCoroutine(FillImage(imageSuperAttackFilled, buttonSuperAttack, timeSuperAttackCoolDown / 10));
        StartCoroutine(FillImage(imageSuperRegenFilled, buttonSuperRegen, timeSuperRegenCoolDown / 10));
        StartCoroutine(FillImage(imageSuperDamageFilled, buttonSuperDamage, timeSuperDamageCoolDown / 10));
    }

    private void SuperAttackEvent()
    {
        MetricEvents.Instance.FightAttackSpeed();
        GlobalEvents.SuperAttackSpeed.Invoke(timeSuperAttack, multiplySuperAttackSpeed);
        StartCoroutine(FillImage(imageSuperAttackFilled, buttonSuperAttack, timeSuperAttackCoolDown));
    }

    private void SuperRegenEvent()
    {
        MetricEvents.Instance.FightHealth();
        GlobalEvents.SuperRegen.Invoke(timeSuperRegen, multiplySuperRegenSpeed);
        StartCoroutine(FillImage(imageSuperRegenFilled, buttonSuperRegen, timeSuperRegenCoolDown));
    }

    private void SuperDamageEvent()
    {
        MetricEvents.Instance.FightAttackDamage();
        GlobalEvents.SuperDamage.Invoke(timeSuperDamage, multiplySuperDamage);
        StartCoroutine(FillImage(imageSuperDamageFilled, buttonSuperDamage, timeSuperDamageCoolDown));
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
