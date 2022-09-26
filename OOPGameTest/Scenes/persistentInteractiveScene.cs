namespace OOPGameTest
{
    public abstract class PersistentInteractiveScene : InteractiveScene
    {
        protected PersistentInteractiveScene(string name, string description = "") : base(name, description) { }
        
        protected new bool Persistence = true;
    }
}