using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VSC.Toolsy.Common.DTOs.Requests;
using VSC.Toolsy.Common.DTOs.Responses;
using VSC.Toolsy.Common.Enums;
using VSC.Toolsy.Common.Interfaces;
using VSC.Toolsy.Common.Models.CoreEntites;
using VSC.Toolsy.Common.Constants;

namespace VSC.Toolsy.Server.Controllers
{
    [Route(RouteMap.Address.Base)]
    [ApiController]
    [Authorize(policy: nameof(Policy.AUTHENTICATED_PROFILE))]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseDto<Address>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SaveAddressAsync([FromBody] AddressRegisterDto addressRegisterDto)
        {
            Address address = await _addressService.SaveAddressAsync(addressRegisterDto);

            return Ok(ApiResponseDto<Address>.SuccessResponse(address, "Address added succesfully"));
        }

        [HttpPut]
        [ProducesResponseType(typeof(ApiResponseDto<Address>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAddressAsync([FromBody] AddressRegisterDto addressRegisterDto)
        {
            Address address = await _addressService.UpdateAddress(addressRegisterDto);

            return Ok(ApiResponseDto<Address>.SuccessResponse(address, "Address Updated succesfully"));
        }
        [HttpGet]
        [Authorize(policy: nameof(Policy.ADMIN_ONLY))]
        [ProducesResponseType(typeof(ApiResponseDto<Address>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAddresses()
        {
            List<Address> addresses = await _addressService.GetAllAddresses();

            return Ok(ApiResponseDto<List<Address>>.SuccessResponse(addresses, "Address fetched succesfully"));
        }
        [HttpGet(RouteMap.Address.GetByProfileId)]
        [ProducesResponseType(typeof(ApiResponseDto<Address>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponseDto<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAddressByProfileIdAsync(Guid profileId)
        {
            Address address = await _addressService.GetByProfileId(profileId);

            return Ok(ApiResponseDto<Address>.SuccessResponse(address, "Address fetched succesfully"));
        }
    }
}
