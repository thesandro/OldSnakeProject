using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        if (AudioManager.instance.GetComponent<AudioSource>().mute == false)
        {
            GameObject.Find("Eat").GetComponent<AudioSource>().mute = false;
            GameObject.Find("Death").GetComponent<AudioSource>().mute = false;
        }
        else
        {
            GameObject.Find("Eat").GetComponent<AudioSource>().mute = true;
            GameObject.Find("Death").GetComponent<AudioSource>().mute = true;
        }
    }
    public void MuteSound()
    {
        if (!AudioManager.instance.GetComponent<AudioSource>().mute == false)
        {
            AudioManager.instance.GetComponent<AudioSource>().mute = false;
            GameObject.Find("Eat").GetComponent<AudioSource>().mute = false;
            GameObject.Find("Death").GetComponent<AudioSource>().mute = false;
        }
        else
        {
            AudioManager.instance.GetComponent<AudioSource>().mute = true;
            GameObject.Find("Eat").GetComponent<AudioSource>().mute = true;
            GameObject.Find("Death").GetComponent<AudioSource>().mute = true;
        }

    }
}
