using FanurApp.Data;
using FanurApp.Models;

namespace FanurApp.Repositories;
public interface IStudentRepository
{
    bool AddPosition(Position position);
    bool UpdatePosition(int positionId, Position newPosition);
    List<Position> GetAllPositions();
    List<Position> GetAllPositionsByTopicId(int topicId);
    List<Position> GetAllPositionsByUserId(int userId);
    void AcceptedVideo(int topicId, int userId);
}
public class StudentRepository : IStudentRepository
{
    private readonly ApplicationContext context;

    public StudentRepository(ApplicationContext _context)
    {
        context = _context;
    }

    public void AcceptedVideo(int topicId, int userId)
    {
        Position position = context.Positions.FirstOrDefault(i => i.TopicId == topicId && i.UserId == userId);
        if (position != null)
        {
            position.IsCompleteVideo = true;
            context.SaveChanges();
        }
    }

    public bool AddPosition(Position position)
    {
        try
        {
            context.Positions.Add(position);
            context.SaveChanges();
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }

    public List<Position> GetAllPositions()
    {
        return context.Positions.ToList();
    }

    public List<Position> GetAllPositionsByTopicId(int topicId)
    {
        return context.Positions.Where(i => i.TopicId == topicId).ToList();
    }

    public List<Position> GetAllPositionsByUserId(int userId)
    {
        return context.Positions.Where(i => i.UserId == userId).ToList();
    }

    public bool UpdatePosition(int positionId, Position newPosition)
    {
        Position position = context.Positions.FirstOrDefault(i => i.Id == positionId);
        if (position == null)
        {
            position.IsCompleteListening = newPosition.IsCompleteListening;
            position.IsCompleteVideo = newPosition.IsCompleteVideo;
            position.IsCompleteTopic = newPosition.IsCompleteTopic;
            position.VideoId = newPosition.VideoId;
            position.TopicId = newPosition.TopicId;
            position.UserId = newPosition.UserId;
            position.ListeningId = newPosition.ListeningId;
            position.ModifiedOn = DateTime.Now;
        }
        return true;
    }
}