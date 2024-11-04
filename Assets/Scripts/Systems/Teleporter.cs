using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField]
    public Transform TP_Start;
    [SerializeField]
    public Transform TP_End;
    [SerializeField]
    public GameObject player;
    [SerializeField]
    public Transform rotation;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>() != null)
        {
            player.SetActive(false);
            TP_Start.position = TP_End.position;
            player.transform.LookAt(rotation);
            player.SetActive(true);
        }
    }
}
