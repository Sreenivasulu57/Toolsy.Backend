namespace VSC.Toolsy.Common.DTOs.Requests
{
    public class ToolSpecificationRequestDto
    {
        public required string Name { get; set; } = string.Empty;

        public required string Value { get; set; } = string.Empty;

        public required string Unit { get; set; }


        public required Guid ToolId { get; set; }
    }
}

