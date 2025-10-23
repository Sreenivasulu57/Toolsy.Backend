namespace VSC.Toolsy.Common.DTOs.Responses
{
    public class SubToolCategoryResponseDto
    {
        public required Guid SubToolCategoryId { get; set; }

        public string Name { get; set; } = string.Empty;

        public required string Description { get; set; }

        public required string IconUrl { get; set; }

        public required bool IsActive { get; set; } = true;

        public required Guid? ParentCategoryId { get; set; }

        public List<ToolResponseDto>? Tools { get; set; } = new List<ToolResponseDto>();
    }
}

