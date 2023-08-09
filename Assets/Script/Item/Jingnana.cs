using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jingnana : Item
{
    public GameObject banana;

    protected override void SpawnArrow()
    {
        base.SpawnArrow();
        obj.GetComponent<SpriteRenderer>().color = new Color32(240, 169, 87, 255);
    }
    public override void Use()
    {
        GameObject obj = Instantiate(banana);

        obj.transform.position = GameManager.Instance.last.position + Vector3.up * 1.5f;
        GameManager.Instance.last = transform;
    }
}
