using System;
using System.Collections.Generic;

namespace Cleverbit.CodingTask.Data.Models
{
    public class Match
    {
        public int Id { get; set; }
        public string MatchName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpireDate { get; set; }

        public virtual ICollection<MatchResult> MatchResult { get; set; }
    }
}
