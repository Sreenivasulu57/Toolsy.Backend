namespace VSC.Toolsy.Common.DTOs.Requests
{
    public class UpdateParentToolCategoryRequestDto
    {
        public required string Name { get; set; }

        public required string Description { get; set; }

        public required string IconUrl { get; set; }
    }
}
