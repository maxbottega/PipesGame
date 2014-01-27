using UnityEngine;

namespace TRNTH{
	[System.Serializable]
	public class Bar:Radio{
		public enum Align{center,left,right}
		public Align align=Align.center;
		public Transform traValue;
		public Transform traSmoothed;
		public Transform traSmoothedUp;
		public Transform traParent;
		public override void update(){
			base.update();
			Transform tra;
			tra=traValue;
			if(tra){
				tra.localScale=new Vector3(rate,1f,1f);
				tra.renderer.enabled=rate!=0;
			}
			tra=traSmoothed;
			if(tra){
				tra.localScale=new Vector3(rateSmoothed,1f,1f);
				tra.renderer.enabled=rateSmoothed!=0;
			}
			tra=traSmoothedUp;
			if(tra){
				tra.localScale=new Vector3(rateSmoothed,1f,1f);
				tra.renderer.enabled=rateSmoothed!=0;
			}
			tra=traParent;
			if(tra){
// 				if(Mathf.Abs(rateSmoothed-rate)>0.01)alarms[1].s=(0.8f);
				tra.gameObject.SetActive(!als[1].a);
			}
		}
	}
}