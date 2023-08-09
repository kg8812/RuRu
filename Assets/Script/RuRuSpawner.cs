using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RuRuSpawner : MonoBehaviour
{
    public RuRu[] prefab;
    public Transform[] spawnPos;
    public GameObject ruru;
    ObjectPool<RuRu> pool;

    RuRu current;

    Queue<RuRu> queue = new();

    private void Awake()
    {
        pool = new(prefab);

    }
    void Start()
    {                
        Spawn();
        GameManager.Instance.OnFall.AddListener(Spawn);
    }
    
    
    public void Spawn()
    {
        
        int x = Random.Range(0, spawnPos.Length);
        Vector2 pos = spawnPos[x].position;       

        current = pool.Get(prefab[0].name);
        current.GetComponent<SpriteRenderer>().sortingLayerName = "RuRu";
        current.GetComponent<SpriteRenderer>().sortingOrder = 1;
        current.transform.position = pos;
        current.transform.position += new Vector3(0, 0, 10);
        current.transform.rotation = Quaternion.identity;
        current.transform.SetParent(Camera.main.transform);
        if (x == 0) current.dir = -1;
        else current.dir = 1;
        GameManager.Instance.now = current.transform;
    }

    public void Enqueue(RuRu current)
    {
        queue.Enqueue(current);

        if (queue.Count > 5)
        {
            pool.Return(queue.Dequeue());
        }

        int count = 0;

        foreach(var x in queue)
        {
            x.GetComponent<SpriteRenderer>().sortingOrder = count++;
        }
    }
}
