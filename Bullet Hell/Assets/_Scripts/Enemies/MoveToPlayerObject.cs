using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayerObject : MonoBehaviour
{
    public GameObject playerObject;

    private float speed = 3;

    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards
            (transform.position, playerObject.transform.position, Time.fixedDeltaTime * speed);
    }
}
