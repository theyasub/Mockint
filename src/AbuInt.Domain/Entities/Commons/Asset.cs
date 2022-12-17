using AbuInt.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbuInt.Domain.Entities.Commons
{
    public class Asset : Auditable
    {
        public string Name { get; set; }
        public string Path { get; set; }
    }
}
