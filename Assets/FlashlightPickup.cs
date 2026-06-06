using UnityEngine;

public class FlashlightPickup : MonoBehaviour
{
    public float pickupRange = 2f;
    private Transform player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        Debug.Log("Player found: " + player);
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= pickupRange && Input.GetKeyDown(KeyCode.F))
        {
            FlashlightController.Instance.PickupFlashlight();
            Destroy(gameObject);
        }
    }
}