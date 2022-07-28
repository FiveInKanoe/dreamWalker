using UnityEngine;

public class BlinkMarker : MonoBehaviour
{
    public Transform PlayerTransform { private get; set; }
    public float MaxRadius { private get; set; }

    private void FixedUpdate()
    {
       
        Vector3 playerPosition = PlayerTransform.position;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Debug.Log(playerPosition);

        float fullDistance = Vector3.Distance(playerPosition, mousePosition);

        if (fullDistance > MaxRadius)
        {
            float distanceRatio = MaxRadius / fullDistance;
            transform.position = new Vector3
            {
                x = Mathf.Lerp(playerPosition.x, mousePosition.x, distanceRatio),
                y = Mathf.Lerp(playerPosition.y, mousePosition.y, distanceRatio),
                z = playerPosition.z
            };
        }
        else
        {
            transform.position = new Vector3
            {
                x = mousePosition.x,
                y = mousePosition.y,
                z = playerPosition.z
            };
        }
    }
}
