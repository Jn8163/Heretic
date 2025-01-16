using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EnemyAudioCalls : MonoBehaviour
{
    public static Action PlayAttackOne = delegate { };
    public static Action PlayAttackTwo = delegate { };
    public static Action PlayAgroYell = delegate { };
    public static Action PlayTaking = delegate { };

    [SerializeField] private AudioClip AttackOne;
    [SerializeField] private AudioClip AttackTwo;
    [SerializeField] private AudioClip AgroYell;
    [SerializeField] private AudioClip TakingDamage;
    [SerializeField] private GameObject DeathPrefab;

    private AudioSource AS;

    private void Awake()
    {
        AS = GetComponent<AudioSource>();
    }

    public void PlayAone()
    {
        AS.PlayOneShot(AttackOne);
    }
    public void PlayAtwo()
    {
        AS.PlayOneShot(AttackTwo);
    }
    public void PlayYell()
    {
        AS.PlayOneShot(AgroYell);
    }
    public void PlayTdamage()
    {
        AS.PlayOneShot(TakingDamage);
    }
    public void Die()
    {
        Instantiate(DeathPrefab);
    }

}
