using Microsoft.Win32;
using Spire.Pdf;
using Spire.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;

namespace VDemyanov.MaintenanceServices.MaintenanceServicesWPF.Views.UserControls.BusinessSectionUC
{
    /// <summary>
    /// Логика взаимодействия для ReportPresentationZone.xaml
    /// </summary>
    public partial class ReportPresentationSection : UserControl
    {
        public ReportPresentationSection()
        {
            InitializeComponent();
        }


        /*private void Save_Click(object sender, RoutedEventArgs e)
        {
            this.UpdateLayout();
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "XPS Files (*.xps)|*.xps";
            if (sfd.ShowDialog() == true)
            {
                XpsDocument doc = new XpsDocument(sfd.FileName, FileAccess.Write);
                XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(doc);

                writer.Write(documentViewer.Document as FixedDocument);

                doc.Close();

            }
        }*/
    }
}
