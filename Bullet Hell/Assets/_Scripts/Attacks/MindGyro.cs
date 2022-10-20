using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MindGyro : MonoBehaviour
{
    public Attack_MindGyro attacker;
    public GameObject player;
    public bool canRotate = false;
    public float circleRadius = 2;

    private Vector3 originalSize;
    private float rotationSpeed = 3;

    private Vector3 positionOffset;
    private float angle;

    void Start()
    {
        originalSize = transform.localScale;
    }

    private void OnEnable()
    {
        angle = 0;
        //transform.localScale = originalSize * GameManager.Instance.currentCharacter.characterProjectileSize;
        transform.position = player.transform.localPosition;
    }

    void LateUpdate()
    {
        if (canRotate)
        {
            positionOffset.Set(Mathf.Sin(angle) * circleRadius, Mathf.Cos(angle) * circleRadius, 0);
            transform.position = player.transform.position + positionOffset;
            angle += Time.deltaTime * rotationSpeed;
        }
    }

    public async void ReadyUp()
    {
        positionOffset.Set(Mathf.Sin(angle) * circleRadius, Mathf.Cos(angle) * circleRadius, 0);
        await MoveGyroToPosition(positionOffset);
    }

    private async Task MoveGyroToPosition(Vector3 startPosition)
    {
        float endTime = Time.time + 0.2f;

        while (Time.time < endTime)
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position + startPosition, 10 * Time.deltaTime);
            await Task.Yield();
        }
        canRotate = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;
        collision.gameObject.GetComponent<Enemy>().TakeDamage(1);
    }
}
