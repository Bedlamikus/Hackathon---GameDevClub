using UnityEngine;

public class FightSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip shootStandart;

    [SerializeField] private AudioClip zombieSpawn;
    [SerializeField] private AudioClip zombieDeath;

    [SerializeField] private AudioClip applyCoin;

    [SerializeField] private AudioClip train;

    private void Start()
    {
        GlobalEvents.EnemyDie.AddListener(ZombieDeath);
    }

    public void PlayShootStandart()
    {
        audioSource.PlayOneShot(shootStandart, 0.2f);
    }

    public void ZombieDeath()
    {
        int i = Random.Range(0, 100);
        if (i < 10) audioSource.PlayOneShot(zombieDeath);
    }

    public void ZombieSpawn()
    {
        audioSource.PlayOneShot(zombieSpawn);
    }

    public void ApplyCoin()
    {
        audioSource.PlayOneShot(applyCoin);
    }

    public void TrainStart()
    {
        audioSource.PlayOneShot(train);
    }

    public void TrainStop()
    {
        audioSource.Stop();
    }
}
