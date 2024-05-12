using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace WorkRegisterScene
{
    public class DataLoder : MonoBehaviour
    {
        [SerializeField] TextAsset dataJson;
        public List<Work> LoadData()
        {
            List<Work> works;

            if(dataJson == null)
            {
                works = new List<Work>();
            }
            else
            {

                works = JsonUtility.FromJson<WorkList>(dataJson.ToString()).works;
            }

            return works;
        }
    }

    [SerializeField]
    public class WorkList
    {
        public List<Work> works;
    }
}
