using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MindMissile : MonoBehaviour
{
    public GameObject target;
    float baseDamage = 1;
    float damage;
    float speed = 5;
    Vector3 originalSize;

    private void Awake()
    {
        originalSize = transform.localScale;
    }

    private void OnEnable()
    {
        damage = baseDamage * GameManager.Instance.currentCharacter.characterBaseDamage;
        transform.localScale = originalSize * GameManager.Instance.currentCharacter.characterProjectileSize;
    }

    private void FixedUpdate()
    {
        if(target != null)
        {
            transform.position = Vector3.Slerp(transform.position, 
                target.transform.position, Time.fixedDeltaTime * speed);
        }
        else if(target == null)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;
        collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        gameObject.SetActive(false);
    }
}
