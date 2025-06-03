using System.Collections.Generic;
using System.Linq;
using Rules;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class RuleManager : MonoBehaviour
    {
        public List<MonoBehaviour> ruleBehaviours;
        private readonly List<IGameRule> _activeRules = new();
        
        [SerializeField] private TextMeshProUGUI activeRuleText;
        
        private void Start()
        {
            activeRuleText.text = "";
            
            var allRules = ruleBehaviours.OfType<IGameRule>().ToList();
            var ruleCount = Random.Range(1, allRules.Count + 1);

            for (var i = 0; i < ruleCount; i++)
            {
                var rule = allRules[Random.Range(0, allRules.Count)];
                rule.Activate();
                _activeRules.Add(rule);
                allRules.Remove(rule);
            }
            
            foreach (var rule in _activeRules) activeRuleText.text += $"{rule.RuleName}\n";
        }

        public void ResetRules()
        {
            foreach (var rule in _activeRules) rule.Deactivate();
            _activeRules.Clear();
        }
    }
}
