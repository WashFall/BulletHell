using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float timer;
    public float points;
    public GameObject playerObject;
    public GameObject restartButton;
    public List<GameObject> enemies = new List<GameObject>();

    private Color spriteColor;
    private Player playerClass;
    private SpriteRenderer playerSpriteRenderer;

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
    }

    private void Start()
    {
        playerClass = playerObject.GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        timer = Time.timeSinceLevelLoad;

        if(timer > 60 || playerClass.health <= 0)
        {
            Time.timeScale = 0;
            restartButton.SetActive(true);
        }
    }
}
