using UnityEngine;
namespace TRNTH{
	public class spinRotate:MonoBehaviour{
		public float speed=30f;
		public Space type;
		protected Transform tra;
		void Awake(){
			tra=transform;
		}
		public virtual void Update () {
			tra.Rotate(0,Time.deltaTime*60*speed,0,type);
		}
	}
}
