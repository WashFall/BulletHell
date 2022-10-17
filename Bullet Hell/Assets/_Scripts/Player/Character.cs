using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "New Character", order = 51)]
public class Character : ScriptableObject
{
    //public Sprite characterSprite;
    //public Animator characterAnimator;
    public string characterName;
    public float characterHealth;
    public float characterWalkingSpeed;
    public float characterPickUpRange;
    public float characterAttackSpeed;
    public float characterAttackRange;
    public float characterBaseDamage;
    //public Attack characterStarterAttack;
}
