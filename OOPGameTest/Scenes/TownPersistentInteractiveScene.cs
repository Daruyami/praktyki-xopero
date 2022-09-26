namespace OOPGameTest
{
    public class TownPersistentInteractiveScene : PersistentInteractiveScene
    {
        public TownPersistentInteractiveScene(string name = "A Town") : base(name) { }

        protected override void InitOptions()
        {
            //tymczasowe!!! TODO: asset loader
            Options[0] = new NPCPersistentInteractiveScene();
            Options[1] = new NPCPersistentInteractiveScene("Andrzej Duda");
            Options[2] = new NPCPersistentInteractiveScene("Radosław Korotkiewicz");
        }
    }
}