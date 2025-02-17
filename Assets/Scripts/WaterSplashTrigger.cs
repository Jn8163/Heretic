using UnityEngine;

public class WaterSplashTrigger : MonoBehaviour
{
    [SerializeField] private AudioSource aSource;
    [SerializeField] private AudioClip aClip;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3 || other.gameObject.layer == 9)
        {
            AudioSource.PlayClipAtPoint(aClip, other.transform.position);
        }
    }
}
