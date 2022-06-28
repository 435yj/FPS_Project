using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public float rotateSpeed = 5f;

    


    /*
     public Transform targetTransfrom;
    private Transform cameraTransform;

    [Range(-1.0f, 20.0f)]
    public float distance = 10f;

    [Range(0.0f, 10.0f)]
    public float height = 2.0f;

    public float moveDamping = 15f;
    public float rotateDamping = 10f;

    public float targetOffset = 2f;

    void Start()
    {
        cameraTransform = GetComponent<Transform>();

    }

    void LateUpdate()
    {
        Vector3 pos = targetTransfrom.position + (-targetTransfrom.forward * distance) 
            + (targetTransfrom.up * height);

        cameraTransform.position = 
            Vector3.Slerp(cameraTransform.position, pos, moveDamping * Time.deltaTime);
        cameraTransform.rotation = 
            Quaternion.Slerp(cameraTransform.rotation, targetTransfrom.rotation, rotateDamping * Time.deltaTime);

        cameraTransform.LookAt(targetTransfrom.position + (targetTransfrom.up * targetOffset));
    }
     */
}
