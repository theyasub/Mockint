using AbuInt.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbuInt.Domain.Entities.Quizes;

public class Quize : Auditable
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public virtual ICollection<Question> Questions { get; set; } 
}
