using UnityEngine;

public class Pickup : MonoBehaviour
{
    public enum PickupType
    {
        Score,      // Regular score pickup
        Speed       // Speed boost pickup
    }

    public PickupType pickupType = PickupType.Score;
    public float effectDuration = 5f;  // Duration for speed boost
    public float effectValue = 1.5f;   // Speed multiplier
    public float moveSpeed = 2f;       // Movement speed for pickups
    public float moveDistance = 1f;    // Distance to move left and right
    private Vector3 startPosition;
    private float timeOffset;

    void Start()
    {
        startPosition = transform.position;
        timeOffset = Random.Range(0f, 2f * Mathf.PI); // Random offset for movement
    }

    void Update()
    {
        // Make the pickup move left and right
        float newX = startPosition.x + Mathf.Sin(Time.time + timeOffset) * moveDistance;
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                ApplyEffect(player);
                gameObject.SetActive(false);
            }
        }
    }

    protected virtual void ApplyEffect(PlayerController player)
    {
        switch (pickupType)
        {
            case PickupType.Score:
                player.AddScore(1);
                break;
            case PickupType.Speed:
                player.ApplySpeedBoost(effectValue, effectDuration);
                break;
        }
    }
} 