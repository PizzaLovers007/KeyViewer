using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KeyViewer
{
    static class DependencyPropertyExtension
    {
        public static DependencyProperty Register(
            string name,
            Type propertyType,
            Type ownerType,
            object defaultValue,
            PropertyChangedCallback propertyChangedCallback)
        {
            return DependencyProperty.Register(name, propertyType, ownerType, new PropertyMetadata(defaultValue, propertyChangedCallback));
        }

        public static DependencyProperty Register(
            string name,
            Type propertyType,
            Type ownerType,
            PropertyChangedCallback propertyChangedCallback)
        {
            return DependencyProperty.Register(name, propertyType, ownerType, new PropertyMetadata(propertyChangedCallback));
        }

        public static DependencyProperty Register(
            string name,
            Type propertyType,
            Type ownerType,
            object defaultValue)
        {
            return DependencyProperty.Register(name, propertyType, ownerType, new PropertyMetadata(defaultValue));
        }
    }
}
