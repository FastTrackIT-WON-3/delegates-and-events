using System;

namespace DelegatesAndEvents
{
    public class MessageReceiver
    {
        public void Receive(string message)
        {
            Console.WriteLine($"MessageReceiver.Receive :: {message}");
        }
    }
}
