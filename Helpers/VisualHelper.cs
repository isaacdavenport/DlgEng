using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace DialogEngine.Helpers
{
    /// <summary>
    /// Helper class which help to serach nodes in visual tree of application/> 
    /// </summary>
    public static class VisualHelper
    {
        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="initial"></param>
        /// <returns>Returns firt container of initial with type T </returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <returns>Returns first child of type T for parent object</returns>
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
