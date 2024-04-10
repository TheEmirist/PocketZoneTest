using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f; 
    public float lifetime = 2f; 

    private int damage = 1; 

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<DamageController>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }
}