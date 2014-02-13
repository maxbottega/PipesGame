using UnityEngine;
namespace TRNTH{
public class FxShake:MonoBehaviour{
	public void play(){
		enabled=true;
		a.s=time;
		switch(space){
		case Space.World:posOrin=transform.position;break;
		}
	}
	void end(){
		if(hasOrinPos){
			switch(space){
			case Space.Self:transform.localPosition=posOrin;break;
			case Space.World:transform.position=posOrin;break;
			}
		}
	}
	Alarm a=new Alarm(); 
	Vector3 posOrin;
	public bool reversed=true;
	public bool hasOrinPos=true; 
	public float time=0.1f;
	public float value=0.3f;
	public Space space=Space.Self;
	public AnimationCurve curve;
	// public bool DestructWhenFinished=false;
	void Start(){
		switch(space){
		case Space.Self:posOrin=transform.localPosition;break;
		}
		play();
	}
	void OnEnable(){
		play();
	}
	void OnDisable(){
		end();
	}
	void Update(){
		Vector3 vec=Random.insideUnitSphere*curve.Evaluate(reversed?(1-a.rate):a.rate)*value;
		if(hasOrinPos){
			switch(space){
			case Space.Self:transform.localPosition=posOrin+vec;break;
			case Space.World:transform.position=posOrin+vec;break;
			}
		}else{
			transform.position+=vec;
		}
		if(a.a){
			// Destroy(this);
			enabled=false;
		}
	}
	void OnDestroy(){
		end();
	}
}
}