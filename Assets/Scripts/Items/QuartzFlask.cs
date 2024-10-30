using UnityEngine;

public class QuartzFlask : InventoryItem
{
    public override void Action()
    {
        //Update Players health - value the same as OG heretic
        GameObject.Find("Player").GetComponent<HealthSystem>().UpdateHealth(25);
    }
}