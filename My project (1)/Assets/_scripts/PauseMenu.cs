using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;      // Asigna el panel de pausa desde el Inspector
    public GameManager gameManager;    // Referencia al GameManager (para llamar a sus métodos)

    private bool isPaused = false;
    private bool gameStarted = false;  // Para saber si el juego ya comenzó (evitar pausar en menú principal)

    void Update()
    {
        // Solo permitir pausa si el juego ya comenzó y el menú de opciones no está abierto (opcional)
        if (gameStarted && Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    // Método para ser llamado desde GameManager cuando se inicia la aventura
    public void SetGameStarted(bool started)
    {
        gameStarted = started;
    }

    public void TogglePause()
    {
        if (isPaused)
            ResumeGame();
        else
            PauseGame();
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;           // Congela el tiempo
        pausePanel.SetActive(true);    // Muestra el menú de pausa
        // Opcional: ocultar el cursor o bloquear inputs adicionales
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        // Restaurar cursor si tu juego lo requiere (ej. FPS)
        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
    }

    // Botón "Menú Principal"
    public void ReturnToMainMenu()
    {
        // Reanudar el tiempo antes de volver al menú principal
        Time.timeScale = 1f;
        isPaused = false;
        pausePanel.SetActive(false);
        // Llamar al GameManager para que muestre el menú principal y resetee el estado
        if (gameManager != null)
            gameManager.VolverAlMenuPrincipal();
    }

    // Botón "Salir" - reutiliza el método de GameManager
    public void QuitGame()
    {
        if (gameManager != null)
            gameManager.SalirJuego();
    }
}
