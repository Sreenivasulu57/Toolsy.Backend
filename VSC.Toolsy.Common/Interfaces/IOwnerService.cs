using VSC.Toolsy.Common.DTOs.Requests;
using VSC.Toolsy.Common.Models.CoreEntites;

namespace VSC.Toolsy.Common.Interfaces
{
    public interface IOwnerService
    {
        Task<Owner> RegisterOwner(OwnerRequestDto ownerRequestDto);
    }
}
