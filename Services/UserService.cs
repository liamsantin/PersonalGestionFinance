using ApiPersonalGestionFinance.Entities;
using ApiPersonalGestionFinance.Models.Responses;
using ApiPersonalGestionFinance.Repository;

namespace ApiPersonalGestionFinance.Services;

public class UserService
{
    private readonly UserRepository _userRepository;

    public UserService(UserRepository userRepo)
    {
        _userRepository = userRepo;
    }

    
    public async Task<List<User>> GetAllUsersService()
    {
        return await _userRepository.GetAllUsersRepo();
    }
}
