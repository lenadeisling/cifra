using UnityEngine;
using UnityEngine.EventSystems;

namespace SortItems
{
    public class DragItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        private Rigidbody _rigidbody;

        [SerializeField] private ItemType _type;
        [SerializeField] private float _force = 50f;

        public bool isDraggable {get; private set;}
        public ItemType Type { get => _type; }

        private void Start ()
        {
            _rigidbody = GetComponent<Rigidbody>();
        } 
        public void OnDrag(PointerEventData eventData)
        {
            if(!eventData.pointerCurrentRaycast.isValid){
                _rigidbody.isKinematic = false;
                isDraggable = false;
                return;
            }
            var pos = eventData.pointerCurrentRaycast.worldPosition;
            var delta = pos - transform.position;
            delta.y = 0;

            transform.position += delta;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _rigidbody.isKinematic = true;
            isDraggable = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
           _rigidbody.isKinematic = false;
           _rigidbody.AddForce(Vector3.up*_force);
           isDraggable = false;
        }
    }
}
