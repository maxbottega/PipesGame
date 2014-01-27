using UnityEngine;
using System.Collections;
namespace TRNTH{ 
[RequireComponent(typeof(Control))]
public class GhostPlayerDiabloStyle:Ghost{
	Control control;
	public LayerMask layerAttack;
	public float disToAtk=3.2f;
	public float disToAtkNearest=1.2f;
	public override void Awake(){
		base.Awake();
		control=GetComponent<Control>();
	}
	public override void Update(){
		bool isHold=control.isHold;
		bool isMove=control.isHover&&isHold;
		RaycastHit hit=control.hit;
		c.stand();
		if(control.hover(layerAttack.value)&&isHold){
			traTarget=control.hit.collider.transform;
		}else if(isMove){
			// Debug.Log("ddfsaf");
			traTarget=null;
			posTarget=hit.point;
			c.walk(posTarget);
			c.lookAt(posTarget);
			if(this.dvec.y>yOffestToJump)c.jump();
		}
		if(traTarget){
			posTarget=traTarget.position;
			Vector3 dvec=this.dvec;
			if(dvec.magnitude<disToAtkNearest){
				// posTarget=(pos-traTarget.position)+pos;
				// c.walk(posTarget);
			}
			else if(dvec.magnitude<disToAtk)c.attack();
			else c.walk(posTarget);
			c.lookAt(posTarget);
		}
	}
}
}