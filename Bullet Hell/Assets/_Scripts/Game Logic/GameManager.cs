using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public List<GameObject> enemies = new List<GameObject>();
    public GameObject player;
    public float points;
    public float timer;
    public GameObject restartButton;
    private int health = 3;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        player.GetComponent<Player>().playerTakeDamage += TakeDamage;
    }

    private void FixedUpdate()
    {
        timer = Time.timeSinceLevelLoad;

        if(timer >= 60 || health <= 0)
        {
            Time.timeScale = 0;
            restartButton.SetActive(true);
        }
    }

    private void TakeDamage()
    {
        health--;
    }
}
