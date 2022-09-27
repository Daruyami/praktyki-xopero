using System;

namespace OOPGameTest
{
    public class ProgramExitScene : IScene
    {
        public string Name { get; } = "Exit";

        public bool Enter()
        {
            //dodać zapisywanie stanu gry przed wyjściem
            Console.Out.WriteLine("bye bye !");
            Environment.Exit(0);
            return false;
        }
    }
}