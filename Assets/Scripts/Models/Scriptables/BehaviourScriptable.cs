using UnityEngine;

[CreateAssetMenu(fileName = "Behaviour", menuName = "ScriptableObjects/Behaviour", order = 5)]
public class BehaviourScriptable : ScriptableObject
{
    public float lookDist;
    public float attackDist;
    public AttackPattern attackPattern;
    public ItemId friendableItem;
}
