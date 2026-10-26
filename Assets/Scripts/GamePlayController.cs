using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GamePlayController : BaseGamePlayController
{

    void Awake()
    {
        RuleSet.all = new List<Rule> {
            new Rule("Box1 is left", () => GamePlayHelpers.GetX("Box1") < 0.0f),
            new Rule("Box2 is left", () => GamePlayHelpers.GetX("Box2") < 0.0f),
            new Rule("Box3 is left", () => GamePlayHelpers.GetX("Box3") < 0.0f),
            new Rule("Box1 is visible", () => GamePlayHelpers.IsVisible("Box1")),
            new Rule("Box2 is visible", () => GamePlayHelpers.IsVisible("Box2")),
            new Rule("Box3 is visible", () => GamePlayHelpers.IsVisible("Box3")),
        };
    }

    private void SetupScene()
    {
        Transform box1 = GamePlayHelpers.Find("Box1");
        Transform box2 = GamePlayHelpers.Find("Box2");
        Transform box3 = GamePlayHelpers.Find("Box3");
        Transform box4 = GamePlayHelpers.Find("Box4");

        GamePlayHelpers.MakeAllInvisible(box1, box2, box3, box4);
        if (GamePlayHelpers.RandomBool()) GamePlayHelpers.MakeVisible(box1, true);
        if (GamePlayHelpers.RandomBool()) GamePlayHelpers.MakeVisible(box2, true);
        if (GamePlayHelpers.RandomBool()) GamePlayHelpers.MakeVisible(box3, true);
        if (GamePlayHelpers.RandomBool()) GamePlayHelpers.MakeVisible(box4, true);
    }

    override protected IEnumerator HandleGameplayLoop() 
    {
        RuleSet ruleSet = new RuleSet();

        do {
            SetupScene();
            yield return new WaitForSeconds(2);

            ShowMessage("Trick or treat!");
            yield return new WaitForSeconds(2);

            Debug.Log("Rules: " + string.Join(",", ruleSet.GetRules()));
            
            ShowMessage("Make a choice!");
            yield return WaitForPlayerInput();

            if (playerWantsToOpenDoor) {
                ShowMessage("You opened the door...");
                yield return new WaitForSeconds(2);
                bool allPassed = ruleSet.TestAll();
                if (allPassed) {
                    ShowMessage("It was SAFE! You were lucky this time...");
                } else {
                    ShowMessage("It was NOT SAFE :(");
                    yield return new WaitForSeconds(1);
                    health -= 25;
                }
            } else {
                ShowMessage("You didnt open the door...");
                yield return new WaitForSeconds(2);
                bool allPassed = ruleSet.TestAll();
                if (allPassed) {
                    ShowMessage("It was SAFE! :( The ghouls are upset now...");
                    yield return new WaitForSeconds(1);
                    health -= 25;
                } else {
                    ShowMessage("It was NOT SAFE :D Good call!");
                }
            }

            yield return new WaitForSeconds(3);

            if (health > 0) {
                string newRule = ruleSet.AddRule();
                ShowMessage($"New rule: {newRule}");
                yield return new WaitForSeconds(4);
            }

        } while (health > 0);

        ShowMessage("Yo ded bruh");
    }

}