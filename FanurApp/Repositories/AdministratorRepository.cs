using FanurApp.Data;
using FanurApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FanurApp.Repositories;
public interface IAdministratorRepository
{
    #region Courses
    List<Course> GetAllCourses();
    Course GetCourseById(int id);
    bool AddCourse(Course course);
    bool UpdateCourse(int courseId, Course newCourse);
    bool DeleteCourse(int courseId);
    #endregion

    #region Topics
    List<Topic> GetAllTopics();
    Topic GetTopicById(int id);
    bool AddTopic(Topic topic);
    bool UpdateTopic(int topicId, Topic newTopic);
    bool DeleteTopic(int topicId);
    #endregion
}
public class AdministratorRepository : IAdministratorRepository
{
    private readonly ApplicationContext context;

    public AdministratorRepository(ApplicationContext _context)
    {
        context = _context;
    }

    #region Courses
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
        if (IsDublicateCourse(course))
        {
            return false;
        }
        context.Courses.Add(course);
        context.SaveChanges();
        return true;
    }

    public bool UpdateCourse(int courseId, Course newCourse)
    {
        if (!IsExistCourse(courseId))
        {
            return false;
        }
        if (IsDublicateCourse(newCourse))
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
        if (!IsExistCourse(courseId) || IsExistCourseTopic(courseId))
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
    private bool IsExistCourse(int courseId)
    {
        return context.Courses.Where(i => i.Id == courseId).Any();
    }
    private bool IsExistCourseTopic(int courseId)
    {
        return context.Topics.Where(i => i.CourseId == courseId).Any();
    }
    private bool IsDublicateCourse(Course course)
    {
        return context.Courses
            .Where(i => i.Name == course.Name)
            .Where(i => i.Author == course.Author)
            .Where(i => i.Description == course.Description)
            .Any();
    }
    #endregion

    #region Topics
    public List<Topic> GetAllTopics()
    {
        return context.Topics.Include(i => i.Course).ToList();
    }

    public Topic GetTopicById(int id)
    {
        var topic = context.Topics.Include(i => i.Course).FirstOrDefault(i => i.Id == id);
        return topic;
    }

    public bool AddTopic(Topic topic)
    {
        if (IsDublicateTopic(topic))
        {
            return false;
        }
        context.Topics.Add(topic);
        context.SaveChanges();
        return true;
    }

    public bool UpdateTopic(int topicId, Topic newTopic)
    {
        if (!IsExistTopic(topicId))
        {
            return false;
        }
        if (IsDublicateTopic(newTopic))
        {
            return false;
        }

        var topic = context.Topics.Include(i => i.Course).FirstOrDefault(c => c.Id == topicId);
        if (topic != null)
        {
            topic.Author = newTopic.Author;
            topic.CourseId = newTopic.CourseId;
            topic.Description = newTopic.Description;
            topic.Name = newTopic.Name;
            topic.UpdatedDate = DateTime.Now;
        }
        context.SaveChanges();
        return true;
    }

    public bool DeleteTopic(int topicId)
    {
        if (!IsExistTopic(topicId))
        {
            return false;
        }
        var topic = context.Topics.Include(i => i.Course).FirstOrDefault(c => c.Id == topicId);
        if (topic != null)
        {
            context.Topics.Remove(topic);
        }
        context.SaveChanges();
        return true;
    }
    private bool IsExistTopic(int topicId)
    {
        return context.Topics.Where(i => i.Id == topicId).Any();
    }
    private bool IsDublicateTopic(Topic topic)
    {
        return context.Topics
            .Where(i => i.Name == topic.Name)
            .Where(i => i.Author == topic.Author)
            .Where(i => i.Description == topic.Description)
            .Where(i => i.CourseId == topic.CourseId)
            .Any();
    }
    #endregion
}