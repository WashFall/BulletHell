using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public delegate void LevelUp();
    public static LevelUp levelUp;

    public float level = 0;
    public float nextLevelAmount = 5;

    public float timer;
    public float points;
    public GameObject playerObject;
    public GameObject playerObjectPrefab;
    public GameObject restartButton;
    public GameObject playerCamera;
    public List<Character> characterList = new List<Character>();
    public Character currentCharacter;
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

        characterList = Resources.LoadAll<Character>("Characters").ToList();
        currentCharacter = Instantiate(characterList[0]);
        playerObject = Instantiate(playerObjectPrefab, Vector3.zero, Quaternion.identity);
        playerClass = playerObject.GetComponent<Player>();
        playerCamera.GetComponent<CinemachineVirtualCamera>().Follow = playerObject.transform;
    }

    private void FixedUpdate()
    {
        timer = Time.timeSinceLevelLoad;

        if(points >= nextLevelAmount)
        {
            level++;
            nextLevelAmount += 10;
            levelUp?.Invoke();
        }

        if(timer > 60 || playerClass.health <= 0)
        {
            Time.timeScale = 0;
            restartButton.SetActive(true);
        }
    }
}
