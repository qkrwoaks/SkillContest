using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : Monster
{
    private void Start()
    {
        Init();
        StartCoroutine(Fire());
    }

    private void FixedUpdate()
    {
        Move();
    }
    public override void OnDamage(float damage)
    {
        base.OnDamage(damage);
    }

    private void OnDestroy()
    {
        for (int i = 0; i < 360; i += 36)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, i)));
            bullet.GetComponent<MonsterBullet>().power = power * 2;
            bullet.GetComponent<MonsterBullet>().speed = speed + 2f;
        }
    }
}
