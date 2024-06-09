using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro; // TextMeshProを使う場合

public class ScrollViewManager : MonoBehaviour
{
    public GameObject workItemPrefab;
    public GameObject qaPrefab;
    public Transform contentParent;

    private List<WorkItem> workItems = new List<WorkItem>
    {
        new WorkItem { title = "バリ取り", progress = "100%" },
        new WorkItem { question = "Q.切削速度や送り速度をどのように設定しましたか？", answer = "A.材料によって設定しています" },
        new WorkItem { question = "Q.力加減を教えてください", answer = "A.気持ち強め" },
        new WorkItem { question = "Q.切削深さをどのように決定しましたか？", answer = "A.図面通りに" },
        new WorkItem { title = "バリ取り", progress = "60%" },
        new WorkItem { question = "Q.力加減を教えてください", answer = "A.気持ち強め" },
        new WorkItem { title = "バリ取り", progress = "90%" },
        new WorkItem { question = "Q.力加減を教えてください", answer = "A.気持ち強め" },
        new WorkItem { title = "バリ取り", progress = "50%" },
        new WorkItem { question = "Q.力加減を教えてください", answer = "A.気持ち強め" },
        new WorkItem { title = "バリ取り", progress = "700%" },
        new WorkItem { question = "Q.力加減を教えてください", answer = "A.気持ち強め" },
        new WorkItem { title = "バリ取り", progress = "80%" },
        new WorkItem { question = "Q.力加減を教えてください", answer = "A.気持ち強め" },
    };

    void Start()
    {
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

    [System.Serializable]
    public class WorkItem
    {
        public string title; // タイトル
        public string progress; // 進捗
        public string question; // 質問
        public string answer; // 回答
    }
}
