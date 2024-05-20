using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WarnPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI warnTMP;

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void SetText(string text)
    {
        warnTMP.SetText(text);
        gameObject.SetActive(true);
    }
}
