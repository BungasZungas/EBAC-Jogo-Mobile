using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollactableCoin : ItemCollactableBase
{
    public Collider2D collider;

    protected override void OnCollect()
    {
        base.OnCollect();
        collider.enabled = false;
        ItemManager.Instance.AddCoins();
    }
}
