using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class Attack_MindMissile : Attacks
{
    public List<GameObject> mindMissiles = new List<GameObject>();

    private Player playerClass;
    private bool canShoot = true;
    private GameObject playerObject;
    private float amountOfMissiles = 30;
    private GameObject mindMissilePrefab;

    public Attack_MindMissile(Player playerClass, GameObject playerObject, GameObject mindMissile)
    {
        this.playerObject = playerObject;
        this.playerClass = playerClass;
        this.mindMissilePrefab = mindMissile;
        GenerateMissilePool();
        playerClass.updateDelegate += Update;
        baseAttackRange = 5;
        baseAttackSpeed = 1;
        baseDamage = 1;
        baseProjectileSize = 1;
        baseProjectileAmount = 1;
        name = "Mind Missile";
        attackLevel = 1;
    }

    void GenerateMissilePool()
    {
        for(int i = 0; i < amountOfMissiles; i++)
        {
            GameObject missile = GameObject.Instantiate(mindMissilePrefab, 
                playerObject.transform.position, Quaternion.identity);
            mindMissiles.Add(missile);
            missile.GetComponent<MindMissile>().attacker = this;
            missile.SetActive(false);
        }
    }

    private async void Update()
    {
        if (canShoot)
        {
            canShoot = false;
            await FireMissile();
        }
    }

    private async Task FireMissile()
    {
        float endTime = Time.time + attackSpeed;

        while (Time.time < endTime)
        {
            await Task.Yield();
        }

        List<GameObject> closestEnemies = new List<GameObject>();
        closestEnemies = GameManager.Instance.enemies
            .OrderBy(enemy => (enemy.transform.position - playerObject.transform.position).sqrMagnitude).ToList();

        if(closestEnemies.Any())
        {
            if (Vector2.Distance(playerObject.transform.position, closestEnemies[0].transform.position) < attackRange)
            {
                GameObject projectile = mindMissiles.FirstOrDefault(missile => !missile.gameObject.activeSelf);

                projectile ??= ExtraMissileForPool();

                projectile.transform.position = playerObject.transform.position;
                projectile.GetComponent<MindMissile>().target = closestEnemies[0];
                projectile.SetActive(true);
            }
        }
        canShoot = true;
    }

    private GameObject ExtraMissileForPool()
    {
        GameObject extraMissile = GameObject.Instantiate(mindMissilePrefab, 
            playerObject.transform.position, Quaternion.identity);
        mindMissiles.Add(extraMissile);
        return extraMissile;
    }
    public override void AttackLevelUp()
    {
        base.AttackLevelUp();
        switch (attackLevel)
        {
            case 1:
                canShoot = true;
                break;
            case 2:
                baseAttackSpeed = 0.9f;
                playerClass.AssignStats();
                break;
            case 3:
                projectileSize *= 1.5f;
                playerClass.AssignStats();
                break;
        }
    }

    public override string GetUpgradeText(float level)
    {
        switch (level)
        {
            case 0:
                return ("Activate the Mind Missile power!");
            case 1:
                return ("Higher attack speed.");
        }
        return ("error lol");
    }
}
