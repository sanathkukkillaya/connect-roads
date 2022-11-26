using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    Vector3 finishPosition;
    NavMeshPath path;
    public NavMeshSurface surface;
    NavMeshAgent agent;
    public GameObject fireworks;
    public GameObject smoke;
    public GameObject[] cars;

    // Start is called before the first frame update
    void Start()
    {
        surface.BuildNavMesh(); // Build the mesh once on scene load. This is required for SetDestination and RemmoveDestination
        agent = GetComponent<NavMeshAgent>();
        finishPosition = GameObject.FindGameObjectWithTag("Finish").transform.position;
        agent.SetDestination(finishPosition);
        agent.speed = 0f;        
        path = new NavMeshPath();

        GameObject carPrefab = cars[0];
        if(PlayerPrefs.HasKey("PlayerCar"))
        {
            switch(PlayerPrefs.GetString("PlayerCar"))
            {
                case "Red Car":
                    carPrefab = cars[0];
                    break;
                case "Yellow Car":
                    carPrefab = cars[1];
                    break;
                case "White Super Car":
                    carPrefab = cars[2];
                    break;
                case "Blue Car":
                    carPrefab = cars[3];
                    break;
                case "White Car":
                    carPrefab = cars[4];
                    break;
                case "Black Car":
                    carPrefab = cars[5];
                    break;
                case "Black Striped Car":
                    carPrefab = cars[6];
                    break;
                case "White Sports Car":
                    carPrefab = cars[7];
                    break;
            }
        }
        Instantiate(carPrefab, gameObject.transform);
    }

    public void CheckPath()
    {
        surface.BuildNavMesh(); // Rebuild the Navmesh

        if (NavMesh.CalculatePath(agent.transform.position, finishPosition, NavMesh.AllAreas, path))
        {
            //Debug.Log("Path: " + path.status);
            //for (int i = 0; i < path.corners.Length - 1; i++)
            //{
            //    Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red, 15f);
            //}
            if (path.status == NavMeshPathStatus.PathComplete)
            {
                GameManager.instance.levelComplete = true;
                FindObjectOfType<AudioManager>().Play("Car Start");
    
                Quaternion smokeRotation = Quaternion.LookRotation(-gameObject.transform.forward);
                //smokeRotation.x += 180;
                GameObject smokeParticle = Instantiate(smoke, gameObject.transform.position, smokeRotation, gameObject.transform);
                agent.speed = 5f;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance == 0)
        {
            //Instantiate(fireworks, new Vector3(transform.position.x + 2.5f, transform.position.y, transform.position.z), transform.rotation);
            //Instantiate(fireworks, new Vector3(transform.position.x - 2.5f, transform.position.y,  transform.position.z), transform.rotation);
            GameManager.instance.LevelCompleted();
        }
    }

    public void SavePlayer()
    {

    }

    public void LoadPlayer()
    {
        PlayerData playerData = SaveSystem.LoadPlayerData();

        Debug.Log("Player Car: " + playerData.playerCar);
    }
}
