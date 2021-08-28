using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : Skill
{
    [SerializeField] private float maxRadius;
    [SerializeField] private Sprite markerSprite;
    [SerializeField] private Color markerColor;

    private GameObject blinkCont;
    private GameObject marker;

    private Entity entity;

    private bool isBlinkPerformed;
    private bool isKeyPressed;

    new void Start()
    {
        base.Start();
        isBlinkPerformed = false;
        isKeyPressed = false;
        entity = GetComponent<Entity>();
        blinkCont = new GameObject("Blink");
        blinkCont.transform.SetParent(skillContainer.transform);
    }

    public override void Usage()
    {
        if (marker == null)
        {
            initMarker();
        }  
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 entityPosition = entity.transform.position;

        float fullDistance = Vector2.Distance(entityPosition, mousePosition);

        if (fullDistance > maxRadius)
        {
            float distanceRatio = maxRadius / fullDistance;
            marker.transform.position = new Vector3
                (
                entityPosition.x + (mousePosition.x - entityPosition.x) * distanceRatio,
                entityPosition.y + (mousePosition.y - entityPosition.y) * distanceRatio,
                entity.transform.position.z
                );
        }
        else
        {
            marker.transform.position = new Vector3
                (
                mousePosition.x,
                mousePosition.y,
                entity.transform.position.z
                );
        }
        if (Input.GetMouseButton(0))
        {
            entity.transform.position = marker.transform.position;
            isBlinkPerformed = true;
            Destroy(marker);
            marker = null;
        }
        if (Input.GetMouseButton(1))
        {
            isKeyPressed = false;
            Destroy(marker);
            marker = null;
        }
    }

    private void initMarker()
    {
        marker = new GameObject("Marker");
        marker.transform.SetParent(blinkCont.transform);
        SpriteRenderer spriteRenderer = marker.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = markerSprite;
        spriteRenderer.color = markerColor;
        marker.transform.localScale = new Vector3(0.5f, 0.5f, 1);
    }

    void FixedUpdate()
    {
        if (Input.GetKey(HotKey) && Time.time > nextUsageTime)
        {
            isKeyPressed = true;
        }
        if (isKeyPressed && !isBlinkPerformed)
        {
            Usage();
            if (isBlinkPerformed)
            {
                isBlinkPerformed = false;
                nextUsageTime = Time.time + CoolDown;
                isKeyPressed = false;
            }           
        }
    }
}
