namespace VSC.Toolsy.Common.DTOs.Requests
{
    public class SubToolCategoryRequestDto
    {
        public required string Name { get; set; }

        public required string Description { get; set; }

        public required string IconUrl { get; set; }

        public required Guid ParentCategoryId { get; set; }
    }
}
