namespace FormsCreator.Domain.Db
{
    public class DbConfig
    {
        public static string Path { get; } = "Database";
        public string ConnectionString { get; set; }
        public string AuthConnectionString { get; set; }
    }
}