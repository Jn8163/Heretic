using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField]
    public Transform playerTPLoc;
    [SerializeField]
    public Transform destination;
    [SerializeField]
    public GameObject playerTP;
    [SerializeField]
    public Transform rotation;

    private void OnTriggerEnter(Collider other)
    {
        playerTP.SetActive(false);
        playerTPLoc.position = destination.position;
        playerTP.transform.LookAt(rotation);
        playerTP.SetActive(true);
    }
}
