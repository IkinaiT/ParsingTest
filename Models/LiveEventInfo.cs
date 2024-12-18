namespace ParsingTest.Models
{
    public class LiveEventInfo
    {
        public long EventId { get; set; }
        public string ScoreFunction { get; set; } = string.Empty;
        public string Timer { get; set; } = string.Empty;
        public long TimerSecond { get; set; }
        public List<List<Score>> Scores { get; set; } = [];
        public List<Subscore> Subscores { get; set; } = [];
    }

    public class Score
    {
        public string C1 { get; set; } = string.Empty;
        public string C2 { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
    }

    public class Subscore
    {
        public string C1 { get; set; } = string.Empty;
        public string C2 { get; set; } = string.Empty;
        public string KindId { get; set; } = string.Empty;
        public string KindName { get; set; } = string.Empty;
    }
}
