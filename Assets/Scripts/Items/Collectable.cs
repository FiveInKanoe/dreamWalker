using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private Item itemInfo;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public Item ItemInfo { get => itemInfo; set => itemInfo = value; }

    void Start()
    {
        if (itemInfo != null)
        {
            transform.localScale = new Vector2(0.1f, 0.1f);
            spriteRenderer.sprite = itemInfo.Sprite;
        }       
    }

    void FixedUpdate()
    {
        
    }
}
