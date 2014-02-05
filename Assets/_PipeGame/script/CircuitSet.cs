using UnityEngine;
using System.Collections;
namespace PipeGame{
public class CircuitSet:MonoBehaviour{
	public int level=1;
	public Circuit circuit;
	public Element[] elementOrderList;
	public Container[] containerOrderList;
	public GameObject successActivate;
	void OnEnable(){
		circuit.elementOrderList=elementOrderList;
		circuit.containerOrderList=containerOrderList;
		circuit.circuitSet=this;
		circuit.isLocked=false;
		circuit.targetFSM.SendEvent("Level_"+level+"_start");
	}
}
}