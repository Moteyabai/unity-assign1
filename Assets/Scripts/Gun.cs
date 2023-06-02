using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject player;
    [SerializeField] private float bulletSpeed=10f;

    private bool isCharging= false;
    [SerializeField] private float chargeTime;
    private PlayerMoment playerMoment;
    [SerializeField] private float chargeSpeed;
    [SerializeField] private GameObject chargedBulletPrefab;

    private void Start()
    {
      playerMoment = player.GetComponent<PlayerMoment>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = bulletSpawnPoint.right * bulletSpeed;
            chargeTime = 0;
        }

        if (Input.GetKey(KeyCode.V) && chargeTime < 2)
        {
            isCharging = true;
            if(isCharging)
            {
                chargeTime += Time.deltaTime * chargeSpeed;
            }
        }

        else if (Input.GetKeyUp(KeyCode.V) && chargeTime >=2)
        {
            ReleaseCharge();
            playerMoment.ChargedShot();
            isCharging = false;
            chargeTime = 0;
        }
    }

    

    private void ReleaseCharge()
    {
        var chargedBullet = Instantiate(chargedBulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        chargedBullet.GetComponent<Rigidbody2D>().velocity = bulletSpawnPoint.right * bulletSpeed;
    }
}
