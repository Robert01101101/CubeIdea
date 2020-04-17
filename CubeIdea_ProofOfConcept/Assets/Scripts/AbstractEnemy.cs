using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEnemy : MonoBehaviour
{
    protected bool travelingThroughTime = false;
    protected List<Vector3> moves = new List<Vector3>();
    protected List<Vector3> rotations = new List<Vector3>();
    WorldCtrl worldCtrl;


    // Start is called before the first frame update
    void Start()
    {
        worldCtrl = GameObject.Find("_WorldCtrl").GetComponent<WorldCtrl>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void EnemyTick(bool timeTravel) {
        travelingThroughTime = timeTravel;
        if (timeTravel)
        {
            StartCoroutine(AnimateGO(transform.position, transform.position - moves[moves.Count - 1], .5f, -rotations[rotations.Count - 1]));
            moves.RemoveAt(moves.Count - 1);
            rotations.RemoveAt(rotations.Count - 1);
        } else
        {
            Move();
        }
    }

    protected abstract void Move();

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "_Player") worldCtrl.GameOver();
    }

    protected IEnumerator AnimateGO(Vector3 aFromPos, Vector3 aToPos, float aDuration, Vector3 rotation)
    {
        if (!travelingThroughTime) transform.Rotate(rotation.x, rotation.y, rotation.z, Space.World);
        // Assuming we draw at 30fps, how many animation steps do we need?
        float tSteps = Mathf.Ceil(aDuration * 30.0f);

        // What is the time to pause per step?
        float tStepDuration = aDuration / tSteps;

        // What is the Lerp amount per step
        float tStepSize = 1.0f / tSteps;

        // Walk each step
        for (int i = 0; i < tSteps; i++)
        {

            // Move to the new position
            float tLerpVal = i * tStepSize;
            transform.localPosition = Vector3.Lerp(aFromPos, aToPos, tLerpVal);

            // Hang out for this step's duration
            yield return new WaitForSeconds(tStepDuration);

        }

        // Set in final position
        transform.localPosition = aToPos;
        if (travelingThroughTime) transform.Rotate(rotation.x, rotation.y, rotation.z, Space.World);
    }
}
