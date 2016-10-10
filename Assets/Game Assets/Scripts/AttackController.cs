using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AttackController : MonoBehaviour
{
    private float Damage;
    public GameObject Bullet;
    public GameObject BulletShell;
    public GameObject Enemy;
    private float BulletSpeed = 50.0f;
    private float ShellSpeed = 2.0f;
    private float RayLengh = 50.0f;
    private GameObject Gun;
    private Transform Weapons;
    public AudioClip PMShootSound;
    public AudioClip M41ShootSound;
    public AudioClip ShotgunShootSound;
    private float TimeToDisapear;
    private float TimeToNextShoot;

    void Start ()
    {
        Weapons = Camera.main.transform.FindChild("Weapons");
        Gun = ChosenGun();
    }
	
	void Update ()
    {
        Gun = ChosenGun();
        TimeToDisapear += Time.deltaTime;
        TimeToNextShoot += Time.deltaTime;
        if (TimeToDisapear > 0.5f && Gun.transform.FindChild("FlashGun").GetChild(0).GetComponent<Image>().enabled == true)
        {
            Gun.transform.FindChild("FlashGun").GetChild(0).GetComponent<Image>().enabled = false;
            TimeToDisapear = 0.0f;
        }

        if (Input.GetKey(KeyCode.Mouse0) == true)
        {
            if (Gun.name == "Shotgun" && TimeToNextShoot > 2.0f && GetComponent<AmmunitionController>().ShotgunAmmoInMagazine > 0  && AmmunitionController.IsReloaded == true)
            {
                Gun.transform.FindChild("FlashGun").GetChild(0).GetComponent<Image>().enabled = true;
                Attack();
                GetComponent<AmmunitionController>().ShotgunAmmoInMagazine--;
                ShootAudio();
                TimeToNextShoot = 0.0f;
            }
            else if (Gun.name == "PistolMakarov" && TimeToNextShoot > 0.7f && GetComponent<AmmunitionController>().PistolMakarovAmmoInMagazine > 0 && AmmunitionController.IsReloaded == true)
            {
                Gun.transform.FindChild("FlashGun").GetChild(0).GetComponent<Image>().enabled = true;
                Attack();
                GetComponent<AmmunitionController>().PistolMakarovAmmoInMagazine--;
                ShootAudio();
                TimeToNextShoot = 0.0f;
            }
            else if (Gun.name == "M41" && TimeToNextShoot > 0.1f  && GetComponent<AmmunitionController>().M41AmmoInMagazine > 0 && AmmunitionController.IsReloaded == true)
            {
                Gun.transform.FindChild("FlashGun").GetChild(0).GetComponent<Image>().enabled = true;
                Attack();
                GetComponent<AmmunitionController>().M41AmmoInMagazine--;
                ShootAudio();
                TimeToNextShoot = 0.0f;
            }
            
        }
	}

    void Attack()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray,out hit,RayLengh) )
        {
            if (hit.collider.gameObject.tag =="Enemy")
            {
                float  Distance = Vector3.Distance(transform.position, hit.collider.transform.position);
                RandomizationDamage(hit.collider.gameObject.layer , Distance);

                hit.collider.transform.parent.gameObject.GetComponent<HealthController>().HealthPoints -= Damage;
                hit.collider.transform.parent.gameObject.GetComponent<EnemyDeath>().BodyPart = hit.collider.gameObject.layer;
            }
        }

        GameObject BulletClone = (GameObject)Instantiate(Bullet, Gun.transform.FindChild("SpawnPoint").transform.position, Gun.transform.FindChild("SpawnPoint").transform.rotation);
        GameObject Shell = (GameObject)Instantiate(BulletShell, Gun.transform.FindChild("SpawnPoint").transform.position, Gun.transform.FindChild("SpawnPoint").transform.rotation);
        BulletClone.GetComponent<Rigidbody>().velocity = Gun.transform.FindChild("SpawnPoint").transform.forward * BulletSpeed;
        Shell.GetComponent<Rigidbody>().velocity = Gun.transform.FindChild("SpawnPoint").transform.right * -ShellSpeed;
        Destroy(BulletClone.gameObject, 0.5f);
        Destroy(Shell.gameObject, 0.5f);
    }

    void RandomizationDamage(int BodyPart , float Distance)
    {

        if (Distance < 10) //blisko
        {
            if (BodyPart == 10) //glowa
            {
                if (Gun.name == "Shotgun")
                {
                    Damage = Random.Range(80.0f, 100.0f);
                }
                else if (Gun.name == "PistolMakarov")
                {
                    Damage = Random.Range(20.0f, 30.0f);
                }
                else if (Gun.name == "M41")
                {
                    Damage = Random.Range(25.0f, 40.0f);
                }
            }
            else if (BodyPart == 11 || BodyPart == 12) // reszta ciala
            {
                if (Gun.name == "Shotgun")
                {
                    Damage = Random.Range(30.0f, 50.0f);
                }
                else if (Gun.name == "PistolMakarov")
                {
                    Damage = Random.Range(10.0f, 20.0f);
                }
                else if (Gun.name == "M41")
                {
                    Damage = Random.Range(10.0f, 25.0f);
                }
            }
        }
        else //daleko
        {
            if (BodyPart == 10) //glowa
            {
                if (Gun.name == "Shotgun")
                {
                    Damage = Random.Range(20.0f, 30.0f);
                }
                else if (Gun.name == "PistolMakarov")
                {
                    Damage = Random.Range(10.0f, 15.0f);
                }
                else if (Gun.name == "M41")
                {
                    Damage = Random.Range(10.0f, 20.0f);
                }
            }
            else if (BodyPart == 11 || BodyPart == 12) // reszta ciala
            {
                if (Gun.name == "Shotgun")
                {
                    Damage = Random.Range(10.0f, 20.0f);
                }
                else if (Gun.name == "PistolMakarov")
                {
                    Damage = Random.Range(5.0f, 10.0f);
                }
                else if (Gun.name == "M41")
                {
                    Damage = Random.Range(5.0f, 15.0f);
                }
            }
        }
    }

    GameObject ChosenGun()
    {
        GameObject Gun = Weapons.GetChild(0).gameObject;

        for (int i = 0; i < Weapons.childCount; i++)
        {
            Gun = Weapons.GetChild(i).gameObject;
            if(Gun.activeInHierarchy)
            {
                break;
            }
        }

        return Gun;
    }

    void ShootAudio()
    {
        if (Gun.name == "Shotgun")
        {
            Gun.GetComponent<AudioSource>().PlayOneShot(ShotgunShootSound);
        }
        else if (Gun.name == "PistolMakarov")
        {
            Gun.GetComponent<AudioSource>().PlayOneShot(PMShootSound);
        }
        else if (Gun.name == "M41")
        {
            Gun.GetComponent<AudioSource>().PlayOneShot(M41ShootSound);
        }
    }

}
