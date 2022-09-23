namespace OOPGameTest
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            
            MainMenuInteractiveScene mainMenuInteractiveScene = new MainMenuInteractiveScene();
            while (true)
            {
                mainMenuInteractiveScene.Enter();
            }
        }
    }
}