using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public float HealthPoints = 100.0f;
    public Image HealthBar;
    private bool IsDead = false;

    void Start ()
    {
    }
	
	void Update ()
    {
        if(HealthPoints < 0)
        {
            HealthPoints = 0.0f;
            if(gameObject.CompareTag("Enemy") == true && IsDead == false)
            {
                gameObject.SendMessage("Death");
                IsDead = true;
            }
            else if(gameObject.CompareTag("Player") == true)
            {
                gameObject.SendMessage("Death");
                Time.timeScale = 0;
                //GameObject.Find("Enemy").GetComponent<EnemyMove>().enabled = false;
                //GameObject.Find("Enemy").GetComponent<EnemyAttack>().enabled = false;
                //GameObject.Find("Enemy").GetComponent<AStarPathfinding>().enabled = false;
                //GameObject.Find("Enemy").GetComponent<GreedyBestFirstSearch>().enabled = false;
                //GameObject.Find("Enemy").GetComponent<BidirectionalBreathFirstSearch>().enabled = false;
            }
        }
        CheckHealth();
    }

    void CheckHealth()
    {
        HealthBar.rectTransform.localScale = new Vector3(HealthPoints / 100, HealthBar.rectTransform.localScale.y, HealthBar.rectTransform.localScale.z);
    }

}
