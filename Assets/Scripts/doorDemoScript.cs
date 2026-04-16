using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class doorDemoScript : MonoBehaviour
{
    public GameObject winDisplay;
    public GameObject winTitle;
    public GameObject winSubtitle;
    public InputManager console;
    public AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void OnEnable()
    {
        EventManager.StartListening("activateEnding", activateEnding);
    }
    void OnDisable()
    {
        EventManager.StopListening("activateEnding", activateEnding);
    }
    public void activateEnding()
    {
        audioSource.Play();
        winDisplay.SetActive(true);
        winTitle.SetActive(true);
        winSubtitle.SetActive(true);
        Invoke(nameof(ReturnToTitleScreen), 8f);
    }

    void ReturnToTitleScreen()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    }
}
