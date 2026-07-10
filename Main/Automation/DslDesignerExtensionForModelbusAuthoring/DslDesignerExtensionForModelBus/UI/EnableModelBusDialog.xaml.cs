//***************************************************************************
//
//    Copyright (c) Microsoft Corporation. All rights reserved.
//    This code is licensed under the MICROSOFT VISUAL STUDIO 2010
//    VISUALIZATION AND MODELING SOFTWARE DEVELOPMENT KIT license terms.
//    THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
//    ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
//    IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
//    PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//***************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Microsoft.VisualStudio.Modeling.Sdk.ModelBusAuthoringDslDesignerExtension.UI
{
    /// <summary>
    /// Dialog presented to the user to enable the modelbus
    /// </summary>
    public partial class EnableModelBusDialog
    {
        /// <summary>
        /// Constructor of the dialog
        /// </summary>
        public EnableModelBusDialog()
        {
            InitializeComponent();
        }

        #region Properties
        /// <summary>
        /// Should we expose this DSL to the modelbus?
        /// </summary>
        public bool ExposeToModelBus
        {
            get
            {
                return exposeToModelBus.IsChecked.Value;
            }
            set
            {
                exposeToModelBus.IsChecked = value;
            }
        }

        /// <summary>
        /// Should we enable this DSL to modelbus consumption?
        /// </summary>
        public bool EnableForModelBusConsumption
        {
            get
            {
                return consumeModelBus.IsChecked.Value;
            }
            set
            {
                consumeModelBus.IsChecked = value;
            }
        }

        /// <summary>
        /// Is the DSL already exposed to ModelBus?
        /// </summary>
        public bool IsAlreadyExposedToModelBus
        {
            get
            {
                return !exposeToModelBus.IsEnabled;
            }
            set
            {
                exposeToModelBus.IsEnabled = !value;
            }
        }

        /// <summary>
        /// Is the DSL already enabled to ModelBus consumption?
        /// </summary>
        public bool IsAlreadyEnabledForModelBusConsumption
        {
            get
            {
                return !consumeModelBus.IsEnabled;
            }
            set
            {
                consumeModelBus.IsEnabled = !value;
            }
        }

        #endregion
        /// <summary>
        /// Click on the ok button hides the dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Hide();
        }
    }
}
