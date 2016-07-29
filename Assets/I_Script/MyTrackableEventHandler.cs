/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
包含了DefaultTrackableEventHandler和模型的旋转上升功能
==============================================================================*/

using UnityEngine;
using System.Collections;
namespace Vuforia
{
    /// <summary>
    /// A custom handler that implements the ITrackableEventHandler interface.
    /// </summary>
    public class MyTrackableEventHandler : MonoBehaviour,
                                                ITrackableEventHandler
    {
        #region PRIVATE_MEMBER_VARIABLES
        public Transform Obj=null ;
        private float save_position ;
        private TrackableBehaviour mTrackableBehaviour;
        bool isFound = false;
        public GameObject UI;  //用来设置按钮的隐藏

        #endregion // PRIVATE_MEMBER_VARIABLES



        #region UNTIY_MONOBEHAVIOUR_METHODS
        
        void Start()
        {
            save_position = Obj.position.y;
            Debug.Log("开始");
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
               
            }
        }

        void Update()
        {
            //添加从下到上移动和旋转代码
            //若是按世界坐标的话，如果先出现另外模型，则世界坐标是以其为模板，
            //这时若是再旋转的话会反向错乱，改为按自身坐标旋转,而且rotate改变的只能是向上轴，不然上移也会不断改变方向
            //以下坐标都是针对该物体本身，这样才能排除其他物体和世界坐标变化的影响
            //把上移过程中x和z轴的位置、角度也控制不变
            if (Obj != null)
            {
                if (Obj.localPosition.y < 0.1f && isFound == true)
                {
                    Obj.localPosition = new Vector3(0, Obj.localPosition.y, 0);
                    Obj.Translate(0, Time.deltaTime * 0.8f, 0, Space.Self);  //上移
                   //transform.Rotate(transform.up* Time.deltaTime * 15,Space.Self);          function Rotate(Vector3, relativeTo:Space = Space.Self)
                    Obj.Rotate(0, Time.deltaTime * 15, 0, Space.Self);//  绕y轴旋转           function Potate(xAngle:float,yAngle,zAngle,Space.Self)
                }
            }
            
        }
        #endregion // UNTIY_MONOBEHAVIOUR_METHODS


        #region PUBLIC_METHODS

        /// <summary>
        /// Implementation of the ITrackableEventHandler function called when the
        /// tracking state changes.
        /// </summary>
        public void OnTrackableStateChanged(
                                        TrackableBehaviour.Status previousStatus,
                                        TrackableBehaviour.Status newStatus)
        {
            if (newStatus == TrackableBehaviour.Status.DETECTED ||
                newStatus == TrackableBehaviour.Status.TRACKED ||
                newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            {
                OnTrackingFound();
            }
            else
            {
                OnTrackingLost();
            }
        }

        #endregion // PUBLIC_METHODS



        #region PRIVATE_METHODS


        private void OnTrackingFound()
        {
            UI.SetActive(true);
            isFound = true;
            Debug.Log("找到");

            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            // Enable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = true;
            }

            // Enable colliders:
            foreach (Collider component in colliderComponents)
            {
                component.enabled = true;
            }
           
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
        }


        private void OnTrackingLost()
        {
            UI.SetActive(false);
            Debug.Log("消失");
            isFound = false;

            //识别图消失，将模型位置重置
            if(!isFound)
            {
                    Obj.position = new Vector3(0, save_position, 0);
            }
                 
 
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            // Disable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = false;
            }

            // Disable colliders:
            foreach (Collider component in colliderComponents)
            {
                component.enabled = false;
            }

            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
        }

        #endregion // PRIVATE_METHODS
    }
}
