using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N5Now.Core.Entities
{
    public class PermissionType
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;

        //Prop de navegacion
        ICollection<Permissions> Permissions { get; set; } = new List<Permissions>();
    }
}
