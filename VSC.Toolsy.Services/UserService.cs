using VSC.Toolsy.Common.DTOs.Requests;
using VSC.Toolsy.Common.Interfaces;
using VSC.Toolsy.Common.Models.CoreEntites;
using VSC.Toolsy.Repositories.Interfaces;
using BCrypt.Net;
namespace VSC.Toolsy.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;


        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> SaveAsync(RegisterUserDto registerUserDto)
        {
            User user = new
       User
            {

                FirstName = registerUserDto.FirstName,
                LastName = registerUserDto.LastName,
                Email = registerUserDto.Email,
                PhoneNumber = registerUserDto.PhoneNumber,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerUserDto.Password),
                ProfileImageUrl = registerUserDto.ProfileImageUrl,
                CreatedBy = registerUserDto.Email,
            };

            await _userRepository.SaveAsync(user);

            await _userRepository.SaveChangesAsync();

            return user;
        }
    }
}
