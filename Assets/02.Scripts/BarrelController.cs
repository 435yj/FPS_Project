using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelController : MonoBehaviour
{ 
    public GameObject expEffect;
    // 이펙트
    public Texture[] textures;
    // 무작위로 사용 할 텍스처 배열
    public float radius = 10f;

    [SerializeField]
    private GameObject heartOnOff;

    private int hitCount = 0;
    private float force = 1500f;

    private Rigidbody barrelRigidBody;
    private Transform barrelTransform;

    private new MeshRenderer renderer;

    void Start()
    {
        barrelRigidBody = GetComponent<Rigidbody>();
        barrelTransform = GetComponent<Transform>();

        renderer = GetComponentInChildren<MeshRenderer>();
        // 하이라이키창에서의 자식의 MeshRenderer를 가져옴

        int idx = Random.Range(0, textures.Length);
        renderer.material.mainTexture = textures[idx];
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Punch")) { 
            hitCount++;
            if(hitCount >= 15)
            {
                ExpBarrel();
                heartOnOff.transform.GetChild(GameManager.Instance().barrelCount).gameObject.SetActive(false);
                GameManager.Instance().barrelCount++;

                Debug.Log(GameManager.Instance().barrelCount);

                if (GameManager.Instance().barrelCount >= 6)
                {
                    GameManager.Instance().IsGameOver = true;
                    Debug.Log("게임오버");
                }
            }
        }
    }

    void ExpBarrel()
    {
        GameObject exp = Instantiate(expEffect, barrelTransform.position, barrelTransform.rotation);

        Destroy(exp, 5f);

        //barrelRigidBody.mass = 1f;
        //barrelRigidBody.AddForce(Vector3.up * force);

        IndirectDamage(barrelTransform.position);

        transform.gameObject.SetActive(false);
    }

    Collider[] colls = new Collider[10];

    void IndirectDamage(Vector3 pos)
    {
        //Collider[] colls = Physics.OverlapSphere(pos, radius, 1 << 3);

        Physics.OverlapSphereNonAlloc(pos, radius, colls, 1 << 3);

        foreach(var col in colls)
        {
            if(col == null)
            {
                continue;
            }

            Rigidbody rb = col.GetComponent<Rigidbody>();

            rb.mass = 1f;
            rb.constraints = RigidbodyConstraints.None;
            //freezerotation 제한값 해제

            rb.AddExplosionForce(force, pos, radius, 100.0f);
        }
    }
}
