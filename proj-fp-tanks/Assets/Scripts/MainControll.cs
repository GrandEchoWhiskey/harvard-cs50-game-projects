using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainControll : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainScene");
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }

    }

    public void ChangePlayScene()
    {
        SceneManager.LoadScene("PlayScene");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ChangeEndScene()
    {
        SceneManager.LoadScene("EndScene");
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

}