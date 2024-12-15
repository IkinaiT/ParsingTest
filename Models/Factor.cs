using System.ComponentModel.DataAnnotations;

namespace ParsingTest.Models
{
    public class Factor
    {
        public int F { get; set; }
        public double V { get; set; }
    }
    public class FactorDB
    {
        public int FactorId { get; set; }
        public double V { get; set; }
    }
}
