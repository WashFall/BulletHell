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

    void Start()
    {
        playerObject = this.gameObject;
        StartCoroutine(nameof(FireMissile));
    }

    private void Update()
    {
        if (canShoot)
        {
            canShoot = false;
            StartCoroutine(nameof(FireMissile));
        }
    }

    private IEnumerator FireMissile()
    {
        yield return new WaitForSeconds(1);
        List<GameObject> closestEnemies = new List<GameObject>();
        closestEnemies = GameManager.Instance.enemies
            .OrderBy(enemy => (enemy.transform.position - transform.position).sqrMagnitude).ToList();

        if(closestEnemies.Any())
        {
            if (Vector2.Distance(transform.position, closestEnemies[0].transform.position) < range)
            {
                GameObject projectile = Instantiate(mindMissilePrefab, playerObject.transform.position, Quaternion.identity);
                projectile.GetComponent<MindMissile>().target = closestEnemies[0];
            }
        }
        canShoot = true;
    }
}
