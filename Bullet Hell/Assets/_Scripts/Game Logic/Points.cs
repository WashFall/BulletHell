using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    public int value = 1;
    public bool isInRange = false;
    private int speed = 7;

    private void Update()
    {
        if (isInRange)
        {
            transform.position =
                Vector3.Lerp(transform.position, 
                GameManager.Instance.player.transform.position, 
                Time.deltaTime * speed);
        }
    }
}
