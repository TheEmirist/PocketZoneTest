using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class PlayerData
{
    public int health;
    public float[] position;
}
public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public GameObject gameOverMenu;
    public Slider healthSlider;

    public static PlayerHealth Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        currentHealth = maxHealth;
        UpdateUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        gameOverMenu.SetActive(true);
    }

    void UpdateUI()
    {
        healthSlider.value = (float)currentHealth / maxHealth;
    }
}
