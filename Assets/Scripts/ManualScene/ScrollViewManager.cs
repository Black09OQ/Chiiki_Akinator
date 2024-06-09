using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro; // TextMeshProを使う場合

public class ScrollViewManager : MonoBehaviour
{
    public GameObject workItemPrefab;
    public GameObject qaPrefab;
    public DataManager dataManager;
    public Transform contentParent;

    private List<WorkItem> workItems;
    void Start()
    {
        workItems = new List<WorkItem>();
        GetData();
        PopulateScrollView();
    }

    void PopulateScrollView()
    {
        foreach (var item in workItems)
        {
            if (!string.IsNullOrEmpty(item.title)) // タイトルがある場合
            {
                var workItemGO = Instantiate(workItemPrefab, contentParent);
                var texts = workItemGO.GetComponentsInChildren<TextMeshProUGUI>(); // TextMeshProUGUIを使用する場合
                if (texts.Length >= 4)
                {
                    texts[1].text = item.title; // work item Text(TMP)(1)を設定
                    texts[3].text = item.progress; // work item Text(TMP)(3)を設定
                }
                else
                {
                    Debug.LogError("WorkItemPrefabのTextMeshProコンポーネントの数が足りません。");
                }
            }
            else // 質問と回答がある場合
            {
                var qaGO = Instantiate(qaPrefab, contentParent);
                var texts = qaGO.GetComponentsInChildren<TextMeshProUGUI>(); // TextMeshProUGUIを使用する場合
                if (texts.Length >= 2)
                {
                    texts[0].text = item.question; // 質問を設定
                    texts[1].text = item.answer; // 回答を設定
                }
                else
                {
                    Debug.LogError("QAPrefabのTextMeshProコンポーネントの数が足りません。");
                }
            }
        }
    }

    private void GetData()
    {   
        List<Protocol> protocols = dataManager.GetProtocolsByWorkId(UserData.work.ID);
        foreach (Protocol p in protocols)
        {
            workItems.Add(new WorkItem{title = p.Name});
            List<Question> questions = dataManager.GetQuestionsByProtocolId(p.ID);
            foreach(Question q in questions)
            {
                List<Result> results = dataManager.GetResultsByQuestionIdAndUserId(q.ID, UserData.Id);
                foreach(Result r in results)
                {
                    workItems.Add(new WorkItem {question = q.Name, answer = r.Answer});
                }
            }
        }


    }

    [System.Serializable]
    public class WorkItem
    {
        public string title; // タイトル
        public string progress; // 進捗
        public string question; // 質問
        public string answer; // 回答
    }
}
