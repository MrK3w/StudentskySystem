namespace SchoolSystem.ViewLayer.Helpers
{
    public static class User
    {
        public static string Name;
        public static string Surname;
        public static Type Typ;
        public static int Id;
        private static bool _exists;
        public static string Login;

        public static void RememberUser(string name, string surname, Type type, int id, string login)
        {
            Name = name;
            Surname = surname;
            Typ = type; 
            Id = id;
            Login = login;
            _exists = true;
        }

        public static bool Exists() => _exists;

        public enum Type
        {
            Teacher,
            Student,
            Admin
        }

        public static void Forget()
        {
            Name = default;
            Surname = default;
            Typ = default;
            Id = default;
            Login = default;
            _exists = false;
        }
    }
}
