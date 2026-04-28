//Name: Rose Machmer
//Date: 4/22/2026
//Purpose: Handle events

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using Unity.VisualScripting;

//This is going to replace notification manager class
//Be sure to attach this to our GameManager class!
public class EventManager : MonoBehaviour
{

    //These are the things we are going to listen to
    private Dictionary<string, UnityAction> eventDictionary;

    //allows us to access the instance from other classes!
    private static EventManager eventManager; 

    //using our C# setter and getter technique to setup our instance
    public static EventManager instance
    {
        get //a different way of implementing our singleton
        {
            if (!eventManager)
            {
                eventManager = Object.FindAnyObjectByType(typeof(EventManager)) as EventManager;

                if (!eventManager)
                {
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                }
                else
                {
                    eventManager.Init();
                    
                }
            }

            return eventManager;
        }
    }

    void Init()
    {
        DontDestroyOnLoad(this.gameObject);
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, UnityAction>();
        }
    }

    public static void StartListening(string eventName, UnityAction listener)
    {
        if (instance.eventDictionary.ContainsKey(eventName))
        {
            instance.eventDictionary[eventName] += listener;
        }
        else
        {
            instance.eventDictionary.Add(eventName, listener);
        }
    }

    public static void StopListening(string eventName, UnityAction listener)
    {
        if (instance.eventDictionary.ContainsKey(eventName))
        {
            instance.eventDictionary[eventName] -= listener;
        }
    }

    public static void TriggerEvent(string eventName)
    {
        UnityAction thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke();
        }
    }
}