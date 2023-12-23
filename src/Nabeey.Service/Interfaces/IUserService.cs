using Nabeey.Domain.Configurations;
using Nabeey.Domain.Enums;
using Nabeey.Service.DTOs.Users;

namespace Nabeey.Service.Interfaces;

public interface IUserService
{
	ValueTask<QuizResultDto> AddAsync(UserCreationDto dto);
	ValueTask<QuizResultDto> ModifyAsync(UserUpdateDto dto);
	ValueTask<bool> RemoveAsync(long id);
	ValueTask<QuizResultDto> RetrieveByIdAsync(long id);
	ValueTask<IEnumerable<QuizResultDto>> RetrieveAllAsync(PaginationParams @params, string search = null);
	ValueTask<IEnumerable<QuizResultDto>> RetrieveAllAsync();
	ValueTask<QuizResultDto> UpgradeRoleAsync(long id, Role role);
}