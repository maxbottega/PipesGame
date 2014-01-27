using UnityEngine;
using System.Collections;
namespace TRNTH{
[ExecuteInEditMode]
public class NguiAnchor : MonoBehaviour {
	public Transform ngui;
	public Vector3 offset;
	void Update(){
		if(ngui==null)return;
		var vec=coor;
		ngui.localPosition=(new Vector3(vec.x,vec.y,0))+(offset);
	}
}
}