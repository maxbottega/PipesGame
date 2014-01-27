using UnityEngine;
using System.Collections;
namespace TRNTH{
[RequireComponent (typeof (Collider))]
public class PersonalSpaceForce:MonoBehaviour{
	public Creature creature;
	void OnTriggerStay(Collider col){
		creature.vecForce+=(pos-col.transform.position).normalized*1f;
	}
}}
