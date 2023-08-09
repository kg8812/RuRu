using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmManager : MonoBehaviour
{
    public AudioClip bgm;
    AudioSource bgmSource;

    private void Awake()
    {
        bgmSource = GetComponent<AudioSource>();
        bgmSource.clip = bgm;
    }
    void Start()
    {
        bgmSource.Play();
        GameManager.Instance.OnGameOver.AddListener(Pause);
        GameManager.Instance.OnEnding.AddListener(Pause);
    }

    void Pause()
    {
        bgmSource.Stop();
    }
}
