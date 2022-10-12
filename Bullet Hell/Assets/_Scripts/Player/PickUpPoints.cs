using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPoints : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Point")) return;

        collision.GetComponent<Points>().isInRange = true;
    }
}
