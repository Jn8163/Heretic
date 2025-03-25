using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class HealthSystem : MonoBehaviour, IManageData
{
    #region Fields

    [SerializeField] public bool bPlayer;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int score;
    [SerializeField]private Image healthBar;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject hurtScreen;

    public int currentHealth;
    public bool bAlive = true;

    public static Action<int> UpdateScore = delegate { };
    public static Action GameOver = delegate { };
    public event Action EnemyDeath;

    #endregion



    #region Methods

    private void Start()
    {
        if (currentHealth == 0)
        {
            currentHealth = maxHealth;
        }
    }



    public int GetMissingHealth()
    {
        return maxHealth - currentHealth;
    }

    public void UpdateHealth(int amount)
    {
        if (bPlayer)
        {
            if(ArmorSystem.instance.shieldEquipped)
            {
                if(amount < 0)
					amount = ArmorSystem.instance.UseShield(amount);
			}
            if (!ActivateRing.isInvincible)
            {
                if(amount < 0)
					StartCoroutine(nameof(FlashHurtScreen));
				currentHealth += amount;
                currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            }
            else if (amount > 0)
            {
				currentHealth += amount;
				currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
			}
		}
        else if (!bPlayer)
        {
			// GetComponentInChildren<EnemyAudioCalls>().PlayTdamage();         This should only play when the enemy is stunned
			currentHealth += amount;
			currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
		}

        if (currentHealth == 0 && bAlive)
        {
            bAlive = false;
            Death();
        }
    }

    IEnumerator FlashHurtScreen()
    {
        hurtScreen.SetActive(true);
        yield return new WaitForSeconds(8f / 60f);
		hurtScreen.SetActive(false);
	}

    public void Death()
    {
        if (!bPlayer)
        {
            if (transform.parent)
            {
                transform.parent.GetComponent<Spawn>().targetActive = false;
            }
            UpdateScore(score);
            EnemyDeath?.Invoke();
        }

        if (bPlayer)
            GameOver();
    }

    public void LoadData(GameData data)
    {
        if (TryGetComponent<GameObjectID>(out GameObjectID goID))
        {
            string id = goID.GetID();
            if (data.currentHealths.ContainsKey(id))
            {
                currentHealth = data.currentHealths[id];
            }

            if (data.deathState.ContainsKey(id))
            {
                bAlive = data.deathState[id];
            }
        }
        else
        {
            Debug.Log($"ID not assigned for gameobject: {gameObject.name}");
        }
    }

    public void SaveData(ref GameData data)
    {
        if (TryGetComponent<GameObjectID>(out GameObjectID goID))
        {
            string id = goID.GetID();
            if (data.currentHealths.ContainsKey(id))
            {
                data.currentHealths[id] = currentHealth;
            }
            else
            {
                data.currentHealths.Add(id, currentHealth);
            }

            if (data.deathState.ContainsKey(id))
            {
                data.deathState[id] = bAlive;
            }
            else
            {
                data.deathState.Add(id, bAlive);
            }
        }
    }

    #endregion
}