using System;
// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable FieldCanBeMadeReadOnly.Global

namespace OOPGameTest
{
    public abstract class InteractiveScene : IScene
    {
        public string Name { get; set; }
        public string Description;

        public IScene[] Options;

        protected abstract void InitOptions();

        public virtual byte GetFirstAvailableOptionIndex()
        {
            byte i;
            for (i = 1; i < 10 && Options[i] != null; i++) { } //postinkrementacja czy coś nwm ++i nie pomaga
            return (byte)(i%10);
        }

        public virtual bool AddOption(IScene option, byte n = 10)
        {
            if (Options.Length >= 10)
                return true;
            if (n >= 10)
                Options[GetFirstAvailableOptionIndex()] = option;
            else
                Options[n] = option;
            return false;
        }

        public virtual bool NullifyOption(byte n)
        {
            if (n >= 10)
            {
                return true;
            }
            Options[n] = null;
            return false;
        }

        public virtual bool MoveOption(byte from, byte to, bool overwrite)
        {
            if (from >= 10 || to >= 10)
                return true;
            if (!overwrite && Options[to] != null)
                return true;
            (Options[to], Options[from]) = (Options[from], Options[to]); //IDE podpowiedziało zamiast wprowadzania trzeciej zmiennej
            return false;
        }

        public bool Persistence; //utrzymuje scene przy życiu po powrocie do niej
        public bool IsPopup; //utrzymuje rodzica scene przy życiu po prowrocie do niej

        protected InteractiveScene(string name, string description = "", bool persistence = true, bool isPopup = false,
            IScene[] options = null)
        {
            this.Name = name;
            this.Description = description;
            this.Persistence = persistence;
            this.IsPopup = isPopup;
            Options = options ?? new IScene[10];
        }

        protected virtual void Act()
        {
            // ReSharper disable once RedundantJumpStatement
            return;
        }
        
        protected virtual void Exit()
        {
            // ReSharper disable once RedundantJumpStatement
            return;
        }

        public virtual bool Enter()
        {
            InitOptions();
            Act();
            byte n;
            do //giga mózg
            {
                Render();
                n = HandleInputs();
            } while (ChildSceneHandler(n));
            return IsPopup;
        }

        protected virtual void Render()
        {
            DisplayHeader();
            Console.Out.WriteLine("");
            ListOptions();

        }

        protected virtual void DisplayHeader()
        {
            Console.Clear();
            Console.Out.WriteLine("— — "+Name+" — —");
            if(!string.IsNullOrEmpty(Description))
                Console.Out.WriteLine(Description);
        } 
        
        protected virtual void ListOptions()
        {
            //opcje z nullem pomiędzy nie są wyświetlane, myśle że tak powinno zostać
            for (byte i = 1; i < 10 && Options[i] != null; i++)
                Console.Out.WriteLine((i) + ". " + Options[i].Name);
            if(Options[0] != null)
                Console.Out.WriteLine("0. "+Options[0].Name);
            Console.Out.WriteLine("");
            Console.Out.WriteLine("q. Exit");
        }

        protected virtual byte HandleInputs()
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                    continue;
                if (input[0] == 'q')
                    return 10;
                byte inputNumber;
                try
                {
                    inputNumber = byte.Parse(input[0].ToString());
                }
                catch (FormatException)
                {
                    Console.Out.WriteLine("Wrong type of input, try again!");
                    continue;
                }
                
                if (Options[inputNumber] == null)
                { 
                    Console.Out.WriteLine("There is no such option!"); 
                    continue;
                }
                return inputNumber;
            }
        }

        protected virtual bool ChildSceneHandler(byte n)
        {
            if (n == 10)
            {
                Exit();
                return false;
            }
            else if (Options[n].Enter())
                return true;
            else return Persistence;
        }
    }
}