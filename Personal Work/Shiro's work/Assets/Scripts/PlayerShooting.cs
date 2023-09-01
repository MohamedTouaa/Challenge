using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject gun; // Attach the gun GameObject in the Inspector
    public Transform firePoint;
    public GameObject bulletPrefab;

    private SpriteRenderer gunSpriteRenderer;
    private float nextShootTime;
    private float currentFireRate;
    private float pistolBulletSpeed = 10f; // Adjust pistol bullet speed
    private float rifleBulletSpeed = 15f; // Adjust rifle bullet speed
    private float pistolBulletSpacing = 0.1f; // Adjust pistol bullet spacing as needed
    private float rifleBulletSpacing = 0.3f; // Adjust rifle bullet spacing as needed

    public Sprite pistolSprite; // Assign the pistol sprite in the Inspector
    public Sprite rifleSprite;  // Assign the rifle sprite in the Inspector

    private bool canShoot = true; // Flag to control shooting

    private void Start()
    {
        gunSpriteRenderer = gun.GetComponent<SpriteRenderer>();
        SwitchWeapon("pistol"); // Start with the pistol
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeapon("pistol");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchWeapon("rifle");
        }

        if (Input.GetMouseButton(0) && canShoot)
        {
            canShoot = false; // Prevent multiple shots in quick succession
            Shoot();
        }
    }

    private void SwitchWeapon(string weaponType)
    {
        if (weaponType == "pistol")
        {
            gunSpriteRenderer.sprite = rifleSprite;
            currentFireRate = 0.5f; // Adjust the pistol fire rate
            pistolBulletSpacing = 0.1f; // Adjust pistol bullet spacing
        }
        else if (weaponType == "rifle")
        {
            gunSpriteRenderer.sprite = pistolSprite;
            currentFireRate = 0.1f; // Adjust the rifle fire rate
            rifleBulletSpacing = 0.3f; // Adjust rifle bullet spacing
        }
    }

    private void Shoot()
    {
        nextShootTime = Time.time + currentFireRate;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 shootDirection = (mousePosition - transform.position).normalized;

        float bulletSpeed = (currentFireRate < 0.2f) ? rifleBulletSpeed : pistolBulletSpeed; // Bullet speed remains constant
        float bulletSpacing = (currentFireRate < 0.2f) ? rifleBulletSpacing : pistolBulletSpacing; // Adjust based on fire rate

        // Instantiate the bullets with the calculated initial velocity and spacing
        StartCoroutine(SpawnBullets(shootDirection, bulletSpeed, bulletSpacing));
    }

    private IEnumerator SpawnBullets(Vector2 shootDirection, float speed, float spacing)
    {
        while (Time.time < nextShootTime)
        {
            GameObject newBullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();
            rb.velocity = shootDirection * speed;

            Destroy(newBullet, 3f);
            yield return new WaitForSeconds(spacing);
        }

        canShoot = true; // Allow shooting again
    }
}
