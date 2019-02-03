using UnityEngine;
using UnityEngine.UI;

namespace MosaicNumbers
{
    public class GameBoard: MonoBehaviour
    {
        [SerializeField]
        private GridLayoutGroup _gridLayout;
        [SerializeField]
        private GameObject _imagePrefab;

        public void Construct(int rows, int columns)
        {
            PrepareGridLayout(rows,columns);
            FillLayoutWithElements(rows*columns);
        }
  
        private void PrepareGridLayout(int rows, int columns)
        {
            _gridLayout.constraintCount = columns;
            _gridLayout.cellSize = new Vector2(Screen.width / columns, Screen.height / rows);
        }
        
        private void FillLayoutWithElements(int amount)
        {
            for (var i = 0; i < amount; i++)
                Instantiate(_imagePrefab, _gridLayout.transform);
        }
    }
}