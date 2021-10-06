using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Blink", menuName = "Skills/Blink")]
public class Blink : Skills
{
    [SerializeField] private float maxRadius;
    [SerializeField] private Sprite markerSprite;
    [SerializeField] private Color markerColor;

    private GameObject blinkCont;
    private GameObject marker;

    private Entity entity;

    private bool isBlinkPerformed;
    private bool isKeyPressed;


    public override void Init(GameObject gameObject, GameObject skillContainer)
    {
        NextUsageTime = 0;
        this.SkillContainer = skillContainer;
        isBlinkPerformed = false;
        isKeyPressed = false;
        entity = gameObject.GetComponent<Entity>();
        blinkCont = new GameObject("Blink");
        blinkCont.transform.SetParent(this.SkillContainer.transform);
    }

    public override void Usage()
    {
        if (Input.GetKey(HotKey) && Time.time > NextUsageTime)
        {
            isKeyPressed = true;
        }
        if (isKeyPressed && !isBlinkPerformed)
        {
            Perform();
            if (isBlinkPerformed)
            {
                isBlinkPerformed = false;
                NextUsageTime = Time.time + CoolDown;
                isKeyPressed = false;
            }
        }
    }

    private void Perform()
    {
        if (marker == null)
        {
            InitMarker();
        }
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 entityPosition = entity.transform.position;

        float fullDistance = Vector2.Distance(entityPosition, mousePosition);

        if (fullDistance > maxRadius)
        {
            float distanceRatio = maxRadius / fullDistance;
            marker.transform.position = new Vector3
                (
                Mathf.Lerp(entityPosition.x, mousePosition.x, distanceRatio),
                Mathf.Lerp(entityPosition.y, mousePosition.y, distanceRatio),
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

    private void InitMarker()
    {
        marker = new GameObject("Marker");
        marker.transform.SetParent(blinkCont.transform);
        SpriteRenderer spriteRenderer = marker.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = markerSprite;
        spriteRenderer.color = markerColor;
        marker.transform.localScale = new Vector3(0.5f, 0.5f, 1);
    }

    

    
}
