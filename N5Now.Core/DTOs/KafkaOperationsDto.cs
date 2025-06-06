using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N5Now.Core.DTOs
{
    public class KafkaOperationsDto
    {
        public Guid Id { get; set; }
        public string OperationName { get; set; } = null!;
    }
}
