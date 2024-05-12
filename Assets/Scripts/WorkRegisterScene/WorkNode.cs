using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace WorkRegisterScene
{
    public class WorkNode : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI workIDTMP;
        [SerializeField] TextMeshProUGUI workNameTMP;
        [SerializeField] Button editButton;

        WorkRegisterSceneController controller;

        private Work work;

        private void Start()
        {
            controller = GameObject.Find("WorkRegisterSceneController").GetComponent<WorkRegisterSceneController>();
        }

        public void SetWorkTMP(string id, string workName)
        {
            workIDTMP.SetText(id);
            workNameTMP.SetText(workName);
        }

        public void OnEditButtonDown()
        {
            controller.EditWork(work);
        }
    }


}

