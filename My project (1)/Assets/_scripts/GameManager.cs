using UnityEngine;
using UnityEngine.UI; // Para trabajar con UI

public class GameManager : MonoBehaviour
{
    public GameObject menuPrincipalPanel; // Arrastra el Panel desde el Inspector
    public GameObject jugador; // Arrastra al jugador (si quieres desactivar su control)

    private bool juegoIniciado = false;

    public CanvasGroup menuCanvasGroup;
    float fadeSpeed = 3f;



    void Update()
    {
        if (menuPrincipalPanel.activeSelf && menuCanvasGroup.alpha < 1)
            menuCanvasGroup.alpha += fadeSpeed * Time.unscaledDeltaTime; // Time.unscaledDeltaTime respeta el pausa
    }

    void Start()
    {
        // Pausar el juego al inicio
        PausarJuego(true);

        // Mostrar menú principal
        menuPrincipalPanel.SetActive(true);

        // Desactivar control del jugador si es necesario
        if (jugador != null)
            jugador.GetComponent<OldInput>().enabled = false;
    }

    void PausarJuego(bool pausar)
    {
        if (pausar)
        {
            Time.timeScale = 0f; // Congela el tiempo
        }
        else
        {
            Time.timeScale = 1f; // Reanuda el tiempo
        }
    }

    // Método que asigna al botón "Iniciar Aventura"
    public void IniciarAventura()
    {
        juegoIniciado = true;
        menuPrincipalPanel.SetActive(false); // Oculta el menú
        PausarJuego(false); // Reanuda el juego

        // Reactivar control del jugador
        if (jugador != null)
            jugador.GetComponent<OldInput>().enabled = true;
    }

    public void SalirJuego()
    {
        Application.Quit(); // Cierra la aplicación
    }
}
