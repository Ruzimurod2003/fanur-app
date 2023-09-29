using FanurApp.Data;
using FanurApp.Models;
using FanurApp.ViewModels.Account;

namespace FanurApp.Repositories;

public interface IAccountRepository
{
    User RegisterUser(RegisterVM viewModel);
    User LoginUser(LoginVM viewModel);
    User ForgetUser(ForgetVM viewModel);
    User ResetUser(ResetVM viewModel);
}
public class AccountRepository : IAccountRepository
{
    private readonly ApplicationContext context;

    public AccountRepository(ApplicationContext _context)
    {
        context = _context;
    }

    public User ForgetUser(ForgetVM viewModel)
    {
        throw new NotImplementedException();
    }

    public User LoginUser(LoginVM viewModel)
    {
        throw new NotImplementedException();
    }

    public User RegisterUser(RegisterVM viewModel)
    {
        throw new NotImplementedException();
    }

    public User ResetUser(ResetVM viewModel)
    {
        throw new NotImplementedException();
    }
}