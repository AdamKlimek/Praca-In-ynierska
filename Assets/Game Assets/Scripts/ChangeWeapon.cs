using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeWeapon : MonoBehaviour
{
    private Transform Weapons;
    private Image ActiveWeapon;
    private Image DeactiveWeaponLeft;
    private Image DeactiveWeaponRight;
    private Image Ammo;
    private Image FullMagazine;

    public Sprite[] Textures;
    void Start ()
    {
        ActiveWeapon = GameObject.Find("Equipment").transform.FindChild("ActiveWeapon").GetComponent<Image>();
        DeactiveWeaponLeft = GameObject.Find("Equipment").transform.FindChild("DeactiveWeaponLeft").GetComponent<Image>();
        DeactiveWeaponRight = GameObject.Find("Equipment").transform.FindChild("DeactiveWeaponRight").GetComponent<Image>();
        Ammo = GameObject.Find("Equipment").transform.FindChild("Ammonition").GetComponent<Image>();
        FullMagazine = GameObject.Find("Equipment").transform.FindChild("FullAmmo").GetComponent<Image>();

        ActiveWeapon.sprite = Textures[10];
        DeactiveWeaponLeft.sprite = Textures[11];
        DeactiveWeaponRight.sprite = Textures[12];
        Ammo.sprite = Textures[13];
        FullMagazine.sprite = Textures[14];

        Weapons = Camera.main.transform.FindChild("Weapons");
        Weapons.FindChild("PistolMakarov").gameObject.SetActive(true);
        Weapons.FindChild("Shotgun").gameObject.SetActive(false);
        Weapons.FindChild("M41").gameObject.SetActive(false);

    }
	
	void Update ()
    {
	    if(Input.GetKeyUp(KeyCode.Alpha1) == true && Weapons.FindChild("PistolMakarov").gameObject.activeInHierarchy == false)
        {
            Weapons.FindChild("PistolMakarov").gameObject.SetActive(true);
            Weapons.FindChild("Shotgun").gameObject.SetActive(false);
            Weapons.FindChild("M41").gameObject.SetActive(false);

            ActiveWeapon.sprite = Textures[10];
            DeactiveWeaponLeft.sprite = Textures[11];
            DeactiveWeaponRight.sprite = Textures[12];
            Ammo.sprite = Textures[13];
            FullMagazine.sprite = Textures[14];

            if (Hint.prompt.enabled == true)
            {
                SendMessage("TurnOFFHint");
            }
        }
        else if (Input.GetKeyUp(KeyCode.Alpha2) == true && Weapons.FindChild("Shotgun").gameObject.activeInHierarchy == false)
        {
            Weapons.FindChild("PistolMakarov").gameObject.SetActive(false);
            Weapons.FindChild("Shotgun").gameObject.SetActive(true);
            Weapons.FindChild("M41").gameObject.SetActive(false);

            ActiveWeapon.sprite = Textures[0];
            DeactiveWeaponLeft.sprite = Textures[1];
            DeactiveWeaponRight.sprite = Textures[2];
            Ammo.sprite = Textures[3];
            FullMagazine.sprite = Textures[4];

            if (Hint.prompt.enabled == true)
            {
                SendMessage("TurnOFFHint");
            }
        }
        else if (Input.GetKeyUp(KeyCode.Alpha3) == true && Weapons.FindChild("M41").gameObject.activeInHierarchy == false)
        {
            Weapons.FindChild("PistolMakarov").gameObject.SetActive(false);
            Weapons.FindChild("Shotgun").gameObject.SetActive(false);
            Weapons.FindChild("M41").gameObject.SetActive(true);

            ActiveWeapon.sprite = Textures[5];
            DeactiveWeaponLeft.sprite = Textures[6];
            DeactiveWeaponRight.sprite = Textures[7];
            Ammo.sprite = Textures[8];
            FullMagazine.sprite = Textures[9];

            if (Hint.prompt.enabled == true)
            {
                SendMessage("TurnOFFHint");
            }
        }
    }
}
