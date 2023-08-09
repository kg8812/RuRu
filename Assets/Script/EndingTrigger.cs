using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EndingTrigger : MonoBehaviour
{
    public Ending ending;
    bool isTriggered;

    IEnumerator MoveCam()
    {
        GameManager.Instance.OnEnding.Invoke();

        yield return new WaitForSeconds(2);
        float moveTime = 1f;
        float time = 0;

        Vector2 startPos = Camera.main.transform.position;
        Vector2 endPos = ending.transform.position;

        while (time < moveTime)
        {
            Camera.main.transform.position = Vector2.Lerp(startPos, endPos, time / moveTime);
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        ending.gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Ãæµ¹");

        GameManager.Instance.OnLand.RemoveAllListeners();
        GameManager.Instance.OnLand.AddListener(() => StartCoroutine(MoveCam()));

    }    
}
