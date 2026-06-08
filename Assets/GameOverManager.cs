using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;

    public Image gameOverImage;
    public Button replayButton;
    public AudioClip jumpscareSound;

    public float showDelay = 0.5f;
    public bool isGameOver = false;

    private AudioSource audioSource;

    void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void TriggerGameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        // 모든 오디오 끄기
        AudioListener.volume = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Invoke("ShowGameOver", showDelay);
        BGMAudio.Instance.StopAll();
    }

    void ShowGameOver()
    {
        // 갑툭튀 사운드만 켜기
        AudioListener.volume = 1f;
        audioSource.PlayOneShot(jumpscareSound);

        gameOverImage.gameObject.SetActive(true);
        replayButton.gameObject.SetActive(true);

        Time.timeScale = 0f;
    }

    public void Replay()
    {
        Time.timeScale = 1f;
        AudioListener.volume = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}