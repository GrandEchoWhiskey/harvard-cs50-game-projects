using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DespawnOnHeight : MonoBehaviour
{
    private GameObject player = null;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("FPSController");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y < 0)
        {
            SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
        }
    }
}
