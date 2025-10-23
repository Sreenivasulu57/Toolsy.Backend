namespace VSC.Toolsy.Common.DTOs.Responses
{
    public class ToolCategoryResponseDto
    {
        public required Guid ToolCategoryId { get; set; }

        public required string Name { get; set; }

        public required string Description { get; set; }

        public required string IconUrl { get; set; }

        public List<SubToolCategoryResponseDto>? SubCategories { get; set; } = new List<SubToolCategoryResponseDto>();

    }
}

