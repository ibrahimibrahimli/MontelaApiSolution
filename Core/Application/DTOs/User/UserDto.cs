namespace Application.DTOs.User
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Fullname { get; set; }
        public bool TwoFactorEnabled { get; set; }
    }
}
