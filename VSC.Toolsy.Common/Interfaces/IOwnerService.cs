using VSC.Toolsy.Common.DTOs.Requests;
using VSC.Toolsy.Common.DTOs.Responses;
using VSC.Toolsy.Common.Models.CoreEntites;

namespace VSC.Toolsy.Common.Interfaces
{
    public interface IOwnerService
    {
        Task<OwnerResponseDto> GetOwnerByEmailAsync(string email);
        Task<OwnerResponseDto> GetOwnerByOwnerId(Guid ownerId);
        Task<Owner> GetOwnerWithProfileByOwerId(Guid ownerId);
        Task<Owner> RegisterOwner(OwnerRequestDto ownerRequestDto);

    }
}
