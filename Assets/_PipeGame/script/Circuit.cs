using UnityEngine;
using System.Collections.Generic;
using TRNTH;
namespace PipeGame{
[RequireComponent (typeof (TRNTH.Control))]
public class Circuit : TRNTH.MonoBehaviour {
	[HideInInspector]public bool refresh=false;
	public bool isAllWork=false;
	public float snapRadius=0.3f;
	public float timePass=0;
	public UILabel timeLabel;
	public PlayMakerFSM targetFSM;
	public CircuitSet[] levels;
	internal Element[] elementOrderList=new Element[]{};
	internal Container[] containerOrderList=new Container[]{};
	internal CircuitSet circuitSet;
	internal bool isLocked=false;
	internal int levelNow=0;
	public void checkCircuit(){
		if(!circuitSet)return;
		isAllWork=true;
		foreach(var e in elementOrderList){
			if(e.status!="work")isAllWork=false;
		}
		if(circuitSet.successActivate)circuitSet.successActivate.SetActive(isAllWork);
		isLocked=false;
		if(isAllWork){
			if(targetFSM)targetFSM.SendEvent("Level_"+circuitSet.level+"_end");
			foreach(var e in elementOrderList){
				e.collider.enabled=false;
			}
			isLocked=true;
		}
	}
	public void checkElementStatus(Element element){
		var e=element;
		element.status="qk";
		if(element.container&&element.container.name!="QK")element.status="broken";
		int ll=Mathf.Min(elementOrderList.Length,containerOrderList.Length);
		for(int i=0;i<ll;i++){
			e=elementOrderList[i];
			if(e==element&&e==containerOrderList[i].element)e.status="work";
		}
		e=element;
		switch(e.status){
		case"work":
			// isLocked=true;
			// e.OnWork();
			if(targetFSM)targetFSM.SendEvent("item_"+e.name[0]+"_work");
			if(e.workActivate)e.workActivate.SetActive(true);
			if(e.brokenActivate)e.brokenActivate.SetActive(false);
			e.collider.enabled=false;
			// Destroy(e.collider);
			break;
		case"broken":
			if(targetFSM)targetFSM.SendEvent("item_"+e.name[0]+"_broken");
			if(e.workActivate)e.workActivate.SetActive(false);
			if(e.brokenActivate)e.brokenActivate.SetActive(true);
			break;
		default:
			if(e.workActivate)e.workActivate.SetActive(false);
			if(e.brokenActivate)e.brokenActivate.SetActive(false);
			// e.tra.localScale=e.scaleDock*Vector3.one;
			break;
		}

	}
	public void checkElementStatus(){
		foreach(var e in elementOrderList){
			if(!e)Debug.LogError("Circuit.elementOrderList is not completed");
			// e.status="none";
		}
		int ll=Mathf.Min(elementOrderList.Length,containerOrderList.Length);
		for(int i=0;i<ll;i++){
			var e=elementOrderList[i];
			if(!e)continue;
			switch(e.status){
			case"put":
				e.status=e==containerOrderList[i].element?"work":"broken";
				break;
			}
			// e.tra.localScale=e.scaleNormal*Vector3.one;
			switch(e.status){
			case"work":
				// isLocked=true;
				// e.OnWork();
				if(targetFSM)targetFSM.SendEvent("item_"+e.name[0]+"_work");
				if(e.workActivate)e.workActivate.SetActive(true);
				if(e.brokenActivate)e.brokenActivate.SetActive(false);
				Destroy(e.collider);
				break;
			case"broken":
				if(targetFSM)targetFSM.SendEvent("item_"+e.name[0]+"_broken");
				if(e.workActivate)e.workActivate.SetActive(false);
				if(e.brokenActivate)e.brokenActivate.SetActive(true);
				break;
			default:
				if(e.workActivate)e.workActivate.SetActive(false);
				if(e.brokenActivate)e.brokenActivate.SetActive(false);
				// e.tra.localScale=e.scaleDock*Vector3.one;
				break;
			}
			// Debug.Log(e.status);
		}
	}
	void toggleEleCollider(bool value){
		foreach(var e in elementOrderList){
			if(e.status!="work")e.collider.enabled=value;
		}
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
		timePass=(Time.realtimeSinceStartup-timeStart);
		if(timeLabel)timeLabel.text=Mathf.Floor(timePass)+"";
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
					toggleEleCollider(false);
				}
			}
			if(element){
				// control.hover(~layerElement.value);
				if(control.isHold){
					element.pos=control.hit.point;
					element.status="air";
					var cols=Physics.OverlapSphere(element.pos,snapRadius,~0);
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
						element.tra.eulerAngles=element.eulerAngles+c.tra.eulerAngles;
					}
					//end
				}
				if(control.isUp&&element.container){
					element.pos=element.container.pos;
					element.container.element=element;
					element.container.collider.enabled=false;
					element.container.gameObject.SetActive(false);
					element.collider.enabled=true;
					element.status="put";
					checkElementStatus(element);
					toggleEleCollider(true);
					element=null;
				}
			}
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