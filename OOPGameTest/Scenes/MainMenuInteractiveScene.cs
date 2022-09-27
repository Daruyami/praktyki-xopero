namespace OOPGameTest
{
    public class MainMenuInteractiveScene : InteractiveScene
    {
        public MainMenuInteractiveScene(string name = "Main Menu", bool isPopup = false) : base(name, "",true, isPopup) { }
        
        protected override void InitOptions()
        {
            //w sumie głupi warunek dla tych opcji, ale i tak pewnie będzie coś podobnego tutaj
            //(Main Menu vs Menu Pauzy w czasie gry żeby np zmienić opcje czy zapisać stan gry)
            if (!IsPopup) 
            {
                Options[GetFirstAvailableOptionIndex()] = new SaveSelectorInteractiveScene("New Game", true);
                Options[GetFirstAvailableOptionIndex()] = new SaveSelectorInteractiveScene("Load Game", false);
            }
            Options[GetFirstAvailableOptionIndex()]= new SceneEditorInteractiveScene();
        }

        protected override void Exit()
        {
            ProgramExitScene programExitScene = new ProgramExitScene();
            programExitScene.Enter();
        }
    }
}