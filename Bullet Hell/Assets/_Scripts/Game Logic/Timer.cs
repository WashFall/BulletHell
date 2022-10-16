using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private TMP_Text timerText;

    private void Awake()
    {
        timerText = GetComponent<TMP_Text>();
    }

    private void FixedUpdate()
    {
        timerText.text = Mathf.Floor(GameManager.Instance.timer).ToString();
    }
}
