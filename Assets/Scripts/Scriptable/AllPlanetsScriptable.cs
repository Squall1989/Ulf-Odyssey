using System.Linq;
using Ulf;
using UnityEngine;

[CreateAssetMenu(fileName = "Planets", menuName = "ScriptableObjects/Planets", order = 3)]
public class AllPlanetsScriptable : ScriptableObject
{
    [SerializeField] private PlanetMono[] planetsMono;

    public PlanetMono[] PlanetMono => planetsMono;

    public PlanetMono GetPlanet(float size, ElementType element)
    {
        return planetsMono.FirstOrDefault(p => p.Size == size && p.ElementType == element);
    }

    public float[] GetSizes(ElementType elementType)
    {
        return planetsMono.Where(p => p.ElementType == elementType).Select(p => p.Size).ToArray();
    }
}
