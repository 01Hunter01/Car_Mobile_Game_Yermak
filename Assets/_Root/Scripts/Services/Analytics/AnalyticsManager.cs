using System.Collections.Generic;
using UnityEngine;

namespace Services.Analytics
{
    internal class AnalyticsManager: MonoBehaviour
    {
        private IAnalyticsService[] _services;

        private void Awake()
        {
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

        public void OnPurchase(string productId, decimal amount, string currency)
        {
            SendOnPurchaseEvent(productId, amount, currency);
            Debug.Log($"Transaction of the '{productId}' has been accomplished!");
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

        private void SendOnPurchaseEvent(string productId, decimal amount, string currency)
        {
            foreach (IAnalyticsService service in _services)
            {
                service.SendOnPurchaseEvent(productId, amount, currency);
            }
        }
        
    }
}