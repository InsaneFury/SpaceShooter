using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [Header("Settings")]
    public float resistance = 10f;
    float tempResistance;

    private void OnEnable()
    {
        tempResistance = resistance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BadLaser") && tempResistance > 0f)
        {
            tempResistance--;
        }
        else if(tempResistance <= 0f)
        {
            gameObject.SetActive(false);
        }
    }
}
