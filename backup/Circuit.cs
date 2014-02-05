using UnityEngine;
using System.Collections.Generic;
using TRNTH;
namespace PipeGame{
[RequireComponent (typeof (TRNTH.Control))]
public class Circuit : TRNTH.MonoBehaviour {
	public bool refresh=false;
	public bool isAllWork=false;
	public UILabel timeRecord;
	public PlayMakerFSM targetFSM;
	public CircuitSet[] levels;
	internal Element[] elementOrderList;
	internal Container[] containerOrderList;
	internal CircuitSet circuitSet;
	internal bool isLocked=false;
	internal int levelNow=0;
	public void check(){
		foreach(var e in elementOrderList){
			if(!e)Debug.LogError("Circuit.elementOrderList is not completed");
			e.status="none";
		}
		var isOkay=true;
		Element upstream=null;
		int ll=Mathf.Min(elementOrderList.Length,containerOrderList.Length);
		var count=0
		foreach(var e in containerOrderList){
			if(e.element)count++;
		}
		if(count==containerOrderList.Length){
			for(int i=0;i<ll;i++){
				if(containerOrderList[i].element!=elementOrderList[i])break;
			}
		}
		for(int i=0;i<ll;i++){
			var c=containerOrderList[i];
			if(!c)Debug.LogError("Circuit.containerOrderList is not completed");
			var e=elementOrderList[i];
			if(c.element==e){
				e.upstream=upstream;
				// e.status=isOkay?"work":"broken";
				e.status="work";
				upstream=e;
			}else{
				isOkay=false;
				// e.status="broken";
				if(c.element!=null)c.element.status="broken";
				// c.element.status="broken";
			}
			switch(e.status){
			case"work":
				// e.OnWork();
				if(targetFSM)targetFSM.SendEvent("item_"+e.name[0]+"_work");
				if(e.workActivate)e.workActivate.SetActive(true);
				if(e.brokenActivate)e.brokenActivate.SetActive(false);
				break;
			case"broken":
				if(targetFSM)targetFSM.SendEvent("item_"+e.name[0]+"_broken");
				if(e.workActivate)e.workActivate.SetActive(false);
				if(e.brokenActivate)e.brokenActivate.SetActive(true);
				break;
			default:
				if(e.workActivate)e.workActivate.SetActive(false);
				if(e.brokenActivate)e.brokenActivate.SetActive(false);
				break;
			}
			// Debug.Log(e.status);
		}
		isAllWork=isOkay;
		if(circuitSet.successActivate)circuitSet.successActivate.SetActive(isAllWork);
		if(isOkay){
			if(targetFSM)targetFSM.SendEvent("Level_"+circuitSet.level+"_end");
			isLocked=true;
		}
		// if(isAllWork&&targetFSM)targetFSM.SendEvent("Level_"+levelNow+"_start");
	}
	Control control;
	Element element;
	float timeStart;
	void Awake(){
		control=GetComponent<Control>();
		timeStart=Time.realtimeSinceStartup;
	}
	void OnEnable(){
		if (targetFSM == null){
			targetFSM = GetComponent<PlayMakerFSM>();
		}
		if (targetFSM == null){
			enabled = false;
			Debug.LogWarning("No Fsm Target assigned");
		}
	}
	void Update(){
		// time record 
		timeRecord.text=Mathf.Floor(Time.realtimeSinceStartup-timeStart)+"";
		// main `Element`s drag and drop
		if(!isLocked){
			if(control.isHover){
				var e=control.hit.collider.GetComponent<Element>();
				if(control.isDown&&e){
					if(e.container){
						e.container.element=null;
						e.container.collider.enabled=true;
						e.container.gameObject.SetActive(true);
					}
					e.collider.enabled=false;
					element=e;
				}
			}
			if(element){
				// control.hover(~layerElement.value);
				if(control.isHold){
					element.pos=control.hit.point;
					element.status="air";
					var cols=Physics.OverlapSphere(element.pos,2,~0);
					var arr=U.filter<Container>(cols);
					// arr=filter(arr);
					if(arr.Length>0){
						Container c=arr[0];
						foreach(var e in arr){
							if(element.dis(e)<element.dis(c)
								&&c.element==null)c=e;
						}
						element.container=c;
						element.pos=c.pos;
						// element.tra.eulerAngles=element.eulerAngles+c.tra.eulerAngles;
					}
					//end
				}
				if(control.isUp&&element.container){
					element.pos=element.container.pos;
					element.container.element=element;
					element.container.collider.enabled=false;
					element.container.gameObject.SetActive(false);
					element.collider.enabled=true;
					element=null;
					refresh=true;
				}
			}
			if(refresh)check();
			refresh=false;
		}
	}
	void OnDestroy(){
		// Container.list.Clear();
	}
	Container[] filter(Container[] arr){
		var list=new List<Container>();
		foreach(var e in arr){
			if(e.element==null)list.Add(e);
		}
		return list.ToArray();
	}
}
}