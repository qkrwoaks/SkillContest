using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public float power;
    public float speed;

    public void Move(Vector2 dir)
    {
        transform.Translate(speed * Time.deltaTime * dir);
    }

    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Border")) Destroy(gameObject);    
    }
}
