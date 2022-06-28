using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    public GameObject sparkEffect;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Bullet")) {

            ContactPoint contactPoint = collision.contacts[0];
            // �浹 ���� ����
            Quaternion rotSpark = Quaternion.LookRotation(contactPoint.normal);
            // �浹ü�� �븻(��������) ���� �ݴ�� ȸ��

            GameObject spark = Instantiate(sparkEffect, contactPoint.point, rotSpark);
            spark.transform.SetParent(transform);

            Destroy(spark, .5f);
            Destroy(collision.gameObject);
        }
    }



}
