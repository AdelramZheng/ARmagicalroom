using UnityEngine;
using System.Collections;

public class SceneStart : MonoBehaviour {
    public Camera firstPersomCam;
    public GameObject stickController;
    private float time = 2.0f;
	// Use this for initialization
	IEnumerator  Start ()
    {
        yield return new WaitForSeconds(0.3f);
        iTween.MoveTo(this.gameObject,firstPersomCam.transform.position, time);
        iTween.RotateTo(this.gameObject,firstPersomCam .transform.rotation.eulerAngles, time);
        Invoke("EndMove", time + 0.1f);//延迟time后调用函数endmove
	}
     
    void EndMove()
    {
        this.gameObject.SetActive(false);//将自身摄像机停止
        //将第一人称视角摄像机的父物体运行，原先是该物体的inspector的勾取消的状态，改为true后摄像机跟着开始运行
        firstPersomCam.transform.parent.gameObject.SetActive(true);
        stickController.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
