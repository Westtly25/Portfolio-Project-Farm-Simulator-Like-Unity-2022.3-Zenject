using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.View_System
{
    [RequireComponent(typeof(UIDocument))]
    public abstract class View : MonoBehaviour
    {
        [SerializeField]
        public UIDocument uIDocument;

        [SerializeField]
        public VisualElement rootVisualElement;

        protected bool isDirty = true;

        [ContextMenu("Initialize")]
        public void Initialize()
        {
            if (TryGetComponent<UIDocument>(out UIDocument uIDocument))
            {
                this.uIDocument = uIDocument;
                rootVisualElement = uIDocument.rootVisualElement;
            }
        }

        [ContextMenu("OnAppear")]
        public void OnAppear()
        {
            if (isDirty)
                BindDocumentData();

            RegisterCallbacks();

            isDirty = false;
        }

        [ContextMenu("OnDisappear")]
        public void OnDisappear() =>
            UnRegisterCallbacks();

        protected abstract void BindDocumentData();
        protected virtual void RegisterCallbacks() { }
        protected virtual void UnRegisterCallbacks() { }

        public void DisplayOrder(int order) =>
            uIDocument.sortingOrder = order;
    }
}