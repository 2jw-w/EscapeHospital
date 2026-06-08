using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ClearManager : MonoBehaviour
{
    public static ClearManager Instance;

    [Header("UI")]
    public GameObject clearPanel;
    public TextMeshProUGUI clearTimeText;
    public TextMeshProUGUI rankingText;
    public Button replayButton;

    private float startTime;
    private bool isCleared = false;

    void Awake() => Instance = this;

    void Start() => startTime = Time.time;

    void Update()
    {
        if (isCleared) return;
    }

    public void TriggerClear()
    {
        if (isCleared) return;
        isCleared = true;

        float clearTime = Time.time - startTime;
        SaveRanking(clearTime);

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        BGMAudio.Instance.StopAll();

        clearTimeText.text = "Clear Time : " + FormatTime(clearTime);
        ShowRanking();
        clearPanel.SetActive(true);
    }

    string FormatTime(float time)
    {
        int min = (int)(time / 60);
        int sec = (int)(time % 60);
        return string.Format("{0:00}:{1:00}", min, sec);
    }

    void SaveRanking(float time)
    {
        List<float> times = LoadRankings();
        times.Add(time);
        times.Sort();
        if (times.Count > 10) times.RemoveRange(10, times.Count - 10);

        for (int i = 0; i < times.Count; i++)
            PlayerPrefs.SetFloat("Rank_" + i, times[i]);
        PlayerPrefs.SetInt("RankCount", times.Count);
        PlayerPrefs.Save();
    }

    List<float> LoadRankings()
    {
        List<float> times = new List<float>();
        int count = PlayerPrefs.GetInt("RankCount", 0);
        for (int i = 0; i < count; i++)
            times.Add(PlayerPrefs.GetFloat("Rank_" + i));
        return times;
    }
    string GetRankSuffix(int rank)
    {
        if (rank == 1) return "st";
        if (rank == 2) return "nd";
        if (rank == 3) return "rd";
        return "th";
    }

    void ShowRanking()
    {
        List<float> times = LoadRankings();
        string result = "=== Ranking ===\n";
        for (int i = 0; i < times.Count; i++)
            result += (i + 1) + GetRankSuffix(i + 1) + ": " + FormatTime(times[i]) + "\n";
        rankingText.text = result;
    }

    public void Replay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}