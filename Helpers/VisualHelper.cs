//Confidential Source Code Property Toys2Life LLC Colorado 2017
//www.toys2life.org

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace DialogEngine.Helpers
{
    /// <summary>
    /// Helper class which helps to search nodes in visual tree of the application
    /// </summary>
    public static class VisualHelper
    {
        /// <summary>
        /// Returns firt container of initial with type T
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
        /// Returns first child of type T for parent object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_parent"></param>
        /// <returns>Returns first child of type T for parent object</returns>
        public static T GetVisualChild<T>(this DependencyObject _parent) where T : FrameworkElement
        {
            T _child = default(T);

            int _numVisuals = VisualTreeHelper.GetChildrenCount(_parent);

            for (int i = 0; i < _numVisuals; i++)
            {
                Visual _visual = (Visual)VisualTreeHelper.GetChild(_parent, i);
                _child = _visual as T;

                if (_child == null)
                {
                    _child = GetVisualChild<T>(_visual);
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
