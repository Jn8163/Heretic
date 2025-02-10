using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class HealthSystem : MonoBehaviour
{
    #region Fields

    [SerializeField] public bool bPlayer;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int score;
    [SerializeField]private Image healthBar;
    [SerializeField] private Animator anim;

    public int currentHealth;
    public bool bAlive = true;

    public static Action<int> UpdateScore = delegate { };
    public static Action GameOver = delegate { };
    public event Action EnemyDeath;

    #endregion



    #region Methods

    private void Start()
    {
        currentHealth = maxHealth;
    }



    public int GetMissingHealth()
    {
        return maxHealth - currentHealth;
    }

    public void UpdateHealth(int amount)
    {
        if (bPlayer && amount < 0)
        {
            if(ArmorSystem.instance.shieldEquipped)
            {
                amount = ArmorSystem.instance.UseShield(amount);
            }
        }
        else if (!bPlayer)
        {
            // GetComponentInChildren<EnemyAudioCalls>().PlayTdamage();         This should only play when the enemy is stunned
            
        }

        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth == 0 && bAlive)
        {
            bAlive = false;
            Death();
        }
    }

    

    public void Death()
    {
        if (!bPlayer)
        {
            UpdateScore(score);
            EnemyDeath?.Invoke();
        }

        if (bPlayer)
            GameOver();
    }

    #endregion

}