using UnityEngine;
namespace TRNTH{
public class Constrain : MonoBehaviour {
	public Transform traTarget;
	public bool keepHierarchy=false;
	public Vector3 offset;
	Transform tra;
	void Start(){
		tra=transform;
		if(!keepHierarchy){
			transform.parent=traTarget;
			Destroy(this);
		}
	}
	void Update(){
		if(traTarget)tra.position=traTarget.position+offset;
		else enabled=false;
	}
}
}