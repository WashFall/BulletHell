using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public delegate void PlayerTakeDamage();
    public event PlayerTakeDamage playerTakeDamage;

    void Start()
    {
        GameManager.Instance.player = this.gameObject;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Point"))
        {
            GameManager.Instance.points += collision.gameObject.GetComponent<Points>().value;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            playerTakeDamage?.Invoke();
        }

    }
}
