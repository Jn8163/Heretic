using UnityEngine;

public class QuartzFlask : InventoryItem
{
    private HealthSystem hSystem;
    public override void Action()
    {
		//Update Players health - value the same as OG heretic
		hSystem = GameObject.Find("Player").GetComponent<HealthSystem>();
        if (hSystem.GetMissingHealth() > 0)
        {
            hSystem.UpdateHealth(25);
        }

	}
}