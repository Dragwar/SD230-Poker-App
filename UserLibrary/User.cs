namespace UserLibrary
{
    public class User : IUser
    {
        public string Name { get; }

        public User(string name)
        {
            Name = name;
        }
    }
}
