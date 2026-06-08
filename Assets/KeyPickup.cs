using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public float pickupRange = 2f;
    private Transform player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= pickupRange && Input.GetKeyDown(KeyCode.F))
        {
            PlayerInventory.Instance.hasKey = true;
            Destroy(gameObject);
        }
    }
}