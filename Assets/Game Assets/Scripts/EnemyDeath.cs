using UnityEngine;
using System.Collections;

public class EnemyDeath : MonoBehaviour
{
    public int BodyPart;
    public AudioClip DeadSound;

    void Start ()
    {
	
	}
	
	void Update ()
    {
	
	}

    void Death()
    {
        if(BodyPart == 10)
        {
            if(GetComponent<Animation>().IsPlaying("walk") || GetComponent<Animation>().IsPlaying("attack") )
            {
                GetComponent<Animation>().Stop();
            }
            GetComponent<AudioSource>().PlayOneShot(DeadSound);
            GetComponent<Animation>().Play("back_fall");
            GetComponent<EnemyMove>().enabled = false;
            GetComponent<EnemyAttack>().enabled = false;
            GetComponent<AStarPathfinding>().enabled = false;
            GetComponent<GreedyBestFirstSearch>().enabled = false;
            GetComponent<BreathFirstSearch>().enabled = false;
        }
        else if(BodyPart == 11)
        {
            if (GetComponent<Animation>().IsPlaying("walk") || GetComponent<Animation>().IsPlaying("attack"))
            {
                GetComponent<Animation>().Stop();
               
            }
            GetComponent<AudioSource>().PlayOneShot(DeadSound);
            GetComponent<Animation>().Play("left_fall");
            GetComponent<EnemyMove>().enabled = false;
            GetComponent<EnemyAttack>().enabled = false;
            GetComponent<AStarPathfinding>().enabled = false;
            GetComponent<GreedyBestFirstSearch>().enabled = false;
            GetComponent<BreathFirstSearch>().enabled = false;
        }
        else if(BodyPart == 12)
        {
            if (GetComponent<Animation>().IsPlaying("walk") || GetComponent<Animation>().IsPlaying("attack"))
            {
                GetComponent<Animation>().Stop();
            }
            GetComponent<AudioSource>().PlayOneShot(DeadSound);
            GetComponent<Animation>().Play("right_fall");
            GetComponent<EnemyMove>().enabled = false;
            GetComponent<EnemyAttack>().enabled = false;
            GetComponent<AStarPathfinding>().enabled = false;
            GetComponent<GreedyBestFirstSearch>().enabled = false;
            GetComponent<BreathFirstSearch>().enabled = false;
        }

        SendMessage("DropAmmo");
        ZombieCounter.ZombieAmount++;
        if(SpawnEnemy.ZombieCounter > 0)
        {
            SpawnEnemy.ZombieCounter--;
        }
        Destroy(gameObject, 2.0f);
    }
}
