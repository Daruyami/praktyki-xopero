using System;

namespace OOPGameTest
{
    public abstract class InteractiveScene : IScene
    {
        public string Name { get; set; }
        protected string Description;

        protected IScene[] Options = new IScene[10];

        protected abstract void InitOptions();

        protected virtual void ListOptions()
        {
            for (int i = 0; i < Options.Length && Options[i] != null; i++)
            {
                    
                Console.Out.WriteLine((i + 1) + ". " + Options[i].Name);
                    
            }
            Console.Out.WriteLine("");
            Console.Out.WriteLine("q. Exit");
        }

        protected bool  Persistence = false;

        protected InteractiveScene(string name, string description = "")
        {
            this.Name = name;
            this.Description = description;
        }

        protected virtual void Exit()
        {
            // ReSharper disable once RedundantJumpStatement
            return;
        }

        public virtual bool Enter()
        {
            InitOptions();
            do //giga mózg
            {
                Render();
            } while (HandleInputs());
            return false;
        }

        protected virtual void Render()
        {
            Console.Clear();
            Console.Out.WriteLine("— — "+Name+" — —");
            Console.Out.WriteLine(Description);
            ListOptions();

        }

        protected virtual bool HandleInputs()
        {
            byte inputNumber = 1;
            bool waitingForInput = true;
            while (waitingForInput)
            {
                string input = Console.ReadLine();
                if (input != null && input[0] == 'q')
                {
                    Exit();
                    return false;
                }

                try
                {
                    inputNumber = byte.Parse(input?[0].ToString() ?? "1");
                }
                catch (FormatException)
                {
                    Console.Out.WriteLine("Wrong type of input, try again!");
                    continue;
                }

                inputNumber -= 1;
                if (Options[inputNumber] == null)
                { 
                    Console.Out.WriteLine("There is no such option!"); 
                    continue;
                }

                waitingForInput = false;
            }

            if (Options[inputNumber].Enter())
            {
                return true;
            }
            else return Persistence;
        }
    }
}