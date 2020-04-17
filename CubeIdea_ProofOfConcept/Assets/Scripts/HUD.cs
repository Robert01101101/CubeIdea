using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public WorldCtrl worldCtrl;
    public GameObject timeTravelOverlay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TimeTravelStart()
    {
        timeTravelOverlay.SetActive(true);
    }

    public void TimeTravelEnd()
    {
        timeTravelOverlay.SetActive(false);
    }
}
