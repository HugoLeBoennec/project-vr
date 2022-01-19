using System;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;



// [RequireComponent(typeof(ThirdPersonCharacter))]
public class ThirdPersonUserControl : MonoBehaviour
{
    //Camera
    public Camera playerCamera;

    //Composant qui permet de faire bouger le joueur
    CharacterController characterController;

    //Vitesse de marche
    public float walkingSpeed = 7.5f;

    //Vitesse de course
    public float runningSpeed = 15f;

    //Vitesse de saut
    public float jumpSpeed = 8f;

    //Gravité
    float gravity = 20f;

    public int health = 5;

    Text TxtHealth;

    //Déplacement
    Vector3 moveDirection;

    //Marche ou court ?
    private bool isRunning = false;

    //Rotation de la caméra
    float rotationX = 0;
    public float rotationSpeed = 6.0f;
    public float rotationXLimit = 45.0f;

    Animator animator;
    int SpeedHash;
    int DirectionHash;
    public Rigidbody rb;

    bool m_IsGrounded;
    Vector3 m_GroundNormal;

    // public gameRule gameRules;
    public GameObject DeathPanel;


    NetworkPlayerSpawner pS;

    public void removeLife()
    {
        health -= 1;
    }

    void OnCollisionEnter(Collision Col)
    {
        if (Col.gameObject.tag == "Bullet")
        {
            Debug.Log("- 1 pv");
            removeLife();
            Destroy(Col.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Cache le curseur de la souris
        Cursor.visible = false;
        characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        SpeedHash = Animator.StringToHash("Speed");
        DirectionHash = Animator.StringToHash("Direction");

        TxtHealth = GameObject.Find("HealthText").GetComponent<Text>();

        GameObject gM = GameObject.Find("GameManager");
        GameConfig gC = gM.GetComponent<GameConfig>();
        Debug.Log(gC.gameRules.LifeNumber);
        pS = gM.GetComponent<NetworkPlayerSpawner>();
        DeathPanel = GameObject.Find("DeathScreen");

    }

    // Update is called once per frame
    void Update()
    {
        //Calcule les directions
        //forward = avant/arrière
        //right = droite/gauche
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        //Est-ce qu'on appuie sur un bouton de direction ?

        // Z = axe arrière/avant
        float speedZ = Input.GetAxis("Vertical");

        // X = axe gauche/droite
        float speedX = Input.GetAxis("Horizontal");

        // Y = axe haut/bas
        float speedY = moveDirection.y;


        //Est-ce qu'on appuie sur le bouton pour courir (ici : Shift Gauche) ?
        // if (Input.GetKey(KeyCode.LeftShift))
        // {
        //     //En train de courir
        //     isRunning = true;
        // }
        // else
        // {
        //     //En train de marcher
        //     isRunning = false;
        // }

        // Est-ce que l'on court ?
        if (isRunning)
        {
            //Multiplie la vitesse par la vitesse de course
            speedX = speedX * runningSpeed;
            speedZ = speedZ * runningSpeed;
        }
        else
        {
            //Multiplie la vitesse par la vitesse de marche
            speedX = speedX * walkingSpeed;
            speedZ = speedZ * walkingSpeed;

        }

        //Calcul du mouvement
        //forward = axe arrière/avant
        //right = axe gauche/droite
        moveDirection = forward * speedZ + right * speedX;


        // TODO
        animator.SetFloat("Speed", speedZ);
        animator.SetFloat("Direction", speedX);


        // Est-ce qu'on appuie sur le bouton de saut (ici : Espace)
        if (Input.GetButton("Jump") && characterController.isGrounded)
        {

            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = speedY;
        }


        //Si le joueur ne touche pas le sol
        // if (!rb .isGrounded)
        // {
        //     //Applique la gravité * deltaTime
        //     //Time.deltaTime = Temps écoulé depuis la dernière frame
        //     moveDirection.y -= gravity * Time.deltaTime;
        // }

        CheckGroundStatus();
        if (moveDirection != Vector3.zero)
        {
            rb.MovePosition(rb.position + moveDirection * Time.deltaTime);
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);//Vector3.zero;
        }
        //Applique le mouvement
        // characterController.Move(moveDirection * Time.deltaTime);
        // moveAmount = Vector3.SmoothDamp(moveAvmount, move)


        //Rotation de la caméra

        //Input.GetAxis("Mouse Y") = mouvement de la souris haut/bas
        //On est en 3D donc applique ("Mouse Y") sur l'axe de rotation X 
        rotationX += -Input.GetAxis("Mouse Y") * rotationSpeed;
        animator.SetFloat("LookDirection", rotationX);
        // Debug.Log(animator.GetFloat("LookDirection"));

        //La rotation haut/bas de la caméra est comprise entre -45 et 45 
        //Mathf.Clamp permet de limiter une valeur
        //On limite rotationX, entre -rotationXLimit et rotationXLimit (-45 et 45)
        rotationX = Mathf.Clamp(rotationX, -rotationXLimit, rotationXLimit);


        //Applique la rotation haut/bas sur la caméra
        playerCamera.transform.parent.localRotation = Quaternion.Euler(rotationX, 0, 0);

        //Input.GetAxis("Mouse X") = mouvement de la souris gauche/droite
        //Applique la rotation gauche/droite sur le Player
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * rotationSpeed, 0);


        TxtHealth.text = "Health : " + health;
        if (health <= 0)
        {

            DeadMulti();
            if (Input.GetButtonDown("Respawn"))
            {
                DeathPanel.SetActive(false);
            }
            // pS.ChangeType();
        }
    }
    void CheckGroundStatus()
    {
        RaycastHit hitInfo;
#if UNITY_EDITOR
        // helper to visualise the ground check ray in the scene view
        Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down));
#endif
        // 0.1f is a small offset to start the ray from inside the character
        // it is also good to note that the transform position in the sample assets is at the base of the character
        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo))
        {
            m_GroundNormal = hitInfo.normal;
            m_IsGrounded = true;
            animator.applyRootMotion = true;
        }
        else
        {
            m_IsGrounded = false;
            m_GroundNormal = Vector3.up;
            animator.applyRootMotion = false;
        }
    }

    void DeadMulti()
    {
        Debug.Log(DeathPanel);
        transform.position = new Vector3(50, 50, 50);
        DeathPanel.SetActive(true);

    }
}
