namespace Linx.Portal.Authentication
{
    public class AuthenticatedUser
    {
        public string Username { get; set; }   // UPN, ex.: joao.silva@empresa.com
        public string Name { get; set; }
        public string TenantId { get; set; }
        public string ObjectId { get; set; }
    }
}
