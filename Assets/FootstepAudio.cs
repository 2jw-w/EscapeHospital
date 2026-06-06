using UnityEngine;

public class FootstepAudio : MonoBehaviour
{
    [Header("발소리 클립")]
    public AudioClip[] walkClips;
    public AudioClip[] runClips;

    [Header("간격 (초)")]
    public float walkInterval = 0.5f;
    public float runInterval = 0.28f;

    private AudioSource audioSource;
    private FPSController player;
    private float timer;
    public AudioSource footstepSource;

    void Start()
    {
        player = GetComponent<FPSController>();
    }

    void Update()
    {
        if (!player.IsMoving) { timer = 0f; return; }

        timer += Time.deltaTime;
        float interval = player.IsRunning ? runInterval : walkInterval;

        if (timer >= interval)
        {
            PlayStep();
            timer = 0f;
        }
    }

    void PlayStep()
    {
        AudioClip[] clips = player.IsRunning ? runClips : walkClips;
        if (clips.Length == 0) return;

        footstepSource.pitch = Random.Range(0.9f, 1.1f);
        footstepSource.PlayOneShot(clips[Random.Range(0, clips.Length)]);
    }
}