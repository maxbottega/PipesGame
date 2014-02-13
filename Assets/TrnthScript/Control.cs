using UnityEngine;
namespace TRNTH{
public class Control:MonoBehaviour{
	public bool hover(int mask){
		return hover(this.disNearPlane,mask);
	}
	public bool hover(float dis,int mask){
		Ray mspTRay=Camera.main.ScreenPointToRay(Input.mousePosition);
		mspTRay.origin+=mspTRay.direction*dis;
		bool iss=Physics.Raycast(mspTRay,out _hit,100,mask);
		if(traLocator)traLocator.position=hit.point;
		return iss;
	}
	virtual public bool isHover{
		get{
			return hover(this.disNearPlane,this.layerMask.value);
		}
	}
	virtual public bool isClick{
		get{
			return Input.GetMouseButtonDown(0)
				||Input.GetMouseButtonDown(1)
				||Input.GetKeyDown(KeyCode.LeftShift)
				||Input.GetKeyDown(KeyCode.RightShift)
				||Input.GetKeyDown(KeyCode.Space)
				||Input.GetKeyDown(KeyCode.Z)
				||(Input.touches.Length>0&&Input.touches[0].phase==TouchPhase.Began);
		}
	}
	virtual public bool isDown{
		get{
			return Input.GetMouseButtonDown(0)
				||Input.GetMouseButtonDown(1)
				||Input.GetKeyDown(KeyCode.LeftShift)
				||Input.GetKeyDown(KeyCode.RightShift)
				||Input.GetKeyDown(KeyCode.Space)
				||Input.GetKeyDown(KeyCode.Z)
				||(Input.touches.Length>0&&Input.touches[0].phase==TouchPhase.Began)
				;
		}
	}
	virtual public bool isUp{
		get{
			return Input.GetMouseButtonUp(0)
				||Input.GetMouseButtonUp(1)
				||Input.GetKeyUp(KeyCode.LeftShift)
				||Input.GetKeyUp(KeyCode.RightShift)
				||Input.GetKeyUp(KeyCode.Space)
				||Input.GetKeyUp(KeyCode.Z)
				;
		}
	}
	virtual public bool isHold{
		get{
			// aClick
			return Input.GetMouseButton(0)
				||Input.GetMouseButton(1)
				||Input.GetKey(KeyCode.LeftShift)
				||Input.GetKey(KeyCode.RightShift)
				||Input.GetKey(KeyCode.Space)
				||Input.GetKey(KeyCode.Z)
				||Input.touches.Length>0;
		}
	}
	virtual public bool isCancel{
		get{
			return Input.GetKeyDown(KeyCode.Escape)
				||Input.GetMouseButtonDown(1)
				||Input.GetKeyDown(KeyCode.X);
		}
	}
	virtual public bool isSkip{
		get{
			return Input.GetKey(KeyCode.LeftControl);
		}
	}
	public Vector3 coor{
		get{
			Vector3 coor=Input.touches.Length>0?((Vector3)Input.touches[0].position):Input.mousePosition;
			coor.y=Screen.height-coor.y;
			return coor;
		}
	}
	public RaycastHit hit{
		get{
			return _hit;
		}
	}
	Alarm aClick=new Alarm();
	RaycastHit _hit;
	public LayerMask layerMask=-1;
	public float disNearPlane=3f;
	public Transform traLocator;
}
}