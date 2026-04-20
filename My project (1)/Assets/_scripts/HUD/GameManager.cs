using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject menuPrincipalPanel;
    public GameObject menuOpcionesPanel;
    public GameObject jugador;
    public PauseMenu pauseMenu;  // Arrastra el objeto que tiene el script PauseMenu

    private bool juegoIniciado = false;

    void Start()
    {
        PausarJuego(true);
        menuPrincipalPanel.SetActive(true);
        menuOpcionesPanel.SetActive(false);
        if (jugador != null)
            jugador.GetComponent<PlayerMovement>().enabled = false; // Ajusta según tu controlador

        // Asegurar que el menú de pausa esté oculto
        if (pauseMenu != null)
        {
            pauseMenu.pausePanel.SetActive(false);
            pauseMenu.SetGameStarted(false);
        }

        if (playerHealth == null)
            playerHealth = FindAnyObjectByType<PlayerHealth>();

        if (playerHealth != null)
            maxHealth = playerHealth.maxHealth;

        UpdatePortrait();
    }

    void PausarJuego(bool pausar)
    {
        Time.timeScale = pausar ? 0f : 1f;
    }

    public void IniciarAventura()
    {
        juegoIniciado = true;
        menuPrincipalPanel.SetActive(false);
        menuOpcionesPanel.SetActive(false);
        PausarJuego(false);
        if (jugador != null)
            jugador.GetComponent<PlayerMovement>().enabled = true;

        // Notificar al pause menu que el juego ha comenzado
        if (pauseMenu != null)
            pauseMenu.SetGameStarted(true);
    }

    public void AbrirOpciones()
    {
        menuPrincipalPanel.SetActive(false);
        menuOpcionesPanel.SetActive(true);
    }

    public void CerrarOpciones()
    {
        menuOpcionesPanel.SetActive(false);
        if (!juegoIniciado)
            menuPrincipalPanel.SetActive(true);
        else
        {
            // Si estamos en juego, al cerrar opciones volvemos al estado de pausa (si corresponde)
            // O simplemente reanudamos. Aquí asumimos que Opciones se abre desde el menú de pausa.
            // Por simplicidad, reanudamos:
            PausarJuego(false);
        }
    }

    // NUEVO: Volver al menú principal desde la pausa
    public void VolverAlMenuPrincipal()
    {
        juegoIniciado = false;
        menuPrincipalPanel.SetActive(true);
        // Desactivar control del jugador
        if (jugador != null)
            jugador.GetComponent<PlayerMovement>().enabled = false;
        // Pausar el juego nuevamente (como al inicio)
        Time.timeScale = 0f;
        // Asegurar que el menú de pausa esté oculto
        if (pauseMenu != null)
        {
            pauseMenu.pausePanel.SetActive(false);
            pauseMenu.SetGameStarted(false);
        }
    }

    //PlayerPortrait

    [Header("Referencias")]
    public Image portraitImage;           // Arrastra el componente Image del retrato
    public PlayerHealth playerHealth;     // Arrastra el script de salud del jugador

    [Header("Sprites por estado de vida")]
    public Sprite fullHealthSprite;       // 100% - 51%
    public Sprite mediumHealthSprite;     // 50% - 26%
    public Sprite lowHealthSprite;        // 25% - 0%

    private float maxHealth;

    public void UpdatePortrait()
    {
        if (playerHealth == null || portraitImage == null) return;

        float healthPercent = playerHealth.currentHealth / maxHealth * 100f;

        if (healthPercent > 50f)  // Mayor a 50% ? vida completa
            portraitImage.sprite = fullHealthSprite;
        else if (healthPercent > 25f)  // Entre 25% y 50% ? media vida
            portraitImage.sprite = mediumHealthSprite;
        else  // 25% o menos ? vida baja
            portraitImage.sprite = lowHealthSprite;
    }


    
    public void SalirJuego()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
