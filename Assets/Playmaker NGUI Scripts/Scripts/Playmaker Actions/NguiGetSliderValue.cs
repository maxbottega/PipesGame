using HutongGames.PlayMaker;
// using Tooltip = HutongGames.PlayMaker;

/*
 * *************************************************************************************
 * Created by: Rocket Games Mobile  (http://www.rocketgamesmobile.com), 2013-2014
 * For use in Unity 3.5, Unity 4.0+
 * 
 * Updated for NGUI v3
 * *************************************************************************************
*/

[ActionCategory("NGUI")]
[HutongGames.PlayMaker.Tooltip("Gets the value of an NGUI progressbar or slider")]
public class NguiGetSliderValue : FsmStateAction
{
    [RequiredField]
    [HutongGames.PlayMaker.Tooltip("NGUI slider or progressbar to update read")]
    public FsmOwnerDefault NguiSlider;

    [RequiredField]
    [UIHint(UIHint.Variable)]
    [HutongGames.PlayMaker.Tooltip("Save the value to a variable")]
    public FsmFloat saveValue;

    [HutongGames.PlayMaker.Tooltip("When true, runs on every frame")]
    public bool everyFrame;

    public override void Reset()
    {
        NguiSlider = null;
        saveValue = null;
        everyFrame = false;
    }

    public override void OnEnter()
    {
        DoGetSliderValue();

        if (!everyFrame)
            Finish();
    }

    public override void OnUpdate()
    {
        DoGetSliderValue();
    }

    private void DoGetSliderValue()
    {
        // exit if objects are null
        if ((NguiSlider == null) || (saveValue == null))
            return;

        // get the object as a progressbar
        UISlider NguiSlide = Fsm.GetOwnerDefaultTarget(NguiSlider).GetComponent<UISlider>();

        // exit if no slider
        if (NguiSlide == null)
            return;

        // save value
        //saveValue.Value = NguiSlide.sliderValue;
        saveValue.Value = NguiSlide.value; // Updated for NGUI v3
    }
}