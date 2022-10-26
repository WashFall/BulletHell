using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AttackLevelCard : MonoBehaviour
{
    GameObject attackNameText, attackLevelText, attackUpgradeText;
    public Attacks attack;
    public LevelUpObserver levelUpObserver;

    private void Awake()
    {
        attackNameText = transform.GetChild(0).gameObject;
        attackLevelText = transform.GetChild(1).gameObject;
        attackUpgradeText = transform.GetChild(2).gameObject;
    }

    public void SetTexts()
    {
        attackNameText.GetComponent<TMP_Text>().text = attack.name;
        attackLevelText.GetComponent<TMP_Text>().text = (attack.attackLevel + 1).ToString();
        attackUpgradeText.GetComponent<TMP_Text>().text = attack.GetUpgradeText(attack.attackLevel);
    }
    void Update()
    {

    }
}
