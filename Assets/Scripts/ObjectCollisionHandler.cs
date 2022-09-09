using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ObjectCollisionHandler : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private int backgroundID;
    private int foregroundID;

    private void Start()
    {
        backgroundID = SortingLayer.NameToID("Object");
        foregroundID = SortingLayer.NameToID("Foreground");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {   
        Entity entity = other.transform.GetComponent<Entity>();
        if (entity != null)
        {
            spriteRenderer.sortingLayerID = foregroundID;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Entity entity = other.transform.GetComponent<Entity>();
        if (entity != null)
        {
            spriteRenderer.sortingLayerID = backgroundID;
        }
    }  

    


}
