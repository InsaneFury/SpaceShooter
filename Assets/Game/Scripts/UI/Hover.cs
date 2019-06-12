using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    SpriteRenderer sr;
    Player player;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        player = Player.Get();
    }

    private void OnMouseOver()
    {
        sr.color = new Color(1f, 1f, 1f, .4f);
    }

    private void OnMouseUp()
    {
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
