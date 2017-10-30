﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Threading;
using DialogEngine.Core;
using DialogEngine.Helpers;
using DialogEngine.ViewModels.Dialog;
using DialogEngine.Views.Dialog;
using log4net;
using log4net.Repository.Hierarchy;
using Path = System.IO.Path;


namespace DialogEngine
{

    public partial class MainWindow : Window
    {
        #region -fields-

        private static readonly ILog mcLogger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        //creating of delegate ( it is similar to pointer on function in C )
        public delegate void PrintMethod(string _message);


     
        #endregion

        public MainWindow()
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo("log4net.config"));

            mcLogger.Info("Application started.");

            InitializeComponent();

            mainFrame.Source=new Uri("Views/Dialog/Dialog.xaml", UriKind.Relative);

        }


        #region - Properties -

        public PrintMethod CurrentPrintMethod
        {
            get
            {
                PrintMethod _printMethod = getPrintMessageMethod();

                return _printMethod;
            }
        }

        #endregion


        #region -private methods-

        private PrintMethod getPrintMessageMethod()
        {
            if (mainFrame.Content == null)
            {
                return null;
            }

            ViewModelBase _currentViewModel = (mainFrame.Content as PageBase).DataContext as ViewModelBase;

            if (_currentViewModel is DialogViewModel)
            {
                return (_currentViewModel as DialogViewModel).AddDialogItem;
            }
            else
            {
                return null;
            }

        }

        #endregion




    }
}
