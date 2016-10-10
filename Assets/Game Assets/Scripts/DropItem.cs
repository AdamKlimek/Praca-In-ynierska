using UnityEngine;
using System.Collections;

public class DropItem : MonoBehaviour
{
    private float AmmoDropRate;
    public GameObject PistolMakarovAmmo;
    public GameObject M41Ammo;
    public GameObject ShotgunAmmo;

    void Start ()
    {
	
	}
	
	void Update ()
    {
	
	}

    void DropAmmo()
    {
        GameObject Ammo;
        Quaternion rotation;

        AmmoDropRate = Random.Range(1, 100);

        if (AmmoDropRate < 60)
        {
            Ammo = PistolMakarovAmmo;
            rotation = PistolMakarovAmmo.transform.rotation;
        }
        else if (AmmoDropRate >= 60 && AmmoDropRate < 80)
        {
            Ammo = M41Ammo;
            rotation = M41Ammo.transform.rotation;
        }
        else
        {
            Ammo = ShotgunAmmo;
            rotation = ShotgunAmmo.transform.rotation;
        }
        Instantiate(Ammo, gameObject.transform.position + Vector3.up * 0.3f, rotation);

    }
}
