namespace Core.Entities.Email
{
    public sealed class SystemUserActivationRequest
    {
        public string Email { get; set; }

        public string EncriptedUsername { get; set; }
    }
}
