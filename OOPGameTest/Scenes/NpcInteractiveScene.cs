namespace OOPGameTest
{
    // ReSharper disable once InconsistentNaming
    public class NpcInteractiveScene : InteractiveScene
    {
        public NpcInteractiveScene(string name = "Unnamed NPC", string description = "", bool persistence = true,
            bool isPopup = false, IScene[] options = null) : base(name, description, persistence, isPopup, options) { }
        
        protected override void InitOptions() { }

    }
}