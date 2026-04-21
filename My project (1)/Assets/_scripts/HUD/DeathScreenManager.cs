using UnityEngine;

public class DeathScreenManager : MonoBehaviour
{
    public GameObject deathPanel;
    public GameManager gameManager;
    public PlayerHealth playerHealth;

    private void OnEnable()
    {
        // Buscar el PlayerHealth cada vez que se activa (después de recargar)
        playerHealth = FindObjectOfType<PlayerHealth>();
        if (playerHealth != null)
            playerHealth.OnPlayerDeath += ShowDeathScreen;
    }

    private void OnDisable()
    {
        if (playerHealth != null)
            playerHealth.OnPlayerDeath -= ShowDeathScreen;
    }

    private void ShowDeathScreen()
    {
        deathPanel.SetActive(true);
        Time.timeScale = 0f;  // Congelar el juego
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Botón Reiniciar nivel
    public void RestartLevel()
    {
        Time.timeScale = 1f;
        // Recargar la escena actual
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
        );
    }

    // Botón Menú principal
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        // Llamar al método del GameManager para volver al menú principal y reiniciar el nivel
        if (gameManager != null)
            gameManager.VolverAlMenuPrincipalConReinicio();
        else
            UnityEngine.SceneManagement.SceneManager.LoadScene(0); // Fallback
    }

    // Botón Salir
    public void QuitGame()
    {
        if (gameManager != null)
            gameManager.SalirJuego();
        else
            Application.Quit();
    }
}
