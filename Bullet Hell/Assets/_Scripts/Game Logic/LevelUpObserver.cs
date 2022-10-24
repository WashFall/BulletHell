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

    private void Awake()
    {
        panelRect = levelUpPanel.GetComponent<RectTransform>().rect;
        panelWidth = panelRect.width;
        levelUpPanel.SetActive(false);
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
        }
        Time.timeScale = 0f;
    }
}
