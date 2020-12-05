namespace Core.Entities
{
    public sealed class SystemUser : BaseEntity
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        public bool Active { get; set; } = false;
    }
}
