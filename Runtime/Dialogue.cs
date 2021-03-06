using Managers;
using UnityEngine;
using UnityEngine.Events;

namespace UI.Core
{
    /// <summary>
    /// A Dialogue is a collection of <see cref="DialogueComponent{T}"/>s that should be presented and interacted with at the same time.
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class Dialogue : MonoBehaviour
    {
        internal readonly UnityEvent promoted = new UnityEvent();
        internal readonly UnityEvent demoted = new UnityEvent();
        internal readonly UnityEvent closed = new UnityEvent();
        
        protected UIManager manager;
        protected CanvasGroup canvasGroup;
        
        
        #region MonoBehaviour

        private void Awake()
        {
            manager = UIManager.Instance;
            canvasGroup = GetComponent<CanvasGroup>();
            
            OnAwake();
            
            manager.Add(this);
        }

        #endregion
        
        
        #region Dialogue

        internal void Promote()
        {
            canvasGroup.interactable = true;
            
            OnPromote();
            promoted.Invoke();
        }

        internal void Demote()
        {
            canvasGroup.interactable = false;
            
            OnDemote();
            demoted.Invoke();
        }
        
        public void Close()
        {
            OnClose();
            Destroy(gameObject);
            closed.Invoke();
        }

        protected virtual void OnAwake() {}

        protected abstract void OnClose();

        protected abstract void OnPromote();

        protected abstract void OnDemote();
        
        #endregion
    }
}
