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

    public void SalirJuego()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
