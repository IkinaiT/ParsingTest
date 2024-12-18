using System.ComponentModel.DataAnnotations;

namespace ParsingTest.Models
{
    public class Factor
    {
        public int F { get; set; }
        public double V { get; set; }
        public double? P { get; set; }
        public string? Pt { get; set; }
        public double? Hi { get; set; }
        public double? Lo { get; set; }
    }
    public class FactorDB
    {
        public int FactorId { get; set; }
        public double V { get; set; }
        public double? P { get; set; }
        public string? Pt { get; set; }
        public double? Hi { get; set; }
        public double? Lo { get; set; }
    }
}
