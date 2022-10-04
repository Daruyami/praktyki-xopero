namespace GitWildcardIssues
{
    public class Exit : IScene
    {
        public string Description { get; }

        public Exit(string description = "exits program")
        {
            Description = description;
        }
        public void Enter()
        {
            //
        }
    }
}