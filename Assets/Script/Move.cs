using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    Vector3 dir;

    public void Init(Vector3 dir)
    {
        this.dir = dir.normalized;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(5 * Time.deltaTime * dir);
    }
}
