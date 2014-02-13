using UnityEngine;
namespace TRNTH{
public class Fx:MonoBehaviour{
	static public Transform summon(GameObject[] gobjs,Vector3 pos,Quaternion rotation){
		if((pos-Camera.main.transform.position).magnitude>10)return null;
		Transform tra=(Instantiate(U.choose(gobjs),pos,rotation) as GameObject).transform;
		return tra;
	}
	static public Transform summon(GameObject[] gobjs,Vector3 pos){
		if((pos-Camera.main.transform.position).magnitude>10)return null;
		Transform tra=(Instantiate(U.choose(gobjs)) as GameObject).transform;
		tra.position=pos;
		return tra;
	}
	static public Fx f;
	public GameObject[] dust;
	public GameObject[] blood;
	public GameObject[] arrow;
	public GameObject[] javelin;
	public GameObject[] leaves;
	// public GameObject[] number;
	void Awake(){
		f=this;
	}
}
}