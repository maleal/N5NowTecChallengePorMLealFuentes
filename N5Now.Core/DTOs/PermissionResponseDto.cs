using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N5Now.Core.DTOs
{
    public class PermissionResponseDto
    {
        public int Id { get; set; }
        public string EmployeeForeName { get; set; }    = string.Empty;
        public string EmployeeSurName { get; set; }     = string.Empty;
        public DateTime PermissionsDate { get; set; }
        public string PermTypeDescription { get; set; } = string.Empty;
    }
}
