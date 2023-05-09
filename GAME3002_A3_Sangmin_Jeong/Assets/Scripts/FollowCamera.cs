using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform targetObject;
    public float followSpeed = 5f;
    public Vector3 offset;

    private void LateUpdate()
    {
        // Fixed Camera movement
        Vector3 desiredPosition = targetObject.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        transform.LookAt(targetObject);
    }
}
