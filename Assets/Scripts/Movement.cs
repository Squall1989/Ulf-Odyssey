using UnityEngine;

namespace Ulf
{
    public class Movement : MonoBehaviour
    {
        CircleMove circleMove;

        private void Start()
        {

        }

        private void Update()
        {
            circleMove.SetDeltaTime(Time.deltaTime);
        }
    }
}