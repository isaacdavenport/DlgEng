//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using log4net;
using System;
using System.Configuration;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;


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
            // add list of all valid COM port names to combobox
            foreach(string port in System.IO.Ports.SerialPort.GetPortNames())
            {
                this.SerialPortsComboBox.Items.Add(port);
            }

            // if there no valid COM port we add item with message
            if(SerialPortsComboBox.Items.Count == 0)
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

                    DialogResult = true;

                    this.Close();
                }
                catch(Exception ex)
                {
                    mcLogger.Error("Error during saving COM port name. "+ ex.Message);
                    MessageBox.Show("Error during saving COM port name.");
                }            
        }


        private void _close_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }




        #endregion


    }
}
