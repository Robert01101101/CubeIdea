using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLine : AbstractEnemy
{
    public enum MoveMode // your custom enumeration
    {
        Right,
        Up,
        Left,
        Down
    };
    public MoveMode moveMode = MoveMode.Right;
    Vector3 moveVector, origMoveVector;
    int clipMin, clipMax;

    // Start is called before the first frame update
    void Start()
    {
        switch (moveMode)
        {
            case MoveMode.Right:
                moveVector = new Vector3(1, 0, 0);
                clipMin = -2; clipMax = 2;
                break;
            case MoveMode.Up:
                moveVector = new Vector3(0, 0, 1);
                break;
            case MoveMode.Left:
                moveVector = new Vector3(-1, 0, 0);
                clipMin = -2; clipMax = 2;
                break;
            case MoveMode.Down:
                moveVector = new Vector3(0, 0, -1);
                break;
        }
        origMoveVector = moveVector;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Move()
    {
        //Reset after timeTravel
        if (moves.Count > 0) { if (moveVector != moves[moves.Count - 1]) moveVector = moves[moves.Count - 1]; } else { moveVector = origMoveVector; }

        Vector3 rotation = Vector3.zero;
        if (moveMode == MoveMode.Right || moveMode == MoveMode.Left) //Horizontal
        {
            //Switch direction
            if (transform.position.x <= clipMin || transform.position.x >= clipMax)
            {
                moveVector = -moveVector;
                rotation = new Vector3(0f, 180f, 0f);
            } 
        }
        else //Vertical
        {

        }

        StartCoroutine(AnimateGO(transform.position, transform.position + moveVector, .5f, rotation));
        moves.Add(moveVector);
        rotations.Add(rotation);
    }
}
