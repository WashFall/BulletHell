using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class MindGyro : MonoBehaviour
{
    public Attack_MindGyro attacker;
    public GameObject player;
    public bool canRotate = false;
    private float circleRadius;

    private Vector3 originalSize;
    private float rotationSpeed = 3;

    private Vector3 positionOffset;
    private float angle;

    private CancellationTokenSource cancellationTokenSource;

    void Awake()
    {
        originalSize = transform.localScale;
        cancellationTokenSource = new CancellationTokenSource();
    }

    void OnDestroy()
    {
        cancellationTokenSource.Cancel();
    }

    private void OnEnable()
    {
        angle = 0;
        circleRadius = GameManager.Instance.currentCharacter.characterPickUpRange;
        transform.localScale = originalSize * GameManager.Instance.currentCharacter.characterProjectileSize;
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
        await MoveGyroToPosition(positionOffset, true);
    }

    public async void CloseDown()
    {
        await MoveGyroToPosition(Vector3.zero, false);
        if(!cancellationTokenSource.IsCancellationRequested) gameObject.SetActive(false);
    }

    private async Task MoveGyroToPosition(Vector3 endPosition, bool isStarting)
    {
        float endTime = Time.time + 0.2f;
        if (!isStarting) canRotate = false;

        while (Time.time < endTime && !cancellationTokenSource.IsCancellationRequested)
        {
            transform.position = Vector3.Lerp(transform.position, 
                player.transform.position + endPosition, 10 * Time.deltaTime);
            await Task.Yield();
        }

        if (isStarting) canRotate = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;
        collision.gameObject.GetComponent<Enemy>().TakeDamage(attacker.damage);
    }
}
