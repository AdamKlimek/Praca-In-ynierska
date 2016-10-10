using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    private GameObject Player;
    public AudioClip AttackSound;
    public AudioClip HurtSound;
    float Damage = 20.0f;

    void Start ()
    {
        Player = GameObject.Find("Hero");
    }
	
	
	void Update ()
    {
	
	}

    void Attack()
    {
        if (GetComponent<Animation>().IsPlaying("walk"))
        {
            GetComponent<Animation>().Stop();
            GetComponent<AudioSource>().Stop();
        }

        if (GetComponent<Animation>().isPlaying == false)
        {
            GetComponent<AudioSource>().PlayOneShot(AttackSound);
            GetComponent<Animation>().Play("attack");
            Player.GetComponent<HealthController>().HealthPoints -= Damage;
            Player.GetComponent<AudioSource>().PlayOneShot(HurtSound);
        }
        
    }
}
