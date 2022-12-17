using AbuInt.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbuInt.Domain.Entities.Quizes;

public class QuestionAnswer : Auditable
{
    public string Content { get; set; }
    public bool IsTrue { get; set; }

    public int QuestionId { get; set; }
    public Question Question { get; set; }
}
