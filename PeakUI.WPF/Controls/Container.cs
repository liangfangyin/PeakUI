using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PeakUI.WPF
{
    public class Container : Control
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Container"/> class.
        /// </summary>
        static Container()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Container), new FrameworkPropertyMetadata(typeof(Container)));
        }

        /// <inheritdoc/>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate(); 
        }

    }
}
