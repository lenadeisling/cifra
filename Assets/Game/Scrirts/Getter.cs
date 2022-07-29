using UnityEngine;

namespace SortItems
{
    public class Getter : MonoBehaviour{

        private GameObject _obj;
        private void OnTriggerStay(Collider other) {
            var item = other.attachedRigidbody.GetComponent<DragItem>();
            if (item != null && item.isDraggable) {
                _obj = item.gameObject;
                return;
            }

            if (_obj == item.gameObject && !item.isDraggable) {
                Destroy(_obj);
                _obj = null;
                return;
            }
        }

    }
}
