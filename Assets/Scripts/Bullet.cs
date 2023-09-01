using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 10f; // Adjust the bullet damage as needed

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with an object tagged as "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Get the EnemyHealth script from the collided object
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();

            // Check if the enemy has an EnemyHealth component
            if (enemyHealth != null)
            {
                // Reduce the enemy's health
                enemyHealth.TakeDamage(damage);
            }

            // Destroy the bullet on collision with an enemy
            Destroy(gameObject);
        }
        else
        {
            // Destroy the bullet if it hits something other than an enemy
            Destroy(gameObject);
        }
    }
}
