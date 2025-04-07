using UnityEngine;

public class PickupVisual : MonoBehaviour
{
    public Material scoreMaterial;
    public Material speedMaterial;

    private Pickup pickup;
    private MeshRenderer meshRenderer;

    void Start()
    {
        pickup = GetComponent<Pickup>();
        meshRenderer = GetComponent<MeshRenderer>();
        UpdateVisual();
    }

    void UpdateVisual()
    {
        switch (pickup.pickupType)
        {
            case Pickup.PickupType.Score:
                meshRenderer.material = scoreMaterial;
                break;
            case Pickup.PickupType.Speed:
                meshRenderer.material = speedMaterial;
                break;
        }
    }
} 