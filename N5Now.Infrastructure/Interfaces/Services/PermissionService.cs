using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using N5Now.Core.DTOs;
using N5Now.Core.Entities;
using N5Now.Core.Interfaces;
using N5Now.Core.Interfaces.Indexer;
using N5Now.Core.Interfaces.Services;
using Nest;

namespace N5Now.Infrastructure.Interfaces.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPermissionsIndexer _permIndexer;
        private readonly ILogger<PermissionService> _logger;

        public PermissionService(IUnitOfWork unitOfWork, IPermissionsIndexer permIndexer,
            ILogger<PermissionService> logger)
        {
            _unitOfWork = unitOfWork;
            _permIndexer = permIndexer;
            _logger = logger;
        }

        public async Task<IEnumerable<PermissionResponseDto>> GetPermissionsAsync()
        {
            try {
                var permissionList = await _unitOfWork.PermissionRepository.GetAllPermissionsWhitTypesAsync();

                var ret = permissionList.Select(p => new PermissionResponseDto
                {
                    Id = p.Id,
                    EmployeeForeName = p.EmployeeForeName,
                    EmployeeSurName = p.EmployeeSurName,
                    PermissionsDate = p.PermissionsDate,
                    PermTypeDescription = p.PermissionType.Description
                });
                return ret;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obteniendo los permisos");
                throw;
            }
            //throw new NotImplementedException ();
        }

        public async Task<bool> ModifyPermissionAsync(int id, PermissionRequestDto dto)
        {
            var permi = await _unitOfWork.PermissionRepository.GetByIdAsync(id);
            if (permi == null) {
                return false;
            }
            permi.EmployeeForeName = dto.EmployeeForeName;
            permi.EmployeeSurName = dto.EmployeeSurName;
            permi.PermissionsDate = dto.PermissionsDate;
            //Ojo, ahora este permi puede apuntar a otro Type
            permi.PermissionTypeId = dto.PermissionTypeId;

            _unitOfWork.PermissionRepository.UpdateAsync(permi);
            await _unitOfWork.SaveChangesAsync();

#region ELASTIC
            PermissionType permiType = await _unitOfWork.PermissionTypeRepository.GetByIdAsync(dto.PermissionTypeId);
            await _permIndexer.IndexAsync(new PermissionsDocument
            {
                Id = permi.Id,
                EmployeeForeName = permi.EmployeeForeName,
                EmployeeSurName = permi.EmployeeSurName,
                PermissionsDate = permi.PermissionsDate,
                PermissionTypeId = permi.PermissionTypeId,
                PermissionTypeDescription = permiType?.Description ?? "Unknown"
            });
#endregion

            return true;
            //throw new NotImplementedException();
        }

        public async Task<PermissionResponseDto> RequestPermissionAsync(PermissionRequestDto dto)
        {
            var permi = new Permissions
            {
                EmployeeForeName = dto.EmployeeForeName,
                EmployeeSurName = dto.EmployeeSurName,
                PermissionsDate = dto.PermissionsDate,
                PermissionTypeId = dto.PermissionTypeId
            };

            await _unitOfWork.PermissionRepository.AddAsync(permi);
            await _unitOfWork.SaveChangesAsync();

            //Obtenemos la descripcion desde PermissionTypes
            PermissionType permiType = await _unitOfWork.PermissionTypeRepository.GetByIdAsync(dto.PermissionTypeId);

            PermissionResponseDto result = new PermissionResponseDto()
            {
                EmployeeForeName = permi.EmployeeForeName,
                EmployeeSurName = permi.EmployeeSurName,
                PermissionsDate = permi.PermissionsDate,
                PermTypeDescription = permiType.Description,
            };

#region ELASTIC
            await _permIndexer.IndexAsync(new PermissionsDocument
            {
                Id = permi.Id,
                EmployeeForeName = permi.EmployeeForeName,
                EmployeeSurName = permi.EmployeeSurName,
                PermissionsDate = permi.PermissionsDate,
                PermissionTypeId = permi.PermissionTypeId,
                PermissionTypeDescription = permiType?.Description ?? "Unknown"
            });
#endregion
            return result;
            //throw new NotImplementedException();
        }
    }
}
