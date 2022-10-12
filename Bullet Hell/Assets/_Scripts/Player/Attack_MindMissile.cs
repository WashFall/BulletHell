using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Attack_MindMissile : MonoBehaviour
{
    public GameObject mindMissilePrefab;
    private GameObject playerObject;
    private bool canShoot = true;
    private float range = 5;
    private float amountOfMissiles = 30;
    public List<GameObject> mindMissiles = new List<GameObject>();
    private float fireRate = 1;

    void Start()
    {
        playerObject = this.gameObject;
        for(int i = 0; i < amountOfMissiles; i++)
        {
            GameObject missile = Instantiate(mindMissilePrefab, transform.position, Quaternion.identity);
            mindMissiles.Add(missile);
            missile.SetActive(false);
        }
    }

    private void Update()
    {
        if (canShoot)
        {
            canShoot = false;
            fireRate = 0.01f;
            StartCoroutine(nameof(FireMissile));
        }
    }

    private IEnumerator FireMissile()
    {
        yield return new WaitForSeconds(fireRate);
        List<GameObject> closestEnemies = new List<GameObject>();
        closestEnemies = GameManager.Instance.enemies
            .OrderBy(enemy => (enemy.transform.position - transform.position).sqrMagnitude).ToList();

        if(closestEnemies.Any())
        {
            if (Vector2.Distance(transform.position, closestEnemies[0].transform.position) < range)
            {
                GameObject projectile = mindMissiles.FirstOrDefault(missile => !missile.gameObject.activeSelf);

                projectile ??= ExtraMissileForPool();

                projectile.transform.position = transform.position;
                projectile.GetComponent<MindMissile>().target = closestEnemies[0];
                projectile.SetActive(true);
            }
        }
        canShoot = true;
    }

    private GameObject ExtraMissileForPool()
    {
        GameObject extraMissile = Instantiate(mindMissilePrefab, transform.position, Quaternion.identity);
        mindMissiles.Add(extraMissile);
        return extraMissile;
    }
}
