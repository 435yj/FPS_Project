using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireController : MonoBehaviour
{
    public int zoomCnt = 0;

    public bool ripleShooting = false;

    public GameObject bulletPrefab;
    public Transform firePos;
    public AudioClip fireSFX;

    private new AudioSource audio;

    private MeshRenderer muzzleFlash;

    [SerializeField]
    private Transform cameraTransform;
    [SerializeField]
    private Transform ripleCameraPos;

    [SerializeField]
    private Image playerAim;

    private void Start()
    {
        ripleShooting = true;

        audio = GetComponent<AudioSource>();
        muzzleFlash = firePos.GetComponentInChildren<MeshRenderer>();
        muzzleFlash.enabled = false;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && ripleShooting)
        {
            StartCoroutine("RipleBulletFire");
        }

        cameraTransform.position = ripleCameraPos.position;
    }

    IEnumerator RipleBulletFire()
    {
        Instantiate(bulletPrefab, ripleCameraPos.position, cameraTransform.rotation);

        audio.PlayOneShot(fireSFX, 1f);

        StartCoroutine("ShowMuzzleFlash");

        ripleShooting = false;
        yield return new WaitForSeconds(0.15f);
        ripleShooting = true;
    }

    IEnumerator ShowMuzzleFlash()
    {
        Vector2 offset = new Vector2(Random.Range(0, 2), Random.Range(0, 2) * 0.5f);

        muzzleFlash.material.mainTextureOffset = offset;
        // ·£´ý ¿ÀÇÁ¼Â

        float angle = Random.Range(0, 360);
        muzzleFlash.transform.localRotation = Quaternion.Euler(Vector3.forward * angle);
        // ·£´ýÇÑ È¸Àü

        float scale = Random.Range(1.0f, 2.0f);
        muzzleFlash.transform.localScale = Vector3.one * scale;
        // ·£´ýÇÑ Å©±â

        muzzleFlash.enabled = true;
        yield return new WaitForSeconds(0.2f);
        muzzleFlash.enabled = false;
    }
}
