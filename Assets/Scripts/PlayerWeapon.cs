using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float rotationSpeed = 5f;
    public int damage = 2;
    public DetectionEvent detectionEvent;
    public int requiredItemId;

    private Transform target;
    private bool isAiming = false;

    private void OnEnable()
    {
        if (detectionEvent != null)
        {
            detectionEvent.OnEnemyDetected += OnEnemyDetected;
            detectionEvent.OnEnemyExited += OnEnemyExited;
        }
    }

    private void OnDisable()
    {
        if (detectionEvent != null)
        {
            detectionEvent.OnEnemyDetected -= OnEnemyDetected;
            detectionEvent.OnEnemyExited -= OnEnemyExited;
        }
    }

    private void ResetAiming()
    {
        target = null;
        isAiming = false;
        transform.rotation = Quaternion.identity;
    }

    private void OnEnemyDetected(Transform enemyTransform)
    {
        target = enemyTransform;
        isAiming = true;
    }

    private void OnEnemyExited(Transform enemyTransform)
    {
        if (enemyTransform == target)
        {
            ResetAiming();
        }
    }

    void Update()
    {
        if (isAiming && target != null)
        {
            Vector2 direction = target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            if (detectionEvent != null)
            {
                target = detectionEvent.GetClosestEnemy(transform.position);
                if (target != null)
                {
                    OnEnemyDetected(target);
                }
            }
        }
    }
    public void ShootButtonPressed()
    {
        Item requiredItem = Inventory.Instance.GetItemById(requiredItemId);

        if (requiredItem != null)
        {
            Inventory.Instance.RemoveItem(requiredItem, 1);
            Shoot();
        }
    }


    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            bulletScript.SetDamage(damage);
        }

        bullet.GetComponent<Rigidbody2D>().velocity = firePoint.right * bulletScript.speed;

        Destroy(bullet, bulletScript.lifetime);
    }
}
