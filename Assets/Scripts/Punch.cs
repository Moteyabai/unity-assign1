using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Punch : MonoBehaviour
{
    private GameOver gameManager;
    private PlayerMoment playerMoment;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameOver>();
        playerMoment = FindObjectOfType<PlayerMoment>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.tag == "Player") )
        {
            playerMoment.TakeDmg(20);
            
        }
    }

}
