using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace PeakUI.WPF.Converters
{
    /// <summary>
    /// bool - 显示状态转换器
    /// </summary>
    public class BooleanToVisibilityConverter : BooleanConverter<Visibility>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BooleanToVisibilityConverter"/> class.
        /// </summary>
        public BooleanToVisibilityConverter() : base(Visibility.Visible, Visibility.Collapsed)
        {
        }
    }

    /// <summary>
    /// bool 转换器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BooleanConverter<T> : IValueConverter
    {
        /// <summary>
        /// bool 转换器
        /// </summary>
        /// <param name="trueValue">true 对应值</param>
        /// <param name="falseValue">false 对应值</param>
        protected BooleanConverter(T trueValue, T falseValue)
        {
            TrueValue = trueValue;
            FalseValue = falseValue;
        }

        /// <summary>
        /// true 对应值
        /// </summary>
        public T TrueValue { get; set; }

        /// <summary>
        /// false 对应值
        /// </summary>
        public T FalseValue { get; set; }

        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return BooleanConverter<T>.ConvertBooleanToType(value, TrueValue, FalseValue);
        }

        /// <summary>
        /// 转换 bool 为类型
        /// </summary>
        /// <param name="value">bool 值</param>
        /// <param name="trueValue">true 时的值</param>
        /// <param name="falseValue">false 时的值</param>
        /// <returns>类型的值</returns>
        internal static T ConvertBooleanToType(object value, T trueValue, T falseValue)
        {
            bool flag = ValidateArgument.NotNullOrEmptyCast<bool>(value, "value");
            return flag ? trueValue : falseValue;
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is T flag && EqualityComparer<T>.Default.Equals(flag, TrueValue);
        }
    }


    /// <summary>
	/// 参数验证
	/// </summary>
	public sealed class ValidateArgument
    {
        /// <summary>
        /// 非 null
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="parameterName">参数名称</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void NotNull(object obj, string parameterName)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        /// <summary>
        /// 非 null 或空
        /// </summary>
        /// <param name="parameterValue">字符串值</param>
        /// <param name="parameterName">参数名称</param>
        /// <exception cref="ArgumentException"></exception>
        public static void NotNullOrEmpty(string parameterValue, string parameterName)
        {
            if (string.IsNullOrEmpty(parameterValue))
            {
                throw new ArgumentException(parameterName);
            }
        }

        /// <summary>
        /// 非 Null 或空类型转换
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="parameterValue">参数值</param>
        /// <param name="parameterName">参数名称</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static T NotNullOrEmptyCast<T>(object parameterValue, string parameterName)
        {
            ValidateArgument.NotNull(parameterValue, parameterName);
            if (parameterValue is T)
            {
                return (T)((object)parameterValue);
            }
            throw new ArgumentException(parameterName);
        }

        /// <summary>
        /// 类型转换
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="parameterValue">参数值</param>
        /// <param name="parameterName">参数名称</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static T Cast<T>(object parameterValue, string parameterName)
        {
            if (parameterValue == null || parameterValue is T)
            {
                return (T)((object)parameterValue);
            }
            throw new ArgumentException(parameterName);
        }

        /// <summary>
        /// 是否在范围内
        /// </summary>
        /// <param name="checkedExpression">检查表达式</param>
        /// <param name="parameterName">参数名称</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void Range(bool checkedExpression, string parameterName)
        {
            if (!checkedExpression)
            {
                throw new ArgumentOutOfRangeException(parameterName);
            }
        }

        /// <summary>
        /// 是否在范围内
        /// </summary>
        /// <param name="checkedExpression">检查表达式</param>
        /// <param name="parameterName">参数名称</param>
        /// <param name="message">错误信息</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void Range(bool checkedExpression, string parameterName, string message)
        {
            if (!checkedExpression)
            {
                throw new ArgumentOutOfRangeException(parameterName, message);
            }
        }
    }

}
