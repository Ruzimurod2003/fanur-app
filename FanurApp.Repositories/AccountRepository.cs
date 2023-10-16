using AutoMapper;
using FanurApp.Data;
using FanurApp.Commons.Enums;
using FanurApp.Models;
using FanurApp.ViewModels.Account;
using Microsoft.EntityFrameworkCore;

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
        var users = context.Users
            .Include(i => i.Role)
            .Where(i => i.Email == viewModel.Email)
            .Where(i => i.Password == viewModel.Password);

        if (users.Any())
        {
            return users.FirstOrDefault();
        }
        return null;
    }

    public User RegisterUser(RegisterVM viewModel)
    {
        var userExists = context.Users
            .Where(i => i.Email == viewModel.Email);

        if (userExists.Any())
        {
            return null;
        }

        var config = new MapperConfiguration(cfg => cfg.CreateMap<RegisterVM, User>());
        var mapper = new Mapper(config);
        var user = mapper.Map<User>(viewModel);

        user.RoleId = (int)RolesEnum.User;

        context.Users.Add(user);
        context.SaveChanges();

        return user;
    }

    public User ResetUser(ResetVM viewModel)
    {
        throw new NotImplementedException();
    }
}