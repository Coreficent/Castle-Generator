using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Coreficent.Main;
using Coreficent.Utility;

public class RestartButton: Script
{
    // Start is called before the first frame update

    
    public TextMeshProUGUI restart;
    public AudioSource Audio;
    public Main main;
    public void RestartGame()
    {
        Audio.Play();
        // SceneManager.LoadScene("Main");
        main.Restart();

    }
}


