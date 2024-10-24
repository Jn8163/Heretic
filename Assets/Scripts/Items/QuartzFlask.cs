using UnityEngine;

public class QuartzFlask : MonoBehaviour
{
    public void Action()
    {
        //Update Players health - value the same as OG heretic
        GameObject.Find("Player").GetComponent<HealthSystem>().UpdateHealth(25);
    }
}
