using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyMove : MonoBehaviour
{
    Grid grid;
    float Speed = 0.5f;
    public AudioClip MoveSound;
    private Transform Player;
	void Start()
    {
        Player = GameObject.Find("Hero").transform;
        grid = GameObject.Find("Grid").GetComponent<Grid>();
    }
	
	void Update ()
    {
	
	}

    void Move()
    {
        if (grid.path.Count > 2)
        {
            List<Node> MovePath = grid.path;
            RaycastHit hit;
            Vector3 Dir = Vector3.down * 200;
            Physics.Raycast(MovePath[1].WorldPosition, Dir, out hit, Mathf.Infinity, LayerMask.GetMask("Terrain"));

            Vector3 dir = new Vector3(MovePath[1].WorldPosition.x, hit.point.y + 1, MovePath[1].WorldPosition.z) - gameObject.transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 3.0f);
            if (GetComponent<AudioSource>().isPlaying == false)
            {
                GetComponent<AudioSource>().PlayOneShot(MoveSound);
            }
            GetComponent<Animation>().Play("walk");
            gameObject.transform.Translate(transform.forward * Speed * Time.deltaTime, Space.World);
        }
        else
        {
            if (Vector3.Distance(Player.position, gameObject.transform.position) < 1.5)
            {
                gameObject.SendMessage("Attack");
            }
            else
            {
                Vector3 dir = Player.position - gameObject.transform.position;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 3.0f);
                if (GetComponent<AudioSource>().isPlaying == false)
                {
                    GetComponent<AudioSource>().PlayOneShot(MoveSound);
                }
                GetComponent<Animation>().Play("walk");
                gameObject.transform.Translate(transform.forward * Speed * Time.deltaTime, Space.World);
            }
        }
   }

}
