namespace VSC.Toolsy.Common.DTOs.Responses
{
    public class ToolAvailabilityResponseDto
    {
        public required Guid ToolAvailabilityId { get; set; }

        public required DateTime Date { get; set; }

        public required TimeSpan? StartTime { get; set; }

        public required TimeSpan? EndTime { get; set; }

        public required bool IsAvailable { get; set; } 

        public required string? Notes { get; set; }

    }
}
