using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace TRNTH{
[RequireComponent (typeof (Renderer))]
[ExecuteInEditMode]
public class Gardener:TRNTH.MonoBehaviour{
	public bool fromTopToBottom=true;
	public float padding=0.0f;
	public float space=20.0f;
	public float rayRadius=0.0f;
	public float rateRandom=1.0f;
	public float random=0.0f;
	public Transform seedsGroup;
	public GameObject seed;
	// public Renderer renderer;
	// public Transform pOutput;
	public Transform parent;
	string _template="grid";
	public bool plantNow=false;
	public string template{
		get{
			return _template;
		}
		set{
			_template=value;
			switch(_template){
			case"grid":
			default:
				this.padding=0;
				this.space=2;
				this.rayRadius=0;
				this.random=0;
				this.rateRandom=1;
				break;
			}
		}
	}
	public Transform plant(){
		Vector3 posSelf=renderer.transform.position;
		RaycastHit hit;
		Transform[] arr=null;
		string text="";
		if(seedsGroup){
			arr=getChildren(seedsGroup);
			text=seedsGroup.name+"Output";
		}else text=seed.name+"Output";
		Transform parent=new GameObject(text).transform;
		parent.gameObject.SetActive(false);
		parent.parent=this.parent;
		float ss=space;
		Vector3 size=renderer.bounds.extents;
		for(float zz=-size.z-padding;zz<=size.z+padding;zz+=ss){
		for(float xx=-size.x-padding;xx<=size.x+padding;xx+=ss){
			if(Random.value>rateRandom)continue;
			Vector3 pos=posSelf+(new Vector3(xx,size.y*(fromTopToBottom?1:-1)*2,zz))+Random.insideUnitSphere*random;
			if(Physics.SphereCast(pos,rayRadius,Vector3.up*(fromTopToBottom?-1:1),out hit)){
				if(seedsGroup)seed=(U.choose(arr) as Transform).gameObject;
				GameObject obj=UnityEngine.Object.Instantiate(seed) as GameObject;
				if(!Application.isPlaying){
		#if UNITY_EDITOR
					PropertyModification[] data=PrefabUtility.GetPropertyModifications(seed);
					obj=(PrefabUtility.InstantiatePrefab(PrefabUtility.GetPrefabParent(seed)) as GameObject);
					if(!obj)obj=Instantiate(seed) as GameObject;
					PrefabUtility.SetPropertyModifications(obj,data);
					// Debug.Log(obj);
		#endif
				}else{
					obj=UnityEngine.Object.Instantiate(seed) as GameObject;
				}
				Transform tra=obj.transform;
				tra.position=hit.point;
				tra.parent=parent;
				tra.eulerAngles=Vector3.up*Random.value*360*random;
			}
		}}
		parent.gameObject.SetActive(true);
		// pOutput=parent;
		return parent;
	}
	void Update(){
		if(plantNow){
			plantNow=false;
			plant();
		}
	}
}
}