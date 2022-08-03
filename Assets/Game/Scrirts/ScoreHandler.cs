using UnityEngine;
using UnityEngine.Events;

namespace SortItems
{
    public class ScoreHandler: MonoBehaviour{
        [SerializeField] private GetterParameters[] _getters;
        
        public UnityEvent onFull;
        
        private void Start() {
            if (_getters == null) {
                Debug.LogError("Getters is null");
                return;
            }

            foreach(var item in _getters){
                item.getter.SetCount(item.targetCount);
                item.getter.onCountChange.AddListener(OnCountChanged);
            }
        }

        private void OnDestroy() {
            foreach(var item in _getters){
                item.getter.onCountChange.RemoveListener(OnCountChanged);
            }
        }

        private void OnCountChanged(Getter getter){
            for(int idx = 0; idx < _getters.Length; idx++){
                ref var item = ref _getters[idx];

                if(item.getter != getter)
                    continue;
                item.count++;
            }

            bool full= true;
            foreach(GetterParameters item in _getters){
                if (item.count < item.targetCount) {
                    full = false;
                    break;
                }
            }

            if (full) {
                Debug.Log("You win!");
                onFull.Invoke();
            }
        }
    }

    [System.Serializable]
    public struct GetterParameters{
        public Getter getter;
        public int targetCount;
        [HideInInspector]public int count;
    }

}
