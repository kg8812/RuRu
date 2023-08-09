using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KongChan : Item
{
    [SerializeField] float stopTime;

    protected override void SpawnArrow()
    {
        base.SpawnArrow();
        obj.GetComponent<SpriteRenderer>().color = new Color32(133, 172, 32, 255);
    }
    public override void Use()
    {
        GameManager.Instance.Stop(stopTime);
    }
}
