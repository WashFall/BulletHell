using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_MindMissile : MonoBehaviour
{
    public GameObject mindMissilePrefab;
    private GameObject playerObject;
    private bool canShoot = true;

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
        if(GameManager.Instance.enemies.Count > 0)
            Instantiate(mindMissilePrefab, playerObject.transform.position, Quaternion.identity);
        canShoot = true;
    }
}
