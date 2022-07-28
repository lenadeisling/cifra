using UnityEngine;
using UnityEngine.EventSystems;

namespace SortItems
{
    public class DragItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        private Rigidbody _rigidbody;

        private void Start ()
        {
            _rigidbody = GetComponent<Rigidbody>();
        } 
        public void OnDrag(PointerEventData eventData)
        {
            var pos = eventData.pointerCurrentRaycast.worldPosition;
            var delta = pos - transform.position;
            delta.y = 0;

            transform.position += delta;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _rigidbody.isKinematic = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
           _rigidbody.isKinematic = false;
        }
    }
}
