namespace OOPGameTest
{
    public class MainMenuInteractiveScene : InteractiveScene
    {
        public MainMenuInteractiveScene( /*IScene options*/) : base("Main Menu")
        {
            _options = new IScene[4];
            GameInteractiveScene gameInteractiveScene = new GameInteractiveScene();
            _options[0] = gameInteractiveScene;
            _options[1] = gameInteractiveScene;
            _options[2] = gameInteractiveScene;
        }

        protected override void Exit()
        {
            ProgramExitScene programExitScene = new ProgramExitScene();
            programExitScene.Enter();
        }
    }
}