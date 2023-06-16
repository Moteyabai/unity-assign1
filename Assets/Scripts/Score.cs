using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI MyScoreText;
    public int ScoreNum;
    public int Bonus;

    // Start is called before the first frame update
    void Start()
    {
        MyScoreText.text = "DESTROYED: "+ ScoreNum;
    }
}
