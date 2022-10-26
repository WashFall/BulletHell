using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class LevelUpObserver : MonoBehaviour
{
    public GameObject levelUpPanel;
    public GameObject weaponImagePrefab;

    private Rect panelRect;
    private float panelWidth;
    private List<Attacks> attacks;

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
        for(int i = -1; i < 2; i++)
        {
            GameObject newImage = Instantiate(weaponImagePrefab, levelUpPanel.transform);
            newImage.transform.localPosition = new Vector3(0 - (panelWidth / 3) * i, 0, 0);
            AttackLevelCard thisCard = newImage.AddComponent<AttackLevelCard>();
            thisCard.attack = attacks[Random.Range(0, attacks.Count)];
            thisCard.levelUpObserver = this;
            thisCard.SetTexts();
        }
        Time.timeScale = 0f;
    }

    public void AttackLevelUp(string attackName)
    {
        switch (attackName)
        {
            case "MindMissile":
                GameManager.Instance.playerClass.attacks[0].AttackLevelUp();
                break;
            case "MindGyro":
                GameManager.Instance.playerClass.attacks[1].AttackLevelUp();
                break;
        }
    }
}
