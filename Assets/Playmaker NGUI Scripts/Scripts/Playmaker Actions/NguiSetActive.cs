using HutongGames.PlayMaker;
// using Tooltip = HutongGames.PlayMaker;

/*
 * *************************************************************************************
 * Created by: Rocket Games Mobile  (http://www.rocketgamesmobile.com), 2013-2014
 * For use in Unity 3.5, Unity 4.0+
 * *************************************************************************************
*/

[ActionCategory("NGUI")]
[HutongGames.PlayMaker.Tooltip("Sets an NGUI object as Active or Inactive")]
public class NguiSetActive : FsmStateAction
{
    [RequiredField]
    [HutongGames.PlayMaker.Tooltip("NGUI object")]
    public FsmOwnerDefault NguiObject;

    [RequiredField]
    [HutongGames.PlayMaker.Tooltip("Active state to make this object")]
    public FsmBool active;

    [HutongGames.PlayMaker.Tooltip("When true, runs on every frame")]
    public bool everyFrame;

    public override void Reset()
    {
        NguiObject = null;
        active = null;
        everyFrame = false;
    }

    public override void OnEnter()
    {
        DoSetActiveState();

        if (!everyFrame)
            Finish();
    }

    public override void OnUpdate()
    {
        DoSetActiveState();
    }

    private void DoSetActiveState()
    {
        // exit if objects are null
        if ((NguiObject == null) || (active == null))
            return;

        // set the active state
        NGUITools.SetActive(Fsm.GetOwnerDefaultTarget(NguiObject), active.Value);
    }
}