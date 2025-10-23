namespace VSC.Toolsy.Common.DTOs.Requests
{
    public class UpdateSubToolCategoryRequestDto
    {
        public required string Name { get; set; } = string.Empty;

        public required string Description { get; set; }

        public required string IconUrl { get; set; }

        public required Guid ParentCategoryId { get; set; }

    }
}
