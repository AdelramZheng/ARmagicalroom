    using UnityEngine;
using System.Collections;

public class CubeMove : MonoBehaviour
{
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
        if (transform.localPosition.y < 0f)
        {
            transform.Translate(0, Time.deltaTime * 0.8f, 0);
            transform.Rotate(transform.up, Time.deltaTime * 15);
        }
       
	}


}
