using System;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Proxy
{
    /// <summary>Возвращает экземпляр <see cref="ProxyValue{T}"/> для указанного типа
    /// и задаёт в нём указанныую привязку.</summary>
    [MarkupExtensionReturnType(typeof(ProxyValue<>))]
    public class ProxyValueExtension : MarkupExtension
    {
        public Type Type { get; set; }

        public BindingBase ValueBinding { get; set; }

        public ProxyValueExtension(Type type)
        {
            Type = type;
        }

        public ProxyValueExtension()
        {
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            DependencyObject proxy;
            DependencyProperty property;
            Type typeT = Type;
            if (typeT == null)
            {
                typeT = typeof(object);
            }

            Type type = typeof(ProxyValue<>).MakeGenericType(typeT);
            proxy = (DependencyObject)Activator.CreateInstance(type);
            property = (DependencyProperty)proxy.GetType().GetField("ValueProperty", BindingFlags.Public | BindingFlags.Static).GetValue(null);

            if(ValueBinding != null)
            {
                _ = BindingOperations.SetBinding(proxy, property, ValueBinding);
            }

            return proxy;
        }
    }

}
