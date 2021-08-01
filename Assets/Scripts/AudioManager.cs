using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    AudioSource audiothingy;
    public AudioClip mainTheme;
    public AudioClip ingameTheme;
    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        audiothingy = GetComponent<AudioSource>();


    }
    public void PlayStartTheme()
    {
        audiothingy.clip = mainTheme;
        audiothingy.Play();
    }
    public void PlayIngameTheme()
    {
        audiothingy.Stop();
        audiothingy.clip = ingameTheme;
        audiothingy.Play();
    }
}
