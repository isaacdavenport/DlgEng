using System;
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
using DialogEngine.UI.Helpers;
using DialogEngine.ViewModels.Dialog;


namespace DialogEngine
{

    public partial class MainWindow : Window
    {
        #region -fields-

        //creating of delegate ( it is similar to pointer on function in C )
        public delegate void PrintMethod(string message);

        private PrintMethod  _currentPrintMethod;

        #endregion

        public MainWindow()
        {
            InitializeComponent();
        }


        #region - Properties -

        public PrintMethod CurrentPrintMethod
        {
            get
            {
                PrintMethod printMethod = GetPrintMessageMethod();

                return printMethod;
            }
        }

        #endregion


        #region -private methods-

        private PrintMethod GetPrintMessageMethod()
        {

            ViewModelBase currentViewModel = (mainFrame.Content as PageBase).DataContext as ViewModelBase;

            if (currentViewModel is DialogViewModel)
            {
                return (currentViewModel as DialogViewModel).AddDialogItem;
            }
            else
            {

                //todo 
                return null;
            }
        }

        #endregion




    }
}
