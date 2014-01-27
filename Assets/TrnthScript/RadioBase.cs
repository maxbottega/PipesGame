using UnityEngine;
using System.Collections;
namespace TRNTH{
[System.Serializable]
public class RadioBase{
	public int length=100;
	public float rate=1.0f;
	public static RadioBase operator +(RadioBase a,float b){
		a.rate+=b/a.length;
		return a;
	}		
	public static RadioBase operator -(RadioBase a,float b){
		return a+b*-1;
	}
	public bool toggle{
		set{
			if(value)rate=1f;
			else rate=0f;
		}
	}
	public int value{
		get{
			return (int)(rate*length);
		}set{
			rate=value*1f/length;
		}
	}
}
}