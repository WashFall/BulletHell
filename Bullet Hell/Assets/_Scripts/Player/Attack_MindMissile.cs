using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class Attack_MindMissile 
{
    public List<GameObject> mindMissiles = new List<GameObject>();

    private float range = 5;
    private Player playerClass;
    private float baseFireRate = 1;
    private float fireRate;
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
    }

    void GenerateMissilePool()
    {
        for(int i = 0; i < amountOfMissiles; i++)
        {
            GameObject missile = GameObject.Instantiate(mindMissilePrefab, 
                playerObject.transform.position, Quaternion.identity);
            mindMissiles.Add(missile);
            missile.SetActive(false);
        }
    }

    private async void Update()
    {
        if (canShoot)
        {
            canShoot = false;
            fireRate = baseFireRate * GameManager.Instance.currentCharacter.characterAttackSpeed;
            await FireMissile();
        }
    }

    private async Task FireMissile()
    {
        float startTime = Time.time;
        float currentTime = startTime;

        while(currentTime < startTime + fireRate)
        {
            currentTime = Time.time;
            await Task.Yield();
        }

        List<GameObject> closestEnemies = new List<GameObject>();
        closestEnemies = GameManager.Instance.enemies
            .OrderBy(enemy => (enemy.transform.position - playerObject.transform.position).sqrMagnitude).ToList();

        if(closestEnemies.Any())
        {
            if (Vector2.Distance(playerObject.transform.position, closestEnemies[0].transform.position) < range)
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
}
