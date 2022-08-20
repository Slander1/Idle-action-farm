using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ResourceViewItem : MonoBehaviour
    {
        [SerializeField] private Text nameText;
        [SerializeField] private Text amountText;

        private int _amount;
        public int Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                amountText.text = value.ToString();
            }
        }

        public void Init(string name)
        {
            Amount = 0;
            nameText.text = name;
        }
    }
}