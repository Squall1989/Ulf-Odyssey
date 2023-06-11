using UnityEngine;
namespace Ulf
{
    public class PlanetMono : MonoBehaviour
    {
        [SerializeField] private UnitMono[] startUnits;

        Planet planet;

        CircleCollider2D planetCollider;

        private void Awake()
        {
            planetCollider = GetComponent<CircleCollider2D>();
        }
        // Start is called before the first frame update
        void Start()
        {
            planet = new Planet(planetCollider.radius);
        }


        private void OnValidate()
        {
            SceneHub sceneHub = FindObjectOfType<SceneHub>();
            sceneHub.UpdateScene(this);
        }
    }
}