//Name: Rose Machmer
//Date: 4/22/2026
//Purpose: Send the player to the main game.

using UnityEngine;
using UnityEngine.SceneManagement;

public class moveToMainScene : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnClick() //Sends player to main game
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    
}
