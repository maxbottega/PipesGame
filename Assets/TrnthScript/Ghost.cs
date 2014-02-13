using UnityEngine;
using System.Collections;
namespace TRNTH{
// [RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Creature))]
public class Ghost:TRNTH.MonoBehaviour{
	public Vector3 dvec{
		get{
			return posTarget-pos;
		}
	}
	public bool isArrived{
		get{
			return Vector3.Scale(posTarget-pos,new Vector3(1,0,1)).magnitude <disStopMove;
		}
	}
	void think(){
		if(traTarget)posTarget=traTarget.position;
		if(isArrived)posTarget+=Random.insideUnitSphere*5*randomValue;
	}
	public float disStopMove=1f;

	// public bool shouldJumpWhileGrounded=true;
	public float timeThinking=1f;
	public float timeJumping=1.1f;
	public float yOffestToJump=0f;
	public float randomValue=1f;
	public Vector3 posTarget;
	public Transform traTarget;
	// protected CharacterController ccr;
	protected Alarm[] als=Alarm.alarms;
	protected Creature c;
	public virtual void Awake(){
		base.Awake();
		c=GetComponent<Creature>();
		// ccr=GetComponent<CharacterController>();
	}
	public virtual void Update(){
		Vector3 dvec=this.dvec;
		als[0].routine(timeThinking*(1+Random.value),think);
		als[1].routine(timeJumping*(1+Random.value),delegate(){
			if(dvec.y>yOffestToJump)c.jump();
		});
		c.walk(posTarget);
		c.lookAt(posTarget);
		// c.lookAt(posTarget);
		// c.walk();
	}
}
}