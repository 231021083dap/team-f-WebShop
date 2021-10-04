using OrangularAPI.Helpers;

namespace OrangularAPI.DTO.Addresses.Responses
{
    public class AddressUserResponse
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }

    }
}
