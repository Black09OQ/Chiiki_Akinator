using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;

public class DataManager : MonoBehaviour
{
    private void Awake()
    {
        DataService.InitDatabase("Database");
        DataService.CreateTable();
    }

    private void OnDestroy()
    {
        DataService.CloseDatabase();
    }

    public void InsertUser(User user)
    {
        DataService.InsertUser(user);
    }

    public List<User> GetAllUsers()
    {
        return DataService.GetAllUsers();
    }

    public List<User> GetUsersById(int id)
    {
        return DataService.GetUsersById(id);
    }

    public List<User> GetUsersByUserId(string userId)
    {
        return DataService.GetUsersByUserId(userId);
    }

    public void InsertWork(Work work)
    {
        DataService.InsertWork(work);
    }

    public void UpdateWork(Work work)
    {
        DataService.UpdateWork(work);
    }    

    public List<Work> GetAllWorks()
    {
        return DataService.GetAllWorks();
    }

    public List<Work> GetWorksById(int workId)
    {
        return DataService.GetWorksById(workId);
    }

    public void InsertProtocol(Protocol protocol)
    {
        DataService.InsertProtocol(protocol);
    }

    public void UpdateProtocol(Protocol protocol)
    {
        DataService.UpdateProtocol(protocol);
    }

    public List<Protocol> GetProtocolsById(int id)
    {
        return DataService.GetProtocolsById(id);
    }

    public List<Protocol> GetProtocolsByWorkId(int workID)
    {
        return DataService.GetProtocolsByWorkId(workID);
    }

    public void InsertQuestions(List<Question> questions)
    {
        DataService.InsertQuestions(questions);
    }

    public List<Question> GetQuestionsByWorkId(int workID)
    {
        return DataService.GetQuestionsByWorkId(workID);
    }

}
