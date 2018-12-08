using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour {

   //存储RopeParent的Rigibody组件
    internal Rigidbody RBody;
	// Use this for initialization
	internal void Start () {
        //给RopeParent添加Rigibody组件
        this.gameObject.AddComponent<Rigidbody>();
        //获取RopeParent的Rigibody组件并赋值给RBody
        this.RBody = this.gameObject.GetComponent<Rigidbody>();
        this.RBody.isKinematic = true;
        //RopeParent中子物体的数量
        //给每一个子物体都加上Hinge Joint组件
        int childcount = this.transform.childCount;
        for (int i = 0; i < childcount;i++) {
            Transform t = this.transform.GetChild(i);
            t.gameObject.AddComponent<HingeJoint>();
 
            HingeJoint hinge = t.gameObject.GetComponent<HingeJoint>();
            hinge.connectedBody = i == 0 ? this.RBody : this.transform.GetChild(i - 1).GetComponent<Rigidbody>();
            hinge.useSpring = true;
            hinge.enableCollision = true;
        }
	}	
}
