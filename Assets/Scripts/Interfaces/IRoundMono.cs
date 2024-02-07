using UnityEngine;

public interface IRoundMono 
{
    Transform TransformRound { get; }
    void LookAtCenter(Transform unitTransform);
}
