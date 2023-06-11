using UnityEngine;

namespace Ulf
{
    public class Movement : MonoBehaviour
    {
        private CircleMove circleMove;
        public CircleMove CircleMove => circleMove;


        private void Start()
        {
            circleMove = new CircleMove();
        }

        private void Update()
        {
            circleMove.SetDeltaTime(Time.deltaTime);
        }
    }
}