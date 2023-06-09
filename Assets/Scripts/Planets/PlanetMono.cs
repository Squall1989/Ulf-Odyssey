using UnityEngine;
namespace Ulf
{
    public class PlanetMono : MonoBehaviour
    {
        [SerializeField] private UnitMono[] startUnits;

        CircleCollider2D planetCollider;

        private void Awake()
        {
            planetCollider = GetComponent<CircleCollider2D>();
        }
        // Start is called before the first frame update
        void Start()
        {

        }

    }
}