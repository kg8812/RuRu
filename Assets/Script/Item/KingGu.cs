using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingGu : Item
{
    protected override void SpawnArrow()
    {
        base.SpawnArrow();
        obj.GetComponent<SpriteRenderer>().color = new Color32(70, 126, 198, 255);
    }
    public override void Use()
    {
        GameManager.Instance.kingguObtained++;
    }
}
