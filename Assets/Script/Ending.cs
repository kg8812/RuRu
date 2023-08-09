using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ending : MonoBehaviour
{   
    public UnityEvent OnEnding = new();
    public GameObject Clear;

    private void OnEnable()
    {
        OnEnding.Invoke();
        GameManager.Instance.IsGameOver = true;
        GameManager.Instance.isStop = true;
        Destroy(GameManager.Instance.now.gameObject);
    }
    
    public void OpenClear()
    {
        Clear.SetActive(true);

    }
}
