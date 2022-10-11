using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayerObject : MonoBehaviour
{
    public GameObject playerObject;

    private float speed = 3;
    private Rigidbody2D rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards
            (transform.position, playerObject.transform.position, Time.fixedDeltaTime * speed);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        rigidBody.velocity = Vector2.zero;
    }
}
