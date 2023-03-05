using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scoreScript : MonoBehaviour
{
    public TMP_Text scoreText = null;

    // Start is called before the first frame update
    void Start()
    {
        int score = PlayerPrefs.GetInt("Score");
        scoreText.text = "Your score: " + score.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
