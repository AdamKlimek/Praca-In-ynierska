using UnityEngine;
using System.Collections;

public class TookItem : MonoBehaviour
{
    

    void Start ()
    {
       
    }
	
	void Update ()
    {
	    
	}

    void AmmoPickup(string Ammo)
    {
        if(Ammo == "ShotgunAmmo")
        {
            GetComponent<AmmunitionController>().ShotgunMagazineAmount++;
        }
        else if(Ammo == "M41Ammo")
        {
            GetComponent<AmmunitionController>().M41MagazineAmount++;
        }
        else
        {
            GetComponent<AmmunitionController>().PistolMakarovMagazineAmount++;
        }
    }

}
