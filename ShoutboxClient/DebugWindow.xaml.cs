using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ShoutboxClient
{
    /// <summary>
    /// Interaction logic for DebugWindow.xaml
    /// </summary>
    public partial class DebugWindow : Window
    {
        public DebugWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Simply logs a string to the "console"
        /// </summary>
        public void Log(string m)
        {
            lbDebug.Items.Add(m);

            lbDebug.SelectedIndex = lbDebug.Items.Count - 1;
            lbDebug.ScrollIntoView(lbDebug.SelectedItem);
        }
    }
}
