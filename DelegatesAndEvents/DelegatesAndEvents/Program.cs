using System;
using System.Collections.Generic;

namespace DelegatesAndEvents
{
    public delegate int Sum3Numbers(int n1, int n2, int n3);

    public delegate T Sum3Values<T>(T n1, T n2, T n3);

    class Program
    {
        private static Func<int> Increment()
        {
            int start = 0;
            return () =>
            {
                start++;
                Console.WriteLine($"Start is: {start}");
                return start;
            };
        }

        private static Func<int> IncrementOther()
        {
            return () =>
            {
                int start = 0;
                start++;
                Console.WriteLine($"Start is: {start}");
                return start;
            };
        }

        static void Main(string[] args)
        {
            List<Action> listOfActions = new List<Action>();
            for (int i = 0; i < 5; i++)
            {
                int temp = i;
                listOfActions.Add(() => Console.WriteLine($"i={temp}"));
            }

            foreach (Action act in listOfActions)
            {
                act();
            }

            Console.WriteLine("-----------------------------------");

            Func<int> increment = Increment();
            increment();
            increment();
            increment();

            Console.WriteLine("-----------------------------------");

            int a = 10;
            Action action = () =>
            {
                Console.WriteLine($"Variable capture: {a}");
                a = 30;
            };

            a = 20;
            action();

            Console.WriteLine(a);
        }

        /*
        private static int Sum(int a, int b, int c)
        {
            return a + b + c;
        }
        */

        private static void MessageInvoker(MessageBroadcast sender, string message)
        {
            sender?.Invoke(message);

            // or manually:
            // if (sender is not null)
            // {
            //     sender(message);
            // }
        }

        private static void ReceiveMessage(string message)
        {
            Console.WriteLine($"Program.ReceiveMessage :: {message}");
        }

        private static void OtherReceiveMessage(string message)
        {
            Console.WriteLine($"OtherReceiveMessage :: {message}");
        }

        private static void Example_MulticastDelegates()
        {
            MessageReceiver receiver = new MessageReceiver();
            MessageBroadcast sender = null;
            sender += receiver.Receive;
            sender += Program.ReceiveMessage;

            // sender("Hello delegates!");
            MessageInvoker(sender, "Hello delegates!");

            sender -= receiver.Receive;
            sender -= Program.ReceiveMessage;
            sender -= Program.OtherReceiveMessage;

            MessageInvoker(sender, "Hello again delegates!");
        }

        private static void Example_Events()
        {
            MessagePublisher publisher = new MessagePublisher();

            MessageReceiver receiver = new MessageReceiver();
            publisher.OnMessageReceived += Program.ReceiveMessage;
            publisher.OnMessageReceived += receiver.Receive;

            publisher.Publish("Hello events!");

            publisher.OnMessageReceived -= receiver.Receive;
            publisher.Publish("Hello again events!");
        }

        private static void Example_GenericDelegates()
        {
            Calculator c = new Calculator();
            Sum3Values<int> genericFunc = c.Sum;
            int resultGeneric = genericFunc(1, 2, 3);
            Console.WriteLine(resultGeneric);

            Sum3Numbers func = delegate (int a, int b, int c)
            {
                return a + b + c;
            };

            // Calculator c = new Calculator();
            // Sum3Numbers func = c.Sum;

            int result = func(1, 2, 3);
            Console.WriteLine(result);
        }
    }
}
