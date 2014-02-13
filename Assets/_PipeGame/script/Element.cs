using UnityEngine;
using HutongGames.PlayMaker;
using System.Collections;
using TRNTH;
namespace PipeGame{
[RequireComponent (typeof (BoxCollider))]
public class Element:TRNTH.MonoBehaviour{
	public float scaleDock=1f;
	public float scaleNormal=1f;
	public string status="";
	[HideInInspector]public Element upstream;
	[HideInInspector]public GameObject workActivate;
	[HideInInspector]public GameObject brokenActivate;
	[HideInInspector]public PlayMakerFSM targetFSM;
	public Container container;
	internal Vector3 eulerAngles;
	void Start(){
		eulerAngles=tra.eulerAngles;
		if(container)container.gameObject.SetActive(false);
	}
	void Update(){
		var isDock=container?false:true;
		if(container){
			isDock=false;
			switch(container.name){
			case"QK":
				isDock=true;
				break;
			}
		}
		tra.localScale=(isDock?scaleDock:scaleNormal)*Vector3.one;
	}
	public void OnWork(){
		// if(targetFSM)targetFSM.SendEvent("item_work");
	}
}
}