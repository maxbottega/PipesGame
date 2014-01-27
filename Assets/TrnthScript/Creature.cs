using UnityEngine;
using System.Collections;
namespace TRNTH{
[RequireComponent (typeof (CharacterController))]
public class Creature:TRNTH.MonoBehaviour{
	public void hurt(){
		StartCoroutine(parameterOnce("hurt"));
	}
	public void stand(){
		vecForce.x*=0.69f;
		vecForce.z*=0.69f;
	}
	public void lookAt(Vector3 pos){
		lookAt(pos,lookRate);
	}
	public virtual void lookAt(Vector3 pos,float dt){
		if(U.dVecY(pos,this.pos).magnitude<lookDisMin)return;
		Transform tra=transform;
		dirTarget=Vector3.Slerp(dirTarget,(new Vector3(pos.x,tra.position.y,pos.z)-tra.position).normalized,dt*6);
		tra.LookAt(tra.position+dirTarget);
	}
	public void walk(Vector3 posTarget){
		Vector3 dvec=posTarget-transform.position;
		dvec.Normalize();
		fWalk+=speedMoveForce;
		Mathf.Clamp(fWalk,0,speedMoveMax);
		dvec*=speedMoveMax;
		vecForce.x=dvec.x;
		vecForce.z=dvec.z;
	}
	protected void triggerFx(Collider col){
		if((Camera.main.transform.position-pos).magnitude>20f)return;
		switch(col.gameObject.tag){
		case"bush":
			Audio.play(tra.position,Audio.a.bush,0.5f);
			break;
		case"leaf":
			Audio.play(tra.position,Audio.a.bush,1f);
			Fx.summon(Fx.f.leaves,tra.position);
			break;
		}
	}
	public IEnumerator parameterOnce(string str){
		if(animator){
			animator.SetBool(str,true);
			yield return new WaitForSeconds(0);
			animator.SetBool(str,false);
		}
	}
	public void attack(){
		if(!aCdAttack.a)return;
		aCdAttack.s=0.2f;
		aPreAttack.s=0.12f;
		sur="atk";
		StartCoroutine(parameterOnce("atk"));
	}
	public void jump(){
		if(!aCdJump.a)return;
		if(onlyJumpWhileGrounded&&!ccr.isGrounded)return;
		vecForce+=Vector3.up*fJump;
		aCdJump.s=0.2f;
	}
	protected CharacterController ccr;
	protected Vector3 dirTarget=Vector3.zero;
	Alarm aLook=new Alarm();
	Alarm aCdJump=new Alarm();
	Alarm aCdAttack=new Alarm();
	Alarm aPreAttack=new Alarm();
	public bool isGravity=true;
	public bool isVital=true;
	public bool onlyJumpWhileGrounded=true;
	public float speedMoveForce=0.1f;
	public float speedMoveMax=3f;
	public float fJump=7f;
	public float lookRate=0.03f;
	public float lookDisMin=0.9f;
	public float disAtk=2.6f;
	public float disAtkRadius=2.6f;
	[HideInInspector]public Vector3 vecForce;
	// public Vector3 posTarget;
	public Animator animator;
	public LayerMask layerHurt;
	public LayerMask layerAttack;
	float fWalk=0.0f;
	string sur="";
	// Alarm aAir=new Alarm();
	// Vector3 prePos;
	public override void Awake(){
		base.Awake();
		ccr=GetComponent<CharacterController>();
	}
	public virtual void FixedUpdate(){
		float dt=Time.deltaTime;
		fWalk*=0.9f;
		if(isGravity&&!ccr.isGrounded){
			vecForce+=Physics.gravity*dt;
			vecForce.y*=0.97f;
		}
		Vector3 vec=vecForce*dt;
		ccr.Move(vec);
	}
	public virtual void Update(){
		float dt=Time.deltaTime;
		// if(ccr.isGrounded)aAir.s=0.4f;
		if(isVital){
			switch(sur){
			case"atk":
				if(!aPreAttack.a)break;
				sur="";
				Collider[] cols=Physics.OverlapSphere(pos+dirTarget*disAtk,layerAttack.value);
				foreach(Collider col in cols){
					Creature creature=col.GetComponent<Creature>();
					if(creature)creature.hurt();
				}
				break;
			}
			// if(animator){
				// animator.SetFloat("speed",Vector3.Scale(vecForce,new Vector3(1,0,1)).magnitude/speedMoveMax);
				// animator.SetBool("air",!ccr.isGrounded);
			// }
		}
	}
	public virtual void OnTriggerExit(Collider col){
		triggerFx(col);
	}
	public virtual void OnTriggerEnter(Collider col){
		// Debug.Log(1<<col.gameObject.layer&layerHurt.value);
		if((1<<col.gameObject.layer&layerHurt.value)!=0)hurt();
		triggerFx(col);
	}
}
}
