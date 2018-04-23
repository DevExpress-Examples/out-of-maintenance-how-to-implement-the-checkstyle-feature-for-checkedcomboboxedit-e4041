Imports Microsoft.VisualBasic
Imports DevExpress.Accessibility
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraEditors.Drawing
Imports DevExpress.XtraEditors.Registrator
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors.ViewInfo
Imports System
Imports System.Collections.Generic

Namespace CustomEdit
	<UserRepositoryItem("Register")> _
	Public Class RepositoryItemCustomCheckedComboBoxEdit
		Inherits RepositoryItemCheckedComboBoxEdit
		Private _checkStyle As CheckStyles = CheckStyles.Standard
		Public Property CheckStyle() As CheckStyles
			Get
				Return _checkStyle
			End Get
			Set(ByVal value As CheckStyles)
                If _checkStyle <> value Then
                    _checkStyle = value
                    OnPropertiesChanged()
                End If
			End Set
		End Property
		Friend Const EditorName As String = "CustomCheckedComboBoxEdit"

		Shared Sub New()
			Register()
		End Sub

		Public Sub New()
		End Sub

		Public Shared Sub Register()
            EditorRegistrationInfo.Default.Editors.Add(New EditorClassInfo(EditorName, GetType(CustomCheckedComboBoxEdit), GetType(RepositoryItemCustomCheckedComboBoxEdit), GetType(PopupContainerEditViewInfo), New ButtonEditPainter(), True, EditImageIndexes.ComboBoxEdit, GetType(PopupEditAccessible)))
		End Sub

		Public Overrides ReadOnly Property EditorTypeName() As String
			Get
				Return EditorName
			End Get
		End Property

		Public Overrides Sub Assign(ByVal item As RepositoryItem)
			BeginUpdate()
			Try
				MyBase.Assign(item)
				Dim source As RepositoryItemCustomCheckedComboBoxEdit = TryCast(item, RepositoryItemCustomCheckedComboBoxEdit)
				If source Is Nothing Then
					Return
				End If
				CheckStyle = source.CheckStyle
			Finally
				EndUpdate()
			End Try
		End Sub
	End Class
End Namespace
