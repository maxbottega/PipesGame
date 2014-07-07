using UnityEngine;
using System.Collections;
using TRNTH;

public class CanJoinRoom : TRNTH.MonoBehaviour {
	public PlayMakerFSM targetFSM;
	Alarm a;
	public void checkRooms(){

		if(targetFSM)targetFSM.SendEvent("HasRooms");

	}
	void Update () {
		a.routine(1f,checkRooms);
	}
}
