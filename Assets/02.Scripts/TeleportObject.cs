using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportObject : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private Transform oneTransform;
    [SerializeField]
    private Transform twoTransform;
    [SerializeField]
    private Transform threeTransform;
    [SerializeField]
    private Transform fourTransform;
    [SerializeField]
    private Transform fiveTransform;
    [SerializeField]
    private Transform sixTransform;
    [SerializeField]
    private Transform sevenTransform;
    [SerializeField]
    private Transform eightTransform;


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet"))
        {
            if (transform.gameObject.name == "Move (1)")
            {
                playerTransform.position = twoTransform.position;
            }
            else if (transform.gameObject.name == "Move (2)")
            {
                playerTransform.position = oneTransform.position;
            }
            else if (transform.gameObject.name == "Move (3)")
            {
                playerTransform.position = fourTransform.position;
            }
            else if (transform.gameObject.name == "Move (4)")
            {
                playerTransform.position = threeTransform.position;
            }
            else if (transform.gameObject.name == "Move (5)")
            {
                playerTransform.position = sixTransform.position;
            }
            else if (transform.gameObject.name == "Move (6)")
            {
                playerTransform.position = fiveTransform.position;
            }
            else if (transform.gameObject.name == "Move (7)")
            {
                playerTransform.position = eightTransform.position;
            }
            else if (transform.gameObject.name == "Move (8)")
            {
                playerTransform.position = sevenTransform.position;
            }
        }
    }

}
