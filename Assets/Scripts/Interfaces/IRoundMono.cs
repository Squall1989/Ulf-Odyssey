using UnityEngine;

public interface IRoundMono 
{
    Transform TransformRound { get; }
    float LookAtCenter(Transform unitTransform);
}
