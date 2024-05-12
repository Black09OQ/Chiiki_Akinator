using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SQLite4Unity3d;

public static class DataService
{
    private static SQLiteConnection _database;

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

    public static void CreateTable()
    {

        try
        {
            _database.CreateTable<Work>();
            _database.CreateTable<Protocol>();
        }
        catch(Exception e)
        {
            Debug.LogError($"Failed to create table: {e.Message}");
        }

    }

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

    public static List<Protocol> GetProtocolsById(int id)
    {
        try
        {
            return _database.Table<Protocol>().Where(p => p.ID == id).ToList();
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

    public static void DeleteAllData()
    {

    }
}
