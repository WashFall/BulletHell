using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float health = 3;
    private bool canTakeDamage = true;
    private SpriteRenderer spriteRenderer;
    private Color spriteColor;

    void Start()
    {
        GameManager.Instance.playerObject = this.gameObject;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteColor = spriteRenderer.color;
    }

    private async void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && canTakeDamage)
        {
            StartCoroutine(nameof(TakeDamage));
            canTakeDamage = false;
            await InvincibilityTime();
        }
    }

    private IEnumerator TakeDamage()
    {
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = spriteColor;
        health--;
    }

    private async Task InvincibilityTime()
    {
        float startTime = Time.time;
        float currentTime = startTime;

        while(currentTime < startTime + 1)
        {
            currentTime = Time.time;
            await Task.Yield();
        }
        canTakeDamage = true; 
    }
}
