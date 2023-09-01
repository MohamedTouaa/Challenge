using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject pistolWeapon; // Attach pistol GameObject as child in the Inspector
    public GameObject rifleWeapon;  // Attach rifle GameObject as child in the Inspector
    public Transform firePoint;
    public GameObject pistolBulletPrefab; // Attach pistol bullet prefab in the Inspector
    public GameObject rifleBulletPrefab;  // Attach rifle bullet prefab in the Inspector
    public float pistolFireRate = 0.5f;   // Adjust as needed
    public float rifleFireRate = 0.1f;    // Adjust as needed
    public float pistolBulletSpeed = 10f; // Adjust as needed
    public float rifleBulletSpeed = 20f;  // Adjust as needed

    private GameObject currentWeapon;
    private GameObject currentBulletPrefab;
    private float nextShootTime;

    private void Start()
    {
        SwitchWeapon(pistolWeapon, pistolBulletPrefab);
        nextShootTime = 0f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeapon(pistolWeapon, pistolBulletPrefab);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchWeapon(rifleWeapon, rifleBulletPrefab);
        }

        if (Input.GetMouseButton(0) && Time.time >= nextShootTime)
        {
            Shoot();
        }
    }

    private void SwitchWeapon(GameObject newWeapon, GameObject newBulletPrefab)
    {
        if (currentWeapon != null)
        {
            currentWeapon.SetActive(false);
        }
        currentWeapon = newWeapon;
        currentWeapon.SetActive(true);

        currentBulletPrefab = newBulletPrefab;
    }

    private void Shoot()
    {
        nextShootTime = Time.time + (currentWeapon == pistolWeapon ? pistolFireRate : rifleFireRate);

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 shootDirection = (mousePosition - transform.position).normalized;

        GameObject newProjectile = Instantiate(currentBulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = newProjectile.GetComponent<Rigidbody2D>();
        rb.velocity = shootDirection * (currentWeapon == pistolWeapon ? pistolBulletSpeed : rifleBulletSpeed);

        Destroy(newProjectile, 3f); // Destroy the projectile after 3 seconds (adjust as needed)
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject); // Destroy the bullet when it collides with something
    }
}
