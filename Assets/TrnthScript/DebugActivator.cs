using UnityEngine;
using System.Collections;
namespace TRNTH{
public class DebugActivator : MonoBehaviour {
	void konami(KeyCode key){
		if(Input.anyKeyDown){
			if(Input.GetKeyDown(key))inputIntRecord+=1;
			else inputIntRecord=0;
		}
	}
	void activate(){
		foreach(GameObject go in goes)go.SetActive(true);
		foreach(Behaviour be in behaviours)be.enabled=!be.enabled;
	}
	int inputIntRecord=0;
	public GameObject[] goes;
	public Behaviour[] behaviours;
	void Update(){
		switch(inputIntRecord){
		case 0:konami(KeyCode.UpArrow);break;
		case 1:konami(KeyCode.UpArrow);break;
		case 2:konami(KeyCode.DownArrow);break;
		case 3:konami(KeyCode.DownArrow);break;
		case 4:konami(KeyCode.LeftArrow);break;
		case 5:konami(KeyCode.RightArrow);break;
		case 6:konami(KeyCode.LeftArrow);break;
		case 7:konami(KeyCode.RightArrow);break;
		case 8:konami(KeyCode.B);break;
		case 9:konami(KeyCode.A);break;
		case 10:
			inputIntRecord=0;
			activate();
			break;
		}
		if(Input.GetKeyDown(KeyCode.Break))activate();
	}
}
}