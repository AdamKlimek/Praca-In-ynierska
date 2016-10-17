using UnityEngine;
using System.Collections;

public class DeathController : MonoBehaviour
{
    public AudioClip DeathSound;
    public GameObject UI;
    public GameObject GameOver;

	void Start ()
    {
	
	}
	

	void Update ()
    {
	
	}

    void Death()
    {
        GetComponent<AudioSource>().PlayOneShot(DeathSound);

        GetComponent<MoveCharacterController>().enabled = false;
        GetComponent<AttackController>().enabled = false;
        GetComponent<RotateCharacterController>().enabled = false;
        GetComponent<JumpCharacterController>().enabled = false;
        GetComponent<AmmunitionController>().enabled = false;
        GetComponent<ChangeWeapon>().enabled = false;
        GetComponent<TookItem>().enabled = false;

        UI.SetActive(false);
        GameOver.SetActive(true);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.01f);
        GetComponent<MoveCharacterController>().enabled = false;
        GetComponent<AttackController>().enabled = false;
        GetComponent<RotateCharacterController>().enabled = false;
        GetComponent<JumpCharacterController>().enabled = false;
        GetComponent<AmmunitionController>().enabled = false;
        GetComponent<ChangeWeapon>().enabled = false;
        GetComponent<TookItem>().enabled = false;
    }
}
