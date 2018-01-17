//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DialogEngine.Controls
{
    using log4net;
    using System.Reflection;

    /// <summary>
    /// Interaction logic for SettingsDialog.xaml
    /// </summary>
    public partial class SettingsDialog : Window
    {
        #region -fields-

        private static readonly ILog mcLogger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        #endregion

        /// <summary>
        /// Creates instance of SettingsDialog
        /// </summary>
        public SettingsDialog()
        {
            InitializeComponent();

            _generateDialog();
        }


        #region -private functions-

        
        private void _generateDialog()
        {

            NameValueCollection _settings = ConfigurationManager.AppSettings;

            int _index = 1;

            MainGrid.RowDefinitions.Add(new RowDefinition());

            Label _versionLabel = new Label();
            _versionLabel.HorizontalAlignment = HorizontalAlignment.Right;
            _versionLabel.Margin = new Thickness(0.0, 5.0, 0.0, 0.0);
            _versionLabel.Content = "Application version";

            MainGrid.Children.Add(_versionLabel);
            Grid.SetRow(_versionLabel, 0);
            Grid.SetColumn(_versionLabel, 0);

            Label _versionNumber = new Label();
            _versionNumber.HorizontalAlignment = HorizontalAlignment.Left;
            _versionNumber.Margin = new Thickness(0.0, 5.0, 0.0, 0.0);
            _versionNumber.Content = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            MainGrid.Children.Add(_versionNumber);
            Grid.SetRow(_versionNumber, 0);
            Grid.SetColumn(_versionNumber, 1);

            foreach (var _key in _settings.AllKeys)
            {
                this.MainGrid.RowDefinitions.Add(new RowDefinition());

                
                Label _label=new Label();
                _label.HorizontalAlignment = HorizontalAlignment.Right;
                _label.Margin=new Thickness(0.0,5.0,0.0,0.0);
                _label.Content = _key;

                MainGrid.Children.Add(_label);

                Grid.SetRow(_label,_index);
                Grid.SetColumn(_label,0);

                bool _flag;

                if (Boolean.TryParse(_settings[_key], out _flag))
                {
                    ToggleButton _toggleButton=new ToggleButton();
                    _toggleButton.HorizontalAlignment = HorizontalAlignment.Left;
                    _toggleButton.IsChecked = _flag;
                    _toggleButton.Style=this.FindResource("ToggleButtonTwoOptionsStyle") as Style;
                    _toggleButton.Width = 100;

                    MainGrid.Children.Add(_toggleButton);

                    Grid.SetRow(_toggleButton,_index);
                    Grid.SetColumn(_toggleButton,1);

                }
                else
                {
                    TextBox _textBox=new TextBox();
                    _textBox.Style= this.FindResource("TextBoxStyle1") as Style;
                    
                    _textBox.Text = _settings[_key];
                    _textBox.Margin=new Thickness(0.0,5.0,10.0,5.0);

                    MainGrid.Children.Add(_textBox);

                    Grid.SetRow(_textBox, _index);
                    Grid.SetColumn(_textBox, 1);

                }

                _index++;
            }



            this.MainGrid.RowDefinitions.Add(new RowDefinition());
            this.MainGrid.RowDefinitions[this.MainGrid.RowDefinitions.Count-1].Height=new GridLength(80);

            Button _closeButton =new Button();
            _closeButton.Style = this.FindResource("btn-primary") as Style;
            _closeButton.HorizontalAlignment = HorizontalAlignment.Right;
            _closeButton.Height = 40;
            _closeButton.Margin=new Thickness(0.0,0.0,20.0,0.0);
            _closeButton.Content = "Close";
            _closeButton.Click += new RoutedEventHandler(_closeDialog_Click);

            this.MainGrid.Children.Add(_closeButton);

            Grid.SetRow(_closeButton,this.MainGrid.RowDefinitions.Count-1);
            Grid.SetColumn(_closeButton,0);



            Button _saveChangesButton = new Button();
            _saveChangesButton.Style = this.FindResource("btn-primary") as Style;
            _saveChangesButton.HorizontalAlignment = HorizontalAlignment.Left;
            _saveChangesButton.Margin = new Thickness(20.0, 0.0,0.0, 0.0);
            _saveChangesButton.Height = 40;
            _saveChangesButton.Content = "Save changes";
            _saveChangesButton.Click += new RoutedEventHandler(_saveChanges_Click);


            this.MainGrid.Children.Add(_saveChangesButton);

            Grid.SetRow(_saveChangesButton, this.MainGrid.RowDefinitions.Count - 1);
            Grid.SetColumn(_saveChangesButton, 1);


            this.Height = this.MainGrid.RowDefinitions.Count * 50;

        }


        private void _saveChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                

                string _configPath = System.IO.Path.Combine(System.Environment.CurrentDirectory, "DialogEngine.exe");

                Configuration _config = ConfigurationManager.OpenExeConfiguration(_configPath);


                int _rowsCount = this.MainGrid.RowDefinitions.Count -2;

                for (int i = 1; i <= _rowsCount; i++)
                {
                    Label _label = _getChildAt(i, 0) as Label;

                    UIElement _value = _getChildAt(i, 1);

                    if (_value is ToggleButton)
                    {
                        _config.AppSettings.Settings[_label.Content.ToString()].Value = (_value as ToggleButton).IsChecked.ToString();
                    }
                    else
                    {
                        _config.AppSettings.Settings[_label.Content.ToString()].Value = (_value as TextBox)?.Text;
                    }
                 
                }

                
                _config.Save();

                ConfigurationManager.RefreshSection("appSettings");

                this.Close();
            }
            catch (Exception _ex)
            {
                mcLogger.Error(_ex.Message);
                MessageBox.Show("Error during saving changes.");

            }

        }



        private void _closeDialog_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private UIElement _getChildAt(int _row, int _column)
        {
            UIElement _childElement = this.MainGrid.Children
                                        .Cast<UIElement>()
                                        .First(e => Grid.GetRow(e) == _row && Grid.GetColumn(e) == _column);

            return _childElement;
        }



        #endregion


    }
}
