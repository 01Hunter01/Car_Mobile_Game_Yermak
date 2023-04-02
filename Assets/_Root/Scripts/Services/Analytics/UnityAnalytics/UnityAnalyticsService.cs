using System.Collections.Generic;

namespace Services.Analytics
{
    internal class UnityAnalyticsService : IAnalyticsService
    {
        public void SendEvent(string eventName) => 
            UnityEngine.Analytics.Analytics.CustomEvent(eventName);

        public void SendEvent(string eventName, Dictionary<string, object> eventData) => 
            UnityEngine.Analytics.Analytics.CustomEvent(eventName, eventData);

        public void SendOnPurchaseEvent(string productId, decimal amount, string currency) => 
            UnityEngine.Analytics.Analytics.Transaction(productId, amount, currency);
    }
}