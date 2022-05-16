using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
//using UnityEngine.Rendering.HighDefinition;


public class PlayerController : MonoBehaviour
{
    ///////////Basic Player Movement///////////
    public Transform PlayerTransform;
    private Animator Ninja;
    private CharacterController Controller;
    private Vector3 PlayerVelocity; 
    private bool GroundedPlayer;
    private bool Running = false;
    private bool Walking = false;
    public float PlayerSpeed;
    public static float Stamina = 7f;
    public float StaminaTimeLimit = 7f;
    public float StaminaTimeUntilRegen = 2f;
    private float TimeSinceRun = 0;
    private float GravityValue = -9.81f;
    public float WalkSpeed = 3f;
    public float RunSpeed = 6f;

    ///////////Variables for XRay///////////
    [Space]

    [Header("XRay Shader variables")]
    public static float xrayTimeLimit = 5f;
    [HideInInspector] public static float xraytime = 5f;
    public float xRayTimeUntilRegen = 2f;
    private float timeSinceXray = 0;
    bool hasPlayedxRay = false;
    public PostProcessVolume volume;
    Vignette vignette;
    Grain filmGrain;

    [Space]

    ///////////Variables for Attacking///////////
    public Transform EnemyTransform;


    ///////////Variables for Distractable///////////
    public GameObject Distractable;
    public static int AmountOfDistractables = 3;
    private float timeToRefillDistractable = 5f;

    ///////////Variables for Audio///////////
    public AudioSource RunAudio;
    public AudioSource WalkAudio;


    ///////////Variables for KeyCard///////////
    public static int KeyCards = 0;

    ///////////Variables for Checking for Slopes///////////
    RaycastHit HitInfo;
    Vector3 forward;
    public float MaxGroundAngle = 120f;
    public float Height = .5f;
    public float HeightPadding = .05f;
    public LayerMask Ground;
    public LayerMask MiniMapWall;
    private float Angle;
    private float GroundAngle;

    

    private void Start()
    {
        Controller = GetComponent<CharacterController>();
        Ninja = GetComponentInChildren<Animator>();
        Shader.SetGlobalFloat("_GlobalPlayerVisibility", 1f);

        if (volume != null)
        {
            
            volume.profile.TryGetSettings(out vignette);
            volume.profile.TryGetSettings(out filmGrain);
        }
        GameManager.Instance.keyCards = 0;

        if (GameManager.Instance.KeyCardCountText != null)
        {
            GameManager.Instance.KeyCardCountText.text = GameManager.Instance.keyCards.ToString();
        }
        GameManager.Instance.isPaused = false;

        AmountOfDistractables = 3;

        Time.timeScale = 1f;
    }

    void Update()
    {
        if (GroundAngle >= MaxGroundAngle)
        {
            PlayerSpeed = 0f;
        }
        else if (Walking == true)
        {
            PlayerSpeed = WalkSpeed;
        }
        else if (Running == true) 
        {
            PlayerSpeed = RunSpeed;
        }

        /*if (WalkAudio != null)
        {
           CheckAudio();
        }
        */

        ///////////Checking for Slopes///////////
        CalculateForward();
        CalculateGroundAngle();
        CheckGrounded();
       
        ///////////Player movement (Left, Right, Forward, Bacward)///////////
        Vector3 Move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if ((Controller.velocity.x > 0f && Controller.velocity.x < 2f) || (Controller.velocity.z > 0f && Controller.velocity.z < 2f)) 
        {
            Walking = true;
        }
        if (Controller.velocity.magnitude > 0)
        {
            if (Running && !Walking)
            {
                SoundManager.PlaySound(SoundManager.Sound.PlayerRun);
            }
            else if (Walking && !Running)
            {
                SoundManager.PlaySound(SoundManager.Sound.PlayerWalk);
            }
        }

        ///////////Camera Movement///////////
        if (Camera.main != null)
        {
            Controller.Move(Camera.main.transform.right * Move.x * Time.deltaTime * PlayerSpeed);
            Controller.Move(Camera.main.transform.forward * Move.z * Time.deltaTime * PlayerSpeed); 
            transform.forward = Camera.main.transform.forward;

            Vector3 angles = transform.rotation.eulerAngles;
            angles.x = 0;
            angles.z = 0;

            transform.rotation = Quaternion.Euler(angles);

        }
        
        
        ///////////Player Run///////////
        if (Controller.velocity.magnitude > 0 && Input.GetButton("Run"))
        {
            
            if (Stamina >= .1f)
            {
                Running = true;
                Ninja.Play("Running");
                PlayerSpeed = RunSpeed;
                Walking = false;
                Stamina -= Time.deltaTime;


            }
            else
            {
                Walking = true;
                Running = false;
                PlayerSpeed = WalkSpeed;
                if (Stamina <= 6.0f)
                {
                    if (TimeSinceRun <= Time.time)
                    {
                        Stamina += Time.deltaTime * 3;

                    }
                }

            }
            SoundManager.PlaySound(SoundManager.Sound.PlayerWalk);
            TimeSinceRun = Time.time + StaminaTimeUntilRegen;
        }
        else 
        {
            
            Walking = true;
            Running = false;
            PlayerSpeed = WalkSpeed;
            if (Stamina <= 6.0f) {
                if (TimeSinceRun <= Time.time)
                {
                    Stamina += Time.deltaTime * 3;

                }
            }
            
        }

        Stamina = Mathf.Clamp(Stamina, 0, StaminaTimeLimit);


        //Check for XRay
        if (!GameManager.Instance.isPaused)
        {
            if (Input.GetButton("XRay"))
            {
                if (xraytime >= .1f)
                {
                    if (!hasPlayedxRay)
                    {
                        SoundManager.PlaySound(SoundManager.Sound.EnableXRay);
                        hasPlayedxRay = true;
                    }
                    Shader.SetGlobalFloat("_GlobalVisibility", 1f);
                    //Debug.Log(vignette);
                    if (vignette != null)
                        vignette.intensity.value = 0.6f;
                    //if (colorAdj != null)
                    //    colorAdj.hueShift.value = -40;
                    if (filmGrain != null)
                        filmGrain.intensity.value = 0.6f;
                    xraytime -= Time.deltaTime;
                }
                else
                {
                    UnXray();
                }
                timeSinceXray = Time.time + xRayTimeUntilRegen;
            }
            else
            {
                UnXray();
            }

            xraytime = Mathf.Clamp(xraytime, 0, xrayTimeLimit);
        }
        
        
        ///////////Create Distractable///////////
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (AmountOfDistractables > 0)
            {
                
                CreateDistractable();

            }
        }

        if (AmountOfDistractables < 3)
        {
            timeToRefillDistractable -= Time.deltaTime;
        }
        if(AmountOfDistractables < 3 && timeToRefillDistractable <= 0)
        {
           AmountOfDistractables++;
           timeToRefillDistractable = 5f;
        }

        ///////////Checking the amount of keycards the player has///////////
        if (Input.GetKeyDown(KeyCode.F))
        {
            CheckKeyCards();
        }


        if (GroundedPlayer == false) 
        {
            PlayerVelocity.y += GravityValue * Time.deltaTime;
        }
        Controller.Move(PlayerVelocity * Time.deltaTime);
        
    }

    public void UnXray()
    {
        hasPlayedxRay = false;
        Shader.SetGlobalFloat("_GlobalVisibility", 0f);
        if (vignette != null)
            vignette.intensity.value = 0f;
        //if (colorAdj != null)
        //    colorAdj.hueShift.value = 0;
        if (filmGrain != null)
            filmGrain.intensity.value = 0f;

        if (timeSinceXray <= Time.time)
        {
            xraytime += Time.deltaTime;

        }
    }


    ///////////Player Actions///////////


    ///Distractions
    void CreateDistractable()
    {
        Vector3 DistractableSpawn = new Vector3(GameManager.Instance.Player.transform.position.x, GameManager.Instance.Player.transform.position.y + 1.5f, GameManager.Instance.Player.transform.position.z + .5f) ;
        GameObject clone = Instantiate(Distractable, DistractableSpawn, transform.rotation);
        SoundManager.PlaySound(SoundManager.Sound.PlayerThrowDistractable);
        AmountOfDistractables -= 1;
        Destroy(clone, 3);
    }

    
    void CheckKeyCards()
    {
        Debug.Log("Keycards = " + KeyCards);
    }

  /*  void CheckAudio() 
    {
        if (Walking == true)
{
            RunAudio.Stop();

            if (WalkAudio.isPlaying == false)
                WalkAudio.Play();
        }
        else if (Running == true)
{
            WalkAudio.Stop();

            if (RunAudio.isPlaying == false)
            {
                RunAudio.Play();
            }
        }
        else
        {
            WalkAudio.Stop();
            RunAudio.Stop();
           
        }
    }
    */
    void CalculateForward() 
    {
        if (GroundedPlayer == false) 
        {
            forward = transform.forward;
        }
        forward = Vector3.Cross(HitInfo.normal, -transform.right);
    }

    void CalculateGroundAngle() 
    {
        if (GroundedPlayer == false) 
        {
            GroundAngle = 90;
            return;
        }

        GroundAngle = Vector3.Angle(HitInfo.normal, transform.forward);
    }

    void CheckGrounded() 
    {
        if (Physics.Raycast(transform.position, -Vector3.up, out HitInfo, Height + HeightPadding, Ground) || Physics.Raycast(transform.position, -Vector3.up, out HitInfo, Height + HeightPadding, MiniMapWall))
        {
            GroundedPlayer = true;
        }
        else 
        {
            GroundedPlayer = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "Water")
        {
            StartCoroutine(WaterDie());
        }
    }

    IEnumerator WaterDie()
    {
        SoundManager.PlaySound(SoundManager.Sound.PlayerWaterDeath);
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene("LOSE CONDITION");
    }
}
   






