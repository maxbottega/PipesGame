using UnityEngine;
using System.Collections.Generic;
namespace TRNTH{
	public class U:Utility{}
	public class Utility{
		static public T[] filter<T>(Component[] arr)where T:Component{
			var list=new List<T>();
			foreach(Component e in arr){
				T c=e.GetComponent<T>();
				// Debug.Log(e);
				if(c)list.Add(c);
			}
			return list.ToArray();
		}
		static public Vector3 dVecY(Vector3 a,Vector3 b){
			return Vector3.Scale(a-b,new Vector3(1,0,1));
		}
		static public string t2s(float time){
			int hh=(int)(Time.realtimeSinceStartup/60/60)%24;
			int mm=(int)(Time.realtimeSinceStartup/60)%60;
			int ss=(int)(Time.realtimeSinceStartup%60);
			return hh+":"+mm+":"+ss;
		}
		static public Vector3 rotateVec(Vector3 vec,Vector3 nor,float theta){
			Vector3 pro=Vector3.Project(vec,nor);
			Vector3 pa=vec-pro;
			return pro+Mathf.Cos(theta)*pa+Vector3.Cross(vec,nor).normalized*Mathf.Sin(theta)*pa.magnitude;
		}
		static public Object[] shuffle(Object[] arrOrin){
			List<Object> list=new List<Object>();
			int ll=arrOrin.Length;
			for(int i=0;i<ll;i++){
				list.Add(choose(arrOrin));
			}
			return list.ToArray();
		}
		static public T choose<T>(Object[] arr)where T:Object{
			if(arr.Length<1)return null;
			return arr[Random.Range(0,arr.Length)] as T;
		}
		static public string choose(string[] arr){
			if(arr.Length<1)return "";
			return arr[Random.Range(0,arr.Length)];
		}
		static public Vector3 choose(Vector3[] arr){
			if(arr.Length<1)return Vector3.zero;
			return arr[Random.Range(0,arr.Length)];
		}
		static public System.Object choose(System.Object[] arr){
			if(arr.Length<1)return null;
			return arr[Random.Range(0,arr.Length)];
		}
		static public Object choose(Object[] arr){
			if(arr.Length<1)return null;
			return arr[Random.Range(0,arr.Length)];
		}
		static public GameObject choose(List<GameObject> list){
			return choose(list.ToArray());
		}
		static public GameObject choose(GameObject[] gos){
			if(gos.Length<1)return null;
			return gos[Random.Range(0,gos.Length)];
		}
		static string getNumString(int num){
			string tagLevel;
			switch(num){
			case 0:tagLevel="　";break;
			case 1:tagLevel="１";break;
			case 2:tagLevel="２";break;
			case 3:tagLevel="三";break;
			case 4:tagLevel="４";break;
			case 5:tagLevel="５";break;
			case 6:tagLevel="６";break;
			case 7:tagLevel="７";break;
			case 8:tagLevel="８";break;
			case 9:tagLevel="９";break;
			case 10:tagLevel="Ａ";break;
			case 11:tagLevel="Ｂ";break;
			case 12:tagLevel="Ｃ";break;
			case 13:tagLevel="Ｄ";break;
			case 14:tagLevel="Ｅ";break;
			case 15:tagLevel="Ｆ";break;
			case 16:tagLevel="Ｇ";break;
			case 17:tagLevel="Ｈ";break;
			case 18:tagLevel="Ｉ";break;
			case 19:tagLevel="Ｊ";break;
			case 20:tagLevel="Ｋ";break;
			default:tagLevel="超";break;
			}
			return tagLevel;
		}
	}
}