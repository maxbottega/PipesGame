using HutongGames.PlayMaker;
// using Tooltip = HutongGames.PlayMaker;

/*
 * *************************************************************************************
 * Created by: Rocket Games Mobile  (http://www.rocketgamesmobile.com), 2013-2014
 * For use in Unity 3.5, Unity 4.0+
 * *************************************************************************************
*/

[ActionCategory("NGUI")]
[HutongGames.PlayMaker.Tooltip("Sets the list of NGUI objects used when loading a scene")]
public class NguiSceneLoaderObjects : FsmStateAction
{
    [RequiredField]
    [HutongGames.PlayMaker.Tooltip("NGUI window which holds the NGUI controls used during scene load")]
    public FsmGameObject NguiWindow;

    [RequiredField]
    [UIHint(UIHint.Variable)]
    [HutongGames.PlayMaker.Tooltip("The variable to hold the NGUI window")]
    public FsmGameObject PmNguiWindow;


    [RequiredField]
    [HutongGames.PlayMaker.Tooltip("NGUI progressbar to display during scene load")]
    public FsmGameObject NguiProgressbar;

    [RequiredField]
    [UIHint(UIHint.Variable)]
    [HutongGames.PlayMaker.Tooltip("The variable to hold the progressbar")]
    public FsmGameObject PmProgressbar;

    [HutongGames.PlayMaker.Tooltip("OPTIONAL - Label to display on the progressbar to show text of progress")]
    public FsmGameObject NguiProgressbarLabel;

    [UIHint(UIHint.Variable)]
    [HutongGames.PlayMaker.Tooltip("The variable to hold the progressbar label")]
    public FsmGameObject PmProgressbarLabel;

    public override void Reset()
    {
        NguiWindow = null;
        PmNguiWindow = null;
        NguiProgressbar = null;
        NguiProgressbarLabel = null;
        PmProgressbar = null;
        PmProgressbarLabel = null;
    }

    public override void OnEnter()
    {
        DoSetNGUI();
        Finish();
    }

    private void DoSetNGUI()
    {
        // save the NGUI objects
        PmProgressbar.Value = NguiProgressbar.Value;
        PmNguiWindow.Value = NguiWindow.Value;

        // set label
        if (NguiProgressbarLabel != null)
            PmProgressbarLabel.Value = NguiProgressbarLabel.Value;
    }
}