using FanurApp.Data;
using FanurApp.ViewModels.Main;

namespace FanurApp.Repositories;
public interface IMainRepository
{
    CourseVM GetCourseById(int id);
}
public class MainRepository : IMainRepository
{
    private readonly ApplicationContext context;

    public MainRepository(ApplicationContext _context)
    {
        context = _context;
    }
    public CourseVM GetCourseById(int id)
    {
        CourseVM result = new CourseVM();
        var dbCourse = context.Courses.FirstOrDefault(i => i.Id == id);

        result.Id = dbCourse.Id;
        result.Name = dbCourse.Name;
        result.UpdatedDate = dbCourse.UpdatedDate;
        result.CreatedDate = dbCourse.CreatedDate;
        result.Author = dbCourse.Author;
        result.Description = dbCourse.Description;

        return result;
    }
}