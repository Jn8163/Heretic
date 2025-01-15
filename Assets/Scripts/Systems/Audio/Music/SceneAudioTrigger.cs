using System;
using UnityEngine;

public class SceneAudioTrigger : MonoBehaviour
{
    public static Action<int> SceneMusicTrigger = delegate { };

    [SerializeField] private int MusicNumb;

    private void Start()
    {
        SceneMusicTrigger(MusicNumb);
    }
}
