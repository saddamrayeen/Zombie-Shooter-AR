using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    bool canFire = true;

    [Header("Gun Properties")]
    [SerializeField]
    GameObject bulletprefab;

    [SerializeField]
    GameObject muzzleFlash;

    [SerializeField]
    GameObject firePoint;

    [SerializeField]
    int totalBulletCount;

    [SerializeField]
    float fireRate;

    int currentBulletCount;

    [SerializeField]
    float reloadTime;

    private void Start()
    {
        currentBulletCount = totalBulletCount;
    }

    // Update is called once per frame
    void Update()
    {
        PointCameraInTheMiddleOfScreen();

        if (Input.GetButton("Fire1") && canFire && currentBulletCount > 0)
        {
            FireBullet();
        }

        // auto reload
        if (currentBulletCount <= 0)
        {
            // For test only
            Debug.Log("Cannot fire, Reloading");

            StartCoroutine(ReloadGun());
        }
    }

    private void PointCameraInTheMiddleOfScreen()
    {
        var target = Camera.main.transform.GetChild(0).transform.position;

        transform.LookAt(target);
    }

    private void FireBullet()
    {
        currentBulletCount--;
        // cannot fire another bullet now
        var cameraPos = Camera.main.transform.position;
        canFire = false;

        // spawn bullet
        var spawnedBullet = Instantiate(
            bulletprefab,
            firePoint.transform.position,
            Quaternion.identity
        );

        var spawnedMuzzleFlash = Instantiate(
            muzzleFlash,
            firePoint.transform.position,
            Quaternion.Euler(0, 0, Random.Range(0, 180))
        );
        Destroy(spawnedMuzzleFlash, .05f);

        spawnedBullet.transform.position = new Vector3(
            cameraPos.x,
            cameraPos.y,
            firePoint.transform.position.z
        );

        // Set the bullet's velocity
        Rigidbody bulletRB = spawnedBullet.GetComponent<Rigidbody>();
        bulletRB.velocity = firePoint.transform.forward * 10f;

        // TODO: Reoil animation



        StartCoroutine(ResetFireRate());
    }

    IEnumerator ResetFireRate()
    {
        yield return new WaitForSeconds(fireRate);
        if (currentBulletCount > 0)
        {
            canFire = true;
        }
    }

    IEnumerator ReloadGun()
    {
        canFire = false;

        // TODO: Play Reload Animation
        yield return new WaitForSeconds(reloadTime);

        canFire = true;
        currentBulletCount = totalBulletCount;
    }
}
