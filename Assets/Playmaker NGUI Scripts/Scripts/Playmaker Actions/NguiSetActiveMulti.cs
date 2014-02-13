using HutongGames.PlayMaker;

/*
 * *************************************************************************************
 * Created by: Rocket Games Mobile  (http://www.rocketgamesmobile.com), 2013-2014
 * For use in Unity 3.5, Unity 4.0+
 * *************************************************************************************
*/

[ActionCategory("NGUI")]
[Tooltip("Sets Multiple NGUI objects as Active or Inactive")]
public class NguiSetActiveMulti : FsmStateAction
{
    [RequiredField]
    [Tooltip("List of NGUI objects to set")]
    public FsmGameObject[] NguiObjects;

    [RequiredField]
    [Tooltip("Active state to set these objects")]
    public FsmBool active;

    [Tooltip("When true, runs on every frame")]
    public bool everyFrame;

    public override void Reset()
    {
        NguiObjects = null;
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
        if ((NguiObjects == null) || (NguiObjects.Length == 0) || (active == null))
            return;

        // set the active states
        foreach (FsmGameObject NObj in NguiObjects)
            NGUITools.SetActive(NObj.Value, active.Value);
    }
}