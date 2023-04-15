using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Shared.Domain.Entities
{
    public class BaseEntity
    {
        public DateTime created_date { get; set; } = DateTime.UtcNow;
    }
}
