namespace OOPGameTest
{
    public class SaveSelectorInteractiveScene : InteractiveScene
    {
        public SaveSelectorInteractiveScene(string name, bool write) : base(name, "Select the save slot: ...todo") { }
        
        protected override void InitOptions()
        {
            //tymczasowe!!! TODO: saves
            Options[0] = new TownPersistentInteractiveScene();
        }
    }
}