using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // Importe este namespace

//lida cm o input
[SelectionBase] //para mover o objeto que tem o script
[RequireComponent(typeof(Rigidbody))] //para prevenir erros diz ao unity que tem de ser um rigidbody
public class PlayerController : MonoBehaviour
{
    private Vector3 lastPosition;//para a bussola

    #region Input Fields
    //recebe o input do playercontrols
    public Vector2 movInput;
    public string currentDirection;

    public string location = "madeira";
    public Vector2 aimInput;
    #endregion
    public AudioClip soundPassosErva;
    public AudioClip soundPassosMadeira; 
    public AudioClip soundPassosAgua;
    private AudioSource audioSource;

    [SerializeField] float movSpeed = 5f;

    [Header("Aiming")]
    [SerializeField] float lookClamp = 70f;
    [SerializeField] float lookSensitivity = 0.1f;//0.5f; para ser mais sensivel a virar a camara

    [Header("Interacting")]
    [SerializeField] float interactRange = 5f;
    [SerializeField] LayerMask interactLayer;
    private bool wasMoving = false; // Flag para rastrear se o jogador estava se movendo no quadro anterior


    #region References
    [Header("References")]
    [SerializeField] Transform cam;
    Rigidbody rb;
    #endregion
//public string gamepadButtonName = "E";


    float mouseRot;

    // Awake happens before Start. Use it to initialize references
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>(); // Adicione esta linha para obter uma referência ao AudioSource

    }

    // Start is called before the first frame update
    void Start()
    {
        mouseRot = cam.rotation.eulerAngles.x;
        lastPosition = transform.position;   //para a bussola
       
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Paredes" ) // Verifica se playerController não é nulo
        {
                   StopFootstepSound();
        }
        if (other.tag == "madeira")
        {
                location = "madeira";
                StopFootstepSound();
                PlayFootstepSound();
                Debug.Log("madeira");
        }
        if (other.tag == "erva" && currentDirection == "Oeste")
        {
                location = "erva";
                StopFootstepSound();
                PlayFootstepSound();
                Debug.Log("erva");
        }
        if (other.tag == "erva1" && currentDirection == "Este")
        {
                location = "erva";
                StopFootstepSound();
                PlayFootstepSound();
                Debug.Log("erva");
        }
        if (other.tag == "agua" && currentDirection == "Oeste")
        {
                location = "agua";
                StopFootstepSound();
                PlayFootstepSound();
                Debug.Log("agua");
        }
        if (other.tag == "normal" && currentDirection == "Este")
        {
                location = "madeira";
                StopFootstepSound();
                PlayFootstepSound();
                Debug.Log("madeira");
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.tag=="Paredes" ) // Verifica se playerController não é nulo
        {
            PlayFootstepSound();
           //Debug.Log("saiu");
        }
    }
    // Update is called once per frame
    void Update()
    {
        //doAim comentado pq nao queremos rodar a camara
        //DoAim();
       
       
    }


    private void DoAim()
    {
        // Apply reduced sensitivity
    Vector2 adjustedAimInput = aimInput * lookSensitivity * 0.1f; // Experimente com diferentes valores menores aqui

    // Rotate Body
    transform.Rotate(Vector3.up * adjustedAimInput.x);
    }
    

    // FixedUpdate is called on every physics loop (constant time intervals)
    private void FixedUpdate()
    {
        DoMovement();
        CheckDirection(); //para a bússola
    }

    private void DoMovement()
    {
        Vector3 forward = transform.forward * movInput.y;
        Vector3 right = transform.right * movInput.x;

        bool isMoving = forward.magnitude > 0 || right.magnitude > 0;

    // Se houve movimento desde o último quadro e o jogador não estava se movendo antes, toque o som de passos
    if (isMoving && !wasMoving)
    {
        PlayFootstepSound();
    }
    // Se o jogador estava se movendo no último quadro, mas parou agora, pare o som de passos
    else if (!isMoving && wasMoving)
    {
        StopFootstepSound();
    }

    wasMoving = isMoving;

       
        rb.velocity = (forward + right) * movSpeed + new Vector3(0, rb.velocity.y);
        
    }

    public void Interact()
    {
        Debug.DrawRay(cam.position, cam.forward * interactRange);

        RaycastHit hit;
     
        if (Physics.Raycast(cam.position, cam.forward, out hit, interactRange, interactLayer))
        {
            hit.collider.GetComponent<Button>().Interact();
        }
    }
    private void PlayFootstepSound()
    {
        if (location == "agua"){
            audioSource.clip = soundPassosAgua;
        }
        if (location == "erva"){
            audioSource.clip = soundPassosErva;
        }
        if (location == "madeira"){
            audioSource.clip = soundPassosMadeira;
        }
        audioSource.Play();
    }
    private void StopFootstepSound()
{
    if (audioSource != null && soundPassosAgua != null && soundPassosMadeira != null && soundPassosErva != null)
    {
        audioSource.Stop();
    }
}
//com a rotacao do player
private void CheckDirection() //para a bussola
{
    Vector3 moveDirection = transform.position - lastPosition;
    //Debug.Log("entrou aqui");
    if (moveDirection != Vector3.zero)  // Ensure there is some movement
    {
        moveDirection.Normalize();  

        float threshold = 0.5f;  
        if (Mathf.Abs(moveDirection.x) > threshold)
        {
            // Horizontal Movement
            if (moveDirection.x > 0){
                currentDirection = "Este";
               // Debug.Log("Moving East");
               }
            else{
                currentDirection = "Oeste";
               // Debug.Log("Moving West");
               }
        }
        if (Mathf.Abs(moveDirection.z) > threshold)
        {
            // Vertical Movement
            if (moveDirection.z > 0){
                currentDirection = "Norte";
               // Debug.Log("Moving North");
               }
            else{
                currentDirection = "Sul";
               // Debug.Log("Moving South");
               }
        }
    }

    lastPosition = transform.position;  // Update lastPosition for the next frame
}



//Com a rotacao da camara 
/*
private void CheckDirection() //para a bussola
{
       // Obter a rotação em Y do jogador
        float yRotation = transform.eulerAngles.y;

        // Normalizar a rotação dentro do intervalo de 0 a 360 graus
        yRotation = (yRotation + 360) % 360;

        // Determinar a direção baseada na rotação usando intervalos mais apropriados
        if ((yRotation >= 0 && yRotation < 45) || (yRotation >= 315 && yRotation < 360))
        {
            currentDirection = "Norte";
            //Debug.Log("Norte");

        }
        else if (yRotation >= 45 && yRotation < 135)
        {
            currentDirection = "Este";
            //Debug.Log("Este");
        }
        else if (yRotation >= 135 && yRotation < 225)
        {
            currentDirection = "Sul";
            //Debug.Log("Sul");
        }
        else if (yRotation >= 225 && yRotation < 315)
        {
            currentDirection = "Oeste";
            //Debug.Log("Oeste");
        }

}
*/

}
