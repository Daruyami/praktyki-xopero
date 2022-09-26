using System;

namespace OOPGameTest
{
    public abstract class DialogInteractiveScene : InteractiveScene
    {
        public string ParentsName;

        protected DialogInteractiveScene(string name, string parentsName, string description = "") : base(name, description)
        {
            this.ParentsName = parentsName;
        }

        protected override void Render()
        {
            Console.Clear();
            Console.Out.WriteLine("— — "+ParentsName+" — —");
            Console.Out.WriteLine(Description);
            ListOptions();
        }
    }
}