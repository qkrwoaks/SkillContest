using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public float speed;
    public int score;

    private void FixedUpdate()
    {
        transform.Translate(speed * Time.deltaTime * Vector2.down);    
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Border")) Destroy(gameObject);
        if (collision.CompareTag("Player")) GameManager.instance.AppendToScore(score);
    }
}
