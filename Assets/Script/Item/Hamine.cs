using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hamine : Item
{
    protected override void SpawnArrow()
    {
        base.SpawnArrow();
        obj.GetComponent<SpriteRenderer>().color = new Color32(138, 43, 226, 255);
    }
    public override void Use()
    {
        GameManager.Instance.CurTime += 20;
    } 
}
