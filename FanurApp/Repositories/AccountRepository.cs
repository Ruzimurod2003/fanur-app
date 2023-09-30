using FanurApp.Data;
using FanurApp.Enums;
using FanurApp.Exceptions;
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
        try
        {
            var user = context.Users.FirstOrDefault(i => i.Email == viewModel.Email && i.Password == viewModel.Password);
            if (user != null)
            {
                return user;
            }
            return null;
        }
        catch (Exception ex)
        {
            throw new AccountException(ex.Message);
        }
    }

    public User RegisterUser(RegisterVM viewModel)
    {
        try
        {
            var userExists = context.Users.FirstOrDefault(i => i.Email == viewModel.Email);
            if (userExists != null)
            {
                throw new Exception("Allaqchon bu foydalanuvchi tizimda bor.");
            }

            var user = new User
            {
                Email = viewModel.Email,
                Password = viewModel.Password,
                Name = viewModel.Name,
                RoleId = (int)RolesEnum.User
            };

            context.Users.Add(user);
            context.SaveChanges();

            return user;
        }
        catch (Exception ex)
        {
            throw new AccountException(ex.Message);
        }
    }

    public User ResetUser(ResetVM viewModel)
    {
        throw new NotImplementedException();
    }
}