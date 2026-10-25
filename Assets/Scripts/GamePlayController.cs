using UnityEngine;
using System.Collections.Generic;

public class GamePlayController : MonoBehaviour
{
    RuleSet ruleSet;

    public void OnGenerate()
    {
        Debug.Log("-----------------------------------------------------------------------");
        Transform box1 = GamePlayHelpers.Find("Box1");
        Transform box2 = GamePlayHelpers.Find("Box2");
        Transform box3 = GamePlayHelpers.Find("Box3");
        Transform box4 = GamePlayHelpers.Find("Box4");

        GamePlayHelpers.MakeAllInvisible(box1, box2, box3, box4);
        if (GamePlayHelpers.RandomBool()) GamePlayHelpers.MakeVisible(box1, true);
        if (GamePlayHelpers.RandomBool()) GamePlayHelpers.MakeVisible(box2, true);
        if (GamePlayHelpers.RandomBool()) GamePlayHelpers.MakeVisible(box3, true);
        if (GamePlayHelpers.RandomBool()) GamePlayHelpers.MakeVisible(box4, true);

        RuleSet.all = new List<Rule> {
            new Rule("Box1 is left", () => GamePlayHelpers.GetX("Box1") < 0.0f),
            new Rule("Box2 is left", () => GamePlayHelpers.GetX("Box2") < 0.0f),
            new Rule("Box3 is left", () => GamePlayHelpers.GetX("Box3") < 0.0f),
            new Rule("Box1 is visible", () => GamePlayHelpers.IsVisible("Box1")),
            new Rule("Box2 is visible", () => GamePlayHelpers.IsVisible("Box2")),
            new Rule("Box3 is visible", () => GamePlayHelpers.IsVisible("Box3")),
        };

        ruleSet = new RuleSet();
        ruleSet.AddRule();
        ruleSet.AddRule();
        ruleSet.AddRule();

        Debug.Log("Rules: " + string.Join(",", ruleSet.GetRules()));
    }

    public void OnOpen()
    {
        print("You opened the door...");
        bool allPassed = ruleSet.TestAll();
        if (allPassed) print("It was SAFE!");
        else print("It was NOT SAFE :(");
    }

    public void OnDontOpen()
    {
        print("You didnt opened the door...");
        bool allPassed = ruleSet.TestAll();
        if (allPassed) print("It was SAFE! :(");
        else print("It was NOT SAFE :D Good call!");
    }

}
