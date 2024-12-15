namespace ParsingTest.Models
{
    public class Event
    {
        public long Id { get; set; }
        public List<string> Applicability { get; set; } = [];
        public string Place { get; set; } = string.Empty;
        public long SportIs { get; set; }
        public long StartTime { get; set; }
        public long Team1Id { get; set; }
        public string Team1 { get; set; } = string.Empty;
        public long Team2Id { get; set; }
        public string Team2 { get; set; } = string.Empty;
    }
}
