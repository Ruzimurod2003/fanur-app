using FanurApp.Data;
using FanurApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FanurApp.Repositories;
public interface IAdministratorRepository
{

    #region Files
    List<Models.File> GetAllFiles();
    Models.File GetFileById(int id);
    bool AddFile(Models.File file);
    bool UpdateFile(int fileId, Models.File newFile);
    bool DeleteFile(int fileId);
    #endregion

    #region Users
    List<User> GetAllUsers();
    User GetUserById(int id);
    bool AddUser(User user);
    bool UpdateUser(int userId, User newUser);
    bool DeleteUser(int userId);
    #endregion

    #region Courses
    List<Course> GetAllCourses();
    Course GetCourseById(int id);
    bool AddCourse(Course course);
    bool UpdateCourse(int courseId, Course newCourse);
    bool DeleteCourse(int courseId);
    #endregion

    #region Topics
    List<Topic> GetAllTopics();
    List<Topic> GetAllTopicsByCourseId(int courseId);
    Topic GetTopicById(int id);
    bool AddTopic(Topic topic);
    bool UpdateTopic(int topicId, Topic newTopic);
    bool DeleteTopic(int topicId);
    #endregion

    #region Videos
    List<Video> GetAllVideos();
    Video GetVideoById(int id);
    bool AddVideo(Video video);
    bool UpdateVideo(int videoId, Video newVideo);
    bool DeleteVideo(int videoId);
    #endregion

    #region Definitions
    List<Definition> GetAllDefinitions();
    Definition GetDefinitionById(int id);
    bool AddDefinition(Definition definition);
    bool UpdateDefinition(int definitionId, Definition newDefinition);
    bool DeleteDefinition(int definitionId);
    #endregion

    #region Resources
    List<Resource> GetAllResources();
    List<Culture> GetAllCultures();
    Resource GetResourceById(int id);
    bool AddResource(Resource resource);
    bool UpdateResource(int resourceId, Resource newResource);
    bool DeleteResource(int resourceId);
    #endregion

    #region Roles
    List<Role> GetAllRoles();
    Role GetRoleById(int id);
    bool AddRole(Role role);
    bool UpdateRole(int roleId, Role newRole);
    bool DeleteRole(int roleId);

    #endregion

    #region Quizzes
    List<Quiz> GetAllQuizzes();
    Quiz GetQuizById(int id);
    bool AddQuiz(Quiz quiz);
    bool UpdateQuiz(int quizId, Quiz newQuiz);
    bool DeleteQuiz(int quizId);

    #endregion
}
public class AdministratorRepository : IAdministratorRepository
{
    private readonly ApplicationContext context;

    public AdministratorRepository(ApplicationContext _context)
    {
        context = _context;
    }

    #region Files
    public List<Models.File> GetAllFiles()
    {
        return context.Files.Include(i => i.Topic).ToList();
    }

    public Models.File GetFileById(int id)
    {
        var file = context.Files.Include(i => i.Topic).FirstOrDefault(i => i.Id == id);
        return file;
    }

    public bool AddFile(Models.File file)
    {
        if (IsDublicateFile(file))
        {
            return false;
        }
        file.CreatedDate = DateTime.Now;

        context.Files.Add(file);
        context.SaveChanges();
        return true;
    }

    public bool UpdateFile(int fileId, Models.File newFile)
    {
        if (!IsExistFile(fileId))
        {
            return false;
        }
        if (IsDublicateFile(newFile))
        {
            return false;
        }

        var file = context.Files.Include(i => i.Topic).FirstOrDefault(c => c.Id == fileId);
        if (file != null)
        {
            file.TopicId = newFile.TopicId;
            file.Path = newFile.Path;
            file.Name = newFile.Name;
            file.FileTypeId = newFile.FileTypeId;
        }
        context.SaveChanges();
        return true;
    }

    public bool DeleteFile(int fileId)
    {
        if (!IsExistFile(fileId))
        {
            return false;
        }
        var file = context.Files.Include(i => i.Topic).FirstOrDefault(c => c.Id == fileId);
        if (file != null)
        {
            context.Files.Remove(file);
        }
        context.SaveChanges();
        return true;
    }
    private bool IsExistFile(int fileId)
    {
        return context.Files.Where(i => i.Id == fileId).Any();
    }
    private bool IsDublicateFile(Models.File file)
    {
        return context.Files
            .Where(i => i.TopicId == file.TopicId)
            .Where(i => i.Name == file.Name)
            .Where(i => i.Path == file.Path)
            .Where(i => i.FileTypeId == file.FileTypeId)
            .Any();
    }
    #endregion

    #region Users
    public List<User> GetAllUsers()
    {
        return context.Users.Include(i => i.Role).ToList();
    }

    public User GetUserById(int id)
    {
        var User = context.Users.Include(i => i.Role).FirstOrDefault(i => i.Id == id);
        return User;
    }

    public bool AddUser(User user)
    {
        if (IsDublicateUser(user))
        {
            return false;
        }
        user.CreatedDate = DateTime.Now;
        user.UpdatedDate = DateTime.Now;

        context.Users.Add(user);
        context.SaveChanges();
        return true;
    }

    public bool UpdateUser(int userId, User newUser)
    {
        if (!IsExistUser(userId))
        {
            return false;
        }
        if (IsDublicateUser(newUser))
        {
            return false;
        }

        var user = context.Users.FirstOrDefault(c => c.Id == userId);
        if (user != null)
        {
            user.Name = newUser.Name;
            user.Email = newUser.Email;
            user.Password = newUser.Password;
            user.RoleId = newUser.RoleId;
            user.UpdatedDate = DateTime.Now;
        }
        context.SaveChanges();
        return true;
    }

    public bool DeleteUser(int userId)
    {
        if (!IsExistUser(userId))
        {
            return false;
        }
        var user = context.Users.FirstOrDefault(c => c.Id == userId);
        if (user != null)
        {
            context.Users.Remove(user);
        }
        context.SaveChanges();
        return true;
    }
    private bool IsExistUser(int userId)
    {
        return context.Users.Where(i => i.Id == userId).Any();
    }
    private bool IsDublicateUser(User User)
    {
        return context.Users
            .Where(i => i.Email.ToLower() == User.Email.ToLower())
            .Where(i => i.Password.ToLower() == User.Password.ToLower())
            .Where(i => i.RoleId == User.RoleId)
            .Any();
    }
    #endregion

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
        course.CreatedDate = DateTime.Now;
        course.UpdatedDate = DateTime.Now;

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
            .Where(i => i.Name.ToLower() == course.Name.ToLower())
            .Where(i => i.Author.ToLower() == course.Author.ToLower())
            .Where(i => i.Description.ToLower() == course.Description.ToLower())
            .Any();
    }
    #endregion

    #region Topics
    public List<Topic> GetAllTopics()
    {
        return context.Topics.Include(i => i.Course).ToList();
    }

    public List<Topic> GetAllTopicsByCourseId(int courseId)
    {
        return context.Topics.Include(i => i.Course)
        .Where(i => i.CourseId == courseId)
        .ToList();
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
        topic.CreatedDate = DateTime.Now;
        topic.UpdatedDate = DateTime.Now;

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
        if (IsExistTopicVideo(topicId))
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
    private bool IsExistTopicVideo(int topicId)
    {
        return context.Videos.Where(i => i.TopicId == topicId).Any();
    }
    private bool IsDublicateTopic(Topic topic)
    {
        return context.Topics
            .Where(i => i.Name.ToLower() == topic.Name.ToLower())
            .Where(i => i.Author.ToLower() == topic.Author.ToLower())
            .Where(i => i.Description.ToLower() == topic.Description.ToLower())
            .Where(i => i.CourseId == topic.CourseId)
            .Any();
    }
    #endregion

    #region Videos
    public List<Video> GetAllVideos()
    {
        return context.Videos.Include(i => i.Topic).ToList();
    }

    public Video GetVideoById(int id)
    {
        var video = context.Videos.Include(i => i.Topic).FirstOrDefault(i => i.Id == id);
        return video;
    }

    public bool AddVideo(Video video)
    {
        if (IsDublicateVideo(video))
        {
            return false;
        }
        video.CreatedDate = DateTime.Now;
        video.UpdatedDate = DateTime.Now;

        context.Videos.Add(video);
        context.SaveChanges();
        return true;
    }

    public bool UpdateVideo(int videoId, Video newVideo)
    {
        if (!IsExistVideo(videoId))
        {
            return false;
        }
        if (IsDublicateVideo(newVideo))
        {
            return false;
        }

        var video = context.Videos.Include(i => i.Topic).FirstOrDefault(c => c.Id == videoId);
        if (video != null)
        {
            video.Author = newVideo.Author;
            video.TopicId = newVideo.TopicId;
            video.Caption = newVideo.Caption;
            video.URLName = newVideo.URLName;
            video.UpdatedDate = DateTime.Now;
        }
        context.SaveChanges();
        return true;
    }

    public bool DeleteVideo(int videoId)
    {
        if (!IsExistVideo(videoId))
        {
            return false;
        }
        var video = context.Videos.Include(i => i.Topic).FirstOrDefault(c => c.Id == videoId);
        if (video != null)
        {
            context.Videos.Remove(video);
        }
        context.SaveChanges();
        return true;
    }
    private bool IsExistVideo(int videoId)
    {
        return context.Videos.Where(i => i.Id == videoId).Any();
    }
    private bool IsDublicateVideo(Video video)
    {
        return context.Videos
            .Where(i => i.URLName.ToLower() == video.URLName.ToLower())
            .Where(i => i.Author.ToLower() == video.Author.ToLower())
            .Where(i => i.Caption.ToLower() == video.Caption.ToLower())
            .Where(i => i.TopicId == video.TopicId)
            .Any();
    }
    #endregion

    #region Definitions
    public List<Definition> GetAllDefinitions()
    {
        return context.Definitions.Include(i => i.Topic).ToList();
    }

    public Definition GetDefinitionById(int id)
    {
        var definition = context.Definitions.Include(i => i.Topic).FirstOrDefault(i => i.Id == id);
        return definition;
    }

    public bool AddDefinition(Definition definition)
    {
        if (IsDublicateDefinition(definition))
        {
            return false;
        }
        definition.CreatedDate = DateTime.Now;
        definition.UpdatedDate = DateTime.Now;

        context.Definitions.Add(definition);
        context.SaveChanges();
        return true;
    }

    public bool UpdateDefinition(int definitionId, Definition newDefinition)
    {
        if (!IsExistDefinition(definitionId))
        {
            return false;
        }
        if (IsDublicateDefinition(newDefinition))
        {
            return false;
        }

        var definition = context.Definitions.Include(i => i.Topic).FirstOrDefault(c => c.Id == definitionId);
        if (definition != null)
        {
            definition.Author = newDefinition.Author;
            definition.TopicId = newDefinition.TopicId;
            definition.HMTLText = newDefinition.HMTLText;
            definition.UpdatedDate = DateTime.Now;
        }
        context.SaveChanges();
        return true;
    }

    public bool DeleteDefinition(int definitionId)
    {
        if (!IsExistDefinition(definitionId))
        {
            return false;
        }
        var definition = context.Definitions.Include(i => i.Topic).FirstOrDefault(c => c.Id == definitionId);
        if (definition != null)
        {
            context.Definitions.Remove(definition);
        }
        context.SaveChanges();
        return true;
    }
    private bool IsExistDefinition(int definitionId)
    {
        return context.Definitions.Where(i => i.Id == definitionId).Any();
    }
    private bool IsDublicateDefinition(Definition definition)
    {
        return context.Definitions
            .Where(i => i.HMTLText.ToLower() == definition.HMTLText.ToLower())
            .Where(i => i.Author.ToLower() == definition.Author.ToLower())
            .Where(i => i.TopicId == definition.TopicId)
            .Any();
    }
    #endregion

    #region Resources
    public List<Resource> GetAllResources()
    {
        return context.Resources.Include(i => i.Culture).ToList();
    }

    public Resource GetResourceById(int id)
    {
        var resource = context.Resources.Include(i => i.Culture).FirstOrDefault(i => i.Id == id);
        return resource;
    }

    public bool AddResource(Resource resource)
    {
        if (IsDublicateResource(resource))
        {
            return false;
        }
        resource.CreatedDate = DateTime.Now;
        resource.UpdatedDate = DateTime.Now;

        context.Resources.Add(resource);
        context.SaveChanges();
        return true;
    }

    public bool UpdateResource(int resourceId, Resource newResource)
    {
        if (!IsExistResource(resourceId))
        {
            return false;
        }
        if (IsDublicateResource(newResource))
        {
            return false;
        }

        var resource = context.Resources.Include(i => i.Culture).FirstOrDefault(c => c.Id == resourceId);
        if (resource != null)
        {
            resource.Key = newResource.Key;
            resource.Value = newResource.Value;
            resource.CultureId = newResource.CultureId;
            resource.UpdatedDate = DateTime.Now;
        }
        context.SaveChanges();
        return true;
    }

    public bool DeleteResource(int resourceId)
    {
        if (!IsExistResource(resourceId))
        {
            return false;
        }
        var resource = context.Resources.Include(i => i.Culture).FirstOrDefault(c => c.Id == resourceId);
        if (resource != null)
        {
            context.Resources.Remove(resource);
        }
        context.SaveChanges();
        return true;
    }
    private bool IsExistResource(int resourceId)
    {
        return context.Resources.Where(i => i.Id == resourceId).Any();
    }
    private bool IsDublicateResource(Resource resource)
    {
        return context.Resources
            .Where(i => i.Key.ToLower() == resource.Key.ToLower())
            .Where(i => i.Value.ToLower() == resource.Value.ToLower())
            .Where(i => i.CultureId == resource.CultureId)
            .Any();
    }

    public List<Culture> GetAllCultures()
    {
        return context.Cultures.ToList();
    }
    #endregion

    #region Roles
    public List<Role> GetAllRoles()
    {
        return context.Roles.ToList();
    }

    public Role GetRoleById(int id)
    {
        return context.Roles.FirstOrDefault(r => r.Id == id);
    }

    public bool AddRole(Role role)
    {
        if (IsDublicateRole(role))
        {
            return false;
        }
        context.Roles.Add(role);
        context.SaveChanges();
        return true;
    }

    public bool UpdateRole(int roleId, Role newRole)
    {
        if (!IsExistRole(roleId))
        {
            return false;
        }
        if (IsDublicateRole(newRole))
        {
            return false;
        }

        var role = context.Roles.FirstOrDefault(c => c.Id == roleId);
        if (role != null)
        {
            role.Name = newRole.Name;
        }
        context.SaveChanges();
        return true;
    }

    public bool DeleteRole(int roleId)
    {
        if (!IsExistRole(roleId) || IsExistRoleUser(roleId))
        {
            return false;
        }
        var course = context.Courses.FirstOrDefault(c => c.Id == roleId);
        if (course != null)
        {
            context.Courses.Remove(course);
        }
        context.SaveChanges();
        return true;
    }
    private bool IsExistRoleUser(int roleId)
    {
        return context.Users.Where(i => i.RoleId == roleId).Any();
    }
    private bool IsExistRole(int roleId)
    {
        return context.Roles.Where(i => i.Id == roleId).Any();
    }
    private bool IsDublicateRole(Role course)
    {
        return context.Roles
            .Where(i => i.Name.ToLower() == course.Name.ToLower())
            .Any();
    }
    #endregion

    #region Quizzes
    public List<Quiz> GetAllQuizzes()
    {
        return context.Quizzes.Include(i => i.Topic).ToList();
    }

    public Quiz GetQuizById(int id)
    {
        return context.Quizzes.Include(i => i.Topic)
            .Where(i => i.Id == id)
            .FirstOrDefault();
    }

    public bool AddQuiz(Quiz quiz)
    {
        if (IsDublicateQuiz(quiz))
        {
            return false;
        }
        quiz.CreatedDate = DateTime.Now;
        quiz.UpdatedDate = DateTime.Now;
        context.Quizzes.Add(quiz);
        context.SaveChanges();
        return true;
    }

    public bool UpdateQuiz(int quizId, Quiz newQuiz)
    {
        if (!IsExistQuiz(quizId))
        {
            return false;
        }
        if (IsDublicateQuiz(newQuiz))
        {
            return false;
        }

        var quiz = context.Quizzes.FirstOrDefault(c => c.Id == quizId);
        if (quiz != null)
        {
            quiz.QuestionText = newQuiz.QuestionText;
            quiz.AnswerA = newQuiz.AnswerA;
            quiz.AnswerB = newQuiz.AnswerB;
            quiz.AnswerC = newQuiz.AnswerC;
            quiz.AnswerD = newQuiz.AnswerD;
            quiz.TopicId = newQuiz.TopicId;
            quiz.IsTrueAnswer = newQuiz.IsTrueAnswer;
        }
        context.SaveChanges();
        return true;
    }

    public bool DeleteQuiz(int quizId)
    {
        if (!IsExistQuiz(quizId))
        {
            return false;
        }
        var quiz = context.Quizzes.FirstOrDefault(c => c.Id == quizId);
        if (quiz != null)
        {
            context.Quizzes.Remove(quiz);
        }
        context.SaveChanges();
        return true;
    }
    private bool IsExistQuiz(int quizId)
    {
        return context.Quizzes.Where(i => i.Id == quizId).Any();
    }
    private bool IsDublicateQuiz(Quiz quiz)
    {
        return context.Quizzes
            .Where(i => i.QuestionText.ToLower() == quiz.QuestionText.ToLower())
            .Where(i => i.AnswerA.ToLower() == quiz.AnswerA.ToLower())
            .Where(i => i.AnswerB.ToLower() == quiz.AnswerB.ToLower())
            .Where(i => i.AnswerC.ToLower() == quiz.AnswerC.ToLower())
            .Where(i => i.AnswerD.ToLower() == quiz.AnswerD.ToLower())
            .Where(i => i.TopicId == quiz.TopicId)
            .Any();
    }
    #endregion
}