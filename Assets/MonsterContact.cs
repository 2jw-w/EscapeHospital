using UnityEngine;

public class MonsterContact : MonoBehaviour
{
    public float contactTime = 0.5f;
    private float timer = 0f;

    void OnTriggerStay(Collider col)
    {
        if (!col.gameObject.CompareTag("Player")) return;

        timer += Time.deltaTime;
        if (timer >= contactTime)
            GameOverManager.Instance.TriggerGameOver();
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
            timer = 0f;
    }
}