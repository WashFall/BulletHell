using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem.Processors;

public class ScoreText : MonoBehaviour
{
    private TMP_Text scoreText;

    private void Awake()
    {
        scoreText = GetComponent<TMP_Text>();
    }

    void FixedUpdate()
    {
        scoreText.text = "Score: " + GameManager.Instance.points.ToString();
    }
}
