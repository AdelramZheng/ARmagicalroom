using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour {
    public Dropdown myDrop;
    // Use this for initialization
    void Start () {
        myDrop.value = 2;
    }
	
	// Update is called once per frame
	void Update () {
        switch (myDrop.value)
        {
            case 0:
                EnterScene();
                break;
            case 1:
                diyhouse();
                break;
        }

    }
    public void EnterScene()
    {
        Application.LoadLevel("insideroom");
    }
    public void ReturnScene()
    {
        Application.LoadLevel("AR_Scene");
   }
        public void House()
    {
        Application.LoadLevel("AR_Scene");
    }
        public void diyhouse()
    {
        Application.LoadLevel("SimplifiedARmagicalroom");
    }
}
