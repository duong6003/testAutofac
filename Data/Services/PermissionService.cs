using AutoMapper;
using testAutofac.Data.Entities;
using testAutofac.Data.Requests;
using testAutofac.Repositories;

namespace testAutofac.Data.Services
{
    public interface IPermissionService
    {
        Task<Permission?> GetByIdAsync(Guid permissionId);

        Task<(Permission? Permission, string? ErrorMessage)> CreateAsync(CreatePermissionRequest request);

        Task<(Permission? Permission, string? ErrorMessage)> UpdateAsync(Permission permission, UpdatePermissionRequest request);

        Task<(Permission? Permission, string? ErrorMessage)> DeleteAsync(Permission permission);
    }

    public class PermissionService : IPermissionService
    {
        private readonly IRepositoryWrapper RepositoryWrapper;
        private readonly IMapper Mapper;

        public PermissionService(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            RepositoryWrapper = repositoryWrapper;
            Mapper = mapper;
        }

        public async Task<(Permission? Permission, string? ErrorMessage)> CreateAsync(CreatePermissionRequest request)
        {
            Permission? permission = Mapper.Map<Permission>(request);
            await RepositoryWrapper.Permissions.AddAsync(permission);
            return (permission, null);
        }

        public async Task<(Permission? Permission, string? ErrorMessage)> DeleteAsync(Permission permission)
        {
            await RepositoryWrapper.Permissions.DeleteAsync(permission);
            return (permission, null);
        }

        public async Task<Permission?> GetByIdAsync(Guid permissionId)
        {
            return await RepositoryWrapper.Permissions.GetByIdAsync(permissionId);
        }

        public async Task<(Permission? Permission, string? ErrorMessage)> UpdateAsync(Permission permission, UpdatePermissionRequest request)
        {
            Mapper.Map(request, permission);
            await RepositoryWrapper.Permissions.UpdateAsync(permission);
            return (permission, null);
        }
    }
}