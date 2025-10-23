namespace VSC.Toolsy.Common.DTOs.Requests
{
    public class ToolAvailabilityRequestDto
    {
        public required DateTime Date { get; set; }

        public required TimeSpan StartTime { get; set; }

        public required TimeSpan EndTime { get; set; }

        public required bool IsAvailable { get; set; } = true;

        public required string Notes { get; set; }


        public required Guid ToolId { get; set; }
    }
}

