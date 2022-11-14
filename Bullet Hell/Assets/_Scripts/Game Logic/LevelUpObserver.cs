using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpObserver : MonoBehaviour
{
    public GameObject levelUpPanel;
    public GameObject weaponImagePrefab;

    private Rect panelRect;
    private float panelWidth;
    private List<Attacks> attacks;
    private List<Attacks> levelUpAttacks;

    private void Awake()
    {
        panelRect = levelUpPanel.GetComponent<RectTransform>().rect;
        panelWidth = panelRect.width;
        levelUpPanel.SetActive(false);
        attacks = GameManager.Instance.playerClass.attacks;
    }

    private void OnEnable()
    {
        GameManager.levelUp += LevelUp;
    }

    private void OnDisable()
    {
        GameManager.levelUp -= LevelUp;
    }

    private void LevelUp()
    {
        levelUpPanel.SetActive(true);
        levelUpAttacks = new List<Attacks>();
        for(int i = -1; i < 2; i++)
        {
            GameObject newImage = Instantiate(weaponImagePrefab, levelUpPanel.transform);
            newImage.transform.localPosition = new Vector3(0 - (panelWidth / 3) * i, 0, 0);
            AttackLevelCard thisCard = newImage.AddComponent<AttackLevelCard>();
            thisCard.attack = RandomAttack();
            levelUpAttacks.Add(thisCard.attack);
            thisCard.levelUpObserver = this;
            thisCard.SetTexts();
        }
        Time.timeScale = 0;
    }

    public void AttackLevelUp(string attackName)
    {
        switch (attackName)
        {
            case "Mind Missile":
                GameManager.Instance.playerClass.attacks[0].AttackLevelUp();
                levelUpPanel.SetActive(false);
                Time.timeScale = 1;
                break;
            case "Mind Gyro":
                GameManager.Instance.playerClass.attacks[1].AttackLevelUp();
                levelUpPanel.SetActive(false);
                Time.timeScale = 1;
                break;
            case "Mind Fire":
                GameManager.Instance.playerClass.attacks[2].AttackLevelUp();
                levelUpPanel.SetActive(false);
                Time.timeScale = 1;
                break;
        }
    }

    private Attacks RandomAttack()
    {
        Attacks randomAttack = attacks[Random.Range(0, attacks.Count)];
        if (levelUpAttacks.Contains(randomAttack))
        {
            List<Attacks> unusedAttacks = attacks.Except(levelUpAttacks).ToList();
            randomAttack = unusedAttacks[Random.Range(0, unusedAttacks.Count)];
        }
        return randomAttack;
    }
}
