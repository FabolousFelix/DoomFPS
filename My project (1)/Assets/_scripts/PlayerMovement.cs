using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private OldInput _oldInput;
    private CharacterController _characterController;
    public float speed;
    public float rotationSpeed;
    public float gravity = -9.81f;
    private float _currentlookingPos;
    public Animator animator;

    [Header("Velocidad")]
    public float baseSpeed; // velocidad original

    private Coroutine slowCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        _oldInput = GetComponent<OldInput>();
        _characterController = GetComponent<CharacterController>();
        baseSpeed = speed; //guardar velocidad original
    }

    // Update is called once per frame
    void Update()
    {
        PlayerWalk();
        PlayerRotation();
        CamAnim();
    }

    public void PlayerWalk()
    {
        Vector3 inputVector = new Vector3(0, 0, _oldInput.vertical);

        inputVector = transform.TransformDirection(inputVector);

        Vector3 movementVector = (inputVector * speed) + (Vector3.up * gravity);

        _characterController.Move(movementVector * Time.deltaTime);
    }

    public void PlayerRotation()
    {
        float rotationInput = _oldInput.horizontal * rotationSpeed * Time.deltaTime;

        _currentlookingPos += rotationInput;

        transform.localRotation = Quaternion.AngleAxis(_currentlookingPos, transform.up);
    }

    public void CamAnim()
    {
        animator.SetBool("IsWalking", _oldInput.vertical != 0);
    }

    //se añadio un metodo pa que el boss pueda aplicar el slow sin problemas
    public void ApplySlow(float slowAmount, float duration)
    {
        if (slowCoroutine != null)
            StopCoroutine(slowCoroutine);

        slowCoroutine = StartCoroutine(SlowCoroutine(slowAmount, duration));
    }

    //corutina para los calculos del slow basicamente(? no me se explicar mb
    IEnumerator SlowCoroutine(float slowAmount, float duration)
    {
        speed = baseSpeed * (1f - slowAmount);

        Debug.Log("Jugador ralentizado");

        yield return new WaitForSeconds(duration);

        speed = baseSpeed;

        Debug.Log("Velocidad restaurada");
    }
}
