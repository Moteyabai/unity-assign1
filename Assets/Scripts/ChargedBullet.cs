using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedBullet : MonoBehaviour
{

    private float life = 3f;
    Score score;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, life);
        score = FindObjectOfType<Score>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            score.ScoreNum += 1;
            score.Bonus += 1;
            score.MyScoreText.text = "DESTROYED: " + score.ScoreNum.ToString();
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Elite"))
        {
            score.ScoreNum += 3;
            score.Bonus += 3;
            score.MyScoreText.text = "DESTROYED: " + score.ScoreNum.ToString();
            Destroy(collision.gameObject);
        }
    }
}
