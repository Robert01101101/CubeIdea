using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerCtrl : MonoBehaviour
{
    Rigidbody rb;
    HingeJoint hj;
    bool moving;
    float h = 99999;
    int moveCount = 2;
    int curMove;
    int flipCount = 0;
    Vector3 lastDir = Vector3.forward;
    public PlayerCollider [] playerColliders = new PlayerCollider [4];
    public GameObject colliderParent;
    private List<Vector3> moves = new List<Vector3>();
    private int timeTravelCount = 0;
    private bool curTimeTravel = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        hj = GetComponent<HingeJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool initiateMove(Vector3 dir)
    {
        bool timeTravel = false;
        if (!moving)
        {
            if (moves.Count > 0)
            {
                //Check for Timetravel
                if (dir + moves[moves.Count - 1] == Vector3.zero)
                {
                    timeTravel = true;
                } else
                {
                    // No Timetravel
                    timeTravel = false;
                }
            }
        }
        bool objectBlockingPath = false;
        for (int i = 0; i < playerColliders.Length; i++)
        {
            if (playerColliders[i].IsObjectInTrigger()) objectBlockingPath = true;
        }
        if (true)//if (!objectBlockingPath)
        {
            if (dir != Vector3.forward)
            {
                Destroy(hj);
                if (dir == Vector3.back)
                {
                    if (lastDir == Vector3.forward)
                    {
                        transform.Rotate(0f, 180f, 0f, Space.World);
                    }
                    else if (lastDir == Vector3.right)
                    {
                        transform.Rotate(0f, 90f, 0f, Space.World);
                    }
                    else if (lastDir == Vector3.left)
                    {
                        transform.Rotate(0f, -90f, 0f, Space.World);
                    }
                }
                else if (dir == Vector3.right)
                {
                    if (lastDir == Vector3.forward)
                    {
                        transform.Rotate(0f, 90f, 0f, Space.World);
                    }
                    else if (lastDir == Vector3.back)
                    {
                        transform.Rotate(0f, -90f, 0f, Space.World);
                    }
                    else if (lastDir == Vector3.left)
                    {
                        transform.Rotate(0f, 180f, 0f, Space.World);
                    }
                }
                else if (dir == Vector3.left)
                {
                    if (lastDir == Vector3.forward)
                    {
                        transform.Rotate(0f, -90f, 0f, Space.World);
                    }
                    else if (lastDir == Vector3.back)
                    {
                        transform.Rotate(0f, 90f, 0f, Space.World);
                    }
                    else if (lastDir == Vector3.right)
                    {
                        transform.Rotate(0f, 180f, 0f, Space.World);
                    }
                }
                hj = gameObject.AddComponent<HingeJoint>();
            }
            else
            {
                if (lastDir != Vector3.forward)
                {
                    Destroy(hj);
                    if (lastDir == Vector3.back)
                    {
                        transform.Rotate(0f, 180f, 0f, Space.World);
                    }
                    else if (lastDir == Vector3.right)
                    {
                        transform.Rotate(0f, -90, 0f, Space.World);
                    }
                    else if (lastDir == Vector3.left)
                    {
                        transform.Rotate(0f, 90f, 0f, Space.World);
                    }
                    hj = gameObject.AddComponent<HingeJoint>();
                }
            }
            if (timeTravel)
            {
                moves.RemoveAt(moves.Count - 1);
            } else
            {
                if (timeTravel != curTimeTravel)
                {
                    moves.Clear();
                }
                moves.Add(dir);
            }
            //Debug.Log("Initiating Move in direction: " + dir);
            curTimeTravel = timeTravel;
            Vector3 anchorPos = calculateHingePosition();
            hj.anchor = anchorPos;
            lastDir = dir;

            moving = true;
            curMove = 0;
        }
        if (timeTravel) {timeTravelCount++; Debug.Log("Reversing: Traveling through time. TimeTravel Count:" + timeTravelCount); }
        return timeTravel;
    }

    private Vector3 calculateHingePosition()
    {
        Vector3 output;
        switch (flipCount)
        {
            case 0:
                output = new Vector3(0, -0.5f, 0.5f);
                break;
            case 1:
                output = new Vector3(0, 0.5f, 0.5f);
                break;
            case 2:
                output = new Vector3(0, 0.5f, -0.5f);
                break;
            case 3:
                output = new Vector3(0, -0.5f, -0.5f);
                break;
            default:
                output = new Vector3(0, -0.5f, 0.5f);
                break;
        }
        flipCount++;
        if (flipCount == 4) flipCount = 0;
        return output;
    }



    void FixedUpdate()
    {
        if (moving)
        {
            rb.AddTorque(transform.right * h, ForceMode.Impulse);
            curMove++;
            if (curMove > moveCount)
            {
                moving = false;
            }
        }
    }

    public void TickEnd()
    {
        colliderParent.transform.rotation = Quaternion.identity;
    }

    
}
