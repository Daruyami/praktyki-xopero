using System;

namespace OOPGameTest
{
    public abstract class InteractiveScene : IScene
    {
        public string Name { get; set; }

        // ReSharper disable once InconsistentNaming
        protected IScene[] _options;

        public virtual IScene[] Options
        {
            get => _options;
            set => throw new NotImplementedException(); //setter listy opcji
        }


        protected InteractiveScene(string name)
        {
            this.Name = name;
        }

        protected virtual void Exit()
        {
            // ReSharper disable once RedundantJumpStatement
            return;
        }

        public virtual void Enter()
        {
            Render();
            HandleInputs();
        }

        protected virtual void Render()
        {
            Console.Clear();
            Console.Out.WriteLine("— — "+Name+" — —");
            if (Options != null)
            {
                for (int i = 0; i < Options.Length; i++)
                {
                    if (Options[i] != null)
                    {
                        Console.Out.WriteLine((i + 1) + ". " + Options[i].Name);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else
            {
                Console.Out.WriteLine("You have no options!");
            }
            Console.Out.WriteLine("q. Exit Game");
            
        }

        protected virtual void HandleInputs()
        {
            int userNum = 0;
            bool waitingForInput = true;
            while (waitingForInput)
            {
                string input = Console.ReadLine();
                if (input != null && input[0] == 'q')
                {
                    Exit();
                    return;
                }

                try
                {
                    userNum = int.Parse(input?[0].ToString() ?? "0");
                }
                catch (FormatException)
                {
                    Console.Out.WriteLine("Wrong type of input, try again!");
                    continue;
                }
                
                if (Options[userNum] == null)
                {
                    Console.Out.WriteLine("There is no such option!");
                    continue;
                }

                waitingForInput = false;
            }
            Options[userNum].Enter();
        }
    }
}