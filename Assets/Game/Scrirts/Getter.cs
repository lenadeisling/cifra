using System;
using UnityEngine;
using UnityEngine.Events;

namespace SortItems
{
    public class Getter : MonoBehaviour{

        [SerializeField] private ItemType type;

        private int _targetCount = 1;
        private int _count = 0;
        private DragItem _item;
        private Material _material;
        private Color _defaultColor;

        private bool _active = true;

        public UnityEvent<Getter> onCountChange;

        public void SetCount(int value){
            _targetCount= value;
            if(_count >= _targetCount) {
                _material.color = Color.gray;
                _active =false;
            }
        }

        private void Start() {
            _material = GetComponent<MeshRenderer>().material;
            _defaultColor = _material.color;
        }
        private void OnTriggerStay(Collider other) {
            if (!_active)
                return;
            var item = other.attachedRigidbody.GetComponent<DragItem>();
            if (item != null && item.isDraggable) {
                _item = item;

                if (_item.Type == type) {
                    _material.color = Color.green;
                } else {
                    _material.color = Color.red;
                }

                return;
            }

            if (_item == item && !item.isDraggable) {
                TryGetItem();
                _item = null;
                _material.color = _defaultColor;
                return;
            }
        }

        private void OnTriggerExit(Collider other) {
            if (!_active)
                return;
            var item = other.attachedRigidbody.GetComponent<DragItem>();
            if (_item == item) {
                _material.color = _defaultColor;
                if (!item.isDraggable)
                    TryGetItem();
                _item = null;
            }
        }

        private void TryGetItem()
        {
            if (_item.Type == type) {
                Destroy(_item.gameObject);
                _count++;

                onCountChange.Invoke(this);

                if (_count >= _targetCount) {
                    _material.color = Color.gray;
                    _active = false;
                }
            }            
        }
    }

}
