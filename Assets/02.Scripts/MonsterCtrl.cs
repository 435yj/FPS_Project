using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterCtrl : MonoBehaviour
{
    public enum MonsterState
    {
        IDLE,
        TRACE,
        ATTACK,
        DIE,
        PlAYERDIE
    }

    public MonsterState state = MonsterState.IDLE;

    public float traceDist = 1f;
    // ���� �����Ÿ�
    public float attackDist = 3.5f;
    // ���� �����Ÿ�
    public bool isDie = false;

    [SerializeField]
    private Transform targetTransform;

    private Transform monsterTransform;
    private NavMeshAgent agent;
    private Animator anim;

    private readonly int hashTrace = Animator.StringToHash("IsTrace");
    private readonly int hashAttack = Animator.StringToHash("IsAttack");
    private readonly int hashHit = Animator.StringToHash("IsHit");
    private readonly int hashPlayerDie = Animator.StringToHash("PlayerDie");
    private readonly int hashSpeed = Animator.StringToHash("Speed");
    private readonly int hashDie = Animator.StringToHash("Die");
    //�ؽ� ���̺� �� ��������

    private GameObject bloodEffect;
    //���� ȿ�� ������
     
    private int currHP;
    //������ ���� �ʱⰪ


    private void Awake()
    {
        currHP = GameManager.Instance().monsterHP;

        monsterTransform = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;

        bloodEffect = Resources.Load<GameObject>("BloodSprayEffect");

    }

    private void OnEnable()
    {
        state = MonsterState.TRACE;

        currHP = GameManager.Instance().monsterHP;
        isDie = false;

        targetTransform = GetComponent<Transform>();
        // �ʱ�ȭ

        GetComponent<CapsuleCollider>().enabled = true;

        SphereCollider[] spheres = GetComponentsInChildren<SphereCollider>();

        foreach (SphereCollider sphere in spheres)
        {
            sphere.enabled = true;
        }

        StartCoroutine(CheckMonsterState());
        // ���� ���� üũ
        StartCoroutine(MonsterAction());
        // ���� �ִϸ��̼� üũ
    }

    private void Update()
    {
        if(agent.remainingDistance > 2.0f)
        {
            Vector3 direction = agent.desiredVelocity;
            // agent ȸ�� ��

            Quaternion rotation = Quaternion.LookRotation(direction);

            monsterTransform.rotation = Quaternion.Slerp(monsterTransform.rotation, rotation, Time.deltaTime * 10.0f);
            // ���� �������� �Լ��� �ε巯�� ȸ��
            // ȸ�� ���� ����
        }
    }

    IEnumerator MonsterAction()
    {
        while(!isDie)
        {
            switch(state)
            {
                case MonsterState.IDLE:
                    agent.isStopped = true;
                    anim.SetBool(hashTrace, false);
                    break;

                case MonsterState.TRACE:
                    agent.SetDestination(targetTransform.position);
                    agent.isStopped = false;
                    anim.SetBool(hashTrace, true);
                    anim.SetBool(hashAttack, false);
                    break;

                case MonsterState.ATTACK:
                    anim.SetBool(hashAttack, true);
                    break;

                case MonsterState.DIE:
                    isDie = true;
                    agent.isStopped = true;
                    
                    anim.SetTrigger(hashDie);
                 
                    GetComponent<CapsuleCollider>().enabled = false;

                    SphereCollider[] spheres = GetComponentsInChildren<SphereCollider>();

                    foreach(SphereCollider sphere in spheres)
                    {
                        sphere.enabled = false;
                    }

                    yield return new WaitForSeconds(3.0f);

                    this.gameObject.SetActive(false);

                    break;

                case MonsterState.PlAYERDIE:
                    StopAllCoroutines();
                    agent.isStopped = true;

                    anim.SetFloat(hashSpeed, Random.Range(0.5f, 1.3f));
                    anim.SetTrigger(hashPlayerDie);
                    GetComponent<CapsuleCollider>().enabled = false;
                    break;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    IEnumerator CheckMonsterState()
    {
        while (!isDie)
        {
            yield return new WaitForSeconds(0.3f);

            if(state == MonsterState.PlAYERDIE)
            {
                yield break;
            }
            if (state == MonsterState.DIE)
            {
                yield break;
            }

            //������ ĳ���� ���� �Ÿ� ����
            float distance = Vector3.Distance(monsterTransform.position, targetTransform.position);
            //float distance = Vector3.Distance(monsterTransform.position, transform.position);

            if (distance <= attackDist)
            {
                state = MonsterState.ATTACK;
            }
            else
            {
                state = MonsterState.TRACE;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            anim.SetTrigger(hashHit);

            Vector3 pos = collision.GetContact(0).point;
            // �浹 ����
            Quaternion rot = Quaternion.LookRotation(-collision.GetContact(0).normal);
            // �Ѿ��� �浹 ���� ���� ����
            ShowBloodEffect(pos, rot);

            currHP -= 10;

            if(currHP <= 0)
            {
                state = MonsterState.DIE;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {                           
        //Debug.Log("Punch");
        if(other.CompareTag("Spawner"))
        {
            targetTransform = other.gameObject.transform.GetChild(0).transform;
        }
    }

    private void ShowBloodEffect(Vector3 pos, Quaternion rot)
    {
        GameObject blood = Instantiate<GameObject>(bloodEffect, pos, rot, monsterTransform);
        Destroy(blood, 1.0f);
    }

    void OnPlayerDie()
    {
        state = MonsterState.PlAYERDIE;
    }

    private void OnDrawGizmos()
    {
        if (state == MonsterState.TRACE)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(monsterTransform.position, traceDist);
        }

        if (state == MonsterState.ATTACK)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(monsterTransform.position, traceDist);
        }
    }
}
