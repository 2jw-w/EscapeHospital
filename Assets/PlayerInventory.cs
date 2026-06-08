using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;
    public bool hasKey = false;

    void Awake() => Instance = this;
}