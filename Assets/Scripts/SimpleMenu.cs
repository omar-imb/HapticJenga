using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleMenu : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject menuButton;

    // Recuerda si ya se inició el juego
    private static bool gameStarted = false;

    void Start()
    {
        if (gameStarted)
        {
            // Si viene de un restart
            mainMenuPanel.SetActive(false);

            menuButton.SetActive(true);

            Time.timeScale = 1f;
        }
        else
        {
            // Primera vez que abre el juego
            mainMenuPanel.SetActive(true);

            menuButton.SetActive(false);

            Time.timeScale = 0f;
        }
    }

    public void PlayGame()
    {
        gameStarted = true;

        mainMenuPanel.SetActive(false);

        menuButton.SetActive(true);

        Time.timeScale = 1f;
    }

    public void OpenMenu()
    {
        mainMenuPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    public void ExitGame()
    {
        Application.Quit();

        Debug.Log("Salir");
    }
}