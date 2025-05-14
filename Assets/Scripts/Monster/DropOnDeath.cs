using UnityEngine;

public class DropOnDeath : MonoBehaviour
{
    [SerializeField] private GameObject healthItemPrefab; 
    [SerializeField][Range(0f, 1f)] private float dropChance = 0.3f;

    private ResourceController resourceController;

    private void Awake()
    {
        resourceController = GetComponent<ResourceController>();

        if (resourceController != null)
        {
            resourceController.AddHealthChangeEvent(OnHealthChanged);
        }
    }

    private void OnDestroy()
    {
        if (resourceController != null)
        {
            resourceController.RemoveHealthChangeEvent(OnHealthChanged);
        }
    }

    private void OnHealthChanged(float currentHealth, float maxHealth)
    {
        if (currentHealth <= 0f)
        {
            TryDropItem();
        }
    }

    private void TryDropItem()
    {
        if (healthItemPrefab != null && Random.value <= dropChance)
        {
            Instantiate(healthItemPrefab, transform.position, Quaternion.identity);
        }
    }
}