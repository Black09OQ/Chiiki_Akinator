using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace WorkRegisterScene
{
    public class ProtocolNode : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI protocolID;
        [SerializeField] TMP_InputField protocolInput;
        public string protocol { get { return protocolInput.text; } }

        public void SetProtocolID(int id)
        {
            protocolID.SetText(id.ToString());
        }
        public void SetInputText(string protocol)
        {
            protocolInput.text = protocol;
        }
    }
}

