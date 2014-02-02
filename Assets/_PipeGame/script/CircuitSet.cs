using UnityEngine;
using System.Collections;
namespace PipeGame{
public class CircuitSet:MonoBehaviour{
	public Circuit circuit;
	public Element[] elementOrderList;
	public Container[] containerOrderList;
	public GameObject successActivate;
	void OnEnable(){
		circuit.elementOrderList=elementOrderList;
		circuit.containerOrderList=containerOrderList;
		circuit.circuitSet=this;
	}
}
}