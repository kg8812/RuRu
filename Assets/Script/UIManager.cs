using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject[] lifes;
    int count;
    static UIManager instance;

    public Image time;

    public static UIManager Instance { get { return instance; } }

    public TextMeshProUGUI scoreText;
    int score = 0;
    public int Score { get { return score; } }
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        count = GameManager.Instance.life - 1;
        score = 0;
        
        foreach(var x in lifes)
        {
            x.SetActive(false);
        }
        for (int i = 0; i <= count; i++)
        {
            lifes[i].SetActive(true);
        }

        GameManager.Instance.OnFall.AddListener(MinusLife);
        GameManager.Instance.OnLifeAdd.AddListener(AddLife);
        GameManager.Instance.OnLand.AddListener(AddScore);
        GameManager.Instance.OnGameOver.AddListener(MinusLife);      
    }
    void AddLife()
    {
        if (count < 3)
        {
            count++;
            lifes[count].SetActive(true);
        }
    }

    void MinusLife()
    {
        if (count >= 0 && GameManager.Instance.CurTime > 0.1f)
        {
            lifes[count].SetActive(false);
            count--;
        }
    }

    void AddScore()
    {
        score++;
    }

    private void Update()
    {
        time.fillAmount = GameManager.Instance.CurTime / GameManager.Instance.MaxTime;
        scoreText.text = score.ToString();
    }
}
