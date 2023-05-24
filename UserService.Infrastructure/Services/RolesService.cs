using AutoMapper;
using i3rothers.Domain.Extensions;
using i3rothers.Domain.Models;
using System.Linq.Expressions;
using UserService.Domain.Models;
using UserService.Domain.Models.Role;
using UserService.Domain.Models.User;
using UserService.Domain.Services;
using UserService.Infrastructure.Entities;
using UserService.Infrastructure.Repositories.Roles;

namespace UserService.Infrastructure.Services
{
    public class RolesService : IRolesService
    {
        private readonly IRolesRepository _rolesRepository;
        private readonly IMapper _mapper;

        public RolesService(IRolesRepository rolesRepository, IMapper mapper)
        {
            _rolesRepository = rolesRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResult<RoleForDetails>> CreateRoleAsync(RoleForCreating role)
        {
            var errorMessages = role.Validate();

            if (errorMessages.Count > 0)
            {
                return new ServiceFailedResult<RoleForDetails>(errorMessages);
            }

            var entity = _mapper.Map<Role>(role);

            await _rolesRepository.CreateAsync(entity);

            return await GetRoleAsync(new GetRoleParams { RoleId = entity.RoleId });
        }

        public async Task<ServiceResult> DeleteRoleAsync(DeleteRoleParams @params)
        {
            var errorMessages = @params.Validate();

            if (errorMessages.Count > 0)
            {
                return new ServiceFailedResult(errorMessages);
            }

            var role = (await _rolesRepository.GetAsync(x => x.RoleId == @params.RoleId)).SingleOrDefault();
            if (role == null)
            {
                return new ServiceNotFoundResult<Role>(@params.RoleId);
            }

            await EditRoleAsync(_mapper.Map<RoleForEditing>(role));
            return new ServiceSuccessfulResult();
        }

        public async Task<ServiceResult<RoleForDetails>> EditRoleAsync(RoleForEditing role)
        {
            var errorMessages = role.Validate();

            if (errorMessages.Count > 0)
            {
                return new ServiceFailedResult<RoleForDetails>(errorMessages);
            }

            var entity = _mapper.Map<Role>(role);

            var result = (await _rolesRepository.GetAsync(x => x.RoleId == role.RoleId)).SingleOrDefault();
            if (result == null)
            {
                return new ServiceNotFoundResult<RoleForDetails>(role.RoleId);
            }

            await _rolesRepository.UpdateAsync(entity);
            return await GetRoleAsync(new GetRoleParams { RoleId = entity.RoleId });
        }

        public async Task<ServiceResult<RoleForDetails>> GetRoleAsync(GetRoleParams @params)
        {
            var errorMessages = @params.Validate();

            if (errorMessages.Count > 0)
            {
                return new ServiceFailedResult<RoleForDetails>(errorMessages);
            }

            var role = (await _rolesRepository.GetAsync(x => x.RoleId == @params.RoleId)).SingleOrDefault();

            if (role == null)
                return new ServiceNotFoundResult<RoleForDetails>(@params.RoleId);

            return await Task.FromResult(new ServiceSuccessfulResult<RoleForDetails>(_mapper.Map<RoleForDetails>(role)));
        }

        public async Task<ServiceResult<GetPaginationResult<RoleForList>>> GetRolesAsync(GetRolesParams getRolesParams)
        {
            var errorMessages = getRolesParams.Validate();

            if (errorMessages.Count > 0)
            {
                return new ServiceFailedResult<GetPaginationResult<RoleForList>>(errorMessages);
            }

            var quetsions = (await _rolesRepository.GetAsync(new i3rothers.Infrastructure.Repository.GetParams<Role>
            {
                Skip = getRolesParams.Skip,
                Take = getRolesParams.Take
            })).Select(section => _mapper.Map<RoleForList>(section));

            var count = await _rolesRepository.CountAsync(new i3rothers.Infrastructure.Repository.GetParams<Role>());

            var result = new GetPaginationResult<RoleForList>
            {
                Data = quetsions.ToList(),
                Page = getRolesParams.Page,
                Take = getRolesParams.Take,
                TotalRecord = count
            };

            return new ServiceSuccessfulResult<GetPaginationResult<RoleForList>>(result);
        }

        public async Task<ServiceResult<IEnumerable<SelectOption>>> GetRolesAsSelectOptionsAsync()
        {
            return new ServiceSuccessfulResult<IEnumerable<SelectOption>>((await _rolesRepository.GetAsync()).Select(x => new SelectOption
            {
                Title = x.Name,
                Value = x.RoleId.ToString()
            }));
        }
    }
}
