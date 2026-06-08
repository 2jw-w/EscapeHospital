using UnityEngine;

public class BGMAudio : MonoBehaviour
{
    public static BGMAudio Instance;

    [Header("BGM")]
    public AudioClip normalBGM;
    public AudioClip chaseBGM;

    [Header("페이드 속도")]
    public float fadeSpeed = 1.5f;

    private AudioSource audioSource;
    private bool isChasing = false;
    private bool fadingOut = false;
    private AudioClip targetClip;

    void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
    }

    void Start()
    {
        audioSource.clip = normalBGM;
        audioSource.volume = 1f;
        audioSource.Play();
    }

    void Update()
    {
        if (fadingOut)
        {
            audioSource.volume -= fadeSpeed * Time.deltaTime;
            if (audioSource.volume <= 0f)
            {
                fadingOut = false;
                audioSource.Stop();
                audioSource.clip = targetClip;
                audioSource.volume = 1f;
                audioSource.Play();
            }
        }
    }

    public void StartChase()
    {
        if (isChasing) return;
        isChasing = true;

        // 페이드 없이 즉시 전환
        audioSource.Stop();
        audioSource.clip = chaseBGM;
        audioSource.volume = 1f;
        audioSource.Play();
    }

    public void StopChase()
    {
        if (!isChasing) return;
        isChasing = false;
        fadingOut = true;
        targetClip = normalBGM;
    }
    public void StopAll()
    {
        audioSource.Stop();
    }
}