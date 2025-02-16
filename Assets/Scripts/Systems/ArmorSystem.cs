using UnityEngine;

public class ArmorSystem : MonoBehaviour
{
    public static ArmorSystem instance;
    [SerializeField] private int maxShield;
    public int currentShieldHealth = 0;
    private float reductionAmount = .5f;
    public bool shieldEquipped;



    private void Start()
    {
        instance = this;   
    }



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
            currentShieldHealth = Mathf.Clamp(health, 0, maxShield);
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
        
        if(currentShieldHealth + reducedDamage <= 0)
        {
            reducedDamage = currentShieldHealth;
            DestroyShield();
        }
        else
        {
            currentShieldHealth += reducedDamage;
        }
        return damage - reducedDamage;
    }



    private void DestroyShield()
    {
        shieldEquipped = false;
        currentShieldHealth = 0;
    }
}