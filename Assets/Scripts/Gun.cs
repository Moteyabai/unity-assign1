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
    [SerializeField] private AudioSource normalShotSfx;
    [SerializeField] private AudioSource chargedShotSfx;
    [SerializeField] private AudioSource chargingSfx;


    private void Start()
    {
      playerMoment = player.GetComponent<PlayerMoment>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.V) && chargeTime < 3)
        {
            isCharging = true;
            if(isCharging == true)
            {
                chargeTime += Time.deltaTime * chargeSpeed;
                chargingSfx.Play();
            }
            
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = bulletSpawnPoint.right * bulletSpeed;
            normalShotSfx.Play();
            chargeTime = 0;
        }

        if (Input.GetKeyUp(KeyCode.V) && chargeTime >=3)
        {
            ReleaseCharge();          
        }
    }

    

    private void ReleaseCharge()
    {
        var chargedBullet = Instantiate(chargedBulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        chargedBullet.GetComponent<Rigidbody2D>().velocity = bulletSpawnPoint.right * bulletSpeed;
        playerMoment.ChargedShot();
        chargedShotSfx.Play();
        chargingSfx.Stop();
        isCharging = false;
        chargeTime = 0f;
    }
}
