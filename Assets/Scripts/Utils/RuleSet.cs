using System;
using System.Collections.Generic;

public class Rule
{
    public string Description;
    public Func<bool> Condition;

    public Rule(string description, Func<bool> condition)
    {
        Description = description;
        Condition = condition;
    }

    public bool Test() => Condition?.Invoke() ?? false;
}

public class RuleSet
{
    public static List<Rule> all = new List<Rule>
    {

    };

    private readonly List<Rule> _activeRules = new List<Rule>();

    public string AddRule()
    {
        List<Rule> available = all.FindAll(r => !_activeRules.Exists(a => a.Description == r.Description));
        if (available.Count == 0) return "";

        int rnd = UnityEngine.Random.Range(0, available.Count);
        Rule newRule = available[rnd];
        _activeRules.Add(newRule);
        return newRule.Description;
    }

    public IEnumerable<string> GetRules()
    {
        foreach (var rule in _activeRules)
        {
            yield return rule.Description;
        }
    }

    public void Randomize()
    {
        for (int i = 0; i < _activeRules.Count; i++)
        {
            int rnd = UnityEngine.Random.Range(i, _activeRules.Count);
            Rule temp = _activeRules[i];
            _activeRules[i] = _activeRules[rnd];
            _activeRules[rnd] = temp;
        }
    }

    public bool TestAll()
    {
        foreach (var rule in _activeRules)
        {
            if (!rule.Test()) return false;
        }
        return true;
    }

    public int Count => _activeRules.Count;
}