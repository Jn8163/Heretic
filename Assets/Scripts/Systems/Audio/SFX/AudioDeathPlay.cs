using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioDeathPlay : MonoBehaviour
{
    private AudioSource AS;
    private void Awake()
    {
        AS = GetComponent<AudioSource>();
        float Dtime = AS.clip.length;
        Die(Dtime);
    }

    private IEnumerator Die(float Dtime)
    {
        yield return new WaitForSeconds(Dtime);
        Destroy(this.gameObject);
    }
}
