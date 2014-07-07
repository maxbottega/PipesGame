// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions{

	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Resets the variables to defaults")]
	public sealed class ResetFsmVariables : FsmStateAction{

        [UIHint(UIHint.Variable)] 
		public FsmVar[] variables;

	    public override void Reset(){
            variables = new FsmVar[1];
		}

		public override void OnEnter(){
			
            foreach (FsmVar _fsmVar in variables) 
			{
				PlayMakerUtils.ApplyValueToFsmVar(this.Fsm,_fsmVar,null);
			}
			
            Finish();		
		}
	}
}