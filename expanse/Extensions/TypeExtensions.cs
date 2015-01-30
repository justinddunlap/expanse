using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Expanse.Extensions
{
    public static class TypeExtensions
    {
        public static T CreateInstance<T>(this Type type, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes = null)
        {
            if(activationAttributes != null)
                return (T)Activator.CreateInstance(type, bindingAttr, binder, args, culture, activationAttributes);
            else
                return (T)Activator.CreateInstance(type, bindingAttr, binder, args, culture);
        }

        public static T CreateInstance<T>(this Type type, object[] args, object[] activationAttributes = null)
        {
            if(activationAttributes != null)
                return (T)Activator.CreateInstance(type, args, activationAttributes);
            else
                return (T)Activator.CreateInstance(type, args);
        }

        public static T CreateInstance<T>(this Type type)
        {
            return (T)Activator.CreateInstance(type);
        }

        /// <summary>
        ///     A Type extension method that creates an instance.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="nonPublic">true to non public.</param>
        /// <returns>The new instance.</returns>
        public static T CreateInstance<T>(this Type @this, Boolean nonPublic)
        {
            return (T)Activator.CreateInstance(@this, nonPublic);
        }
    }
}
