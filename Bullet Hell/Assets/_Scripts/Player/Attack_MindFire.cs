using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Attack_MindFire : Attacks
{
    public List<GameObject> mindFires = new List<GameObject>();

    private CancellationTokenSource cancellationTokenSource;

    private GameObject playerObject;
    private Player playerClass;
    private bool canShoot = false;
    private GameObject mindFirePrefab;

    public Attack_MindFire(Player playerClass, GameObject playerObject, GameObject mindFirePrefab)
    {
        this.playerClass = playerClass;
        this.playerObject = playerObject;
        this.mindFirePrefab = mindFirePrefab;
        name = "Mind Fire";

        baseAttackSpeed = 1;
        baseAttackRange = 1;
        baseDamage = 2;
        baseProjectileSize = 1;
        baseProjectileAmount = 1;
    }

    void Update()
    {
        if (canShoot)
        {

        }
    }
}
