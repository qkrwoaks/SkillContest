using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteCell : Cell
{
    public GameObject[] itemPrefabs;

    public override void OnDead()
    {
        Instantiate(itemPrefabs[Random.Range(0, itemPrefabs.Length)], transform.position, transform.rotation);
        base.OnDead();
    }
}
