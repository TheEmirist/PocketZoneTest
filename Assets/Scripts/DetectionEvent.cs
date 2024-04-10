using System;
using System.Collections.Generic;
using UnityEngine;

public class DetectionEvent : MonoBehaviour
{
    public Action<Transform> OnEnemyDetected;
    public Action<Transform> OnEnemyExited;

    public List<Transform> detectedEnemies = new List<Transform>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Transform enemyTransform = collision.transform;
            detectedEnemies.Add(enemyTransform);

            if (enemyTransform != null)
            {
                OnEnemyDetected?.Invoke(enemyTransform);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Transform enemyTransform = collision.transform;
            detectedEnemies.Remove(enemyTransform);

            if (enemyTransform != null)
            {
                OnEnemyExited?.Invoke(enemyTransform);
            }
        }
    }

    public Transform GetClosestEnemy(Vector2 position)
    {
        Transform closestEnemy = null;
        float closestDistance = float.MaxValue;

        foreach (Transform enemy in detectedEnemies)
        {
            float distance = Vector2.Distance(position, enemy.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }
}
