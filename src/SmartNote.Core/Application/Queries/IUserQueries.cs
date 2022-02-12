using SmartNote.Core.Application.Dto;

namespace SmartNote.Core.Application.Queries;

public interface IUserQueries
{
    Task<IEnumerable<UserDto>> GetListAsync();
    Task<UserDto> GetCurrentUserAsync();
}