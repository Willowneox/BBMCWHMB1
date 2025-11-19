using UnityEngine;
using UnityEngine.SceneManagement;



public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _inGameText;
    [SerializeField] private GameObject _pauseMenu;

    public void OnRestartPress()
    {
        AudioListener.pause = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnPausePress()
    {
        AudioListener.pause = true;

        _inGameText.SetActive(false);
        _pauseMenu.SetActive(true);
    }

    public void OnHomePress()
    {
        SceneManager.LoadScene("main menu");
    }

    public void OnContinuePress()
    {
        _inGameText.SetActive(true);
        _pauseMenu.SetActive(false);

        AudioListener.pause = false;
    }

    public void OnLevelThreePress()
    {
        SceneManager.LoadScene("Delivery_Level");
        AudioListener.pause = false;
    }
}
