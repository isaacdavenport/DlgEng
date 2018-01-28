using log4net;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
using System.Windows.Shapes;

namespace DialogEngine.Dialogs
{
    /// <summary>
    /// Interaction logic for SerialComPortErrorDialog.xaml
    /// </summary>
    public partial class SerialComPortErrorDialog : Window
    {

        #region - fields -

        private static readonly ILog mcLogger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region - constructor -

        public SerialComPortErrorDialog()
        {
            InitializeComponent();

            _populateComPortsComboBox();
        }

        #endregion

        #region - private functions -

        private void _populateComPortsComboBox()
        {
            foreach(string port in System.IO.Ports.SerialPort.GetPortNames())
            {
                this.SerialPortsComboBox.Items.Add(port);
            }

            if(SerialPortsComboBox.Items.Count == 0)
            {
                this.SerialPortsComboBox.Items.Add("No valid com ports");
            }

            SerialPortsComboBox.SelectedIndex = 0;
        }

        #endregion

        #region - event handlers -

        private void _saveChanges_Click(object sender, RoutedEventArgs e)
        {
            if(SerialPortsComboBox.SelectedIndex > 0)
            {
                try
                {
                    string _configPath = System.IO.Path.Combine(System.Environment.CurrentDirectory, "DialogEngine.exe");

                    Configuration _config = ConfigurationManager.OpenExeConfiguration(_configPath);

                    _config.AppSettings.Settings["UseSerialPort"].Value = SerialPortsComboBox.SelectedValue.ToString();

                    _config.Save();

                    ConfigurationManager.RefreshSection("appSettings");

                    DialogResult = true;

                    this.Close();
                }
                catch(Exception ex)
                {
                    mcLogger.Error("Error during saving COM port name. "+ ex.Message);
                    MessageBox.Show("Error during saving COM port name.");
                }

            }
        }


        private void _close_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }


        private void _comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((e.Source as ComboBox).SelectedIndex > 0)
            {
                SaveChangesBtn.IsEnabled = true;
            }
            else
            {
                SaveChangesBtn.IsEnabled = false;
            }

        }

        #endregion


    }
}
