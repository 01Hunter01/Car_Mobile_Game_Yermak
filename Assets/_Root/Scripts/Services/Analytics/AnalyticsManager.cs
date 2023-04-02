using System;
using System.Collections.Generic;
using UnityEngine;

namespace Services.Analytics
{
    internal class AnalyticsManager: MonoBehaviour
    {
        // private static AnalyticsManager _instance;
        // public static AnalyticsManager Instance
        // {
        //     get
        //     {
        //         if (_instance == null)
        //         {
        //             _instance = new AnalyticsManager();
        //         }
        //         return _instance;
        //     }
        // }
        
        // public static AnalyticsManager Instance { get; private set; }
        
        private IAnalyticsService[] _services;

        private void Awake()
        {
            // if (Instance == null)
            // {
            //     Instance = this;
            //     return;
            // }
            
            _services = new IAnalyticsService[]
            {
                new UnityAnalyticsService()
            };
        }

        public void SendGameStartedEvent()
        {
            SendEvent("The game is started!");
            Debug.Log("The game is started!");
        } 

        private void SendEvent(string eventName)
        {
            foreach (IAnalyticsService service in _services)
            {
                service.SendEvent(eventName);
            }
        }
        
        private void SendEvent(string eventName, Dictionary<string, object> eventData)
        {
            foreach (IAnalyticsService service in _services)
            {
                service.SendEvent(eventName, eventData);
            }
        }
        
    }
}