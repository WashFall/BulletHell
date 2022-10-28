using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class Attack_MindFire : Attacks
{
    public List<GameObject> mindFires = new List<GameObject>();

    private CancellationTokenSource cancellationTokenSource;

    private GameObject playerObject;
    private Player playerClass;
    private bool canShoot = false;
    private GameObject mindFirePrefab;
    private float amountOfFires = 10;

    public Attack_MindFire(Player playerClass, GameObject playerObject, GameObject mindFirePrefab)
    {
        this.playerClass = playerClass;
        this.playerObject = playerObject;
        this.mindFirePrefab = mindFirePrefab;
        GenerateFirePool(amountOfFires);
        name = "Mind Fire";
        attackLevel = 0;
        baseAttackSpeed = 3;
        baseAttackRange = 1;
        baseDamage = 2;
        baseProjectileSize = 1;
        baseProjectileAmount = 1;
        playerClass.updateDelegate += Update;
        playerClass.disableDelegate += OnDisable;
        cancellationTokenSource = new CancellationTokenSource();
    }

    void OnDisable()
    {
        cancellationTokenSource.Cancel();
    }

    private void GenerateFirePool(float amount)
    {
        for(int i = 0; i < amount; i++)
        {
            GameObject fire = GameObject.Instantiate(
                mindFirePrefab, playerObject.transform.position, Quaternion.identity);
            mindFires.Add(fire);
            fire.GetComponent<MindFire>().playerObject = playerObject;
            fire.GetComponent<MindFire>().attacker = this;
            fire.SetActive(false);
        }
    }

    async void Update()
    {
        if (canShoot)
        {
            canShoot = false;
            await ShootMindFire();
        }
    }

    private async Task ShootMindFire()
    {
        float endTime = Time.time + attackSpeed;

        while (Time.time < endTime)
        {
            await Task.Yield();
        }

        if (!cancellationTokenSource.IsCancellationRequested)
        {
            for (int i = 0; i < projectileAmount; i++)
            {
                GameObject fire = mindFires.FirstOrDefault(fire => !fire.gameObject.activeSelf);

                if (fire is null)
                    fire = ExtraFireForPool();

                fire.transform.position = playerClass.transform.position;
                fire.SetActive(true);
                canShoot = true;
            }
        }
    }

    private GameObject ExtraFireForPool()
    {
        GameObject extraFire = GameObject.Instantiate(mindFirePrefab,
            playerObject.transform.position, Quaternion.identity);
        mindFires.Add(extraFire);
        extraFire.GetComponent<MindFire>().playerObject = playerObject;
        extraFire.GetComponent<MindFire>().attacker = this;
        extraFire.SetActive(false);
        return extraFire;
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
                baseProjectileSize *= 1.5f;
                playerClass.AssignStats();
                break;
            case 3:
                baseProjectileAmount++;
                playerClass.AssignStats();
                break;
        }
    }

    public override string GetUpgradeText(float level)
    {
        switch (level)
        {
            case 0:
                return ("Activate the Mind Fire power!");
            case 1:
                return ("Larger Fires");
            case 2:
                return ("Add one Fire.");
            case 3:
                return ("Add one Fire.");
        }
        return ("error lol");
    }
}
