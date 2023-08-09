using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public GameObject arrow;
    protected GameObject obj;

    private void Start()
    {
        SpawnArrow();
    }
    protected virtual void SpawnArrow()
    {
        obj = Instantiate(arrow);
        obj.transform.SetParent(Camera.main.transform);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.position = new Vector3(transform.position.x, obj.transform.position.y, 2);
        obj.transform.position += Vector3.up * 5;       
    }
    public abstract void Use();   
   
    private void Update()
    {
        if(transform.position.y < GameManager.Instance.floor.position.y)
        {
            Destroy(gameObject);
        }    
        
        if(obj!=null && transform.position.y < obj.transform.position.y)
        {
            Destroy(obj);
        }
    }

    private void OnDestroy()
    {
        if (obj != null) Destroy(obj);
    }
}
