using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private float life = 3f;
    Score score;
    Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, life);
        score = FindObjectOfType<Score>();
        enemy = FindObjectOfType<Enemy>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
            score.ScoreNum += 1;
            score.Bonus += 1;
            score.MyScoreText.text = "DESTROYED: " + score.ScoreNum.ToString();
        }
        if(collision.CompareTag("Elite"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
            score.ScoreNum += 3;
            score.Bonus += 3;
            score.MyScoreText.text = "DESTROYED: " + score.ScoreNum.ToString();
            
        }
    }
}
