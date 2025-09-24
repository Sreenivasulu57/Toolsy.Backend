using VSC.Toolsy.Common.DTOs.Requests;
using VSC.Toolsy.Common.Enums;
using VSC.Toolsy.Common.Interfaces;
using VSC.Toolsy.Common.Models.CoreEntites;
using VSC.Toolsy.Repositories.Interfaces;


namespace VSC.Toolsy.Services
{
    public class UserService : IUserService
    {
        private readonly IProfileRepository _profileRepository;

        public UserService(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task<Profile> DeleteUserByEmailAsync(string email)
        {
            DateTime now = DateTime.UtcNow;

            Profile userFromDb = await GetByEmailAsync(email);

            if (userFromDb.IsDeleted)
            {
                return userFromDb;
            }

            userFromDb.IsActive = false;
            userFromDb.IsDeleted = true;
            userFromDb.DeletedAt = now;
            userFromDb.DeletedBy = Role.User.ToString();

            _profileRepository.Update(userFromDb);
            int result = await _profileRepository.SaveChangesAsync();

            if (result > 0)
            {
                return userFromDb;
            }
            throw new Exception("Internal Server Error");
        }

        public async Task<List<Profile>> GetAllUserAsync()
            => await _profileRepository.GetAllUserAsync();

        public async Task<Profile> GetByEmailAsync(string email)
            => await _profileRepository.GetByEmailAsync(email);

        public async Task<Profile> SaveAsync(RegisterUserDto registerUserDto)
        {
            Profile user = new Profile
            {
                FirstName = registerUserDto.FirstName,
                LastName = registerUserDto.LastName,
                Email = registerUserDto.Email,
                PhoneNumber = registerUserDto.PhoneNumber,
                DateOfBirth = registerUserDto.DateOfBirth,
                Gender = registerUserDto.Gender,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerUserDto.Password),
                ProfileImageUrl = registerUserDto.ProfileImageUrl,
                CreatedBy = registerUserDto.Email,
                Role = Role.User
            };

            await _profileRepository.SaveAsync(user);

            await _profileRepository.SaveChangesAsync();

            return user;
        }

        public async Task<Profile> UpdateUser(UserUpdateDTO userUpdateDTO, string email)
        {
            Profile userFromDb = await GetByEmailAsync(email);

            userFromDb.FirstName = userUpdateDTO.FirstName;
            userFromDb.LastName = userUpdateDTO.LastName;
            userFromDb.PhoneNumber = userUpdateDTO.PhoneNumber;
            userFromDb.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userUpdateDTO.Password);
            userFromDb.ProfileImageUrl = userUpdateDTO.ProfileImageUrl;
            userFromDb.DateOfBirth = userUpdateDTO.DateOfBirth;

            userFromDb.UpdatedBy = Role.User.ToString();
            userFromDb.UpdatedAt = DateTime.UtcNow;

            _profileRepository.Update(userFromDb);
            int result = await _profileRepository.SaveChangesAsync();

            if (result > 0)
            {
                return userFromDb;
            }

            throw new Exception("Internal Server Error");

        }

        public async Task<int> UpdateUserAsync(Profile userFromDb)
        {
            _profileRepository.Update(userFromDb);
            return await _profileRepository.SaveChangesAsync();
        }
    }
}