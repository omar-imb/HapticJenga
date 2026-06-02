using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleMenu : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject menuButton;
    public GameObject controlsPanel;
    public GameObject playButton;
    public GameObject continueButton;
    public GameObject optionsPanel;

    // Recuerda si ya se inició el juego
    private static bool gameStarted = false;

    void Start()
    {
        if (gameStarted)
        {
            mainMenuPanel.SetActive(false);

            menuButton.SetActive(true);

            playButton.SetActive(false);
            continueButton.SetActive(true);

            Time.timeScale = 1f;
        }
        else
        {


            mainMenuPanel.SetActive(true);

            menuButton.SetActive(false);

            playButton.SetActive(true);
            continueButton.SetActive(false);

            Time.timeScale = 0f;
        }
    }

    public void PlayGame()
    {
        gameStarted = true;

        mainMenuPanel.SetActive(false);

        menuButton.SetActive(true);

        Time.timeScale = 1f;

        playButton.SetActive(false);
        continueButton.SetActive(true);
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
        Debug.Log("Salir");

        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
    public void OpenControls()
    {
        mainMenuPanel.SetActive(false);

        controlsPanel.SetActive(true);
    }

    public void CloseControls()
    {
        controlsPanel.SetActive(false);

        mainMenuPanel.SetActive(true);
    }

    public void OpenOptions()
    {
        mainMenuPanel.SetActive(false);

        optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);

        mainMenuPanel.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Si estamos en Controles
            if (controlsPanel.activeSelf)
            {
                CloseControls();
            }
            // Si estamos en Opciones
            else if (optionsPanel.activeSelf)
            {
                CloseOptions();
            }
            // Si estamos en el menú principal
            else if (mainMenuPanel.activeSelf)
            {
                mainMenuPanel.SetActive(false);
                Time.timeScale = 1f;
            }
            // Si estamos jugando
            else
            {
                OpenMenu();
            }
        }
    }
}