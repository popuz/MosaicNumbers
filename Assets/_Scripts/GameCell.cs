using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MosaicNumbers
{
    [RequireComponent(typeof(Button))]
    public class GameCell : MonoBehaviour, IPointerDownHandler
    {
        public event Action<int> OnCellPressed; 
        
        private TMP_Text _text;
        private int _number;
        
        private void Awake() => _text = GetComponentInChildren<TMP_Text>();

        public void SetNumber(int number)
        {
            _number = number;
            _text.text = number.ToString();
        }

        public void OnPointerDown(PointerEventData eventData) => OnCellPressed?.Invoke(_number);
    }
}
