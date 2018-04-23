Imports Microsoft.VisualBasic
Imports DevExpress.Utils
Imports DevExpress.Utils.Drawing
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors.ViewInfo
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Namespace CustomEdit
	Public Class CustomCheckedComboBoxEdit
		Inherits CheckedComboBoxEdit
		Private listBoxControl As CheckedListBoxControl
		Private images As ImageCollection

		Shared Sub New()
			RepositoryItemCustomCheckedComboBoxEdit.Register()
		End Sub

		Public Sub New()
		End Sub

		Protected Overrides Sub Finalize()
			If listBoxControl IsNot Nothing Then
				RemoveHandler listBoxControl.DrawItem, AddressOf CustomCheckedComboBoxEdit_DrawItem
			End If
			images.Dispose()
		End Sub

		Public Overrides ReadOnly Property EditorTypeName() As String
			Get
				Return RepositoryItemCustomCheckedComboBoxEdit.EditorName
			End Get
		End Property

		<DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
		Public Shadows ReadOnly Property Properties() As RepositoryItemCustomCheckedComboBoxEdit
			Get
				Return TryCast(MyBase.Properties, RepositoryItemCustomCheckedComboBoxEdit)
			End Get
		End Property

		Protected Overrides Function CreatePopupCheckListControl() As PopupContainerControl
			Dim containerControl As PopupContainerControl = MyBase.CreatePopupCheckListControl()
			SetControl(containerControl)
			Return containerControl
		End Function

		Private Sub SetControl(ByVal containerControl As PopupContainerControl)
			For Each con As Control In containerControl.Controls
				If con.GetType().BaseType Is GetType(CheckedListBoxControl) Then
					listBoxControl = TryCast(con, CheckedListBoxControl)
					AddHandler listBoxControl.DrawItem, AddressOf CustomCheckedComboBoxEdit_DrawItem
					Return
				End If
			Next con
		End Sub

		Private Sub CustomCheckedComboBoxEdit_DrawItem(ByVal sender As Object, ByVal e As ListBoxDrawItemEventArgs)
			If listBoxControl IsNot Nothing AndAlso Properties.CheckStyle <> DevExpress.XtraEditors.Controls.CheckStyles.Standard Then
				Dim lbControl As CheckedListBoxControl = TryCast(sender, CheckedListBoxControl)
				Dim vi As CheckedListBoxViewInfo = TryCast(lbControl.GetViewInfo(), CheckedListBoxViewInfo)
				Dim checkItemInfo As CheckedListBoxViewInfo.CheckedItemInfo = TryCast(vi.GetItemByIndex(e.Index), CheckedListBoxViewInfo.CheckedItemInfo)
				checkItemInfo.CheckArgs.CheckStyle = Properties.CheckStyle
				If Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio Then
					Return
				End If
				SetProperties(checkItemInfo)
			End If
		End Sub

		Protected Sub SetProperties(ByVal checkItemInfo As CheckedListBoxViewInfo.CheckedItemInfo)
			Dim e As CheckObjectInfoArgs = checkItemInfo.CheckArgs
			If e.DefaultImages Is Nothing Then
				e.DefaultImages = LoadImages()
			End If

			Dim style As Integer = CInt(Fix(e.CheckStyle))
			style = style * 4
			If style > e.DefaultImages.Count - 1 Then
				Return
			End If
			e.PictureUnchecked = TryCast(e.DefaultImages(style), Bitmap)
			e.PictureChecked = TryCast(e.DefaultImages(style + 1), Bitmap)
			e.PictureGrayed = TryCast(e.DefaultImages(style + 3), Bitmap)
		End Sub

		Protected Function LoadImages() As ArrayList
			If images Is Nothing Then
				images = DevExpress.Utils.Controls.ImageHelper.CreateImageCollectionFromResources("DevExpress.XtraEditors.Images.CheckBoxes.gif", GetType(RepositoryItemCheckEdit).Assembly, New Size(18, 18), Color.Magenta)
			End If
			Return New ArrayList(images.Images)
		End Function
	End Class
End Namespace
