using UnityEngine;
using System.Collections.Generic;

namespace TRNTH{
	
// [System.Serializable]
	public class Alarm{
		static public Alarm[] alarms{
			get{
				Alarm[] a=new Alarm[10];
				for(int i=0;i<a.Length;i++){
					a[i]=new Alarm();
				}
				return a;
			}			
		}
		public Alarm(){}
		public Alarm(float time){
			this.s=time;
		}
		float start=0.0f;
		float end=1.0f;
		public bool a{
			get {
				return Time.time>end;;
			}
		}
		public float s{
			set{
				start=Time.time;
				end=start+value;
			 }
		}
		public void iss(float num){
			s=num;
		}
		public bool iss(){
			return a;
		}
		public float rate{
			get{
				return (Time.time-start)/(end-start+Mathf.Epsilon);
			}
		}
		public float countDown{
			get{
				return Mathf.Clamp(end-Time.time,0,Mathf.Infinity);
			}
		}
		public void routine(float time,System.Action boo){
			if(!a)return;
			s=(time);
			boo();
		}
	}
}