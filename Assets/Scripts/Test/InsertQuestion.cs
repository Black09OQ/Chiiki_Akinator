using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class InsertQuestion : MonoBehaviour
{
    public string questionName;
    public int workId;
    public int protocolId;

    [SerializeField]
    public List<Question> questions = new List<Question>();

    [SerializeField] DataManager dataManager;
    public void AddQuestion()
    {
        Question q = new Question();
        q.Name = questionName;
        q.WorkID = workId;
        q.ProtocolID = protocolId;
        dataManager.InsertQuestion(q);

    }
}
