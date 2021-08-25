using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchCube : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider otherObj)
    {
        Debug.Log("touched the cube!");
        AdjustScore.AddScore();
        Destroy(gameObject);
    }
}
