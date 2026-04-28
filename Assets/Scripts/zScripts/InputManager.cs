//Name: Rose Machmer
//Date: 4/22/2026
//Purpose: Handle the commands for the console and anything the console controls.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InputManager : MonoBehaviour
{
    

    public TMP_Text storyText; // the story 
    public TMP_InputField userInput; // the input field object
    public TMP_Text inputText; // part of the input field where user enters response
    public TMP_Text placeHolderText; // part of the input field for initial placeholder text
    public ScrollRect scrollRect; //scrolls
    [TextArea]
    public string commandDescription;
    [TextArea]
    public string consoleLogs;
    [TextArea]
    public string accessQuestion;
    public string accessAnswer;
    
    public float textPosition=0f;

    public bool doorPermission = false;
    public bool doorOpened = false;
    
    
    private string story; // holds the story to display
    private List<string> commands = new List<string>();

    
    void Start()
    {
        
        //add all commands
        commands.Add("commands");
        commands.Add("open");
        commands.Add("log");
        commands.Add("logs");
        commands.Add("message");
        commands.Add("access");
        commands.Add("answer");
        story = storyText.text;
        userInput.onEndEdit.AddListener(GetInput);
        scrollRect.verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
        

        
    }

    IEnumerator ScrollToBottom()//move to bottom.
    {
        yield return new WaitForEndOfFrame();
        
        scrollRect.verticalNormalizedPosition = textPosition; 
    }


    void GetInput(string input) //Grabs an input and applies the proper output
    {
        
        userInput.text = "";  
        userInput.ActivateInputField();

        if(input != "") //makes sure there's actually something in there
        {
            char[] delims = { ' ' };
            string[] parts = input.ToLower().Split(delims); //part[0] is command, [1] is directive
            if(parts.Length >= 2)
            {
                
            
                if (commands.Contains(parts[0]))
                {
                    UpdateStory(">" + input);
                    if(parts[0] == "open" && parts[1] == "door") //allows you to leave the garage
                    {
                        if (doorPermission)
                        {
                            doorOpened=true;
                            UpdateStory("Door Unlocked!");
                        }
                        else
                        {
                            UpdateStory("You do not have the necessary permissions to access this. Please type 'access door' ");
                        }
                    }
                    else if(parts[0] == "answer" && parts[1] == accessAnswer) //Checks if you've solved the puzzle
                    {
                        doorPermission=true;
                        UpdateStory("Correct! Door access has been granted. Type 'Open Door' to unlock it.");
                    }
                    
                    else if(parts[0] == "message") //Exists so player can't do what Niel does
                    {
                        UpdateStory("You do not have the necessary permissions to access this. Please try again later.");
                    }
                    
                    else
                    {
                        UpdateStory("Invalid Command, try using 'commands' to see your options");
                    }
                }
                else //command not valid
                {
                    UpdateStory(">" + input);
                    UpdateStory("Invalid Command, try using 'commands' to see your options");
                }
            }//two
            else if(parts.Length > 0)
            {
                UpdateStory(">" + input);
                if(parts[0] == "log" || parts[0] == "logs") //get clues for puzzle
                {
                    UpdateStory(consoleLogs);
                }
                else if(parts[0] == "commands") //get all allowed actions
                {
                    UpdateStory(commandDescription);
                }
                else if(parts[0] == "access") //Get question
                {
                        
                    UpdateStory("Opening Access Question: " + accessQuestion);
                }
                else
                {
                    UpdateStory("Invalid Command, try using 'commands' to see your options");
                }
                
        
        
        
        
            }
            else //command not valid
            {
                UpdateStory(">" + input);
                UpdateStory("Invalid Command, try using 'commands' to see your options");
            }
        }
        
    }

    public void UpdateStory(string msg) //Adds message to console display
    {
        story += "\n" + msg;
        storyText.text = story;
        consoleLogs+= "\nApril 19th:\n" + msg + "\n" + "By new_user"; //Adds your messages to console
        StartCoroutine("ScrollToBottom");
    }
}
