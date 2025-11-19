using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void load_level_3()
    {
        SceneManager.LoadScene("Delivery_Level");
    }
}
