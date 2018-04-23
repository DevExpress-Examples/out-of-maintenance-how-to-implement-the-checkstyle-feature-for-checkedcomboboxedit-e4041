using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CustomEdit
{
    public partial class Form1 : Form
    {
        RepositoryItemCustomCheckedComboBoxEdit riCustomEdit;
        public Form1()
        {
            InitializeComponent();

            List<GridRowData> source = new List<GridRowData>();
            source.Add(new GridRowData());
            source.Add(new GridRowData());

            CreateRepository();
            
            //imageComboBoxEdit1
            FillImageComboBox();
            imageComboBoxEdit1.EditValueChanged += imageComboBoxEdit1_EditValueChanged;

            //gridControl1
            gridControl1.DataSource = source;
            gridControl1.RepositoryItems.Add(riCustomEdit);

            //gridView1
            gridView1.Columns["List"].ColumnEdit = riCustomEdit;
        }
        ~Form1()
        {
            imageComboBoxEdit1.EditValueChanged -= imageComboBoxEdit1_EditValueChanged;
        }

        void imageComboBoxEdit1_EditValueChanged(object sender, EventArgs e)
        {
            ImageComboBoxEdit edit = sender as ImageComboBoxEdit;
            riCustomEdit.CheckStyle = (CheckStyles)edit.EditValue;
        }

        private void FillImageComboBox()
        {
            imageComboBoxEdit1.Properties.Items.AddEnum(typeof(CheckStyles));
        }

        private void CreateRepository()
        {
            riCustomEdit = new RepositoryItemCustomCheckedComboBoxEdit();
            riCustomEdit.Items.Add("Item 1");
            riCustomEdit.Items.Add("Item 2");
            riCustomEdit.Items.Add("Item 3");
        }
    }
}
