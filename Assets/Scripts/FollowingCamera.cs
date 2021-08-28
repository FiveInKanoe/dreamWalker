using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    [SerializeField] private Entity target;


    void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 lerpPos = Vector3.Lerp
                (
                transform.position,
                target.transform.position,
                target.Speed * Time.deltaTime
                );
            transform.position = new Vector3(lerpPos.x, lerpPos.y, -2);
        }    
    }
}
