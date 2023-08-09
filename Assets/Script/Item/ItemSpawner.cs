using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public Item[] items;
    public Transform spawnPos1;
    public Transform spawnPos2;

    Item curItem;
    void Start()
    {
        GameManager.Instance.OnLand.AddListener(Spawn);
        GameManager.Instance.OnEnding.AddListener(End);
    }

    void Spawn()
    {
        float rand = Random.Range(0f, 1f);

        if (rand < 0.3f && curItem == null)
        {
            float x = Random.Range(spawnPos1.position.x, spawnPos2.position.x);
            Item item = Instantiate(items[Random.Range(0, items.Length)]);
            item.transform.position = new Vector3(x, spawnPos1.position.y, 1);
            curItem = item;
        }
    }

    void End()
    {
        if(curItem!=null) Destroy(curItem.gameObject);
    }
}
