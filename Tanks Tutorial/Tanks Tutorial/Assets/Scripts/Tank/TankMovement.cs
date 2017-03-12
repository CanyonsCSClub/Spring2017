using UnityEngine;

public class TankMovement : MonoBehaviour
{
    public int m_PlayerNumber = 1;         
    public float m_Speed = 12f;            
    public float m_TurnSpeed = 180f;       
    public AudioSource m_MovementAudio;    
    public AudioClip m_EngineIdling;       
    public AudioClip m_EngineDriving;      
    public float m_PitchRange = 0.2f;
	public GameObject pauseMenu;
	private bool active = false;
    
    private string m_MovementAxisName;     
    private string m_TurnAxisName;         
    private Rigidbody m_Rigidbody;         
    private float m_MovementInputValue;    
    private float m_TurnInputValue;        
    private float m_OriginalPitch;         


    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }


    private void OnEnable ()
    {
        m_Rigidbody.isKinematic = false;
        m_MovementInputValue = 0f;
        m_TurnInputValue = 0f;
    }


    private void OnDisable ()
    {
        m_Rigidbody.isKinematic = true;
    }


    private void Start()
    {
        m_MovementAxisName = "Vertical" + m_PlayerNumber;
        m_TurnAxisName = "Horizontal" + m_PlayerNumber;

        m_OriginalPitch = m_MovementAudio.pitch;
    }


    private void Update()
    {
        // Store the player's input and make sure the audio for the engine is playing.
		m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
		m_TurnInputValue = Input.GetAxis (m_TurnAxisName);

		EngineAudio ();
    }


    private void EngineAudio()
    {
        // Play the correct audio clip based on whether or not the tank is moving and what audio is currently playing.
		if(Mathf.Abs(m_MovementInputValue) < 0.1f && Mathf.Abs(m_TurnInputValue) < 0.1f){
			if (m_MovementAudio.clip == m_EngineDriving) {// is the current audio clip that is playing the driving sound?
				//because the tank is moving or reallt slow...it should be idleing ;P
				m_MovementAudio.clip = m_EngineIdling;
				m_MovementAudio.pitch = Random.Range (m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
				m_MovementAudio.Play ();
			}
		}else{
			if (m_MovementAudio.clip == m_EngineIdling) {// is the current audio clip that is playing the idling sound?
				//because the tank is now moving it should be making the moving noise :P
				m_MovementAudio.clip = m_EngineDriving;
				m_MovementAudio.pitch = Random.Range (m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
				m_MovementAudio.Play ();
			}
		}


    }


    private void FixedUpdate()
    {
        // Move and turn the tank.
		Move();
		Turn ();
		if (Input.GetKeyDown (KeyCode.Escape)){
			pauseMenu.SetActive (!active);
			active = !active;
		}

    }


    private void Move(){// Adjust the position of the tank based on the player's input.
		Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;
		//Time.delta time does.... instead of moving 12 units per frame (@m_Speed) it changes it so that it is changed 
		//to 12 units per second...smoothing it out.
		m_Rigidbody.MovePosition(m_Rigidbody.position + movement);//move that tank
    }


    private void Turn(){// Adjust the rotation of the tank based on the player's input.
		float turn = m_TurnInputValue * m_TurnSpeed *Time.deltaTime;
		//"number of degrees rotated per frame"
		Quaternion turnRotation = Quaternion.Euler (0f, turn , 0f);
		m_Rigidbody.MoveRotation (m_Rigidbody.rotation * turnRotation);
		//Note: you cannot add quarternions you have to multiply them 

		
		}

}