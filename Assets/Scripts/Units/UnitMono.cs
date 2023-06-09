using UnityEngine;

namespace Ulf
{
    public class UnitMono : MonoBehaviour
    {
        protected Unit unit;

        // Start is called before the first frame update
        void Start()
        {
            unit = new Unit();
        }

    }
}