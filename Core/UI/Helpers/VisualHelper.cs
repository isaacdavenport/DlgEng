using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace DialogEngine.UI.Helpers
{
    public static class VisualHelper
    {
        public static T GetNearestContainer<T>(DependencyObject initial) where T : DependencyObject
        {
            DependencyObject visual = initial;

            if (visual is Visual || visual is System.Windows.Media.Media3D.Visual3D)
            {
                while (visual != null && visual.GetType() != typeof(T))
                {
                    visual = VisualTreeHelper.GetParent(visual);
                }
            }

            return visual as T;
        }

        public static T GetVisualChild<T>(this DependencyObject parent) where T : FrameworkElement
        {
            T child = default(T);

            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);

            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }
    }
}
