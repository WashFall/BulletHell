using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Attack_MindGyro : Attacks
{
    public List<GameObject> mindGyros = new List<GameObject>();

    private CancellationTokenSource cancellationTokenSource;

    private GameObject playerObject;
    private Player playerClass;
    private GameObject mindGyroPrefab;
    private float amountOfGyros = 5;
    private bool canShoot = false;
    private float gyroDownTime;
    private float gyroDeploymentTime;

    public Attack_MindGyro(Player playerClass, GameObject playerObject, GameObject mindGyro)
    {
        this.playerObject = playerObject;
        this.playerClass = playerClass;
        this.mindGyroPrefab = mindGyro;
        GenerateGyroPool(amountOfGyros);
        playerClass.updateDelegate += Update;
        playerClass.disableDelegate += Disable;
        baseAttackRange = 5;
        baseAttackSpeed = 5;
        baseDamage = 0.6f;
        baseProjectileSize = 1;
        baseProjectileAmount = 1;
        gyroDownTime = 2;
        CalculateGyroDeployment(amountOfGyros);
        cancellationTokenSource = new CancellationTokenSource();
        name = "Mind Gyro";
    }

    private void CalculateGyroDeployment(float amount)
    {
        gyroDeploymentTime = 360 / amount * Mathf.Deg2Rad / (amount / (amount / 2) + 1);
    }

    private void GenerateGyroPool(float amount)
    {
        for(int i = 0; i < amount; i++)
        {
            GameObject gyro = GameObject.Instantiate(mindGyroPrefab, 
                playerObject.transform.position, Quaternion.identity);
            mindGyros.Add(gyro);
            gyro.GetComponent<MindGyro>().attacker = this;
            gyro.GetComponent<MindGyro>().player = playerObject;
            gyro.SetActive(false);
        }
    }

    private void Disable()
    {
        cancellationTokenSource.Cancel();
    }

    private async void Update()
    {
        if (canShoot)
        {
            canShoot = false;
            await BufferTimer(gyroDownTime);

            float checkIfNewGyros = amountOfGyros + projectileAmount - 1;
            if (checkIfNewGyros > mindGyros.Count) 
            {
                GenerateGyroPool(projectileAmount - 1);
                CalculateGyroDeployment(mindGyros.Count);
            }

            foreach(var gyro in mindGyros)
            {
                await StartGyros(gyro);
            }

            await BufferTimer(attackSpeed);

            foreach(var gyro in mindGyros)
            {
                await ResetGyros(gyro);
            }

            canShoot = true;
        }
    }

    private async Task StartGyros(GameObject gyro)
    {
        float endTime = Time.time + gyroDeploymentTime;

        while (Time.time < endTime)
        {
            await Task.Yield();
        }

        if (!cancellationTokenSource.IsCancellationRequested)
        {
            gyro.gameObject.SetActive(true);
            gyro.GetComponent<MindGyro>().ReadyUp();
        }
    }

    private async Task ResetGyros(GameObject gyro)
    {
        float endTime = Time.time + gyroDeploymentTime;

        while (Time.time < endTime)
        {
            await Task.Yield();
        }

        if (!cancellationTokenSource.IsCancellationRequested)
            gyro.GetComponent<MindGyro>().CloseDown();
    }

    private async Task BufferTimer(float bufferTime)
    {
        float endTime = Time.time + bufferTime;

        while (Time.time < endTime && !cancellationTokenSource.IsCancellationRequested)
        {
            await Task.Yield();
        }
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
                baseProjectileAmount++;
                playerClass.AssignStats();
                break;
            case 3:
                baseProjectileSize *= 1.5f;
                playerClass.AssignStats();
                break;
        }
    }

    public override string GetUpgradeText(float level)
    {
        switch (level)
        {
            case 0:
                return ("Activate the Mind Gyro power!");
            case 1:
                return ("Add one Gyro.");
            case 2:
                return ("Larger Gyros.");
            case 3:
                return ("Add one Gyro.");
        }
        return ("error lol");
    }
}
