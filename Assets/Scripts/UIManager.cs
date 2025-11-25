using UnityEngine;
using UnityEngine.SceneManagement;



public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _inGameText;
    [SerializeField] private GameObject _pauseMenu;

    public bool paused = false;

    void Update()
    {
        if (SceneManager.GetActiveScene().name != "main menu" && Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                OnContinuePress();
            } else
            {
                OnPausePress();
            }
        }
    }

    public void OnRestartPress()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnPausePress()
    {
        AudioListener.pause = true;
        paused = true;

        _inGameText.SetActive(false);
        _pauseMenu.SetActive(true);
    }

    public void OnHomePress()
    {
        SceneManager.LoadScene("main menu");
        paused = false;
    }

    public void OnContinuePress()
    {
        _inGameText.SetActive(true);
        _pauseMenu.SetActive(false);

        AudioListener.pause = false;
        paused = false;
    }

    public void OnLevelTwoPress()
    {
        SceneManager.LoadScene("Level2");
        AudioListener.pause = false;
        paused = false;
    }

    public void OnLevelThreePress()
    {
        SceneManager.LoadScene("Delivery_Level");
        AudioListener.pause = false;
        paused = false;
    }
}
