using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nyopa : Item
{
    protected override void SpawnArrow()
    {
        base.SpawnArrow();
        obj.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 128, 255);
    }
    public override void Use()
    {
        GameManager.Instance.AddLife();
    }
}
