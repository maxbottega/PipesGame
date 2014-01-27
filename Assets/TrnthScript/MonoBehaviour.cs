using UnityEngine;
using System.Collections.Generic;
namespace TRNTH{
public class MonoBehaviour:UnityEngine.MonoBehaviour{
	internal Transform tra;
	internal GameObject gobj;
	internal Transform[] children;
	public virtual void Awake(){
		tra=transform;
		gobj=gameObject;
		getChildren();
	}
	public Vector3 pos{
		get{
			return tra.position;
		}
		set{
			tra.position=value;
		}
	}
	public Transform[] getChildren(){
		return getChildren(tra);
	}
	public Transform[] getChildren(Transform tra){
		List<Transform> arr=new List<Transform>();
		foreach(Transform child in tra){arr.Add(child);}
		children=arr.ToArray();
		return children;
	}
	public Transform findNearest(Transform[] arr){
		if(arr.Length<1)return null;
		Transform nearest=arr[0];
		foreach(Transform tra in arr){
			if((tra.position-pos).magnitude<(nearest.position-pos).magnitude)nearest=tra;
		}
		return nearest;
	}
	public Vector3 coor{
		get{
			return Camera.main.WorldToScreenPoint(transform.position);
		}
	}
	public Rect rect{
		get{
			var vec=coor;
			return new Rect(vec.x,Screen.height-vec.y,100,100);
		}
	}
	public float dis(Component c){
		return (pos-c.transform.position).magnitude;
	}
	public Component findNearest(Component[] arr){
		if(arr.Length<1)return null;
		var nearest=arr[0];
		foreach(var e in arr){
			if(this.dis(e)<this.dis(nearest))nearest=e;
		}
		return nearest;
	}
}
}