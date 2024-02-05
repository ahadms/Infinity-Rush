using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController Controller;
    public float speed = 5.0f;
    private Vector3 moveVector;
    private float verticalVelocity = 0.0f;
    private float gravity = 12.0f;
    private float animationDuration = 3.0f;
    private bool isDead = false;
    private float startTime;
    public AudioSource deathsound;
    public AudioSource runSound;


    // Start is called before the first frame update
    void Start()
    {
        Controller = GetComponent<CharacterController>();
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            return;
        }
            

        if(Time.time -startTime < animationDuration)
        {
            Controller.Move(Vector3.forward * speed * Time.deltaTime);
            return;
        }

        moveVector = Vector3.zero;


        if (Controller.isGrounded)
        {
            verticalVelocity = -5.0f;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        //x - left and Right
        moveVector.x = Input.GetAxisRaw("Horizontal") * speed;
        //y up and down
        moveVector.y = verticalVelocity;
        //z front and back
        moveVector.z = speed;



        Controller.Move(moveVector * Time.deltaTime);
    }

    public void SetSpeed(float SpeedMOD)
    {
        speed =5.0f + SpeedMOD;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.point.z > transform.position.z + Controller.radius)
        {
            Death();
            deathsound.Play();
            runSound.Pause();
            
        }
    }

    private void Death()
    {
       isDead = true;
        GetComponent<Score>().OnDeath();
    }
}
