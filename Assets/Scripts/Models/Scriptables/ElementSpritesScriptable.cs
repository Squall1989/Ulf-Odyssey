using UnityEngine;
namespace Ulf
{
    [CreateAssetMenu(fileName = "ElementSprites", menuName = "ScriptableObjects/ElementSprites", order = 8)]
    public class ElementSpritesScriptable : ScriptableObject
    {
        [SerializeField] private SpriteElementBind[] elementBinds;

        public Sprite GetByElement(ElementType elementType)
        {
            for (int i = 0; i < elementBinds.Length; i++)
            {
                if (elementBinds[i].element == elementType)
                {
                    return elementBinds[i].sprite;
                }
            }
            throw new System.InvalidOperationException("Element type not found!");
        }
    }

    [System.Serializable]
    public struct SpriteElementBind
    {
        public ElementType element;
        public Sprite sprite;
    }
}