namespace OOPGameTest
{
    public class SceneSelectorInteractiveScene : DialogInteractiveScene
    {
        protected SceneSelectorInteractiveScene(string name = "Select a scene", string parentsName = "Scene Editor",
            string description = "", bool persistence = true, bool isPopup = true, IScene[] options = null) : base(
            name, parentsName, description, persistence, isPopup, options) { }


        protected override void InitOptions()
        {
            //Nowa scena (-> exituje/pyta o typ i exituje?)
            //Podaj ścieżke sceny (-> przekierowuje do TextInputScene [nowy typ])
            //-> metoda returnująca scene (nową lub wybraną)
        }
    }
}