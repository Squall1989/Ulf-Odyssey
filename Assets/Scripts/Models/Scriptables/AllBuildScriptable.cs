using System.Linq;
using Ulf;
using UnityEngine;

[CreateAssetMenu(fileName = "Builds", menuName = "ScriptableObjects/Builds", order = 4)]
public class AllBuildScriptable : ScriptableObject
{
    [SerializeField] private BuildMono[] builds;

    public BuildMono[] Builds => builds;

    internal BuildMono[] GetBuilds(CreateBuildStruct[] createUnits)
    {
        BuildMono[] units = new BuildMono[createUnits.Length];

        for (int i = 0; i < createUnits.Length; i++)
        {
            units[i] = builds.First(p => p.DefaultBuild.View == createUnits[i].View);
        }

        return units;
    }
}
