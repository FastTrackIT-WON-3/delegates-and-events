namespace DelegatesAndEvents
{
    public class MessagePublisher
    {
        private event MessageBroadcast onMessageReceived;

        public event MessageBroadcast OnMessageReceived
        {
            add { onMessageReceived += value; }
            remove { onMessageReceived -= value; }
        }

        public void Publish(string message)
        {
            onMessageReceived?.Invoke(message);
        }
    }
}
