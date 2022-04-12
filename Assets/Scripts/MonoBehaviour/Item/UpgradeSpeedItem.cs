using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSpeedItem : Item
{
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.CompareTag("Player"))
        {
            UIManager.instance.ChangeToCurrentItem(new Color(0, 0, 1), "�ӵ� ���׷��̵�");
            collision.GetComponent<Player>().UpgradeSpeed();
            Destroy(gameObject);
        }
    }
}
