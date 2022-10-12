using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MindMissile : MonoBehaviour
{
    public GameObject target;
    float damage = 1;
    float speed = 5;

    private void Awake()
    {
        float pointAdjustment = GameManager.Instance.points / 10;

        damage += pointAdjustment;
        transform.localScale *= (1 + pointAdjustment);
    }

    private void FixedUpdate()
    {
        if(target != null)
        {
            //var targetSpeed = Mathf.Abs((transform.position - target.transform.position).sqrMagnitude);
            //Vector3 center = (transform.position + target.transform.position) * 0.5f;
            //Vector3 startPos = transform.position - center;
            //Vector3 endPos = target.transform.position - center;
            transform.position = Vector3.Slerp(transform.position, target.transform.position, Time.fixedDeltaTime * speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;
        collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        print(damage);
        Destroy(this.gameObject, 0.1f);
    }
}
