using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CineMachine.Examples
{
	public class ActivateCamOnPlay : MonoBehaviour {
	public Cinemachine.CinemachineVirtualCameraBase vcam;
 
	// Use this for initialization
	void Start () 
    {
	    if (vcam)
	    {
	        vcam.MoveToTopOfPrioritySubqueue();
	    }
	}

	}
}

