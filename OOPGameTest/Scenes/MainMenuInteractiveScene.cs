namespace OOPGameTest
{
    public class MainMenuInteractiveScene : InteractiveScene
    {
        public MainMenuInteractiveScene() : base("Main Menu") { }
        
        protected override void InitOptions()
        {
            Options[0] = new SaveSelectorInteractiveScene("New Game", true);
            Options[0] = new SaveSelectorInteractiveScene("Load Game", false);
        }

        protected override void Exit()
        {
            ProgramExitScene programExitScene = new ProgramExitScene();
            programExitScene.Enter();
        }
    }
}