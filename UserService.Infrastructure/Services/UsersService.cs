using AutoMapper;
using i3rothers.Domain.Extensions;
using i3rothers.Domain.Models;
using i3rothers.Infrastructure.Repository;
using System.Linq.Expressions;
using UserService.Domain.Models.User;
using UserService.Domain.Services;
using UserService.Infrastructure.Entities;
using UserService.Infrastructure.Repositories.Roles;
using UserService.Infrastructure.Repositories.RoleUsers;
using UserService.Infrastructure.Repositories.Users;

namespace UserService.Infrastructure.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IRoleUsersRepository _roleUsersRepository;
        private readonly IMapper _mapper;

        public UsersService(
            IUsersRepository usersRepository,
            IRoleUsersRepository roleUsersRepository,
            IMapper mapper)
        {
            _usersRepository = usersRepository;
            _roleUsersRepository = roleUsersRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<UserForDetails>> CreateUserAsync(UserForCreating user)
        {
            var errorMessages = user.Validate();

            if (errorMessages.Count > 0)
            {
                return new ServiceFailedResult<UserForDetails>(errorMessages);
            }

            var entity = _mapper.Map<User>(user);

            await _usersRepository.CreateAsync(entity);

            var roleUsers = new List<UserRole>();
            user.RoleIds.ForEach(roleId =>
            {
                roleUsers.Add(new UserRole
                {
                    RoleId = roleId,
                    UserId = entity.UserId
                });
            });

            await _roleUsersRepository.CreateAsync(roleUsers);

            return await GetUserAsync(new GetUserParams { UserId = entity.UserId });
        }

        public async Task<ServiceResult> DeleteUserAsync(DeleteUserParams @params)
        {
            var errorMessages = @params.Validate();

            if (errorMessages.Count > 0)
            {
                return new ServiceFailedResult(errorMessages);
            }

            var user = (await _usersRepository.GetAsync(x => x.UserId == @params.UserId)).SingleOrDefault();

            if (user == null)
                return new ServiceNotFoundResult<User>(@params.UserId);

            await _usersRepository.DeleteAsync(x => x.UserId == @params.UserId);
            await _roleUsersRepository.DeleteAsync(x => x.UserId == @params.UserId);

            return new ServiceSuccessfulResult();
        }

        public async Task<ServiceResult<UserForDetails>> EditUserAsync(UserForEditing user)
        {
            var errorMessages = user.Validate();

            if (errorMessages.Count > 0)
            {
                return new ServiceFailedResult<UserForDetails>(errorMessages);
            }

            var count = await _usersRepository.CountAsync(x => x.UserId == user.UserId);
            if (count == 0)
            {
                return new ServiceNotFoundResult<UserForDetails>(user.UserId);
            }

            await _usersRepository.UpdateAsync(_mapper.Map<User>(user));
            await _roleUsersRepository.RemoveRoleUserByUserIdAsync(user.UserId);
            var roleUsers = new List<UserRole>();
            user.RoleIds.ForEach(roleId =>
            {
                roleUsers.Add(new UserRole
                {
                    RoleId = roleId,
                    UserId = user.UserId
                });
            });
            await _roleUsersRepository.CreateAsync(roleUsers);

            return await GetUserAsync(new GetUserParams { UserId = user.UserId });
        }

        public async Task<ServiceResult<UserForDetails>> GetUserAsync(GetUserParams @params)
        {
            var errorMessages = @params.Validate();

            if (errorMessages.Count > 0)
            {
                return new ServiceFailedResult<UserForDetails>(errorMessages);
            }

            var user = (await _usersRepository.GetAsync(x => x.UserId == @params.UserId)).SingleOrDefault();

            if (user == null)
                return new ServiceNotFoundResult<UserForDetails>(@params.UserId);

            var userForDetails = _mapper.Map<UserForDetails>(user);

            var roleUsers = await _roleUsersRepository.GetAsync(x => x.UserId == user.UserId);
            var roleIds = roleUsers.Select(x => x.RoleId).ToList();

            userForDetails.RoleIds = roleIds;

            return new ServiceSuccessfulResult<UserForDetails>(userForDetails);
        }

        public async Task<ServiceResult<GetPaginationResult<UserForList>>> GetUsersAsync(GetUsersParams getUserParams)
        {
            var errorMessages = getUserParams.Validate();

            if (errorMessages.Count > 0)
            {
                return new ServiceFailedResult<GetPaginationResult<UserForList>>(errorMessages);
            }

            var quetsions = (await _usersRepository.GetAsync(new GetParams<User>
            {
                Skip = getUserParams.Skip,
                Take = getUserParams.Take
            })).Select(section => _mapper.Map<UserForList>(section));

            var count = await _usersRepository.CountAsync(new GetParams<User>
            {
                Skip = getUserParams.Skip,
                Take = getUserParams.Take
            });
            var result = new GetPaginationResult<UserForList>
            {
                Data = quetsions.ToList(),
                Page = getUserParams.Page,
                Take = getUserParams.Take,
                TotalRecord = count
            };

            return new ServiceSuccessfulResult<GetPaginationResult<UserForList>>(result);
        }
    }
}
