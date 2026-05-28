using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [Header("Mensaje")]
    [TextArea]
    public string message;

    public float messageDuration = 3f;

    // Evita que se active más de una vez
    private bool alreadyTriggered;

    private void OnTriggerEnter(Collider other)
    {
        // Verifica que sea el jugador
        if (other.CompareTag("Player") && !alreadyTriggered)
        {
            alreadyTriggered = true;

            // Muestra el mensaje
            ItemMessageUI.instance.ShowMessage(
                message,
                messageDuration
            );
        }
    }
}