using UnityEngine;
using UnityEngine.UI;
using System;

public class HealthSystem : MonoBehaviour
{
    #region Fields

    [SerializeField] public bool bPlayer;
    [SerializeField] private int score;
    [SerializeField] private int maxHealth = 100;

    [SerializeField]private Image healthBar;

    public int currentHealth;
    private bool bAlive = true;

    public static Action<int> UpdateScore = delegate { };
    public static Action GameOver = delegate { };
    public event Action EnemyDeath;

    #endregion



    #region Methods

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void UpdateHealth(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        //talk to healthbar
        if (healthBar)
        {
            healthBar.fillAmount = currentHealth / (maxHealth * 1f);
        }

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