Imports System.IO

Public Class Issue

	Public Const Root = "D:\Imran\htdocs\jssw\app\issues"

	Public Sub New(line As String)
		Dim bits = line.Split({"	"}, StringSplitOptions.None)
		IssueNumber = bits(0) & "/" & bits(1)
	End Sub

	Private Sub New()
	End Sub

	Public Shared Function Create(number As String)
		Dim issue As New Issue
		issue.IssueNumber = number.Substring(1)
		If issue.IssueNumber.Count(Function(c As Char) c = "/") > 1 Then
			Dim title = issue.IssueNumber.Substring(issue.IssueNumber.LastIndexOf("/") + 1)
			issue.Title = title
			issue.IssueNumber = issue.IssueNumber.Substring(0, issue.IssueNumber.Length - title.Length - 1)
			Dim fol As New DirectoryInfo(Path.Combine(Root, issue.IssueNumber.Replace("/", "\")))
			If Not fol.Exists Then Return String.Empty
			issue._titleFile = fol.GetFiles().First(Function(fi As FileInfo) GetTitle(Path.GetFileNameWithoutExtension(fi.Name)) = title)
		End If
		Return issue
	End Function

	Private _issue As String
	Public Property IssueNumber() As String
		Get
			Return _issue
		End Get
		Private Set(ByVal value As String)
			_issue = value
		End Set
	End Property

	Private _title As String
	Public Property Title() As String
		Get
			Return _title
		End Get
		Private Set(ByVal value As String)
			_title = value
		End Set
	End Property


	Private _titleFile As FileInfo

	Public ReadOnly Property HasTitle() As Boolean
		Get
			Return _titleFile IsNot Nothing
		End Get
	End Property

	Public ReadOnly Property Author() As String
		Get
			Dim bits = Path.GetFileNameWithoutExtension(_titleFile.Name).Split("-")
			Return bits(2)
		End Get
	End Property

	Public ReadOnly Property Content() As String
		Get
			Return "<p>" & File.ReadAllText(_titleFile.FullName).Replace(Environment.NewLine & Environment.NewLine, _
				"</p>" & Environment.NewLine & Environment.NewLine & "<p>") & "</p>"
		End Get
	End Property


	Function GetTitles() As String()
		Dim fol As New DirectoryInfo(Path.Combine(Root, IssueNumber.Replace("/", "\")))
		If Not fol.Exists Then Return {}
		Return fol.GetFiles().Select(Function(fi As FileInfo) GetTitle(Path.GetFileNameWithoutExtension(fi.Name))).ToArray()
	End Function

	Private Shared Function GetTitle(name As String) As String
		Dim bits = name.Split("-").ToList()
		bits.RemoveAt(2)
		Return String.Join("-", bits)
	End Function


End Class
