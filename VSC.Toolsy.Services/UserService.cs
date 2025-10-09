using Microsoft.Extensions.Configuration;
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
        private readonly IProfileService _profileService;
        private readonly string _defaultImage;

        public UserService(IProfileRepository profileRepository, IProfileService profileService,IConfiguration config)
        {
            _profileRepository = profileRepository;
            _profileService = profileService;
            _defaultImage = config["UserSettings:DefaultProfileImage"];
        }

        public async Task<Profile> DeleteUserByProfileId(Guid profileId)
        {
            DateTime now = DateTime.UtcNow;

            Profile userFromDb = await _profileService.GetByProfileId(profileId);

            if (userFromDb.IsDeleted)
            {
                return userFromDb;
            }

            userFromDb.IsActive = false;
            userFromDb.IsDeleted = true;
            userFromDb.DeletedAt = now;
            userFromDb.DeletedBy = UserRole.User.ToString();

            int result = await _profileRepository.UpdateAsync(userFromDb);

            if (result > 0)
            {
                return userFromDb;
            }
            throw new Exception("Internal Server Error");
        }

        public async Task<List<Profile>> GetAllUserAsync()
            => await _profileRepository.GetAllUserAsync();

        public async Task<Profile> GetProfileWithAddressByProfileId(Guid profileId)
            => await _profileService.GetProfileWithAddressByProfileId(profileId);

        public async Task<Profile> GetByProfileId(Guid profileId)
            => await _profileRepository.GetByProfileId(profileId);

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
                ProfileImageUrl = _defaultImage,
                CreatedBy = registerUserDto.Email,
                Roles = new List<UserRole> { UserRole.User}
            };

            int result = await _profileRepository.SaveAsync(user);

            if (result <= 0)
            {
                throw new Exception("Internal Server Error");
            }

            return user;
        }

        public async Task<Profile> UpdateUser(UserUpdateDTO userUpdateDTO, Guid profileId)
        {
            Profile userFromDb = await _profileService.GetByProfileId(profileId);

            userFromDb.FirstName = userUpdateDTO.FirstName;
            userFromDb.LastName = userUpdateDTO.LastName;
            userFromDb.PhoneNumber = userUpdateDTO.PhoneNumber;
            userFromDb.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userUpdateDTO.Password);
            userFromDb.ProfileImageUrl = _defaultImage;
            userFromDb.DateOfBirth = userUpdateDTO.DateOfBirth;

            userFromDb.UpdatedBy = UserRole.User.ToString();
            userFromDb.UpdatedAt = DateTime.UtcNow;

            int result = await _profileRepository.UpdateAsync(userFromDb);

            if (result <= 0)
            {
                throw new Exception("Internal Server Error");
            }
            return userFromDb;


        }

        public async Task<int> UpdateUserAsync(Profile userFromDb)
            => await _profileRepository.UpdateAsync(userFromDb);
    }
}