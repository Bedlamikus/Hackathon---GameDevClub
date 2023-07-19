using UnityEngine;
using YG;

public class FightSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip shootStandart;

    [SerializeField] private AudioClip zombieSpawn;
    [SerializeField] private AudioClip zombieDeath;

    [SerializeField] private AudioClip applyCoin;

    [SerializeField] private AudioClip train;

    private PlayerStats settings;

    private void Start()
    {
        GlobalEvents.EnemyDie.AddListener(ZombieDeath);
    }

    public void PlayShootStandart()
    {
        if (Settings.Mute)  return;
        audioSource.PlayOneShot(shootStandart, 0.2f);
    }

    public void ZombieDeath()
    {
        if (Settings.Mute) return;
        int i = Random.Range(0, 100);
        if (i < 10) audioSource.PlayOneShot(zombieDeath);
    }

    public void ZombieSpawn()
    {
        if (Settings.Mute) return;
        audioSource.PlayOneShot(zombieSpawn);
    }

    public void ApplyCoin()
    {
        if (Settings.Mute) return;
        audioSource.PlayOneShot(applyCoin);
    }

    public void TrainStart()
    {
        if (Settings.Mute) return;
        audioSource.PlayOneShot(train);
    }

    public void TrainStop()
    {
        audioSource.Stop();
    }

    private PlayerStats Settings
    {
        get
        {
            if (settings == null)
            {
                settings = FindObjectOfType<PlayerStats>();
            }
            return settings;
        }
    }
}

