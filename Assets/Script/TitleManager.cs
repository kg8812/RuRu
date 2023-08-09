using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public GameObject title;
    public GameObject Exit;

    public void StartStory()
    {
        SceneManager.LoadScene(1);
    }

    public void StartInfinity()
    {
        SceneManager.LoadScene(2);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Exit.SetActive(!Exit.activeSelf);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
