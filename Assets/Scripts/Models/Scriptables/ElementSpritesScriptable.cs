using UnityEngine;
namespace Ulf
{

    [CreateAssetMenu(fileName = "ElementSprites", menuName = "ScriptableObjects/ElementSprites", order = 8)]
    public class ElementSpritesScriptable : ScriptableObject
    {
        [SerializeField] private SpriteElementBind[] elementBinds;

        public Sprite GetByElement(ElementType elementType, bool isFront)
        {
            for (int i = 0; i < elementBinds.Length; i++)
            {
                if (elementBinds[i].element == elementType)
                {
                    return isFront ? elementBinds[i].sprite : elementBinds[i].back;
                }
            }
            throw new System.InvalidOperationException("Element type not found!");
        }

        [System.Serializable]
        struct SpriteElementBind
        {
            public ElementType element;
            public Sprite sprite;
            public Sprite back;
        }
    }

    
}