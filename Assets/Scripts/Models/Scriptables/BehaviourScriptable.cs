using UnityEngine;

[CreateAssetMenu(fileName = "Behaviour", menuName = "ScriptableObjects/Behaviour", order = 5)]
public class BehaviourScriptable : ScriptableObject
{
    public string behavId;
    public AttackCondition[] attackPatterns;
    public ItemId friendableItem;
}
