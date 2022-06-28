using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float force = 1500.0f;

    private Rigidbody bulletRigidbody;
    private Transform bulletTransfrom;

    private void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
        bulletTransfrom = GetComponent<Transform>();

        bulletRigidbody.AddForce(bulletTransfrom.forward * force);
    }
}
