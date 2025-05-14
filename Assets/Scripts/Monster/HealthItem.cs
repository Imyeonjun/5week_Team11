using UnityEngine;

public class HealthItem : MonoBehaviour
{
    [SerializeField] private float healAmount = 20f;
    [SerializeField] private float activateDelay = 1f; 
    [SerializeField] private GameObject pickupEffect;

    private Collider2D col;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>(); 
    }

    private void OnEnable()
    {
        col.enabled = false;
        if (spriteRenderer != null)
            spriteRenderer.enabled = false;

        Invoke(nameof(ActivateItem), activateDelay);
    }

    private void ActivateItem()
    {
        col.enabled = true;
        if (spriteRenderer != null)
            spriteRenderer.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ResourceController resource = collision.GetComponent<ResourceController>();
        if (resource != null)
        {
            bool healed = resource.ChangeHealth(healAmount);
            if (healed)
            {
                if (pickupEffect != null)
                {
                    Instantiate(pickupEffect, transform.position, Quaternion.identity);
                }

                Destroy(gameObject);
            }
        }
    }
}