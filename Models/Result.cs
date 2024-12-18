namespace ParsingTest.Models
{
    public class Result
    {
        public long PacketVersion { get; set; }
        public List<Sport> Sports { get; set; } = [];
        public List<CustomFactor> CustomFactors { get; set; } = [];
        public List<Event> Events { get; set; } = [];
        public List<LiveEventInfo> LiveEventInfos { get; set; } = [];
    }
}
