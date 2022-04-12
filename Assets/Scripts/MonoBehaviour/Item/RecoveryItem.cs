using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryItem : Item
{
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.CompareTag("Player"))
        {
            UIManager.instance.ChangeToCurrentItem(new Color(1, 1, 0), "체력 회복");
            collision.GetComponent<Player>().Recovery();
            Destroy(gameObject);
        }
    }
}
