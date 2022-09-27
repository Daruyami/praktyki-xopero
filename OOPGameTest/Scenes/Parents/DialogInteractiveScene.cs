using System;

namespace OOPGameTest
{
    public class DialogInteractiveScene : InteractiveScene
    {
        public string ParentsName;

        protected DialogInteractiveScene(string name, string parentsName, string description = "",
            bool persistence = false, bool isPopup = false, IScene[] options = null) : base(name, description,
            persistence, isPopup, options)
        {
            this.ParentsName = parentsName;
        }

        protected override void InitOptions() { }

        protected override void DisplayHeader()
        { 
            Console.Clear();
            Console.Out.WriteLine("— — "+ParentsName+" — —");
            if(!string.IsNullOrEmpty(Description))
                Console.Out.WriteLine(Description);
            Console.Out.WriteLine("");
        }
    }
}