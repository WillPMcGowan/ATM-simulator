using System;

namespace stateExample
{
    class TestAtmMachine
    {
        static private ITeller control;
        static private IMessage message;
        static void Main(string[] args)
        {

            control = new AtmMachineControl();
            message = new Message();

            message.Connect(control);
            control.Connect(message);
            control.Init();

            MenuOption userOption;
            do
            {
                userOption = ReadUserOption();
                switch (userOption)
                {
                    case MenuOption.InsertCard:
                        control.InsertCard();
                        break;
                    case MenuOption.EjectCard:
                        control.EjectCard();
                        break;
                    case MenuOption.InsertPin:
                        control.InsertPin();
                        break;
                    case MenuOption.RequestCash:
                        control.RequestCash();
                        break;
                    case MenuOption.Quit:
                        break;
                }
            } while (userOption != MenuOption.Quit);
        }


        private class Message : IMessage
        {
            private ITeller control;
            public void Connect(ITeller control)
            {
                this.control = control;
            }

            public void Init()
            {
                SetMessage("Insert Card");
            }

            public void SetMessage(string s)
            {
                Console.Clear();
                Console.SetCursorPosition(1, 1);
                Console.WriteLine(s);
                
            }
        }

        enum MenuOption
        {
            InsertCard = 1,
            InsertPin,
            EjectCard,
            RequestCash,
            Quit
        }
        static MenuOption ReadUserOption()
        {
            int? userOption = null;
            do
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(0, 3);
                Console.WriteLine("\n1. Insert Card \n2. Enter Pin \n3. Eject Card \n4. Request Cash \n5. Quit");
                try
                {
                    userOption = Convert.ToInt32(Console.ReadLine());
                    if (userOption < 0 || userOption > 7)
                    {
                        userOption = null;
                        Console.WriteLine("Error: Enter number from 1 to 5");
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Error: {ex.Message} ");
                }
            } while (userOption == null);

            return (MenuOption)userOption;
        }
    }
}