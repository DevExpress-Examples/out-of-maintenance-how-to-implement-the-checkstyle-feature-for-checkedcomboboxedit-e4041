Imports Microsoft.VisualBasic
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports System
Imports System.Collections.Generic
Imports System.Windows.Forms

Namespace CustomEdit
	Partial Public Class Form1
		Inherits Form
		Private riCustomEdit As RepositoryItemCustomCheckedComboBoxEdit
		Public Sub New()
			InitializeComponent()

			Dim source As New List(Of GridRowData)()
			source.Add(New GridRowData())
			source.Add(New GridRowData())

			CreateRepository()

			'imageComboBoxEdit1
			FillImageComboBox()
			AddHandler imageComboBoxEdit1.EditValueChanged, AddressOf imageComboBoxEdit1_EditValueChanged

			'gridControl1
			gridControl1.DataSource = source
			gridControl1.RepositoryItems.Add(riCustomEdit)

			'gridView1
			gridView1.Columns("List").ColumnEdit = riCustomEdit
		End Sub
		Protected Overrides Sub Finalize()
			RemoveHandler imageComboBoxEdit1.EditValueChanged, AddressOf imageComboBoxEdit1_EditValueChanged
		End Sub

		Private Sub imageComboBoxEdit1_EditValueChanged(ByVal sender As Object, ByVal e As EventArgs)
			Dim edit As ImageComboBoxEdit = TryCast(sender, ImageComboBoxEdit)
			riCustomEdit.CheckStyle = CType(edit.EditValue, CheckStyles)
		End Sub

		Private Sub FillImageComboBox()
			imageComboBoxEdit1.Properties.Items.AddEnum(GetType(CheckStyles))
		End Sub

		Private Sub CreateRepository()
			riCustomEdit = New RepositoryItemCustomCheckedComboBoxEdit()
			riCustomEdit.Items.Add("Item 1")
			riCustomEdit.Items.Add("Item 2")
			riCustomEdit.Items.Add("Item 3")
		End Sub
	End Class
End Namespace
