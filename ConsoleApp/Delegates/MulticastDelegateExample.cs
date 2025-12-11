namespace ConsoleApp.Delegates
{
    internal class MulticastDelegateExample
    {
        delegate void MulticastDeledate(string @string);
        void Message1(string message)
        {
            Console.WriteLine("1st message: " + message);
        }
        void Message2(string message)
        {
            Console.WriteLine("2nd message: " + message);

        }
        void Message3(string message)
        {
            Console.WriteLine("3rd message: " + message);
        }

        public void Test()
        {
            MulticastDeledate? multicastDelegate = null;

            multicastDelegate += Message1; // dodanie metody do delegata
            multicastDelegate += Message2; // dodanie kolejnej metody do delegata
            multicastDelegate += Message3; // dodanie kolejnej metody do delegata
            multicastDelegate += Console.WriteLine; // dodanie metody statycznej do delegata

            multicastDelegate?.Invoke("Hello World!"); // wywołanie wszystkich metod w delegacie

            multicastDelegate -= Message2; // usunięcie metody z delegata
            multicastDelegate?.Invoke("Hello World after removing 2nd message!"); // wywołanie pozostałych metod w delegacie

            // = - delegat od teraz wskazuje tylko na tę jedną konkretną metodę
            multicastDelegate = Message2;
            multicastDelegate.Invoke("Bye!");
        }
    }
}
