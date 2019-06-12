using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBlurFollowPlayer : MonoBehaviour
{

    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Follow();
    }

    void Follow()
    {
        transform.position = player.transform.position;
    }
}
