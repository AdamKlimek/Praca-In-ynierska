using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject Zombie;
    private int ZombieLimit = 3;
    public static int ZombieCounter = 0;
    private GameObject  ActiveSpawnPoint;
    private float DistanceToNearestSpawnPoint = Mathf.Infinity;
    public int ActiveSpawnPointsAmount = 3;
    private float TimeToSpawnEnemy = 40.0f;
    private List<GameObject> SpawnPointList;
    private List<GameObject> NearestSpawnPointList;

    void Start ()
    {
        SpawnPointList = new List<GameObject>();
        NearestSpawnPointList = new List<GameObject>();
        SpawnPointList = GameObject.FindGameObjectsWithTag("SpawnPoint").ToList();
    }
	
	void Update ()
    {
        TimeToSpawnEnemy += Time.deltaTime;
        if (ZombieCounter < ZombieLimit)
        {
            if (TimeToSpawnEnemy > 15.0f)
            {
                SpawnZombie();
                TimeToSpawnEnemy = 0.0f;
            }
        }
        SpawnPointList = SpawnPointList.Concat(NearestSpawnPointList).ToList();
        NearestSpawnPointList.Clear();
    }

    void SpawnZombie()
    {
        for (int i = 0; i < ActiveSpawnPointsAmount; i++)
        {
            foreach (GameObject SpawnPoint in SpawnPointList)
            {
                float Distance = Vector3.Distance(SpawnPoint.transform.position, gameObject.transform.position);

                if (Distance < DistanceToNearestSpawnPoint)
                {
                    DistanceToNearestSpawnPoint = Distance;
                    ActiveSpawnPoint = SpawnPoint;
                }
            }

            SpawnPointList.Remove(ActiveSpawnPoint);
            NearestSpawnPointList.Add(ActiveSpawnPoint);
            ActiveSpawnPoint = null;
            DistanceToNearestSpawnPoint = Mathf.Infinity;

            if (ZombieCounter < ZombieLimit)
            {
                Instantiate(Zombie, NearestSpawnPointList[i].transform.position, NearestSpawnPointList[i].transform.rotation);
                ZombieCounter++;
            }
        }
    }
 }