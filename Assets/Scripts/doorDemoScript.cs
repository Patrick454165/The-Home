//Name: Rose Machmer
//Date: 4/22/2026
//Purpose: End the game when the door is clicked on.

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

    void OnEnable() //Enables Event
    {
        EventManager.StartListening("activateEnding", activateEnding);
    }
    void OnDisable() //Disables Event
    {
        EventManager.StopListening("activateEnding", activateEnding);
    }
    public void activateEnding() 
    {
        audioSource.Play(); //Plays creaking sound
        winDisplay.SetActive(true); //*
        winTitle.SetActive(true); //These three reveal the end scene
        winSubtitle.SetActive(true); //*
        Invoke(nameof(ReturnToTitleScreen), 8f); //Sends you back after 8 seconds
    }

    void ReturnToTitleScreen() //Returns player to start screen
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    }
}
