using UnityEngine;
using System.Collections.Generic;
namespace TRNTH{
public class Audio:MonoBehaviour{
	static public void play(AudioSource aus,AudioClip[] clips){
		play(aus,clips,1);
	}
	static public void play(AudioSource aus,AudioClip[] clips,float scale){
		AudioClip clip=U.choose(clips) as AudioClip;
		// System.Collections.Generic.Dictionary<AudioClip,Alarm> dic=dic;
		if(!clip||!(!dic.ContainsKey(clip)||dic[clip].a))return;
		dic[clip]=new Alarm(0.2f);
		aus.PlayOneShot(clip,scale);
	}
	static public void play(Vector3 pos,AudioClip[] clips,float scale){
		 GameObject gobj=new GameObject("AudioPlayer");
		 gobj.AddComponent<AudioSource>();
		 gobj.transform.position=pos;
		 play(gobj.audio,clips,scale);
		 Destroy(gobj,10);
	}
	// static public void play(Vector3 pos,AudioClip[] clips,float scale){
	// }
	static public Dictionary<AudioClip,Alarm> dic=new Dictionary<AudioClip,Alarm>();
	static public Audio a;
	public AudioClip[] auHit;
	public AudioClip[] auSwing;
	public AudioClip[] auBlade;
	public AudioClip[] pick;
	public AudioClip[] leap;
	public AudioClip[] bush;
	public AudioClip[] horn;
	public AudioClip[] earth;
	public AudioClip[] explode;
	public AudioClip[] magic;
	public override void Awake(){
		a=this;
	}
}
}