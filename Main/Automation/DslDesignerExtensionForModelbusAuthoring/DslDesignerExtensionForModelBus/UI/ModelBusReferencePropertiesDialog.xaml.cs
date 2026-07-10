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
using System.Windows;

namespace Microsoft.VisualStudio.Modeling.Sdk.ModelBusAuthoringDslDesignerExtension
{
    /// <summary>
    /// Dialog enabling the user to specify custom properties on the ModelBusReference typed DomainProperty
    /// </summary>
    public partial class ModelBusReferencePropertiesDialog : Window
    {
        /// <summary>
        /// Constructor for the ModelBusReferencePropertiesUI modal dialog
        /// </summary>
        public ModelBusReferencePropertiesDialog()
        {
            InitializeComponent();
        }

        #region Properties
        /// <summary>
        /// Caption that the ModelBus Picker UI will display for this ModelBusReference
        /// </summary>
        public string PickerUiCaption
        {
            get
            {
                return pickerUiCaption.Text;
            }
            set
            {
                pickerUiCaption.Text = value;
            }
        }

        /// <summary>
        /// filter string that the ModelBus Picker UI will display for this ModelBusReference
        /// </summary>
        public string PickerUiFilterString
        {
            get
            {
                return pickerUiFilterString.Text;
            }
            set
            {
                pickerUiFilterString.Text = value;
            }
        }


        /// <summary>
        /// Is the ModelBus reference a model reference?
        /// </summary>
        /// <seealso cref="P:IsModelElementReference"/>
        public bool IsModelReference
        {
            get
            {
                return isReferencetoModel.IsChecked.HasValue ? isReferencetoModel.IsChecked.Value : false;
            }
            set
            {
                isReferencetoModel.IsChecked = value;
            }
        }

        /// <summary>
        /// Is the ModelBus reference a model reference?
        /// </summary>
        /// <seealso cref="P:IsModelReference"/>
        public bool IsModelElementReference
        {
            get
            {
                return isReferenceToModelElement.IsChecked.HasValue ? isReferenceToModelElement.IsChecked.Value : false;
            }
            set
            {
                isReferenceToModelElement.IsChecked = value;
            }
        }

        /// <summary>
        /// Authorised types for the picker UI
        /// </summary>
        public string[] PickerUiAuthorizedTypes
        {
            get
            {
                return pickerUiAuthorizedTypes.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            }
            set
            {
                if (value != null)
                {
                    pickerUiAuthorizedTypes.Text = string.Join(Environment.NewLine, value);
                }
                else
                {
                    pickerUiAuthorizedTypes.Text = string.Empty;
                }
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
