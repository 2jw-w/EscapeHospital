using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialPanel;
    public FPSController fpsController;

    void Start()
    {
        tutorialPanel.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        fpsController.enabled = false;
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            tutorialPanel.SetActive(false);
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            fpsController.enabled = true;
            enabled = false;
        }
    }
}