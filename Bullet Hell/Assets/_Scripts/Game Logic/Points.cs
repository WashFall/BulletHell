using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    public float value = 1;
    public bool isInRange = false;
    private float speed = 7;

    private void Update()
    {
        if (isInRange)
        {
            transform.position =
                Vector3.Lerp(transform.position, 
                GameManager.Instance.player.transform.position, 
                Time.deltaTime * speed);
            speed += 0.01f;
        }
    }
}
