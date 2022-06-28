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
            // 충돌 지점 정보
            Quaternion rotSpark = Quaternion.LookRotation(contactPoint.normal);
            // 충돌체의 노말(법선벡터) 방향 반대로 회전

            GameObject spark = Instantiate(sparkEffect, contactPoint.point, rotSpark);
            spark.transform.SetParent(transform);

            Destroy(spark, .5f);
            Destroy(collision.gameObject);
        }
    }



}
