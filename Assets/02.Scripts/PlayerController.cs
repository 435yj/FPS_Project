using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float detailX = 5.0f;
    public float detailY = 5.0f;
    public float rotationX = 0.0f;
    public float rotationY = 0.0f;

    public float walkSpeed = 5f;
    public float moveSpeed = 10f;

    [SerializeField]
    private Transform cameraTransform;

    private Transform playerTransform;
    private Animation anim;

    private readonly float iniHp = 100.0f;
    // 초기 생명 값
    public float currHp;
    // 현재 생명 값

    private ePlayerState playerState = ePlayerState.WALK;

    IEnumerator Start()
    {
        playerState = ePlayerState.WALK;

        currHp = iniHp;

        playerTransform = GetComponent<Transform>();
        anim = GetComponent<Animation>();

        anim.Play("Idle");

        GameManager.Instance().turnSpeed = 0f;
        yield return new WaitForSeconds(0.3f);
        GameManager.Instance().turnSpeed = 80.0f;
    }

    void Update()
    { 

        if (Input.GetKey(KeyCode.LeftShift))
            playerState = ePlayerState.RUN;
        else
            playerState = ePlayerState.WALK;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        moveDir.Normalize();

        rotationX = cameraTransform.eulerAngles.y + x * detailX;
        rotationX = (rotationX > 180.0f) ? rotationX - 360 : rotationX;

        rotationY = rotationY + y * detailY;
        rotationY = Mathf.Clamp(rotationY, -45, 80);

        cameraTransform.eulerAngles = new Vector3(-rotationY, rotationX, 0);
        cameraTransform.position = cameraTransform.position;

        transform.eulerAngles = new Vector3(0, rotationX, 0);

        if (playerState == ePlayerState.RUN)
            playerTransform.Translate(moveDir * moveSpeed * Time.deltaTime);
        else if (playerState == ePlayerState.WALK)
            playerTransform.Translate(moveDir * walkSpeed * Time.deltaTime);

        PlayerAnim(h, v);

    }

    void PlayerAnim(float h, float v)
    {
        if (h <= -0.1f)
            anim.CrossFade("RunL", 0.25f);
        else if (h >= 0.1f)
            anim.CrossFade("RunR", 0.25f);
        else if (v <= -0.1f)
            anim.CrossFade("RunB", 0.25f);
        else if (v >= 0.1f)
            anim.CrossFade("RunF", 0.25f);
        else
            anim.CrossFade("Idle", 0.25f);
    }
}
