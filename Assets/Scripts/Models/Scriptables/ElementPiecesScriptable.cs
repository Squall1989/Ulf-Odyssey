using UnityEngine;
namespace Ulf
{
    [CreateAssetMenu(fileName = "ElementPieces", menuName = "ScriptableObjects/ElementPieces", order = 9)]
    public class ElementPiecesScriptable : ScriptableObject
    {
        [SerializeField] private ElementType element;
        [SerializeField] private ElementPieceStart[] pieces;

        public ElementType Element => element;
        public ElementPieceStart[] Pieces => pieces;

        
    }

    [System.Serializable]
    public struct ElementPieceStart
    {
        public Sprite sprite;
        public Vector2 size;
        public Vector2 position;
    }
}