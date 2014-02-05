using UnityEngine;
using HutongGames.PlayMaker;
using System.Collections;
using TRNTH;
namespace PipeGame{
[RequireComponent (typeof (BoxCollider))]
public class Element:TRNTH.MonoBehaviour{
	public Element upstream;
	public string status="";
	public GameObject workActivate;
	public GameObject brokenActivate;
	public PlayMakerFSM targetFSM;
	internal Container container;
	internal Vector3 eulerAngles;
	void Start(){
		eulerAngles=tra.eulerAngles;
	}
	public void OnWork(){
		// if(targetFSM)targetFSM.SendEvent("item_work");
	}
}
}