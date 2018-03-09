using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DialogEngine.Dialogs
{
    /// <summary>
    /// Interaction logic for SerialComPortErrorDialogUserControl.xaml
    /// </summary>
    public partial class SerialComPortErrorDialogControl : UserControl
    {
        #region - fields -

        private static readonly ILog mcLogger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region - constructor -

        public SerialComPortErrorDialogControl()
        {
            InitializeComponent();

            _populateComPortsComboBox();
        }

        #endregion

        #region - private functions -

        private void _populateComPortsComboBox()
        {
            if (SerialPortsComboBox.HasItems)
            {
                SerialPortsComboBox.Items.Clear();
            }

            this.SaveChangesBtn.IsEnabled = true;

            // add list of all valid COM port names to combobox
            foreach (string port in System.IO.Ports.SerialPort.GetPortNames())
            {
                this.SerialPortsComboBox.Items.Add(port);
            }

            // if there no valid COM port we add item with message
            if (SerialPortsComboBox.Items.Count == 0)
            {
                this.SerialPortsComboBox.Items.Add("No valid com ports");

                this.SaveChangesBtn.IsEnabled = false;
            }

            SerialPortsComboBox.SelectedIndex = 0;
        }

        #endregion

        #region - event handlers -

        private void _saveChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string _configPath = System.IO.Path.Combine(System.Environment.CurrentDirectory, "DialogEngine.exe");

                Configuration _config = ConfigurationManager.OpenExeConfiguration(_configPath);

                _config.AppSettings.Settings["ComPortName"].Value = SerialPortsComboBox.SelectedValue.ToString();

                _config.Save();

                ConfigurationManager.RefreshSection("appSettings");

                //DialogResult = true;

                DialogHost.CloseDialogCommand.Execute(null, sender as Button);
            }
            catch (Exception ex)
            {
                mcLogger.Error("Error during saving COM port name. " + ex.Message);
                MessageBox.Show("Error during saving COM port name.");
            }
        }


        private void _close_Click(object sender, RoutedEventArgs e)
        {
            //DialogResult = false;
            DialogHost.CloseDialogCommand.Execute(null, sender as Button);
        }


        private void _refresh_Click(object sender, RoutedEventArgs e)
        {
            _populateComPortsComboBox();
        }

        #endregion
    }
}