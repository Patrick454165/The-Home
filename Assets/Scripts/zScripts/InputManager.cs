using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

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
        

        
    }

    IEnumerator ScrollToBottom()
    {
        yield return new WaitForEndOfFrame();
        
        scrollRect.verticalNormalizedPosition = textPosition; //move to bottom.
    }


    void GetInput(string input)
    {
        
        userInput.text = "";  
        userInput.ActivateInputField();

        if(input != "")
        {
            char[] delims = { ' ' };
            string[] parts = input.ToLower().Split(delims); //part[0] is command, [1] is directive
            if(parts.Length >= 2)
            {
                
            
                if (commands.Contains(parts[0]))
                {
                    UpdateStory(">" + input);
                    if(parts[0] == "open" && parts[1] == "door") 
                    {
                        if (doorPermission)
                        {
                            UpdateStory("Door Unlocked!");
                        }
                        else
                        {
                            UpdateStory("You do not have the necessary permissions to access this. Please type 'access door' ");
                        }
                    }
                    else if(parts[0] == "answer" && parts[1] == accessAnswer)
                    {
                        doorPermission=true;
                        UpdateStory("Correct! Door access has been granted.");
                    }
                    
                    else if(parts[0] == "message") //Exists so player can't do what Niel does
                    {
                        UpdateStory("You do not have the necessary permissions to access this. Please try again later.");
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
        else //command not valid
        {
            UpdateStory(">" + input);
            UpdateStory("Invalid Command, try using 'commands' to see your options");
        }
    }

    public void UpdateStory(string msg) 
    {
        story += "\n" + msg;
        storyText.text = story;
        consoleLogs+= "\nApril 19th:\n" + msg + "\n" + "By new_user"; //Adds your messages to console
        StartCoroutine("ScrollToBottom");
    }
}
