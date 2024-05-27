using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SQLite4Unity3d;

public static class DataService
{
    private static SQLiteConnection _database;

    // データベースの初期設定 --------------------------------------------------------------------------------
    public static void InitDatabase(string databaseName)
    {
        try
        {
            string DBPath = $"Assets/StreamingAssets/{databaseName}";
            _database = new SQLiteConnection(DBPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        }
        catch(Exception e)
        {
            Debug.LogError($"Failed to initialize database: {e.Message}");
        }
    }

    // データベースを閉じる ----------------------------------------------------------------------------------
    public static void CloseDatabase()
    {
        if(_database != null)
        {
            try
            {
                _database.Close();
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to close database: {e.Message}");
            }
        }
    }

    // テーブルの作成 ----------------------------------------------------------------------------------------
    public static void CreateTable()
    {

        try
        {
            _database.CreateTable<Work>();
            _database.CreateTable<Protocol>();
            _database.CreateTable<User>();
            _database.CreateTable<Question>();
        }
        catch(Exception e)
        {
            Debug.LogError($"Failed to create table: {e.Message}");
        }

    }

    // User --------------------------------------------------------------------------------------------------
    public static void InsertUser(User user)
    {
        try
        {
            _database.Insert(user);
        }
        catch(Exception e)
        {
            Debug.LogError($"Failed to insert user: {e.Message}");
        }
    }

    public static List<User> GetAllUsers()
    {
        try
        {
            return _database.Table<User>().ToList();
        }
        catch(Exception e)
        {
            Debug.LogError($"Failed to get users: {e.Message}");
            return new List<User>();
        }
    }

    public static List<User> GetUsersById(int id)
    {
        try
        {
            return _database.Table<User>().Where(u => u.ID == id).ToList();
        }
        catch(Exception e)
        {
            Debug.LogError($"Failed to get users: {e.Message}");
            return new List<User>();
        }
    }

    public static List<User> GetUsersByUserId(string userId)
    {
        try
        {
            return _database.Table<User>().Where(u => u.UserID == userId).ToList();
        }
        catch(Exception e)
        {
            Debug.LogError($"Failed to get users: {e.Message}");
            return new List<User>();
        }
    }


    // Work --------------------------------------------------------------------------------------------------
    public static void InsertWork(Work work)
    {
        try
        {
            _database.Insert(work);
        }
        catch(Exception e)
        {
            Debug.LogError($"Failed to insert work: {e.Message}");
        }
    }

    public static void UpdateWork(Work work)
    {
        try
        {
            _database.Update(work);
        }
        catch(Exception e)
        {
            Debug.LogError($"Failed to update work: {e.Message}");
        }
    }

    public static List<Work> GetAllWorks()
    {
        try
        {
            return _database.Table<Work>().ToList();
        }
        catch(Exception e)
        {
            Debug.LogError($"Failed to get works: {e.Message}");
            return new List<Work>();
        }
    }

    public static List<Work> GetWorksById(int id)
    {
        try
        {
            return _database.Table<Work>().Where(w => w.ID == id).ToList();
        }
        catch(Exception e)
        {
            Debug.LogError($"Failed to get works: {e.Message}");
            return new List<Work>();
        }
    }

    // Protocol -----------------------------------------------------------------------------------------------
    public static void InsertProtocol(Protocol protocol)
    {
        try
        {
            _database.Insert(protocol);
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to insert protocol: {e.Message}");
        }
    }

    public static void UpdateProtocol(Protocol protocol)
    {
        try
        {
            _database.Update(protocol);
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to insert protocol: {e.Message}");
        }
    }

    public static List<Protocol> GetProtocolsById(int id)
    {
        try
        {
            return _database.Table<Protocol>().Where(p => p.ID == id).OrderBy(p => p.Order).ToList();
        }
        catch(Exception e)
        {
            Debug.LogError($"Failed to get protocols: {e.Message}");
            return new List<Protocol>();
        }
    }

    public static List<Protocol> GetProtocolsByWorkId(int workId)
    {
        try
        {
            return _database.Table<Protocol>().Where(p => p.WorkID == workId).OrderBy(p => p.Order).ToList();
        }
        catch(Exception e)
        {
            Debug.LogError($"Failed to get protocols: {e.Message}");
            return new List<Protocol>();
        }
    }

    // Question ----------------------------------------------------------------------------------------------------

    public static void InsertQuestions(List<Question> questions)
    {
        try
        {
            _database.Insert(questions);
        }
        catch(Exception e)
        {
            Debug.LogError($"Failed to insert questions: {e.Message}");
        }
    }

    public static List<Question> GetQuestionsByWorkId(int workId)
    {
        try
        {
            return _database.Table<Question>().Where(p => p.WorkID == workId).ToList();
        }
        catch(Exception e)
        {
            Debug.LogError($"Failed to get questions: {e.Message}");
            return new List<Question>();
        }
    }

    public static void DeleteAllData()
    {

    }
}
