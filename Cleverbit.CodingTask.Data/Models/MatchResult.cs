

namespace Cleverbit.CodingTask.Data.Models
{
    public class MatchResult
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public int UserId { get; set; }
        public int GeneratedNumber { get; set; }

        public virtual User User { get; set; }
        public virtual Match Match { get; set; }
    }
}
