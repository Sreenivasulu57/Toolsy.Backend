namespace VSC.Toolsy.Common.DTOs.Responses
{
    public class ToolSpecificationResponseDto
    {
        public required Guid ToolSpecificationId { get; set; }

        public required string Name { get; set; } = string.Empty;

        public required string Value { get; set; } = string.Empty;

        public required string Unit { get; set; }
    }
}
