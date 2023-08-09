using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] sfx;
    AudioSource sfxSource;

    static SoundManager instance;
    public static SoundManager Instance { get { return instance; } }
    // Start is called before the first frame update
    void Awake()
    {
        sfxSource = GetComponent<AudioSource>();
        instance = this;
    }
    private void Start()
    {
        GameManager.Instance.OnJump.AddListener(PlayJump);
        GameManager.Instance.OnLand.AddListener(PlayLand);
        GameManager.Instance.OnGameOver.AddListener(PlayGameOver);
        GameManager.Instance.OnEnding.AddListener(PlayLand);
    }
    void PlayJump()
    {
        sfxSource.clip = sfx[0];
        sfxSource.Play();
    }

    void PlayLand()
    {
        sfxSource.clip = sfx[1];
        sfxSource.Play();
    }

    public void PlayGameOver()
    {
        sfxSource.clip = sfx[2];
        sfxSource.Play();
    }
   
}
