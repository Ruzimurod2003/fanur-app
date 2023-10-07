﻿using FanurApp.Data;
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
            .Where(i => i.Name == topic.Name)
            .Where(i => i.Author == topic.Author)
            .Where(i => i.Description == topic.Description)
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
            .Where(i => i.URLName == video.URLName)
            .Where(i => i.Author == video.Author)
            .Where(i => i.Caption == video.Caption)
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
            .Where(i => i.HMTLText == definition.HMTLText)
            .Where(i => i.Author == definition.Author)
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
            .Where(i => i.Key == resource.Key)
            .Where(i => i.Value == resource.Value)
            .Where(i => i.CultureId == resource.CultureId)
            .Any();
    }

    public List<Culture> GetAllCultures()
    {
        return context.Cultures.ToList();
    }
    #endregion
}