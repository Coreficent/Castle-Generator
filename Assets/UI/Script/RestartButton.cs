using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RestartButton: MonoBehaviour
{
    // Start is called before the first frame update

    
    public TextMeshProUGUI restart;
    public AudioSource Audio;

    void Start()
    {
        
    }

    public void RestartGame()
    {
        Audio.Play();
        SceneManager.LoadScene("Main");
        

    }
}


