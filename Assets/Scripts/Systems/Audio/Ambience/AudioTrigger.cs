using System.Collections;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class AudioTrigger : GameAction
{
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private Mesh mesh;
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(playAudio());
        StartCoroutine(destroyPickup());
    }

    private IEnumerator playAudio()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
        yield return null;
    }

    private IEnumerator destroyPickup()
    {
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
