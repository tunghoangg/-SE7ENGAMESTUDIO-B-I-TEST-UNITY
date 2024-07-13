using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    
    public  Transform target;
    public float smoothTime = 0.3f;
    public Vector3 offset;
    private Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
    public void ChangeTargetTemporarily(Transform newTarget, float duration)
    {
        StartCoroutine(ChangeTargetCoroutine(newTarget, duration));
    }

    private IEnumerator ChangeTargetCoroutine(Transform newTarget, float duration)
    {
        Vector3 tempOffset = offset;
        offset.y = -2;
        offset.z = 2;
        Transform originalTarget = target;
        target = newTarget;
        yield return new WaitForSeconds(duration);
        target = originalTarget;
        offset = tempOffset;
    }
}
