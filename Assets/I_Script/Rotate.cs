using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Rotate : MonoBehaviour, IPointerDownHandler,IDragHandler{
   private Transform target;
    public GameObject gameObj;
    private PointerEventData data;
    public static Rotate instance;//不知有什么用
    float speed = .7f;
    public GUISkin mySkin;
    void Awake()
    {
       Rotate.instance = this; 
       
    }
    
 
   
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
             Ray ray = Camera.main.ScreenPointToRay(data.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit,5000))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.red);//画一条只有在Secen窗口才看的到的红色射线
             gameObj = hit.collider.gameObject;//获得碰撞体的信息
            if (hit.collider != null&&gameObj .name !="house")//射线有碰到物体但不是house时执行循环
            {

                target = GameObject.Find(gameObj.name).GetComponent<Transform> ();
                Vector3 vec3rote = new Vector3(0, -data.delta.x);
                 target.Rotate(vec3rote * speed, Space.Self);
                }
        }
        }
     }
    public void OnPointerDown(PointerEventData eventData)
    {
       data = eventData;
    }

    public void OnDrag(PointerEventData eventData)//用了接口，所以接口中的方法一定要实现的
         {
          }
    void OnGUI()
    {
        GUI.skin = mySkin;
        GUI.Label(new Rect(0, 100, 500, 500), "---> hello" + name);
    }
}
