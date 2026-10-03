using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Service.Services
{
    public class UserService : IUserService
    {
        private IRepository<UserEntity> _repository;
        private readonly IMapper _mapper;

        public UserService(IRepository<UserEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        private UserEntity GetEntity(UserDto user)
        {
            var model = _mapper.Map<UserModel>(user);
            var entity = _mapper.Map<UserEntity>(model);
            return entity;
        }

        public async Task<bool> Delete(Guid id)
            => await _repository.DeleteAsync(id);

        public async Task<UserDto> Get(Guid id)
        {
            var user = await _repository.SelectAsync(id);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            var users = await _repository.SelectAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDtoCreateResult> Post(UserDto user)
        {
            var entity = GetEntity(user);
            var result = await _repository.InsertAsync(entity);
            return _mapper.Map<UserDtoCreateResult>(result);
        }        

        public async Task<UserDtoUpdateResult> Put(UserDto user)
        {
            var entity = GetEntity(user);
            var result = await _repository.UpdateAsync(entity);
            return _mapper.Map<UserDtoUpdateResult>(result);
        }
    }
}
