using UnityEngine;
using System.Collections;
using TRNTH;
namespace PipeGame{
[RequireComponent (typeof (BoxCollider))]
public class Element:TRNTH.MonoBehaviour{
	public Element upstream;
	public string status="";
	public GameObject workActivate;
	public GameObject brokenActivate;
	internal Container container;
	internal Vector3 eulerAngles;
	void Start(){
		eulerAngles=tra.eulerAngles;
	}
}
}