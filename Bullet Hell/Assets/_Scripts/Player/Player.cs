using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class Player : MonoBehaviour
{
    public delegate void UpdateDelegate();
    public UpdateDelegate updateDelegate;

    public float health = 3;
    public GameObject mindMissilePrefab;

    private Color spriteColor;
    private Character characterStats;
    private bool canTakeDamage = true;
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D pickUpTrigger;

    private List<Attacks> attacks = new List<Attacks>();
    private Attack_MindMissile mindMissile;

    void Start()
    {
        characterStats = GameManager.Instance?.currentCharacter;
        GameManager.Instance.playerObject = this.gameObject;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteColor = spriteRenderer.color;
        List<CircleCollider2D> colliders = GetComponents<CircleCollider2D>().ToList();
        pickUpTrigger = colliders.Where(c => c.isTrigger).Single();
        pickUpTrigger.radius = characterStats.characterPickUpRange;
        mindMissile = new Attack_MindMissile(this, gameObject, mindMissilePrefab);
        attacks.Add(mindMissile);
        if (characterStats is not null) AssignStats();
    }

    private void Update()
    {
        updateDelegate?.Invoke();
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

    private void AssignStats()
    {
        foreach(var attack in attacks)
        {
            attack.attackSpeed = attack.baseAttackSpeed * characterStats.characterAttackSpeed;
            attack.attackRange = attack.baseAttackRange * characterStats.characterAttackRange;
            attack.damage = attack.baseDamage * characterStats.characterBaseDamage;
            attack.projectileSize = attack.baseProjectileSize * characterStats.characterProjectileSize;
            attack.projectileAmount = attack.baseProjectileAmount * characterStats.characterProjectileAmount;
        }
    }
}
