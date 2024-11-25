using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private int maxShield;
    private int currentShieldHealth = 0;
    private float reductionAmount = .5f;
    private bool shieldEquipped;



    /// <summary>
    /// Initilizes the eqipped shield.
    /// </summary>
    /// <param name="health"></param>
    /// <returns>If the shield was created</returns>
    public bool CreateShield(int health)
    {
        if (!shieldEquipped)
        {
            shieldEquipped = true;
            currentShieldHealth = Mathf.Clamp(health, 0, 200);
            return true;
        }
        return false;
    }



    /// <summary>
    /// decreases shield's health and returns decreased damage
    /// </summary>
    /// <param name="damage"></param>
    /// <returns></returns>
    public int UseShield(int damage)
    {
        int reducedDamage = (int)(damage * reductionAmount);
        
        if(currentShieldHealth <= reducedDamage)
        {
            reducedDamage = currentShieldHealth;
            DestroyShield();
        }
        else
        {
            currentShieldHealth -= reducedDamage;
        }
        return damage - reducedDamage;
    }



    private void DestroyShield()
    {
        shieldEquipped = false;
        currentShieldHealth = 0;
    }
}