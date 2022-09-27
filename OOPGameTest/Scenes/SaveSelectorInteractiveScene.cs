namespace OOPGameTest
{
    public class SaveSelectorInteractiveScene : InteractiveScene
    {
        public SaveSelectorInteractiveScene(string name, bool write, string description = "", bool persistence = true,
            bool isPopup = false, IScene[] options = null) : base(name, description, persistence, isPopup, options) { }
        
        protected override void InitOptions() { } //parametr write: "Czy nadpisywać sloty, czy je załadowywać?"
    }
}