using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public InputField Name;
    public void StartGameScene()
    {
        if(Name.text != "")
        {
            PlayerStats.playerName = Name.text;
            SceneManager.LoadScene(1);
            AudioManager.instance.PlayIngameTheme();
        }
    }
    public void MuteSound()
    {
        AudioManager.instance.GetComponent<AudioSource>().mute = !AudioManager.instance.GetComponent<AudioSource>().mute;
    }
}
