using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    RuRuSpawner spawner;
    public RuRuSpawner Spawner { get { return spawner; } }

    public static GameManager Instance { get { return instance; } }

    public UnityEvent OnJump = new();
    public UnityEvent OnLand = new();
    public UnityEvent OnGameOver = new();

    [HideInInspector] public Transform now;
    public Transform last;
    public Transform floor;

    public UnityEvent OnFall = new();
    public UnityEvent OnLifeAdd = new();
    public UnityEvent OnEnding = new();

    public int life;

    [SerializeField] float maxTime;

    public bool isStop = false;
    public float MaxTime { get { return maxTime; } }
    float curTime;
    public float CurTime
    {
        get { return curTime; }
        set
        {
            if (value > maxTime) curTime = maxTime;
            else if (value < 0) curTime = 0;
            else curTime = value;
        }
    }

    bool isGameover;
    public bool IsGameOver { get { return isGameover; } set { isGameover = value; } }

    [HideInInspector] public int kingguObtained;

    public GameObject ExitWindow;

    float speed = 10;
    public float Speed { get { return speed; } }

    public GameObject speedUp;

    public Image timerImage;


    private void Awake()
    {
        Application.runInBackground = true;

        Time.timeScale = 1;
        instance = this;
        kingguObtained = 0;
        life = 3;
        spawner = gameObject.GetComponent<RuRuSpawner>();
        OnLand.AddListener(() => StartCoroutine(MoveCamera(true, now.position)));
        curTime = maxTime;
        OnLand.AddListener(() =>
        {
            int score = UIManager.Instance.Score;

            if (score % 10 == 0 && score / 10 != 0 && speed < 15)
            {
                speed += 0.5f;
                speedUp.SetActive(true);
            }
           
        });
    }

    private void Update()
    {
        if (curTime > 0 && !isStop)
        {
            curTime -= Time.deltaTime;
        }
        else if (curTime <= 0 && !isGameover)
        {
            GameOver();
        }

        if ((!IsGameOver || ExitWindow.activeSelf) && Input.GetKeyDown(KeyCode.Escape))
        {
            ExitWindow.SetActive(!ExitWindow.activeSelf);
            if (ExitWindow.activeSelf)
            {
                Pause();
            }
            else
            {
                UnPause();
            }
        }
    }
   
    Coroutine cr;
    public void Stop(float time)
    {
        if (cr != null)
        {
            StopCoroutine(cr);
        }
        cr = StartCoroutine(StopForTime(time));
    }

    IEnumerator StopForTime(float time)
    {
        isStop = true;
        timerImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(time);
        timerImage.gameObject.SetActive(false);

        isStop = false;
    }
    public void GameOver()
    {
        OnGameOver.Invoke();
        isGameover = true;
        isStop = true;
        Time.timeScale = 0;
        Destroy(now.gameObject);
    }

    public void Pause()
    {
        isStop = true;
        isGameover = true;
        Time.timeScale = 0;
    }

    public void UnPause()
    {
        isStop = false;
        isGameover = false;
        Time.timeScale = 1;
    }

    public void Fall()
    {
        life--;


        if (life <= 0)
        {
            GameOver();
        }
        else
        {
            OnFall.Invoke();
        }
    }

    public void AddLife()
    {
        if (life < 4)
        {
            life++;
            OnLifeAdd.Invoke();
        }
    }
    public IEnumerator MoveCamera(bool spawn, Vector3 pos)
    {
        float moveTime = 0.2f;
        float time = 0;

        float p = 0;

        if (pos.y + 1.5f > Camera.main.transform.position.y)
        {
            p = pos.y - Camera.main.transform.position.y + 1.5f;
        }

        Vector2 startPos = Camera.main.transform.position;
        Vector2 endPos = Camera.main.transform.position + Vector3.up * p;

        while (time < moveTime)
        {
            Camera.main.transform.position = Vector2.Lerp(startPos, endPos, time / moveTime);
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        if (spawn)
        {
            spawner.Spawn();
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        life = 3;
        Time.timeScale = 1;
    }

    public void QuitGame()
    {

        SceneManager.LoadScene("Main Screen");
    }
}
