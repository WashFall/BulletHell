using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.player = this.gameObject;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!collision.gameObject.CompareTag("Point")) return;

        GameManager.Instance.points += collision.gameObject.GetComponent<Points>().value;
        Destroy(collision.gameObject);
    }
}
