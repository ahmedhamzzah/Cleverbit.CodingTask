using System;
using System.Collections.Generic;
using System.Text;

namespace Cleverbit.CodingTask.Core.Dto
{
    public class GetAvailableMatchDto
    {
        public int Id { get; set; }
        public string MatchName { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
