using UnityEngine;
using System.Collections;
namespace TRNTH{
[ExecuteInEditMode]
public class NguiCameraOffset : MonoBehaviour {
	void Update(){
		tra.localPosition=new Vector3(Screen.width*0.5f,Screen.height*0.5f,0);
	}
}
}