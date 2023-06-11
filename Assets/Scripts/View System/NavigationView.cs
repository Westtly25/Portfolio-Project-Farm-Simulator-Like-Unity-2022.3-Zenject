using Zenject;
using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.View_System
{
    public class NavigationView
    {
        [SerializeField]
        private View activeView;

        [SerializeField]
        private List<View> cachedViews = new List<View>(6);

        [SerializeField]
        private readonly Stack<View> viewStack = new Stack<View>(6);

        [Header("Injected Data")]
        private readonly ViewFactory viewFactory;

        [Inject]
        public NavigationView(ViewFactory viewFactory)
        {
            this.viewFactory = viewFactory;
        }

        public void CleanUp()
        {
            viewStack.Clear();
            cachedViews.Clear();
        }

        public T GetView<T>() where T : View
        {
            for (var i = 0; i < cachedViews.Count; i++)
            {
                if (cachedViews[i] is T tView)
                {
                    return tView;
                }
            }

            return null;
        }

        public void Show<T>(bool remember = true) where T : View
        {
            for (var i = 0; i < cachedViews.Count; i++)
            {
                if (cachedViews[i] is T)
                {
                    if (remember)
                        viewStack.Push(activeView);

                    activeView.OnDisappear();
                    cachedViews[i].OnAppear();
                    activeView = cachedViews[i];
                }
            }
        }

        public void Show(View view, bool remember = true)
        {
            if (activeView != null)
            {
                if (remember)
                {
                    viewStack.Push(activeView);
                }

                activeView.OnDisappear();
            }

            view.OnAppear();

            activeView = view;
        }

        public void Pop<T>() where T : View
        {
            T view = GetView<T>();

            viewStack.Pop();
            view.OnDisappear();
        }
       
        public void ShowPopup(View view)
        {
            viewStack.Push(activeView);
            view.DisplayOrder(100);
            view.OnAppear();
            activeView = view;
        }

        public void ShowLast()
        {
            if (viewStack.Count != 0)
            {
                Show(viewStack.Pop(), false);
            }
        }
    }
}