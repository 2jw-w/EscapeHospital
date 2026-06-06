using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    public static FlashlightController Instance;

    public GameObject flashlightObject; // 카메라 자식 손전등 오브젝트
    private bool hasFlashlight = false;

    void Awake() => Instance = this;

    void Update()
    {
        if (!hasFlashlight) return;

        // 줍고 나서 F키로 켜기/끄기
        if (Input.GetKeyDown(KeyCode.F))
            flashlightObject.SetActive(!flashlightObject.activeSelf);
    }

    public void PickupFlashlight()
    {
        hasFlashlight = true;
        flashlightObject.SetActive(true);
    }
}