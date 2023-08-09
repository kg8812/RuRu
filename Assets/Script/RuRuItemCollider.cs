using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuRuItemCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Item item))
        {
            item.Use();
            Destroy(item.gameObject);
        }        
    }
}
