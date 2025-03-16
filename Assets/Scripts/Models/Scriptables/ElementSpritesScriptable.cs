using UnityEngine;
namespace Ulf
{
    [CreateAssetMenu(fileName = "ElementSprites", menuName = "ScriptableObjects/ElementSprites", order = 8)]
    public class ElementSpritesScriptable : ScriptableObject
    {
        [SerializeField] private SpriteElementBind[] elementBinds;
    }

    [System.Serializable]
    public struct SpriteElementBind
    {
        public ElementType element;
        public Sprite sprite;
    }
}