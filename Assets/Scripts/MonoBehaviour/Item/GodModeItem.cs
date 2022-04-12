using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodModeItem : Item
{
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.CompareTag("Player"))
        {
            UIManager.instance.ChangeToCurrentItem(new Color(1, 0.6f, 0), "일시 무적");
            collision.GetComponent<Player>().ItemGodMode();
            Destroy(gameObject);
        }
    }

}
