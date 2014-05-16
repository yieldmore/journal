Imports System.IO
Imports System.Collections.Generic

Public Class _Default1
    Inherits System.Web.UI.Page

	Private _issue As Issue

	Public Property Issue() As Issue
		Get
			Return _issue
		End Get
		Set(ByVal value As Issue)
			_issue = value
		End Set
	End Property

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		'TODO: Replace PathInfo with url rewrite
		If Request.PathInfo <> "" Then
			Issue = Issue.Create(Request.PathInfo)
			If (Issue.HasTitle) Then
				TitleCtl.Visible = True
			Else
				TitlesCtl.DataSource = Issue.GetTitles
				TitlesCtl.DataBind()
			End If
			Exit Sub
		End If

		Dim issues = File.ReadAllLines(Path.Combine(Issue.Root, "issues.tsv")).Select(Function(line As String) New Issue(line)).ToList
		issues.RemoveAt(0)
		IssuesCtl.DataSource = issues
		IssuesCtl.DataBind()
	End Sub

End Class