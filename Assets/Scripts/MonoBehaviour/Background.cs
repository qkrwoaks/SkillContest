using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    Color originColor;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originColor = spriteRenderer.color;
        StartCoroutine(ChangeSprite());
    }

    IEnumerator ChangeSprite()
    {
        while (true)
        {
            if (GameManager.instance.stageStep == 2) spriteRenderer.color = Color.red;
            else spriteRenderer.color = originColor;
            yield return new WaitForSeconds(0.1f);
        }
    }

    //private void Update()
    //{
    //    if (GameManager.instance.stageStep == 2) spriteRenderer.color = Color.red;
    //    else spriteRenderer.color = originColor;
    //}

}
