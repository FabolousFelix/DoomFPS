using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Referencia a script de input personalizado
    private OldInput _oldInput;
    // Referencia al CharacterController 
    private CharacterController _characterController;
    // Velocidad actual del jugador
    public float speed;
    // Velocidad de rotación
    public float rotationSpeed;
    // Fuerza de gravedad 
    public float gravity = -9.81f;
    // Acumulador de rotación horizontal
    private float _currentlookingPos;
    // Referencia al Animator
    public Animator animator;

    [Header("Velocidad")]
    public float baseSpeed; // velocidad original

    // Referencia a la corrutina del efecto de slow
    private Coroutine slowCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        // Obtiene el script de input del mismo objeto
        _oldInput = GetComponent<OldInput>();
        // Obtiene el CharacterController
        _characterController = GetComponent<CharacterController>();
        baseSpeed = speed; //guardar velocidad original
    }

    // Update is called once per frame
    void Update()
    {
        PlayerWalk();// Movimiento hacia adelante/atrás
        PlayerRotation();// Rotación izquierda/derecha
        CamAnim();// Animación de caminar (cam)
    }

    // Método que controla el movimiento del jugador
    public void PlayerWalk()
    {
        // Crea un vector de movimiento usando solo el eje Z (adelante/atrás)
        Vector3 inputVector = new Vector3(0, 0, _oldInput.vertical);
        // Convierte ese vector a dirección local del jugador
        inputVector = transform.TransformDirection(inputVector);
        // Combina movimiento + gravedad
        Vector3 movementVector = (inputVector * speed) + (Vector3.up * gravity);
        // Mueve al jugador usando CharacterController
        _characterController.Move(movementVector * Time.deltaTime);
    }

    // Método que controla la rotación del jugador
    public void PlayerRotation()
    {
        // Calcula cuánto debe rotar este frame
        float rotationInput = _oldInput.horizontal * rotationSpeed * Time.deltaTime;
        // Acumula la rotación
        _currentlookingPos += rotationInput;
        // Aplica la rotación en el eje Y (tipo FPS clásico)
        transform.localRotation = Quaternion.AngleAxis(_currentlookingPos, transform.up);
    }

    // Controla la animación de caminar
    public void CamAnim()
    {
        // Activa o desactiva el parámetro "IsWalking" en el Animator
        animator.SetBool("IsWalking", _oldInput.vertical != 0);
    }

    // Método para aplicar ralentización (slow)
    public void ApplySlow(float slowAmount, float duration)
    {
        // Si ya hay un slow activo, lo cancela
        if (slowCoroutine != null)
            StopCoroutine(slowCoroutine);
        // Inicia una nueva corrutina de slow
        slowCoroutine = StartCoroutine(SlowCoroutine(slowAmount, duration));
    }

    // Corrutina que aplica el efecto de slow temporalmente
    IEnumerator SlowCoroutine(float slowAmount, float duration)
    {
        // Reduce la velocidad según el porcentaje recibido
        speed = baseSpeed * (1f - slowAmount);

        Debug.Log("Jugador ralentizado");
        // Espera el tiempo indicado
        yield return new WaitForSeconds(duration);
        // Restaura la velocidad original
        speed = baseSpeed;

        Debug.Log("Velocidad restaurada");
    }
}
