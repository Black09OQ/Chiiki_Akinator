using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace WorkRegisterScene
{
    public class ProtocolNode : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI orderTMP;
        [SerializeField] TMP_InputField protocolInput;
        public Protocol protocol;

        public void SetProtocol(Protocol p)
        {
            protocol = p;
            SetProtocolTMP();
        }

        public void SetProtocolData(int workID)
        {
            protocol.Name = protocolInput.text;
            protocol.Order = Convert.ToInt32(orderTMP.text);
            protocol.WorkID = workID;
        }

        private void SetProtocolTMP()
        {
            orderTMP.SetText(protocol.Order.ToString());
            protocolInput.text = protocol.Name;
        }
    }
}

