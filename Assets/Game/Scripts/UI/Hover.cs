using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    [Header("Settings")]
    public SpriteRenderer sr;
    public bool isEnabled = false;
    Player player;
    UIGameplayManager ugManager;

    private void Start()
    {
        player = Player.Get();
        ugManager = UIGameplayManager.Get();
    }

    private void OnMouseOver()
    {
        sr.color = new Color(1f, 1f, 1f, .4f);
    }

    private void OnMouseDown()
    {
        ugManager.disableBulletTime();
        player.Nova();
        string tag = gameObject.tag;

        switch (tag)
        {
            case "nova":
                player.Nova();
                break;
            case "missile":
                player.Missile();
                break;
            case "ray":
                player.Ray();
                break;
        }
    }

    private void OnMouseExit()
    {
        sr.color = new Color(1f, 1f, 1f, 0f);



    }
}
