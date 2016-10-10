using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AmmunitionController : MonoBehaviour
{
    public int ShotgunMagazineAmount;
    public int M41MagazineAmount;
    public int PistolMakarovMagazineAmount;
    public int ShotgunAmmoInMagazine;
    public int M41AmmoInMagazine;
    public int PistolMakarovAmmoInMagazine;

    public static bool IsReloaded = true;

    private Transform Weapons;

    public Sprite[] Textures;
    public Image FirstNumber;
    public Image SecondNumber;
    public Image MagazineAmount;

    public AudioClip ShotgunReload;
    public AudioClip M41Reload;
    public AudioClip PistolMakarovReload;
    private int First, Second;

    void Start()
    {
        Weapons = Camera.main.transform.FindChild("Weapons");

        ShotgunAmmoInMagazine = 8;
        M41AmmoInMagazine = 90;
        PistolMakarovAmmoInMagazine = 12;
    }
	
	void Update ()
    {
        CheckAmmo();

        if(Input.GetKeyUp(KeyCode.R) == true)
        {
            Reload();
        }
	}

    void CheckAmmo()
    {
        GameObject Gun = GetWeapon();

        if(Gun.name == "Shotgun")
        {
            First = ShotgunAmmoInMagazine / 10;
            Second = ShotgunAmmoInMagazine % 10;
            CheckMagazine(ShotgunMagazineAmount);

            if (First == 0 && Second == 0 && ShotgunMagazineAmount > 0)
            {
                SendMessage("ShowHint", "[R]Reload");
            }
            else if (First == 0 && Second == 0 && M41MagazineAmount == 0)
            {
                SendMessage("ShowHint", "No Ammo - Switch to [1]PM or [3]M41");
            }
        }
        else if (Gun.name == "PistolMakarov" )
        {
            First = PistolMakarovAmmoInMagazine / 10;
            Second = PistolMakarovAmmoInMagazine % 10;
            CheckMagazine(PistolMakarovMagazineAmount);

            if (First == 0 && Second == 0 && PistolMakarovMagazineAmount > 0)
            {
                SendMessage("ShowHint", "[R]Reload");
            }
            else if (First == 0 && Second == 0 && M41MagazineAmount == 0)
            {
                SendMessage("ShowHint", "No Ammo - Switch to [2]Shotgun or [3]M41");
            }
        }
        else if (Gun.name == "M41")
        {
            First = M41AmmoInMagazine / 10;
            Second = M41AmmoInMagazine % 10;
            CheckMagazine(M41MagazineAmount);

            if (First == 0 && Second == 0 && M41MagazineAmount > 0)
            {
                SendMessage("ShowHint", "[R]Reload");
            }
            else if(First == 0 && Second == 0 && M41MagazineAmount == 0)
            {
                SendMessage("ShowHint", "No Ammo - Switch to [1]PM or [2]Shotgun");
            }
        }

       
        FirstNumber.sprite = Textures[First];
        SecondNumber.sprite = Textures[Second];
    }

    void Reload()
    {
        IsReloaded = false;
        GameObject Gun = GetWeapon();

        if (Gun.name == "Shotgun")
        {
            if (ShotgunMagazineAmount > 0)
            {
                GetComponent<AudioSource>().PlayOneShot(ShotgunReload);
                StartCoroutine(ReloadShotgun());
            }
        }
        else if (Gun.name == "PistolMakarov")
        {
            if (PistolMakarovMagazineAmount > 0)
            {
                GetComponent<AudioSource>().PlayOneShot(PistolMakarovReload);
                StartCoroutine(ReloadPM());
            }
        }
        else if (Gun.name == "M41")
        {
            if (M41MagazineAmount > 0)
            {
                GetComponent<AudioSource>().PlayOneShot(M41Reload);
                StartCoroutine(ReloadM41());
            }
        }
    }

    GameObject GetWeapon()
    {
        GameObject Gun = Weapons.GetChild(0).gameObject;

        for (int i = 0; i < Weapons.childCount; i++)
        {
            Gun = Weapons.GetChild(i).gameObject;
            if (Gun.activeInHierarchy)
            {
                break;
            }
        }

        return Gun;
    }

    IEnumerator ReloadShotgun()
    {
        yield return new WaitForSeconds(5);
        ShotgunAmmoInMagazine = 8;
        ShotgunMagazineAmount--;
        IsReloaded = true;

        if (Hint.prompt.enabled == true)
        {
            SendMessage("TurnOFFHint");
        }
    }

    IEnumerator ReloadPM()
    {
        yield return new WaitForSeconds(2);
        PistolMakarovAmmoInMagazine = 12;
        PistolMakarovMagazineAmount--;
        IsReloaded = true;

        if (Hint.prompt.enabled == true)
        {
            SendMessage("TurnOFFHint");
        }
    }

    IEnumerator ReloadM41()
    {
        yield return new WaitForSeconds(3);
        M41AmmoInMagazine = 90;
        M41MagazineAmount--;
        IsReloaded = true;

        if (Hint.prompt.enabled == true)
        {
            SendMessage("TurnOFFHint");
        }
    }

    void CheckMagazine( int number)
    {
        MagazineAmount.sprite = Textures[number];


    }
}
