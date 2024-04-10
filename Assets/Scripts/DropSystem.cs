using UnityEngine;

public class DropSystem : MonoBehaviour
{
    public GameObject itemPrefab; 
    public void DropLoot()
    {
        GameObject newItem = Instantiate(itemPrefab, transform.position, Quaternion.identity);
    }
}