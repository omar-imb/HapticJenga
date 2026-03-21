using UnityEngine;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    public GameObject menu;
    public GameObject button;
    
    public void Pause()
    {
        Time.timeScale = 0;
        button.SetActive(false);
        menu.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        button.SetActive(true);
        menu.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
