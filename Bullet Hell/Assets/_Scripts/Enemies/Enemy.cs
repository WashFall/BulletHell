using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int health = 2;
    private SpriteRenderer spriteRenderer;
    private Color spriteColor;
    private bool canDie;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteColor = spriteRenderer.color;
        GameManager.Instance?.enemies.Add(this.gameObject);
    }

    private void OnDestroy()
    {
        GameManager.Instance?.enemies.Remove(this.gameObject);
    }

    public void TakeDamage()
    {
        health--;
        StartCoroutine(nameof(DamageFlash));
    }

    private IEnumerator DamageFlash()
    {
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        if (health <= 0) canDie = true;
        spriteRenderer.color = spriteColor;
    }

    private void Update()
    {
        if(canDie) Destroy(gameObject);
    }
}
