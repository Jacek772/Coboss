namespace Coboss.Persistance
{
    public class DatabaseConfiguration
    {
        public string Server { get; set; } = default!;
        public int Port { get; set; } = default!;
        public string Database { get; set; } = default!;
        public string Schema { get; set; } = default!;
        public string UserId { get; set; } = default!;
        public string Password { get; set; } = default!;

        public string ConnectionString
            => $"Server={Server};Port={Port};Database={Database};User Id={UserId}; Password={Password};";
    }
}
