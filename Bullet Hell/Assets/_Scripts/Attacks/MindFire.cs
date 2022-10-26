using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MindFire : MonoBehaviour
{
    public GameObject playerObject;
    public Attack_MindFire attacker;

    private Vector2 originalSize;

    void OnEnable()
    {
        if (attacker is not null)
            transform.localScale = originalSize * attacker.projectileSize;
    }

    void Update()
    {
        
    }
}
