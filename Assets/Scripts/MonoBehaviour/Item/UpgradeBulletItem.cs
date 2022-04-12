using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeBulletItem : Item
{
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.CompareTag("Player"))
        {
            UIManager.instance.ChangeToCurrentItem(new Color(1, 0, 0), "공격 업그레이드");
            collision.GetComponent<Player>().UpgradeBullet();
            Destroy(gameObject);
        }
    }
}
