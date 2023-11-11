using System.Linq;
using Ulf;
using UnityEngine;

[CreateAssetMenu(fileName = "Planets", menuName = "ScriptableObjects/Planets", order = 3)]
public class AllPlanetsScriptable : ScriptableObject
{
    [SerializeField] private PlanetMono[] planetsMono;

    public PlanetMono[] PlanetMono => planetsMono;

    public PlanetMono GetPlanet(CreatePlanetStruct planetStruct)
    {
        return planetsMono.FirstOrDefault(p => p.Size == planetStruct.planetSize && p.ElementType == planetStruct.ElementType);
    }

    public float[] GetSizes(ElementType elementType)
    {
        return planetsMono.Where(p => p.ElementType == elementType).Select(p => p.Size).ToArray();
    }
}
