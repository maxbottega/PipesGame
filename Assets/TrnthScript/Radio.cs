using UnityEngine;
namespace TRNTH{
[System.Serializable]
public class Radio:RadioBase{
	public static Radio[] radios{
		get{
			Radio[] rdos=new Radio[10];
			for(int i=0;i<rdos.Length;i++)rdos[i]=new Radio();
			return rdos;
		}
	}
	public void delay(){
		als[0].s=smoothDelay;
	}
	protected Alarm[] als=Alarm.alarms;
	protected float vel=0f;
	public string name="radio";
	public float rateSmoothed=1.0f;
	public float timeSmooth=0.5f;
	public float smoothDelay=1f;
	public static Radio operator +(Radio a,float b){
		if(a.als[0].a&&a.smoothDelay>0)a.rateSmoothed=a.rate;
		a.rate+=b/a.length;
		a.als[0].s=a.smoothDelay;
		a.als[1].s=1.8f;
		return a;
	}		
	public static Radio operator -(Radio a,float b){
		return a+b*-1;
	}
	public virtual void update(){
		rate=Mathf.Clamp01(rate);
		if(als[0].a)rateSmoothed=Mathf.SmoothDamp(rateSmoothed,rate,ref vel,timeSmooth);
		rateSmoothed=Mathf.Clamp01(rateSmoothed);
	}
}
}