using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ZombieCounter : MonoBehaviour
{
    public static int ZombieAmount;
    //public Text Counter;
    public Image First;
    public Image Second;
    public Image Third;
    public Sprite[] Textures;

    void Start ()
    {
        First.sprite = Textures[0];
        Second.sprite = Textures[0];
        Third.sprite = Textures[0];
    }
	
	void Update ()
    {
        First.sprite = Textures[ZombieAmount/100];
        Second.sprite = Textures[(ZombieAmount%100)/10];
        Third.sprite = Textures[ZombieAmount%10];

    }
}
