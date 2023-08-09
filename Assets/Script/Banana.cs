using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : MonoBehaviour
{
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Head"))
        {
            rb.velocity = Vector3.zero;
            Destroy(rb);
            gameObject.layer = LayerMask.NameToLayer("Head");
            StartCoroutine(GameManager.Instance.MoveCamera(false, transform.position - Vector3.up));
            foreach (var x in collision.transform.parent.GetComponentsInChildren<Collider2D>())
            {
                x.enabled = false;
            }
        }
    }
}
