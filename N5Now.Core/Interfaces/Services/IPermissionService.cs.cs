using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using N5Now.Core.DTOs;

namespace N5Now.Core.Interfaces.Services
{
    public interface IPermissionService
    {
        Task<PermissionResponseDto> RequestPermissionAsync(PermissionRequestDto dto);
        Task<bool> ModifyPermissionAsync(int id, PermissionRequestDto dto);
        Task<IEnumerable<PermissionResponseDto>> GetPermissionsAsync();
    }
}
