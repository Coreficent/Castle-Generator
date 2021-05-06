namespace Coreficent.Interface
{
    using UnityEngine;
    using TMPro;
    using UnityEngine.SceneManagement;
    using Coreficent.Main;
    using Coreficent.Utility;

    public class RestartButton : MonoBehaviour
    {
        // Start is called before the first frame update
        public TextMeshProUGUI restart;
        public AudioSource Audio;
        // always null in "Start"
        public Main main;

        public void RestartGame()
        {
            if (SceneManager.GetActiveScene().name == "Start")
            {
                SceneManager.LoadScene("Main");
            }
            else if (SceneManager.GetActiveScene().name == "Main")
            {
                Audio.Play();
                //SceneManager.LoadScene("Main");
                main.Restart();
            }
            else
            {
                Test.Warn("unexpcted scene loaded");
            }
        }
    }
}
