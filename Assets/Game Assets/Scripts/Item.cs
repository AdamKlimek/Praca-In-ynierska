using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour
{
    private float RotationSpeed = 100.0f;
    public AudioClip Clip;

    void Start ()
    {
	}
	
	void Update ()
    {
        transform.Rotate(0, RotationSpeed * Time.deltaTime, 0, Space.World);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if( (gameObject.tag == "ShotgunAmmo" && col.gameObject.GetComponent<AmmunitionController>().ShotgunMagazineAmount < 9) || (gameObject.tag == "PMAmmo" && col.gameObject.GetComponent<AmmunitionController>().PistolMakarovMagazineAmount < 9) || (gameObject.tag == "M41Ammo" && col.gameObject.GetComponent<AmmunitionController>().M41MagazineAmount < 9) )
            {
                GetComponent<AudioSource>().PlayOneShot(Clip);
                StartCoroutine(Wait());
                col.gameObject.SendMessage("AmmoPickup", gameObject.tag);

            }
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
