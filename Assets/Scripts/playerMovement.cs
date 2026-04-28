//Name: Rose Machmer
//Date: 4/22/2026
//Purpose: Allow the player to move, open consoles, and open doors.

using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using TMPro;
using UnityEngine.UI;


public class playerMovement : MonoBehaviour
{

    public Rigidbody rb;
    public PlayerInput pi;
    Vector2 getMovement;
    public bool canMove;
    public float moveModifier;
    public float checkThisFarForConsole;
    public GameObject cam;
    public Camera camAsset;
    public Camera conCam;
    public GameObject conConsole;
    public GameObject conCanvas;
    public TextMeshProUGUI Prompt;
    public float fadeDuration;
    
    public Image PromptBackground;
    
    bool seenEscTutorial=false;
    bool seenConTutorial=false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public void Awake()
    {
        activatePrompt("Use WASD to move.");
        
    }
    public void activatePrompt(string text) //Adds tutorial information to explain the controls
    {
        Prompt.text=text;
        Prompt.color = new Color(0, 0, 0, 0);
        StopCoroutine("FadeInText"); //Makes sure the fades don't overlap
        StopCoroutine("FadeOutText");
        StartCoroutine("FadeInText"); 
    }
    public void activatePrompt(string text, Color color) //Same as previous but lets you set the text color
    {
        Prompt.color=color;
        Prompt.text=text;
        Prompt.color = new Color(Prompt.color.r, Prompt.color.g, Prompt.color.b, 0);
        StopCoroutine("FadeInText"); //Makes sure the fades don't overlap
        StopCoroutine("FadeOutText");
        StartCoroutine("FadeInText");
    }
    public void OnMove(InputValue value) //Applies momentum
    {
        if (canMove) //Makes sure you can move
        {
            getMovement = value.Get<Vector2>(); //gets movement values
            if (!seenConTutorial)
            {
                activatePrompt("Click on the computer with the left mouse\nbutton to turn it on.");
                seenConTutorial=true;
            }
        }
        
    }

    IEnumerator FadeInText() //Make text slowly appear
    {
        float elapsed = 0f;
        
        Color color = Prompt.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            // Linearly interpolate alpha from 0 to 1
            float alpha = Mathf.Lerp(0f, 1f, elapsed / fadeDuration);
            Prompt.color = new Color(color.r, color.g, color.b, alpha);
            PromptBackground.color = new Color(color.r, color.g, color.b, alpha/2);
            yield return null; // Wait for the next frame
        }
        yield return new WaitForSeconds(2);
        yield return StartCoroutine(FadeOutText()); //Have the text vanish now that they've probably read it
    }
    IEnumerator FadeOutText() //Make text slowly vanish
    {

        float elapsed = fadeDuration;
        
        
        Color color = Prompt.color;

        while (elapsed >= 0)
        {
            
            elapsed -= Time.deltaTime;
            // Linearly interpolate alpha from 0 to 1
            float alpha = Mathf.Lerp(0f, 1f, elapsed / fadeDuration);
            Prompt.color = new Color(color.r, color.g, color.b, alpha);
            PromptBackground.color = new Color(color.r, color.g, color.b, alpha/2);
            yield return null; // Wait for the next frame
        }
    }
    public void OnAttack(InputValue value) //Find the console, attach to it and open console view
    {
        if (canMove) //only runs if not already zoomed in
        {
            Debug.DrawRay(cam.transform.position, cam.transform.TransformDirection(Vector3.forward)*checkThisFarForConsole, Color.red, 10f);
            RaycastHit hit; //this all checks if you're looking at the console
            if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, checkThisFarForConsole))
            {
                if (hit.collider.gameObject.CompareTag("Console"))
                {
                    consoleScript con = hit.collider.gameObject.GetComponent<consoleScript>();
                    conCam = con.cameraAsset; //grab canvas and camera for this console
                    conCanvas=con.canvas; //
                    con.audioSource.Play(); //Play console hiss
                    conConsole = hit.collider.gameObject;
                    camAsset.targetDisplay=1;camAsset.enabled=false; //Changes display so you are looking through console camera
                    conCam.targetDisplay=0;conCam.enabled=true; //
                    Cursor.lockState = CursorLockMode.None; //lets you use cursor
                    canMove=false; 
                    conCanvas.SetActive(true);
                    if (!seenEscTutorial)
                    {
                        activatePrompt("Click on input bar to type. Press ESC to back out");
                        seenEscTutorial=true;
                    }
                }
                if (hit.collider.gameObject.CompareTag("Door")) //Open door and end Demo
                {
                    if (hit.collider.gameObject.GetComponent<doorDemoScript>().console.GetComponent<InputManager>().doorOpened)
                    {
                        canMove=false;
                        EventManager.TriggerEvent("activateEnding");
                    }
                }
            }
        }
        
    }

    public void OnExit(InputValue value) //exists to remove you from viewing the console
    {
        if (!canMove)
        {
            Cursor.lockState = CursorLockMode.Locked;
            canMove=true;
            
            camAsset.targetDisplay=0;camAsset.enabled=true;
            conCam.targetDisplay=1;conCam.enabled=false;
            conCanvas.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(new(getMovement.x*moveModifier*Time.deltaTime, 0, getMovement.y*moveModifier*Time.deltaTime)); //handles movement

    }

    
    void OnCollisionStay(Collision collision) //mostly here so I don't clip through walls
    {
        if (collision.gameObject.CompareTag("restricted"))
        {
            transform.Translate(new(-getMovement.x*moveModifier*Time.deltaTime, 0, -getMovement.y*moveModifier*Time.deltaTime));
        }
        
    }
}
