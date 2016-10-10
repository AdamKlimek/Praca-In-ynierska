using UnityEngine;
using System.Collections;

public class MoveCharacterController : MonoBehaviour
{
    private float StepTime = 1.5f;
    private float WalkSpeed = 3.0f;
    private float RunSpeed = 9.0f;
    private bool IsWalking = true;
    private float STime;
    public AudioClip WalkSound;
    JumpCharacterController Jump;

    void Start ()
    {
        Jump = GetComponent<JumpCharacterController>();
	}
	
	void Update ()
    {
        STime += Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.LeftShift) == true )
        {
            //GetComponent<Animation>().Play("runneutral");
            IsWalking = false;
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift) == true)
        {
            //GetComponent<Animation>().Play("walkneutral");
            IsWalking = true;
        }

        if (Jump.Isgrounded == true && (Input.GetKey(KeyCode.A) == true || Input.GetKey(KeyCode.S) == true || Input.GetKey(KeyCode.W) == true || Input.GetKey(KeyCode.D) == true) )
        {
            //GetComponent<Animation>().Play("walkneutral");
            PlayAudioFootStep();
        }

            Move();
    }

    void Move()
    {
        float X = Input.GetAxis("Horizontal") * Time.deltaTime;
        float Z = Input.GetAxis("Vertical") * Time.deltaTime;

        if (IsWalking == true)
        {
            X *= WalkSpeed;
            Z *= WalkSpeed;
        }
        else
        {
            X *= RunSpeed;
            Z *= RunSpeed;
        }

        transform.Translate(X, 0, Z);
    }

    void PlayAudioFootStep()
    {
        if (IsWalking == true)
        {
            if (STime > StepTime)
            {
                STime = 0.0f;
                if (GetComponent<AudioSource>().isPlaying == false)
                {
                    GetComponent<AudioSource>().PlayOneShot(WalkSound);
                }
            }
        }
        else
        {
            if (GetComponent<AudioSource>().isPlaying == false)
            {
                GetComponent<AudioSource>().PlayOneShot(WalkSound);
            }
        }
    }
}
