using UnityEngine;
namespace TRNTH{
[ExecuteInEditMode]
public class CameraFollow:MonoBehaviour{
	public void setTarget(Transform tra){
		canSkip=false;
		traTarget=tra;
		sur="smooth";
		als[1].s=smoothTime;
	}
	static public bool canSkip=false;
	public bool isFixable=true;
	public bool lookNow=false;
	public float scale=1.0f;
	public float heightOffsetWhileObstacle=3.0f;
	public float timeKeepWhileObstacle=1.0f;
	public float smoothTime=1.0f;
	public Vector3 posTargetOffset=new Vector3(0f,-0.5f,0f);
	public Vector3 posSelfOffset=new Vector3(3,3,-4);
	public LayerMask layerObstacle;
	public Transform traTarget;
	public Radio[] rdos=Radio.radios;
	public Camera cameraInstruction;
	public Control control;
	private string sur="";
	// private Transform tra;
	private Vector3 vecVel=Vector3.zero;
	private Vector3 posTarget=Vector3.zero;
	private Alarm[] als=Alarm.alarms;
	void Update(){
		bool isClick=control!=null&&control.isClick;
		if(traTarget)posTarget=traTarget.position+posTargetOffset;
		Vector3 posSelf=posTarget+posSelfOffset*scale+Vector3.up*rdos[0].rateSmoothed*heightOffsetWhileObstacle;
		if(lookNow){
			lookNow=false;
			sur="fixed";
		}
		switch(sur){
		case"smooth":
			tra.position=Vector3.SmoothDamp(tra.position,posSelf,ref vecVel,smoothTime*0.2f*(Time.fixedDeltaTime/0.02f));
			Quaternion roationTarget=Quaternion.LookRotation(posTarget-posSelf);
			tra.rotation=Quaternion.Lerp(tra.rotation,roationTarget,0.3f);
			if(isFixable){
				if(als[1].a||(isClick&&canSkip))sur="fixed";
			}
			break;
		case"fixed":
			RaycastHit hit;
			tra.position=posSelf;
			Vector3 dvec=posSelf-posTarget;
			if(Physics.Raycast(posTarget,posSelfOffset,out hit,posSelfOffset.magnitude*1.0f,layerObstacle.value)
				||Physics.Raycast(posTarget,Vector3.forward+Vector3.up,out hit,3,layerObstacle.value)){
				als[0].s=timeKeepWhileObstacle;
			};
			tra.LookAt(posTarget);
			rdos[0].toggle=!als[0].a;
			rdos[0].update();
			break;
		default:
			if(cameraInstruction){
				Animation ani=cameraInstruction.animation;
				if(ani.isPlaying){
					tra.position=cameraInstruction.transform.position;
					tra.rotation=cameraInstruction.transform.rotation;
				}else {
					sur="smooth";
					canSkip=true;
				}
			}else{
				sur="smooth";
				canSkip=true;
			}
			if(canSkip&&isClick){
				sur="smooth";
			}
			als[1].s=smoothTime;
			break;
		}
	}
}
}