namespace OOPGameTest
{
    // ReSharper disable once InconsistentNaming
    public class NPCPersistentInteractiveScene : PersistentInteractiveScene
    {
        public NPCPersistentInteractiveScene(string name = "Unnamed NPC", string description = "") : base(name, description) { }

        protected override void InitOptions()
        {
            //tymczasowe!!! TODO: asset loader
            Options[0] = new QuickDialogInteractiveScene("Greet :)", this.Name, ("You greet "+this.Name));
            Options[1] = new QuickDialogInteractiveScene("Trade", this.Name, (this.Name+" says he doesnt want to trade with you"));
            Options[2] = new QuickDialogInteractiveScene("Attack(!)", this.Name, "You missed");

        }
    }
}