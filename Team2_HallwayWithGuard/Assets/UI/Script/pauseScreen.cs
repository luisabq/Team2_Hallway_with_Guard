using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    public static bool isPaused = false;

    public GameObject targetObject;

    [Header("UI Ref")]
    public GameObject pauseMenuUI;
    public GameObject hudUI;

    private FPSController playerController;

    void Start()
    {
        playerController = targetObject.GetComponent<FPSController>();

        playerController.enabled = true;

        pauseMenuUI.SetActive(false);
        hudUI.SetActive(true);

        Time.timeScale = 1f;
        isPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        playerController.enabled = true;

        pauseMenuUI.SetActive(false);
        hudUI.SetActive(true);

        Time.timeScale = 1f;
        isPaused = false;

        AudioListener.pause = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Pause()
    {
        playerController.enabled = false;

        pauseMenuUI.SetActive(true);
        hudUI.SetActive(false);

        Time.timeScale = 0f;
        isPaused = true;

        AudioListener.pause = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LoadMenu(string sceneName)
    {
        Time.timeScale = 1f;
        isPaused = false;

        AudioListener.pause = false;

        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}