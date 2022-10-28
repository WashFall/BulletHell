using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MindFire : MonoBehaviour
{
    public GameObject playerObject;
    public Attack_MindFire attacker;

    private Vector2 originalSize;
    private float speed = 5;
    private Rigidbody2D rigidBody;
    private float travelDistance;
    private float cameraWidth;

    private void Awake()
    {
        originalSize = transform.localScale;
        rigidBody = GetComponent<Rigidbody2D>();
        cameraWidth = Camera.main.aspect * (Camera.main.orthographicSize) + 1;
    }

    void OnEnable()
    {
        if (attacker is not null)
            transform.localScale = originalSize * attacker.projectileSize;

        rigidBody.velocity = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized * speed;
    }

    private void FixedUpdate()
    {
        travelDistance = Vector3.Distance(transform.position, playerObject.transform.position);
        if (travelDistance > cameraWidth)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;
        collision.gameObject.GetComponent<Enemy>().TakeDamage(attacker.damage);
    }
}
