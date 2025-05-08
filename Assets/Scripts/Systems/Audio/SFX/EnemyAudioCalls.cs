using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EnemyAudioCalls : MonoBehaviour, IManageData
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
    [SerializeField] private EnemyType enemyType;
    private GameObject spawnedPFab;

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
        spawnedPFab = Instantiate(DeathPrefab, transform.position, DeathPrefab.transform.rotation);
    }

    public void LoadData(GameData data)
    {
        string id = "";
        if (gameObject.transform.parent != null && transform.parent.TryGetComponent<GameObjectID>(out GameObjectID goID))
        {
            id = goID.GetID();
        }

        switch (enemyType)
        {
            case EnemyType.Gargoyle:
                if (data.gargCorpsePos.ContainsKey(id))
                {
                    spawnedPFab = Instantiate(DeathPrefab, data.gargCorpsePos[id], DeathPrefab.transform.rotation);
                }
                break;
            case EnemyType.Golem:
                if (data.golemCorpsePos.ContainsKey(id))
                {
                    spawnedPFab = Instantiate(DeathPrefab, data.golemCorpsePos[id], DeathPrefab.transform.rotation);
                }
                break;
            case EnemyType.UndeadWarrior:
                if (data.undeadWCorpsePos.ContainsKey(id))
                {
                    spawnedPFab = Instantiate(DeathPrefab, data.undeadWCorpsePos[id], DeathPrefab.transform.rotation);
                }
                break;
            default:
#if UNITY_EDITOR
                Debug.Log("Enemy Remains Cannot be saved because enemy type hasn't been set in Inspector");
#endif
                break;
        }
    }

    public void SaveData(ref GameData data)
    {
        string id = "";
        if(gameObject.transform.parent != null && transform.parent.TryGetComponent<GameObjectID>(out GameObjectID goID))
        {
            id = goID.GetID();
        }

        switch (enemyType)
        {
            case EnemyType.Gargoyle:
                if (data.gargCorpsePos.ContainsKey(id))
                {
                    data.gargCorpsePos[id] = transform.position;
                }
                else
                {
                    data.gargCorpsePos.Add(id, transform.position);
                }
                break;
            case EnemyType.Golem:
                if (data.golemCorpsePos.ContainsKey(id))
                {
                    data.golemCorpsePos[id] = transform.position;
                }
                else
                {
                    data.golemCorpsePos.Add(id, transform.position);
                }
                break;
            case EnemyType.UndeadWarrior:
                if (data.undeadWCorpsePos.ContainsKey(id))
                {
                    data.undeadWCorpsePos[id] = transform.position;
                }
                else
                {
                    data.undeadWCorpsePos.Add(id, transform.position);
                }
                break;
            default:
#if UNITY_EDITOR
                Debug.Log("Enemy Remains Cannot be saved because enemy type hasn't been set in Inspector");
#endif
                break;
        }
    }

    enum EnemyType
    {
        None,
        Gargoyle,
        Golem,
        UndeadWarrior
    }
}
