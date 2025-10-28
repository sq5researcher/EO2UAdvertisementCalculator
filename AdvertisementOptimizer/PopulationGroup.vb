' ''  EO2U Advertisement Calculator
' ''    Copyright (C) 2015 aktai0 @ GitHub, Tokenx @ Gamefaqs

' ''    This program is free software; you can redistribute it and/or modify
' ''    it under the terms of the GNU General Public License as published by
' ''    the Free Software Foundation; either version 2 of the License, or
' ''    (at your option) any later version.

' ''    This program is distributed in the hope that it will be useful,
' ''    but WITHOUT ANY WARRANTY; without even the implied warranty of
' ''    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
' ''    GNU General Public License for more details.

' ''    You should have received a copy of the GNU General Public License along
' ''    with this program; if not, write to the Free Software Foundation, Inc.,
' ''    51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.

Public Class PopulationGroup
#Region "Constants"
   Shared POPULATIONS() As String = {"1", "2", "3", "4"}
    Shared SouthCustomers() As String = {"南区の家具工房職人", "ハイラガ木こりの会", "森林マタギ団", "冒険者支援センター", "ユグドラシル建設作業員", "公国ガンナー管理組合 (グリモア)", "ソードマン闘技研鑽会 (グリモア)", "一時雇いの大工集団 (イベント)"}
    Shared EastCustomers() As String = {"ともしび老人会", "仕立て屋ギルドの職人", "東区担当の衛兵隊", "ハイラガ開墾団", "道で遊ぶ子供たち", "レンジャー協会 (グリモア)", "施療院のお抱えメディック (グリモア)", "野菜直売会に訪れた客 (イベント)"}
    Shared SlumsCustomers() As String = {"路上にたむろする若者たち", "昼からのんだくれ隊", "アヤしい露店の商人団", "青空教室の生徒たち", "見回り中の衛士隊", "呪言の普及振興を願う会 (グリモア)", "ダークハンター束縛連合 (グリモア)", "入国審査待ちの移民団 (イベント)"}
    Shared WestCustomers() As String = {"国立ハイ・ラガード学院生", "ハイ・ラガード学院教授連", "オーダーメイド靴工房員", "西区美術家ギルド員", "衛士隊訓練生", "錬金術師互助組合 (グリモア)", "現代巫術研究会 (グリモア)", "学院開催のバザー客 (イベント)"}
    Shared NorthCustomers() As String = {"ラガード陶芸俱楽部", "鉄工鍛冶ギルドの職人", "大道芸人の一座", "ハイラガ酒蔵の蔵人", "ひまわり商店街の店員", "アナタのためのバード隊 (グリモア)", "樹海流武士道場の門徒 (グリモア)", "訪問中のキャラバン隊員 (イベント)"}
    Shared UptownCustomers() As String = {"貴族子弟の社交クラブ", "街路の靴磨き職人", "公国メイド組合員", "中央区婦人会", "城付き近衛兵団員", "プリンセス茶会のメンバー (グリモア)", "黄金樹聖騎士団員 (グリモア)", "逗留中の外交使節団 (イベント)"}
#End Region

    Private _GivenWard As WardGroup.Ward
   Public Property GivenWard As WardGroup.Ward
      Set(value As WardGroup.Ward)
         _GivenWard = value
         SetStrListToComboBox(POPULATIONS, PopulationComboBox)
         Select Case _GivenWard
            Case WardGroup.Ward.南区
               SetStrListToComboBox(SouthCustomers, CustomerComboBox)
            Case WardGroup.Ward.東区
               SetStrListToComboBox(EastCustomers, CustomerComboBox)
            Case WardGroup.Ward.西区
               SetStrListToComboBox(WestCustomers, CustomerComboBox)
            Case WardGroup.Ward.北区
               SetStrListToComboBox(NorthCustomers, CustomerComboBox)
            Case WardGroup.Ward.中央区
               SetStrListToComboBox(UptownCustomers, CustomerComboBox)
            Case WardGroup.Ward.街外れ
               SetStrListToComboBox(SlumsCustomers, CustomerComboBox)
            Case WardGroup.Ward.None

            Case Else
               Throw New Exception("Unknown ward!?!?!?!?")
         End Select
      End Set
      Get
         Return _GivenWard
      End Get
   End Property

   Public ReadOnly Property SelectedCustomer As String
      Get
         Return CustomerComboBox.Text
      End Get
   End Property

   Public ReadOnly Property SelectedPopulation As Integer
      Get
         Select Case PopulationComboBox.Text
            Case "1"
               Return 1
            Case "2"
               Return 2
            Case "3"
               Return 3
            Case "4"
               Return 4
                Case "グリモア" ' Diamond seems to give about the same amount as 4 people
                    Return 4
                Case "イベント"
                    Return 5
            Case Else
               Throw New Exception("Unacceptable population amount: " & PopulationComboBox.Text)
         End Select
      End Get
   End Property

   Shared Sub SetStrListToComboBox(ByVal strs As String(), ByRef box As ComboBox, Optional exclusions As List(Of String) = Nothing)

      box.Items.Clear()
      For Each s In strs
         ' Add the item if it's not excluded
         If exclusions Is Nothing Then
            box.Items.Add(s)
         ElseIf Not exclusions.Contains(s) Then
            box.Items.Add(s)
         End If
      Next
   End Sub

   Private Sub Button2_Click(sender As Object, e As EventArgs) Handles ResetButton.Click
      CustomerComboBox.SelectedItem = Nothing
      PopulationComboBox.SelectedItem = Nothing
      SetStrListToComboBox(POPULATIONS, PopulationComboBox)
   End Sub

   Private oldText As String = ""
   Private Sub CustomerComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CustomerComboBox.SelectedIndexChanged
        If CustomerComboBox.Text.Contains("イベント") Then
            PopulationComboBox.Items.Clear()
            PopulationComboBox.Items.Add("イベント")
            PopulationComboBox.SelectedItem = "イベント"
        ElseIf CustomerComboBox.Text.Contains("グリモア") Then
            PopulationComboBox.Items.Clear()
            PopulationComboBox.Items.Add("グリモア")
            PopulationComboBox.SelectedItem = "グリモア"
        Else
            If oldText.Contains("イベント") OrElse oldText.Contains("グリモア") Then
                SetStrListToComboBox(POPULATIONS, PopulationComboBox)
            End If
            PopulationComboBox.SelectedItem = "1"
      End If
      oldText = CustomerComboBox.Text
   End Sub
End Class