using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.CrashReportHandler;

public class Drag : MonoBehaviour
{
    float offsetX;
    float offsetY;
    float offsetZ;

    float gridSize = 5f;

    Ray ray;
    RaycastHit hit;
    //public LayerMask gridLayer;

    PlayerController playerController;
    ////public NavMeshSurface[] surfaces;

    //NavMeshAgent agent;
    //NavMeshPath path;

    GameObject originalGridSlot, currentGridSlot;

    //int moveCount = 0;
    bool isHovering;

    public void OnMouseDown()
    {
        // to prevent drag once path is found
        if (GameManager.instance.levelComplete)
        {
            return;
        }

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 150, LayerMask.GetMask("Grid")))
        {
            // Set this game object as the starting point of the move
            originalGridSlot = hit.collider.gameObject;

            // Get the distance between the mouse click position and the transform position
            offsetX = hit.point.x - transform.position.x;
            offsetY = transform.position.y; // the object should not move around the Y-axis
            offsetZ = hit.point.z - transform.position.z;
        }
    }

    public void OnMouseDrag()
    {
        // to prevent drag once path is found
        if (GameManager.instance.levelComplete)
        {
            //if (isHovering)
            //{
            //    transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
            //    isHovering = false;
            //}
            return;
        }        

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 150, LayerMask.GetMask("Grid")))
        {
            // to snap the block to the grid
            GameObject gridSlot = hit.collider.gameObject;
            if (!gridSlot.GetComponent<GridController>().isOccupied)
            {
                if (Math.Abs(gridSlot.transform.position.x - (hit.point.x - offsetX)) <= 2f &&
                    Math.Abs(gridSlot.transform.position.z - (hit.point.z - offsetZ)) <= 2f)
                {
                    // check if the current grid is adjacent to the original grid and prevent diagonal movement
                    if ((Math.Abs(gridSlot.transform.position.x - originalGridSlot.transform.position.x) == gridSize &&
                        Math.Abs(gridSlot.transform.position.z - originalGridSlot.transform.position.z) == 0f) ||
                        (Math.Abs(gridSlot.transform.position.z - originalGridSlot.transform.position.z) == gridSize &&
                        Math.Abs(gridSlot.transform.position.x - originalGridSlot.transform.position.x) == 0f))
                    {
                        // match the block's position to the grid's position
                        transform.position = new Vector3(gridSlot.transform.position.x, offsetY, gridSlot.transform.position.z);
                        currentGridSlot = gridSlot;
                        if (currentGridSlot != originalGridSlot)
                        {
                            GameObject.FindObjectOfType<AudioManager>().Play("Block Move");
                            currentGridSlot.GetComponent<GridController>().isOccupied = true;
                            originalGridSlot.GetComponent<GridController>().isOccupied = false;
                            originalGridSlot = currentGridSlot;
                            Stats.instance.moves++;
                            playerController.CheckPath();
                        }
                    }
                }
                else
                {
                    // Set the new position of the object based on the offset
                    //transform.position = new Vector3(hit.point.x - offsetX, offsetY, hit.point.z - offsetZ);
                }
            }
        }
        else
        {
            CrashReportHandler.SetUserMetadata("Drag", "ErrorOnDrag");
            throw new Exception("Invalid drag");
        }
    }

    public void OnMouseOver()
    {
        // to prevent drag once path is found
        if (GameManager.instance.levelComplete)
        {
            if (isHovering)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
                isHovering = false;
            }
            return;
        }
        if (!isHovering && !Input.GetMouseButton(0))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
            isHovering = true;
        }
    }

    public void OnMouseExit()
    {
        if (isHovering && !Input.GetMouseButton(0))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
            isHovering = false;
        }
    }

    public void OnMouseUp()
    {
        if (isHovering)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
            isHovering = false;
        }
        //if (currentGridSlot)
        //    currentGridSlot.GetComponent<GridController>().isOccupied = true;
        //if (originalGridSlot)
        //    originalGridSlot.GetComponent<GridController>().isOccupied = false;
        //playerController.CheckPath();

        //ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //if (Physics.Raycast(ray, out hit, 150, LayerMask.GetMask("Grid")))
        //{
        //    //print("true");
        //    GameObject gridSlot = hit.collider.gameObject;
        //    //Debug.Log("GridName: " + gridSlot.name);
        //    //Debug.Log("Gridslot: " + gridSlot.transform.position);
        //    //Debug.Log("Object: " + transform.position);
        //    if (Math.Abs(gridSlot.transform.position.x - transform.position.x) <= 1f &&
        //        Math.Abs(gridSlot.transform.position.z - transform.position.z) <= 1f)
        //    {
        //        transform.position = new Vector3(gridSlot.transform.position.x, offsetY, gridSlot.transform.position.z);
        //        moveCount++;
        //        Stats.moves++;
        //        //GameManager.instance.ShowHUDScore();
        //        gridSlot.GetComponent<GridController>().isOccupied = true;
        //        if (originalGridSlot)
        //            originalGridSlot.GetComponent<GridController>().isOccupied = false;
        //    }
        //    else
        //    {
        //        // if the mouse is placed at an adjacent grid but does not 
        //    }

        //    playerController.CheckPath();
        //    //CheckIfPathExists();
        //}
    }

    private void CheckIfPathExists()
    {
        //NavMeshPath path = new NavMeshPath();
        //agent.CalculatePath(agent.destination, path);
        //Debug.Log("Path: " + path.ToString());
        //if (path.status == NavMeshPathStatus.PathComplete)
        //{
        //    agent.speed = 3f;
        //}

        //for (int i = 0; i < surfaces.Length; i++)
        //{
        //    surfaces[i].BuildNavMesh();
        //}
        //if (NavMesh.CalculatePath(agent.transform.position, GameObject.FindGameObjectWithTag("Finish").transform.position, NavMesh.AllAreas, path))
        //{
        //    Debug.Log("Path: " + path.status);
        //    for (int i = 0; i < path.corners.Length - 1; i++)
        //    {
        //        Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red, 15f);
        //    }
        //    if (path.status == NavMeshPathStatus.PathComplete)
        //    {
        //        agent.speed = 3f;
        //        Debug.Log("No of moves: " + moveCount);
        //    }
        //}
    }

    // Start is called before the first frame update
    void Start()
    {
        //agent = GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>();
        //agent.SetDestination(GameObject.FindGameObjectWithTag("Finish").transform.position);
        //agent.speed = 0;
        //path = new NavMeshPath();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        isHovering = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
