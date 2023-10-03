using FanurApp.Data;
using FanurApp.Models;

namespace FanurApp.Repositories;
public interface IAdministratorRepository
{
    List<Course> GetAllCourses();
    Course GetCourseById(int id);
    bool AddCourse(Course course);
    bool UpdateCourse(int courseId, Course newCourse);
    bool DeleteCourse(int courseId);
}
public class AdministratorRepository : IAdministratorRepository
{
    private readonly ApplicationContext context;

    public AdministratorRepository(ApplicationContext _context)
    {
        context = _context;
    }
    public List<Course> GetAllCourses()
    {
        return context.Courses.ToList();
    }

    public Course GetCourseById(int id)
    {
        var course = context.Courses.FirstOrDefault(i => i.Id == id);
        return course;
    }

    public bool AddCourse(Course course)
    {
        if (context.Courses.Where(i => i.Name == course.Name && i.Author == course.Author && i.Description == course.Description).Any())
        {
            return false;
        }
        context.Courses.Add(course);
        context.SaveChanges();
        return true;
    }

    public bool UpdateCourse(int courseId, Course newCourse)
    {
        if (!context.Courses.Where(i => i.Id == courseId).Any())
        {
            return false;
        }
        if (context.Courses.Where(i => i.Name == newCourse.Name && i.Author == newCourse.Author && i.Description == newCourse.Description).Any())
        {
            return false;
        }

        var course = context.Courses.FirstOrDefault(c => c.Id == courseId);
        if (course != null)
        {
            course.Author = newCourse.Author;
            course.Description = newCourse.Description;
            course.Name = newCourse.Name;
            course.UpdatedDate = DateTime.Now;
        }
        context.SaveChanges();
        return true;
    }

    public bool DeleteCourse(int courseId)
    {
        if (!context.Courses.Where(i => i.Id == courseId).Any())
        {
            return false;
        }
        var course = context.Courses.FirstOrDefault(c => c.Id == courseId);
        if (course != null)
        {
            context.Courses.Remove(course);
        }
        context.SaveChanges();
        return true;
    }
}