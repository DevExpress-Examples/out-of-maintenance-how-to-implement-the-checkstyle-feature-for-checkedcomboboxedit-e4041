Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic

Namespace CustomEdit
	Public Class GridRowData
		Private privateName As String
		Public Property Name() As String
			Get
				Return privateName
			End Get
			Set(ByVal value As String)
				privateName = value
			End Set
		End Property
		Private privateList As String
		Public Property List() As String
			Get
				Return privateList
			End Get
			Set(ByVal value As String)
				privateList = value
			End Set
		End Property
	End Class
End Namespace
