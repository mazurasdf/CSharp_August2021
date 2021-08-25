using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdjustScore : MonoBehaviour
{
    public static int ScoreNum = 0;
    public Text UIScore;
    // Start is called before the first frame update
    void Start()
    {
        UIScore = GetComponent<Text>();
        UIScore.text = "SCORE: 0";
    }

    // Update is called once per frame
    void Update()
    {
        UIScore.text = $"SCORE: {ScoreNum}";
    }

    public static void AddScore()
    {
        ++ScoreNum;
    }
}
