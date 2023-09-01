using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f; // Adjust the maximum health as needed
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    // Function to take damage
    public void TakeDamage(float damageAmount)
    {
        // Reduce the current health by the damage amount
        currentHealth -= damageAmount;

        // Check if the enemy's health has reached zero or below
        if (currentHealth <= 0)
        {
            // Enemy is defeated, you can add death animations/effects or destroy the enemy GameObject
            Destroy(gameObject);
        }
    }
}
