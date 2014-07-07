using HutongGames.PlayMaker;
// using Tooltip = HutongGames.PlayMaker;

/*
 * *************************************************************************************
 * Created by: Rocket Games Mobile  (http://www.rocketgamesmobile.com), 2013-2014
 * For use in Unity 3.5, Unity 4.0+
 * *************************************************************************************
*/

[ActionCategory(ActionCategory.String)]
[HutongGames.PlayMaker.Tooltip("Compares a string value to multiple options")]
public class StringCompareMulti : FsmStateAction
{
    [RequiredField]
    [HutongGames.PlayMaker.Tooltip("String to check")]
    public FsmString StringVariable;

    [CompoundArray("Strings", "CompareTo", "CompareEvent")]

    [HutongGames.PlayMaker.Tooltip("String to compare to")]
    [UIHint(UIHint.Variable)]
    public FsmString[] CompareTos;

    [HutongGames.PlayMaker.Tooltip("Event to raise on match")]
    public FsmEvent[] CompareEvents;

    [HutongGames.PlayMaker.Tooltip("Event to raise if no matches are found")]
    public FsmEvent NoMatchEvent;

    [HutongGames.PlayMaker.Tooltip("When true, compare strings on case")]
    public FsmBool CaseSensitive;

    [HutongGames.PlayMaker.Tooltip("When true, runs on every frame")]
    public bool everyFrame;

    public override void Reset()
    {
        StringVariable = null;
        CompareTos = null;
        CompareEvents = null;
        NoMatchEvent = null;
        everyFrame = false;
    }

    public override void OnEnter()
    {
        DoStringCompare();

        if (!everyFrame)
            Finish();
    }

    public override void OnUpdate()
    {
        DoStringCompare();
    }

    private void DoStringCompare()
    {
        // exit if objects are null
        if ((StringVariable == null) || (CompareTos == null) || (CompareEvents == null))
            return;

        // get our case sensitivity
        bool UseCase = ((CaseSensitive != null) && CaseSensitive.Value);

        // get our base string, changing case as needed
        string BaseString = UseCase ? StringVariable.Value : StringVariable.Value.ToLower();

        // loop until we find a match
        int j = CompareTos.Length;
        for (int i = 0; i < j; i++)
        {
            // get compare string
            string tempStr = UseCase ? CompareTos[i].Value : CompareTos[i].Value.ToLower();

            if (BaseString.Equals(tempStr, System.StringComparison.CurrentCulture))
            {
                // fire the event
                Fsm.Event(CompareEvents[i]);

                // exit
                return;
            }
        }

        // nothing found, so fire no math event
        if (NoMatchEvent != null)
            Fsm.Event(NoMatchEvent);
    }
}