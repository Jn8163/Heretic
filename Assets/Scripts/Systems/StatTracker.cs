using UnityEngine;

public class StatTracker : MonoBehaviour
{
    public static int killCount;
    public static int itemCount;
    public static int secretCount;

    public static int maxKills;
    public static int maxItems;
    public static int maxSecrets;

    public static int time;
    public static void ResetStats()
    {
        killCount = 0;
        itemCount = 0;
        secretCount = 0;

        maxKills = 0;
        maxItems = 0;
        maxSecrets = 0;

        time = 0;
    }
}
