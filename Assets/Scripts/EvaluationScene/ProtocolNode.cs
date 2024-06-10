using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using System;

namespace EvaluationScene
{
    public class ProtocolNode : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI orderTMP;
        [SerializeField] TextMeshProUGUI protocolNameTMP;
        [SerializeField] Slider valueSlider;
        public Protocol protocol;
        public float nowValue;

        void start()
        {
            valueSlider.minValue = 0;
            valueSlider.maxValue = 100;

            nowValue = valueSlider.value;

            valueSlider.direction = Slider.Direction.RightToLeft;

            UnityAction<float> setValue = (float value) =>
            {
                nowValue = valueSlider.value;
            };

            valueSlider.onValueChanged.AddListener(setValue);
        }


        public void SetProtocol(Protocol p)
        {
            protocol = p;
            SetProtocolTMP();
        }

        public void SetProtocolData(int workID)
        {
            protocol.Name = protocolNameTMP.text;
            protocol.Order = Convert.ToInt32(orderTMP.text);
            protocol.WorkID = workID;
        }

        private void SetProtocolTMP()
        {
            orderTMP.SetText(protocol.Order.ToString());
            protocolNameTMP.text = protocol.Name;
        }

        
    }
}

