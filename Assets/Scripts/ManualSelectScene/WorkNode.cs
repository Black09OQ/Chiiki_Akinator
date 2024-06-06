using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ManualSelectScene
{
    public class WorkNode : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI workIDTMP;
        [SerializeField] TextMeshProUGUI workNameTMP;
        [SerializeField] Button startButton;

        WorkListPanel parent;

        private Work work;

        private void Start()
        {
            parent = GameObject.Find("WorkListPanel").GetComponent<WorkListPanel>();
            startButton.onClick.AddListener(OnWorkSelectButtonDown);
        }

        public void SetWork(Work w)
        {
            work = w;
            SetWorkTMP();
        }

        void SetWorkTMP()
        {
            workIDTMP.SetText(work.ID.ToString());
            workNameTMP.SetText(work.Name);
        }

        public void OnWorkSelectButtonDown()
        {
            parent.StartWork(work);
        }
    }


}

