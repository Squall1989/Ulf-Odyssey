using UnityEngine;

namespace Ulf
{
    public class UnitMono : MonoBehaviour
    {
        [SerializeField] private Movement movement;

        protected Unit unit;
        
        public Unit Unit => unit;

        // Start is called before the first frame update
        void Start()
        {

        }


        public void Init(Planet planet)
        {
            unit = new Unit(planet, movement.CircleMove, null);
        }
    }
}