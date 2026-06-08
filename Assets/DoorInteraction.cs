using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public float interactRange = 2f;
    public float openAngle = -90f;
    public float openSpeed = 2f;

    private Transform player;
    private bool isOpen = false;
    private Quaternion closedRot;
    private Quaternion openRot;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        closedRot = transform.rotation;
        openRot = Quaternion.Euler(transform.eulerAngles + new Vector3(0, openAngle, 0));
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= interactRange && Input.GetMouseButtonDown(0) && !isOpen)
        {
            if (PlayerInventory.Instance.hasKey)
            {
                isOpen = true;
                PlayerInventory.Instance.hasKey = false; // 열쇠 소모
            }
            else
            {
                Debug.Log("열쇠가 필요합니다.");
            }
        }

        if (isOpen) //문열리는 애니메이션
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, openRot, Time.deltaTime * openSpeed);
            ClearManager.Instance.TriggerClear(); // 추가
        }
    }
}