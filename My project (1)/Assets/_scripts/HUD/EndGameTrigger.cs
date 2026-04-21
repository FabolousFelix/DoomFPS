using UnityEngine;
using static UnityEngine.ProBuilder.AutoUnwrapSettings;

public class EndGameTrigger : MonoBehaviour
{
    // UI que se muestra cuando termina el juego (panel final)
    public GameObject endGameUI;
    // Referencia al jugador
    public GameObject player;
    // Evita que el trigger se active más de una vez
    private bool activated = false;
    // Se ejecuta cuando otro collider entra en el trigger
    private void OnTriggerEnter(Collider other)
    {
        // Si no es el jugador o ya fue activado, no hace nada
        if (!other.CompareTag("Player") || activated) return;
        // Marca el trigger como activado para evitar repetición
        activated = true;
        // Llama al método que muestra la pantalla final
        ShowEndGame();
    }

    //mostrar el panel 
    public void ShowEndGame()
    {
        // Mensaje en consola para debug
        Debug.Log("FIN DEL JUEGO");
        // Activa la UI de fin de juego si existe
        if (endGameUI != null)
            endGameUI.SetActive(true);
        // Desactiva el movimiento del jugador
        if (player != null)

            player.GetComponent<PlayerMovement>().enabled = false;
        // Pausa el juego completamente (tiempo en 0)
        Time.timeScale = 0f;
    }

    // Método para ocultar la pantalla de fin (reinicio o menú)
    public void HideEndGame()
    {
        // Desactiva la UI del final
        if (endGameUI != null)
            endGameUI.SetActive(false);

        // Permite que el trigger pueda volver a activarse
        activated = false;
    }
}