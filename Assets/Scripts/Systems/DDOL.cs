using UnityEngine;

public class DDOL : MonoBehaviour
{
    public static DDOL Main;
    private void Awake()
    {
        if (Main == null)
        {
            Main = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
