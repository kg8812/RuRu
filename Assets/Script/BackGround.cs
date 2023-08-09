using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public GameObject bg1;
    public GameObject bg2;

    Sprite sprite { get { return bg1.GetComponent<SpriteRenderer>().sprite; } }
    Camera cam { get { return Camera.main; } }

    void Update()
    {
        if(cam.transform.position.y > bg2.transform.position.y)
        {
            bg1.transform.position = bg2.transform.position + Vector3.up * sprite.bounds.size.y;
            Swap();
        }
    }

    void Swap()
    {
        (bg2, bg1) = (bg1, bg2);
    }
}
