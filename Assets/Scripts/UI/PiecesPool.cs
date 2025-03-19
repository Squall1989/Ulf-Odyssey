using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ulf
{
    public class PiecesPool : MonoBehaviour
    {
        [SerializeField] private Image[] pieces;

        private Stack<Image> spritesStack;

        private void Awake()
        {
            spritesStack = new Stack<Image>(pieces.Length);
            for (int i = 0; i < pieces.Length; i++)
            {
                spritesStack.Push(pieces[i]);
            }
        }

        public Image GetPiece()
        {
            if (spritesStack.Count == 0)
            {
                return Instantiate(pieces[0], pieces[0].transform.parent);
            }
            else
            {
                return spritesStack.Pop();

            }
        }

        public void ReturnPiece(Image piece)
        {
            piece.enabled = false;
            spritesStack.Push(piece);
        }
    }
}