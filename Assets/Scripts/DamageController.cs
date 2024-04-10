using UnityEngine;
using TMPro;

public class DamageController : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public SpriteRenderer healthBar; 
    private Vector3 initialScale; 
    public DropSystem dropSystem; 
    void Start()
    {
        currentHealth = maxHealth;
        initialScale = healthBar.transform.localScale;
        UpdateHealthUI(); 
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0); 
        UpdateHealthUI(); 

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        dropSystem.DropLoot(); 
        
        Destroy(gameObject);
    }

    void UpdateHealthUI()
    {
        float scaleX = (float)currentHealth / maxHealth; 
        healthBar.transform.localScale = new Vector3(initialScale.x * scaleX, initialScale.y, initialScale.z); 
    }
}
