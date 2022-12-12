using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RefreshScore : MonoBehaviour
{

    private Text scoreBoard = null;

    // Start is called before the first frame update
    void Start()
    {
        scoreBoard = GameObject.Find("Hud/Score").GetComponent<Text>();
        scoreBoard.text = "Score: " + DontDestroy.score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
