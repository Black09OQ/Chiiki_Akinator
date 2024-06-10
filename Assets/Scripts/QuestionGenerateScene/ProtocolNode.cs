using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace QuestionGenerateScene
{
    public class ProtocolNode : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI orderTMP;
        [SerializeField] TextMeshProUGUI protocolTMP;
        public Protocol protocol;

        public void SetProtocol(Protocol p)
        {
            protocol = p;
            SetProtocolTMP();
        }

        public void SetProtocolData(int workID)
        {
            protocol.Name = protocolTMP.text;
            protocol.Order = Convert.ToInt32(orderTMP.text);
            protocol.WorkID = workID;
        }

        private void SetProtocolTMP()
        {
            orderTMP.SetText(protocol.Order.ToString());
            protocolTMP.text = protocol.Name;
        }
    }
}

