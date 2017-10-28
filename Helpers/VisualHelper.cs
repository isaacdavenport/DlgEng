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
        /// <param name="_initial"></param>
        /// <returns>Returns firt container of initial with type T </returns>
        public static T GetNearestContainer<T>(DependencyObject _initial) where T : DependencyObject
        {
            DependencyObject _visual = _initial;

            if (_visual is Visual || _visual is System.Windows.Media.Media3D.Visual3D)
            {
                while (_visual != null && _visual.GetType() != typeof(T))
                {
                    _visual = VisualTreeHelper.GetParent(_visual);
                }
            }

            return _visual as T;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_parent"></param>
        /// <returns>Returns first child of type T for parent object</returns>
        public static T GetVisualChild<T>(this DependencyObject _parent) where T : FrameworkElement
        {
            T _child = default(T);

            int _numVisuals = VisualTreeHelper.GetChildrenCount(_parent);

            for (int _i = 0; _i < _numVisuals; _i++)
            {
                Visual _v = (Visual)VisualTreeHelper.GetChild(_parent, _i);
                _child = _v as T;
                if (_child == null)
                {
                    _child = GetVisualChild<T>(_v);
                }
                if (_child != null)
                {
                    break;
                }
            }
            return _child;
        }
    }
}
