using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private float life = 3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        Destroy(gameObject, life);
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(collision.gameObject);
        Destroy(gameObject);
    }
}
