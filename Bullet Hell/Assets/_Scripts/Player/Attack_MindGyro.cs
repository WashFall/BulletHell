using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Attack_MindGyro : Attacks
{
    public List<GameObject> mindGyros = new List<GameObject>();

    private GameObject playerObject;
    private Player playerClass;
    private GameObject mindGyroPrefab;
    private float amountOfGyros = 5;
    private bool canShoot = true;
    private float gyroDeploymentTime;

    public Attack_MindGyro(Player playerClass, GameObject playerObject, GameObject mindGyro)
    {
        this.playerObject = playerObject;
        this.playerClass = playerClass;
        this.mindGyroPrefab = mindGyro;
        GenerateGyroPool();
        playerClass.updateDelegate += Update;
        baseAttackRange = 5;
        baseAttackSpeed = 5;
        baseDamage = 0.6f;
        baseProjectileSize = 1;
        baseProjectileAmount = 1;
        gyroDeploymentTime = 1;
    }

    private void GenerateGyroPool()
    {
        for(int i = 0; i < amountOfGyros; i++)
        {
            GameObject gyro = GameObject.Instantiate(mindGyroPrefab, 
                playerObject.transform.position, Quaternion.identity);
            mindGyros.Add(gyro);
            gyro.GetComponent<MindGyro>().attacker = this;
            gyro.GetComponent<MindGyro>().player = playerObject;
            gyro.SetActive(false);
        }
    }

    private async void Update()
    {
        if (canShoot)
        {
            canShoot = false;
            foreach(var gyro in mindGyros)
            {
                await StartGyros(gyro);
            }
            //await ResetGyros();
        }
    }

    private async Task StartGyros(GameObject gyro)
    {
        float endTime = Time.time + gyroDeploymentTime;

        while (Time.time < endTime)
        {
            await Task.Yield();
        }

        gyro.gameObject.SetActive(true);
        gyro.GetComponent<MindGyro>().ReadyUp();
    }

    private async Task ResetGyros()
    {
        float startTime = Time.time;
        float currentTime = startTime;

        while (currentTime < startTime + attackSpeed)
        {
            currentTime = Time.time;
            await Task.Yield();
        }
    }
}
