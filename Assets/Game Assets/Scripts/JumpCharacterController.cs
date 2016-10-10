using UnityEngine;
using System.Collections;

public class JumpCharacterController : MonoBehaviour
{
    private float JumpSpeed = 4.0f;
    public  bool Isgrounded = true;
    public AudioClip JumpSound;
    private Rigidbody rb;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}
	
	void Update ()
    {
        if (Input.GetKey(KeyCode.Space) == true)
        {
            jump();
        }

        if (Isgrounded == false && Physics.Raycast(transform.position, Vector3.down, 0.1f, LayerMask.GetMask("Terrain")))
        {
            GetComponent<AudioSource>().PlayOneShot(JumpSound);
            Isgrounded = true;
        }
    }

    void jump()
    {
        if(Isgrounded == true)
        {
            rb.velocity = JumpSpeed * Vector3.up;
            Isgrounded = false;
        }
    }

}
