

namespace Cleverbit.CodingTask.Data.Models
{
    public class WinnerOfMatchView
    {
        public int Id { get; set; }
        public string MatchName { get; set; }
        public int MatchId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
