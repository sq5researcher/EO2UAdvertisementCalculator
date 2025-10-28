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

Public Class WardGroup
    Public Enum Ward
        None
        南区
        東区
        西区
        北区
        街外れ
        中央区
    End Enum

    Private _GivenWard As Ward
    Public Property GivenWard As Ward
        Get
            Return _GivenWard
        End Get
        Set(value As Ward)
            GourmetCheckBox.Checked = False
            _GivenWard = value
            ClearPopulationsPanel()
            If FOODS Is Nothing Then
                Return
            End If
            Select Case value
                Case Ward.南区
                    LoadSouthWardCustomers()
                Case Ward.北区
                    LoadNorthWardCustomers()
                Case Ward.東区
                    LoadEastWardCustomers()
                Case Ward.西区
                    LoadWestWardCustomers()
                Case Ward.街外れ
                    LoadSlumCustomers()
                Case Ward.中央区
                    LoadUptownCustomers()
                Case Ward.None

                Case Else
                    Throw New Exception("Unknown Ward??!?!")
            End Select
            For i = 0 To 4
                AddPopulation()
            Next
        End Set
    End Property

    Private Sub AddPopulation()
        Dim newPopGroup As New PopulationGroup()
        newPopGroup.GivenWard = Me.GivenWard
        InnerPanel.Controls.Add(newPopGroup)
    End Sub

    Private Function GetPopulationGroups() As PopulationGroup()
        Dim toRet As New List(Of PopulationGroup)
        For Each c In InnerPanel.Controls
            If GetType(PopulationGroup) = c.GetType Then
                toRet.Add(CType(c, PopulationGroup))
            End If
        Next
        Return toRet.ToArray
    End Function

    Private Sub WardGroup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadFoods()
        StarLimitComboBox.SelectedIndex = 5
        DistrictComboBox.SelectedIndex = 0
        FoodResultLabel.Text = ""
    End Sub


    Private Sub CalculateButton_Click(sender As Object, e As EventArgs) Handles CalculateButton.Click
        Dim groups() As PopulationGroup = GetPopulationGroups()
        Dim customers As New List(Of String)
        Dim populations As New List(Of Integer)

        For Each p In groups
            Dim cust = p.SelectedCustomer
            If cust.Equals("") Then
                Continue For
            Else
                Dim pop = p.SelectedPopulation
                customers.Add(cust)
                populations.Add(pop)
            End If
        Next

        LatestFoodValueResults = GetOptimalFood(customers, populations, Integer.Parse(StarLimitComboBox.Text(8)), GourmetCheckBox.Checked)
        DisplayFoodLabel()
    End Sub

    Private Sub DisplayFoodLabel()
        If LatestFoodValueResults IsNot Nothing Then
            FoodResultLabel.Text = LatestFoodValueResults(0).ToString
            FoodValueIndex = 0
        End If
    End Sub

    Private Sub GoUpOneFood()
        If (FoodValueIndex > 0 AndAlso LatestFoodValueResults IsNot Nothing) Then
            FoodValueIndex -= 1
            FoodResultLabel.Text = LatestFoodValueResults(FoodValueIndex).ToString
        End If
    End Sub

    Private Sub GoDownOneFood()
        If (LatestFoodValueResults IsNot Nothing AndAlso FoodValueIndex < LatestFoodValueResults.Count - 1) Then
            FoodValueIndex += 1
            FoodResultLabel.Text = LatestFoodValueResults(FoodValueIndex).ToString
        End If
    End Sub

    Private FoodValueIndex As Integer = 0
    Private LatestFoodValueResults As List(Of FoodValue) = Nothing

    Private Sub ClearPopulationsPanel()
        InnerPanel.Controls.Clear()
        'AddPopulation()
    End Sub

    Private Sub ResetButton_Click(sender As Object, e As EventArgs) Handles ResetButton.Click
        GivenWard = GivenWard
        FoodResultLabel.Text = ""
    End Sub

    Private Shared Function ScalePopulation(p1 As Integer) As Integer
        Select Case p1
            Case 1
                Return 2
            Case 2
                Return 6
            Case 3
                Return 10
            Case 4
                Return 15
            Case 5
                Return 39
        End Select
        Return 0
    End Function

#Region "FOOD"
    Private Shared FOODS() As Food

    Private Class Food
        Public Name As String
        Public Stars As Integer
        Public Price As Integer
        Public Customers() As String

        Public Sub New(ByVal n As String, ByVal s As Integer, ByVal p As Integer)
            Me.Name = n
            Me.Stars = s
            Me.Price = p
            Customers = {}
        End Sub
    End Class

    Private Shared Sub LoadFoods()
        FOODS = {
            New Food("球獣肉とアスパラの中華炒め", 1, 73), New Food("軟骨揚げのシトロン餡かけ丼", 1, 78), New Food("シカ肉のタタキ風", 1, 97), New Food("黒茶", 1, 81),
            New Food("アゲハの姿佃煮", 1, 77), New Food("梟の軟骨からあげ", 1, 81), New Food("くるみ羊羹", 1, 83), New Food("鹿肉と樹海野菜のすき鍋", 1, 105),
            New Food("エスカルゴ焼シトロンソース添え", 1, 77), New Food("ハイラガコーヒー", 1, 74), New Food("クルミ入りライ麦パン", 1, 90), New Food("シカ肉のステーキ", 1, 99),
            New Food("麻辣獄火鍋", 2, 135), New Food("鶏唐揚げの甘酢餡かけ", 2, 106), New Food("栗月餅", 2, 130), New Food("火龍果杏仁", 2, 124),
            New Food("怪しい石焼き鍋", 2, 113), New Food("巨猪の豚汁", 2, 114), New Food("鬼いが栗の茶巾絞り", 2, 119), New Food("紅葉狩り串団子", 2, 137),
            New Food("かみつきのサンドイッチ", 2, 95), New Food("旬の秋野菜ポトフ", 2, 105), New Food("パーシモンプディング", 2, 124), New Food("ジビエカレーライス", 2, 137),
            New Food("リンゴ入り愛玉子風ブルーゼリー", 3, 219), New Food("野牛肉拉麺", 3, 179), New Food("馬肉中華包子", 3, 164), New Food("雪鳥の蟹玉", 3, 169),
            New Food("東国の伝統兜焼き", 3, 152), New Food("雪鳥卵の熱々おでん", 3, 183), New Food("カニの樹海茶漬け", 3, 210), New Food("林檎と抹茶のかき氷", 3, 190),
            New Food("発酵怪魚パニーノ", 3, 222), New Food("野牛ステーキのリンゴソース添え", 3, 160), New Food("カニクリームコロッケ", 3, 203), New Food("樹海パエリア", 3, 186),
            New Food("蜘蛛の姿揚げ", 4, 202), New Food("樹海鳥の雷炒飯", 4, 207), New Food("森林サイ角煮", 4, 243), New Food("石化鳥と樹海野菜の細切り炒め", 4, 226),
            New Food("亀の甲羅焼き肉", 4, 243), New Food("大苺大福", 4, 216), New Food("樹海桜茶", 4, 213), New Food("石化鳥の肉じゃが", 4, 235),
            New Food("サソリのグリーンパスタ", 4, 243), New Food("ハチミツジャーマンポテト", 4, 205), New Food("ももステーキの桜苺ジャム添え", 4, 247), New Food("サイ肉のとろとろシチュー", 4, 292),
            New Food("ノヅチ丸ごとスープ", 5, 329), New Food("宝龍包", 5, 338), New Food("皇帝ツバメの巣と岩茸の幻スープ", 5, 264), New Food("きょうか鶏", 5, 273),
            New Food("特製大釜の鎧竜炊き込みご飯", 5, 329), New Food("桜肉のしゃぶしゃぶ	", 5, 320), New Food("雲海山鳥の茶碗蒸し", 5, 301), New Food("宝石煮こごりのツバメの巣入り", 5, 268),
            New Food("禍々しいアスピック", 5, 386), New Food("３種食べ比べハンバーグ", 5, 333), New Food("タルタルステーキ", 5, 329), New Food("ストーンガレット", 5, 336),
            New Food("危険な花茶", 6, 450), New Food("シュウマイ", 6, 421), New Food("樹海特選の回鍋肉", 6, 434), New Food("蜥蜴肉の蜜汁火方", 6, 444),
            New Food("古代ヤドカリの姿造り", 6, 368), New Food("黒獣のお吸い物", 6, 444), New Food("力士秘伝のちゃんこ風鍋", 6, 440), New Food("東国伝来の煮しめ", 6, 450),
            New Food("大芋虫のキャセロール", 6, 430), New Food("オレンジソースの怪獣ステーキ", 6, 441), New Food("タケノコが入ったサルマーレ", 6, 450), New Food("パンプキンパイ", 6, 421)
        }
    End Sub

    Private Shared Function GetFood(ByVal givenName As String) As Food
        Return Array.Find(FOODS, Function(s) s.Name = givenName)
    End Function

    Private Class FoodValue
        Implements IComparable(Of FoodValue)
        Public Food As Food
        Public Value As Integer
        Public Patrons As String

        Public Sub New(ByVal a As Food, ByVal b As Integer, ByVal p As String)
            Food = a
            Value = b
            Patrons = p
        End Sub

        Function CompareTo(ByVal other As FoodValue) As Integer Implements IComparable(Of AdvertisementOptimizer.WardGroup.FoodValue).CompareTo
            Return other.Value - Me.Value
        End Function

        Public Overrides Function ToString() As String
            If Food Is Nothing Then
                Return "Error!"
            Else
                Dim stars As String = ""
                Dim starChar As Char = "★"c
                For i = 1 To Food.Stars
                    stars = stars & starChar
                Next
                Return Food.Name & " (価格: " & Food.Price & ", " & stars & ")" & vbNewLine &
                   "顧客: " & Patrons
            End If
        End Function
    End Class
    Private Shared Function GetOptimalFood(ByVal customers As IEnumerable(Of String), ByVal populations As IEnumerable(Of Integer), Optional starLimit As Integer = 5, Optional gourmetKing As Boolean = False) As List(Of FoodValue)

        Dim foodValuesList As New List(Of FoodValue)

        For Each food As Food In FOODS
            Dim patronString As String = ""
            If food.Stars > starLimit Then
                Continue For
            End If

            Dim currentFoodValue As Integer = 0

            If Not gourmetKing Then
                Dim patronList As New List(Of String)
                For i = 0 To customers.Count - 1
                    Dim currentCustomer As String = customers(i)
                    If food.Customers.Contains(currentCustomer) Then
                        currentFoodValue += food.Price * ScalePopulation(populations(i))
                        patronList.Add(currentCustomer)
                    End If
                Next

                If patronList.Count = 1 Then
                    patronString = patronList(0)
                ElseIf patronList.Count = customers.Count Then
                    patronString = "全員"
                ElseIf patronList.Count > 0 Then
                    For Each p In patronList
                        patronString += p.Split(" "c)(0) & ", "
                    Next
                    patronString = patronString.Substring(0, patronString.Length - 2)
                End If
            Else
                currentFoodValue += food.Price
                patronString = "全員"
            End If

            foodValuesList.Add(New FoodValue(food, currentFoodValue, patronString))
        Next

        foodValuesList.Sort()


        Return foodValuesList
    End Function
#End Region

#Region "LOADING"
    Private Shared Sub LoadEastWardCustomers()
        GetFood("球獣肉とアスパラの中華炒め").Customers = {"東区担当の衛兵隊", "ハイラガ開墾団", "ともしび老人会", "仕立て屋ギルドの職人"}
        GetFood("軟骨揚げのシトロン餡かけ丼").Customers = {"東区担当の衛兵隊"}
        GetFood("シカ肉のタタキ風").Customers = {"仕立て屋ギルドの職人", "レンジャー協会 (グリモア)"}
        GetFood("黒茶").Customers = {"ハイラガ開墾団", "ともしび老人会", "施療院のお抱えメディック (グリモア)"}
        GetFood("アゲハの姿佃煮").Customers = {"ハイラガ開墾団", "ともしび老人会"}
        GetFood("梟の軟骨からあげ").Customers = {"東区担当の衛兵隊"}
        GetFood("くるみ羊羹").Customers = {"ハイラガ開墾団", "道で遊ぶ子供たち", "ともしび老人会", "レンジャー協会 (グリモア)", "施療院のお抱えメディック (グリモア)"}
        GetFood("鹿肉と樹海野菜のすき鍋").Customers = {"ハイラガ開墾団", "ともしび老人会", "仕立て屋ギルドの職人"}
        GetFood("エスカルゴ焼シトロンソース添え").Customers = {}
        GetFood("ハイラガコーヒー").Customers = {"ハイラガ開墾団", "ともしび老人会", "仕立て屋ギルドの職人", "施療院のお抱えメディック (グリモア)"}
        GetFood("クルミ入りライ麦パン").Customers = {"仕立て屋ギルドの職人"}
        GetFood("シカ肉のステーキ").Customers = {"ハイラガ開墾団", "ともしび老人会", "仕立て屋ギルドの職人"}
        GetFood("麻辣獄火鍋").Customers = {"ハイラガ開墾団", "ともしび老人会"}
        GetFood("鶏唐揚げの甘酢餡かけ").Customers = {"東区担当の衛兵隊", "ハイラガ開墾団"}
        GetFood("栗月餅").Customers = {"ハイラガ開墾団", "道で遊ぶ子供たち", "ともしび老人会", "仕立て屋ギルドの職人", "レンジャー協会 (グリモア)"}
        GetFood("火龍果杏仁").Customers = {"道で遊ぶ子供たち", "レンジャー協会 (グリモア)"}
        GetFood("怪しい石焼き鍋").Customers = {}
        GetFood("巨猪の豚汁").Customers = {"ハイラガ開墾団"}
        GetFood("鬼いが栗の茶巾絞り").Customers = {"ハイラガ開墾団", "道で遊ぶ子供たち", "ともしび老人会", "レンジャー協会 (グリモア)", "施療院のお抱えメディック (グリモア)"}
        GetFood("紅葉狩り串団子").Customers = {"道で遊ぶ子供たち", "ともしび老人会", "施療院のお抱えメディック (グリモア)"}
        GetFood("かみつきのサンドイッチ").Customers = {"ハイラガ開墾団", "仕立て屋ギルドの職人", "レンジャー協会 (グリモア)"}
        GetFood("旬の秋野菜ポトフ").Customers = {"ハイラガ開墾団", "ともしび老人会"}
        GetFood("パーシモンプディング").Customers = {"道で遊ぶ子供たち", "施療院のお抱えメディック (グリモア)"}
        GetFood("ジビエカレーライス").Customers = {"ハイラガ開墾団", "ともしび老人会", "施療院のお抱えメディック (グリモア)"}
        GetFood("リンゴ入り愛玉子風ブルーゼリー").Customers = {"道で遊ぶ子供たち"}
        GetFood("野牛肉拉麺").Customers = {"仕立て屋ギルドの職人"}
        GetFood("馬肉中華包子").Customers = {"ハイラガ開墾団", "ともしび老人会", "仕立て屋ギルドの職人", "施療院のお抱えメディック (グリモア)"}
        GetFood("雪鳥の蟹玉").Customers = {"東区担当の衛兵隊", "仕立て屋ギルドの職人"}
        GetFood("東国の伝統兜焼き").Customers = {"ハイラガ開墾団", "ともしび老人会", "仕立て屋ギルドの職人"}
        GetFood("雪鳥卵の熱々おでん").Customers = {"ともしび老人会"}
        GetFood("カニの樹海茶漬け").Customers = {"ハイラガ開墾団"}
        GetFood("林檎と抹茶のかき氷").Customers = {"ハイラガ開墾団", "道で遊ぶ子供たち", "レンジャー協会 (グリモア)"}
        GetFood("発酵怪魚パニーノ").Customers = {"仕立て屋ギルドの職人"}
        GetFood("野牛ステーキのリンゴソース添え").Customers = {"仕立て屋ギルドの職人"}
        GetFood("カニクリームコロッケ").Customers = {"東区担当の衛兵隊", "ハイラガ開墾団", "仕立て屋ギルドの職人"}
        GetFood("樹海パエリア").Customers = {"ハイラガ開墾団", "ともしび老人会"}
        GetFood("蜘蛛の姿揚げ").Customers = {"東区担当の衛兵隊", "ハイラガ開墾団", "ともしび老人会"}
        GetFood("樹海鳥の雷炒飯").Customers = {"東区担当の衛兵隊"}
        GetFood("森林サイ角煮").Customers = {"ハイラガ開墾団", "野菜直売会に訪れた客 (イベント)"}
        GetFood("石化鳥と樹海野菜の細切り炒め").Customers = {"東区担当の衛兵隊", "ハイラガ開墾団", "ともしび老人会"}
        GetFood("亀の甲羅焼き肉").Customers = {"仕立て屋ギルドの職人"}
        GetFood("大苺大福").Customers = {"道で遊ぶ子供たち", "レンジャー協会 (グリモア)", "施療院のお抱えメディック (グリモア)", "野菜直売会に訪れた客 (イベント)"}
        GetFood("樹海桜茶").Customers = {"施療院のお抱えメディック (グリモア)", "野菜直売会に訪れた客 (イベント)"}
        GetFood("石化鳥の肉じゃが").Customers = {"ハイラガ開墾団", "ともしび老人会"}
        GetFood("サソリのグリーンパスタ").Customers = {"ハイラガ開墾団", "仕立て屋ギルドの職人"}
        GetFood("ハチミツジャーマンポテト").Customers = {"東区担当の衛兵隊", "ハイラガ開墾団", "ともしび老人会", "野菜直売会に訪れた客 (イベント)"}
        GetFood("ももステーキの桜苺ジャム添え").Customers = {"仕立て屋ギルドの職人"}
        GetFood("サイ肉のとろとろシチュー").Customers = {"ハイラガ開墾団", "ともしび老人会"}
        GetFood("ノヅチ丸ごとスープ").Customers = {"ハイラガ開墾団"}
        GetFood("宝龍包").Customers = {"ハイラガ開墾団", "仕立て屋ギルドの職人", "施療院のお抱えメディック (グリモア)"}
        GetFood("皇帝ツバメの巣と岩茸の幻スープ").Customers = {"ハイラガ開墾団"}
        GetFood("きょうか鶏").Customers = {"施療院のお抱えメディック (グリモア)"}
        GetFood("特製大釜の鎧竜炊き込みご飯").Customers = {}
        GetFood("桜肉のしゃぶしゃぶ	").Customers = {}
        GetFood("雲海山鳥の茶碗蒸し").Customers = {"ハイラガ開墾団", "施療院のお抱えメディック (グリモア)"}
        GetFood("宝石煮こごりのツバメの巣入り").Customers = {"レンジャー協会 (グリモア)"}
        GetFood("禍々しいアスピック").Customers = {}
        GetFood("３種食べ比べハンバーグ").Customers = {"仕立て屋ギルドの職人"}
        GetFood("タルタルステーキ").Customers = {"レンジャー協会 (グリモア)"}
        GetFood("ストーンガレット").Customers = {"ハイラガ開墾団", "仕立て屋ギルドの職人"}
        GetFood("危険な花茶").Customers = {"施療院のお抱えメディック (グリモア)"}
        GetFood("シュウマイ").Customers = {"ハイラガ開墾団", "仕立て屋ギルドの職人", "施療院のお抱えメディック (グリモア)"}
        GetFood("樹海特選の回鍋肉").Customers = {"東区担当の衛兵隊", "ハイラガ開墾団"}
        GetFood("蜥蜴肉の蜜汁火方").Customers = {"ハイラガ開墾団", "施療院のお抱えメディック (グリモア)", "野菜直売会に訪れた客 (イベント)"}
        GetFood("古代ヤドカリの姿造り").Customers = {"レンジャー協会 (グリモア)"}
        GetFood("黒獣のお吸い物").Customers = {"ハイラガ開墾団"}
        GetFood("力士秘伝のちゃんこ風鍋").Customers = {"ハイラガ開墾団"}
        GetFood("東国伝来の煮しめ").Customers = {"ハイラガ開墾団", "野菜直売会に訪れた客 (イベント)"}
        GetFood("大芋虫のキャセロール").Customers = {"ハイラガ開墾団"}
        GetFood("オレンジソースの怪獣ステーキ").Customers = {"仕立て屋ギルドの職人"}
        GetFood("タケノコが入ったサルマーレ").Customers = {"ハイラガ開墾団"}
        GetFood("パンプキンパイ").Customers = {"ハイラガ開墾団", "道で遊ぶ子供たち", "仕立て屋ギルドの職人", "レンジャー協会 (グリモア)", "野菜直売会に訪れた客 (イベント)"}
    End Sub

    Private Shared Sub LoadWestWardCustomers()
        GetFood("球獣肉とアスパラの中華炒め").Customers = {"国立ハイ・ラガード学院生", "オーダーメイド靴工房員"}
        GetFood("軟骨揚げのシトロン餡かけ丼").Customers = {"錬金術師互助組合 (グリモア)"}
        GetFood("シカ肉のタタキ風").Customers = {"国立ハイ・ラガード学院生", "オーダーメイド靴工房員", "衛士隊訓練生"}
        GetFood("黒茶").Customers = {"学院開催のバザー客 (イベント)"}
        GetFood("アゲハの姿佃煮").Customers = {}
        GetFood("梟の軟骨からあげ").Customers = {"錬金術師互助組合 (グリモア)"}
        GetFood("くるみ羊羹").Customers = {"ハイ・ラガード学院教授連", "西区美術家ギルド員", "衛士隊訓練生", "現代巫術研究会 (グリモア)"}
        GetFood("鹿肉と樹海野菜のすき鍋").Customers = {"国立ハイ・ラガード学院生", "ハイ・ラガード学院教授連", "オーダーメイド靴工房員"}
        GetFood("エスカルゴ焼シトロンソース添え").Customers = {"オーダーメイド靴工房員", "錬金術師互助組合 (グリモア)", "現代巫術研究会 (グリモア)"}
        GetFood("ハイラガコーヒー").Customers = {"学院開催のバザー客 (イベント)"}
        GetFood("クルミ入りライ麦パン").Customers = {"オーダーメイド靴工房員", "現代巫術研究会 (グリモア)"}
        GetFood("シカ肉のステーキ").Customers = {"国立ハイ・ラガード学院生", "オーダーメイド靴工房員"}
        GetFood("麻辣獄火鍋").Customers = {"国立ハイ・ラガード学院生", "ハイ・ラガード学院教授連"}
        GetFood("鶏唐揚げの甘酢餡かけ").Customers = {"国立ハイ・ラガード学院生", "錬金術師互助組合 (グリモア)"}
        GetFood("栗月餅").Customers = {"オーダーメイド靴工房員", "西区美術家ギルド員", "衛士隊訓練生", "現代巫術研究会 (グリモア)"}
        GetFood("火龍果杏仁").Customers = {"西区美術家ギルド員", "衛士隊訓練生", "現代巫術研究会 (グリモア)"}
        GetFood("怪しい石焼き鍋").Customers = {"ハイ・ラガード学院教授連", "錬金術師互助組合 (グリモア)"}
        GetFood("巨猪の豚汁").Customers = {"国立ハイ・ラガード学院生"}
        GetFood("鬼いが栗の茶巾絞り").Customers = {"ハイ・ラガード学院教授連", "西区美術家ギルド員", "衛士隊訓練生", "現代巫術研究会 (グリモア)"}
        GetFood("紅葉狩り串団子").Customers = {"ハイ・ラガード学院教授連", "西区美術家ギルド員", "衛士隊訓練生", "現代巫術研究会 (グリモア)"}
        GetFood("かみつきのサンドイッチ").Customers = {"オーダーメイド靴工房員", "衛士隊訓練生"}
        GetFood("旬の秋野菜ポトフ").Customers = {"国立ハイ・ラガード学院生"}
        GetFood("パーシモンプディング").Customers = {"ハイ・ラガード学院教授連", "西区美術家ギルド員", "衛士隊訓練生", "現代巫術研究会 (グリモア)"}
        GetFood("ジビエカレーライス").Customers = {"国立ハイ・ラガード学院生"}
        GetFood("リンゴ入り愛玉子風ブルーゼリー").Customers = {"西区美術家ギルド員", "衛士隊訓練生", "現代巫術研究会 (グリモア)"}
        GetFood("野牛肉拉麺").Customers = {"国立ハイ・ラガード学院生"}
        GetFood("馬肉中華包子").Customers = {"国立ハイ・ラガード学院生", "ハイ・ラガード学院教授連"}
        GetFood("雪鳥の蟹玉").Customers = {"国立ハイ・ラガード学院生", "オーダーメイド靴工房員", "錬金術師互助組合 (グリモア)"}
        GetFood("東国の伝統兜焼き").Customers = {"オーダーメイド靴工房員", "錬金術師互助組合 (グリモア)"}
        GetFood("雪鳥卵の熱々おでん").Customers = {"ハイ・ラガード学院教授連", "錬金術師互助組合 (グリモア)"}
        GetFood("カニの樹海茶漬け").Customers = {"錬金術師互助組合 (グリモア)"}
        GetFood("林檎と抹茶のかき氷").Customers = {"西区美術家ギルド員", "衛士隊訓練生", "現代巫術研究会 (グリモア)"}
        GetFood("発酵怪魚パニーノ").Customers = {"オーダーメイド靴工房員", "錬金術師互助組合 (グリモア)"}
        GetFood("野牛ステーキのリンゴソース添え").Customers = {"国立ハイ・ラガード学院生", "オーダーメイド靴工房員"}
        GetFood("カニクリームコロッケ").Customers = {"錬金術師互助組合 (グリモア)"}
        GetFood("樹海パエリア").Customers = {"国立ハイ・ラガード学院生", "錬金術師互助組合 (グリモア)"}
        GetFood("蜘蛛の姿揚げ").Customers = {}
        GetFood("樹海鳥の雷炒飯").Customers = {"国立ハイ・ラガード学院生", "錬金術師互助組合 (グリモア)", "現代巫術研究会 (グリモア)"}
        GetFood("森林サイ角煮").Customers = {"国立ハイ・ラガード学院生"}
        GetFood("石化鳥と樹海野菜の細切り炒め").Customers = {"錬金術師互助組合 (グリモア)"}
        GetFood("亀の甲羅焼き肉").Customers = {"国立ハイ・ラガード学院生", "オーダーメイド靴工房員", "錬金術師互助組合 (グリモア)"}
        GetFood("大苺大福").Customers = {"ハイ・ラガード学院教授連", "西区美術家ギルド員", "衛士隊訓練生", "現代巫術研究会 (グリモア)"}
        GetFood("樹海桜茶").Customers = {"学院開催のバザー客 (イベント)"}
        GetFood("石化鳥の肉じゃが").Customers = {"錬金術師互助組合 (グリモア)"}
        GetFood("サソリのグリーンパスタ").Customers = {}
        GetFood("ハチミツジャーマンポテト").Customers = {"国立ハイ・ラガード学院生"}
        GetFood("ももステーキの桜苺ジャム添え").Customers = {"オーダーメイド靴工房員", "錬金術師互助組合 (グリモア)"}
        GetFood("サイ肉のとろとろシチュー").Customers = {"国立ハイ・ラガード学院生"}
        GetFood("ノヅチ丸ごとスープ").Customers = {"オーダーメイド靴工房員"}
        GetFood("宝龍包").Customers = {"ハイ・ラガード学院教授連", "オーダーメイド靴工房員"}
        GetFood("皇帝ツバメの巣と岩茸の幻スープ").Customers = {}
        GetFood("きょうか鶏").Customers = {"ハイ・ラガード学院教授連", "錬金術師互助組合 (グリモア)"}
        GetFood("特製大釜の鎧竜炊き込みご飯").Customers = {"オーダーメイド靴工房員"}
        GetFood("桜肉のしゃぶしゃぶ	").Customers = {"国立ハイ・ラガード学院生", "ハイ・ラガード学院教授連"}
        GetFood("雲海山鳥の茶碗蒸し").Customers = {"ハイ・ラガード学院教授連", "錬金術師互助組合 (グリモア)"}
        GetFood("宝石煮こごりのツバメの巣入り").Customers = {"オーダーメイド靴工房員", "衛士隊訓練生"}
        GetFood("禍々しいアスピック").Customers = {"衛士隊訓練生"}
        GetFood("３種食べ比べハンバーグ").Customers = {"オーダーメイド靴工房員", "錬金術師互助組合 (グリモア)"}
        GetFood("タルタルステーキ").Customers = {"国立ハイ・ラガード学院生", "衛士隊訓練生"}
        GetFood("ストーンガレット").Customers = {"オーダーメイド靴工房員"}
        GetFood("危険な花茶").Customers = {"現代巫術研究会 (グリモア)", "学院開催のバザー客 (イベント)"}
        GetFood("シュウマイ").Customers = {"国立ハイ・ラガード学院生", "ハイ・ラガード学院教授連"}
        GetFood("樹海特選の回鍋肉").Customers = {"国立ハイ・ラガード学院生", "オーダーメイド靴工房員"}
        GetFood("蜥蜴肉の蜜汁火方").Customers = {"ハイ・ラガード学院教授連", "オーダーメイド靴工房員"}
        GetFood("古代ヤドカリの姿造り").Customers = {"衛士隊訓練生", "現代巫術研究会 (グリモア)"}
        GetFood("黒獣のお吸い物").Customers = {"国立ハイ・ラガード学院生"}
        GetFood("力士秘伝のちゃんこ風鍋").Customers = {"オーダーメイド靴工房員"}
        GetFood("東国伝来の煮しめ").Customers = {}
        GetFood("大芋虫のキャセロール").Customers = {}
        GetFood("オレンジソースの怪獣ステーキ").Customers = {"オーダーメイド靴工房員"}
        GetFood("タケノコが入ったサルマーレ").Customers = {"オーダーメイド靴工房員"}
        GetFood("パンプキンパイ").Customers = {"オーダーメイド靴工房員", "西区美術家ギルド員", "衛士隊訓練生", "現代巫術研究会 (グリモア)"}

    End Sub

    Private Shared Sub LoadNorthWardCustomers()
        GetFood("球獣肉とアスパラの中華炒め").Customers = {}
        GetFood("軟骨揚げのシトロン餡かけ丼").Customers = {"ひまわり商店街の店員", "ハイラガ酒蔵の蔵人", "樹海流武士道場の門徒 (グリモア)"}
        GetFood("シカ肉のタタキ風").Customers = {"ひまわり商店街の店員"}
        GetFood("黒茶").Customers = {"ラガード陶芸俱楽部", "訪問中のキャラバン隊員 (イベント)"}
        GetFood("アゲハの姿佃煮").Customers = {"大道芸人の一座"}
        GetFood("梟の軟骨からあげ").Customers = {"ひまわり商店街の店員", "ハイラガ酒蔵の蔵人"}
        GetFood("くるみ羊羹").Customers = {"鉄工鍛冶ギルドの職人", "ひまわり商店街の店員"}
        GetFood("鹿肉と樹海野菜のすき鍋").Customers = {"ラガード陶芸俱楽部", "訪問中のキャラバン隊員 (イベント)"}
        GetFood("エスカルゴ焼シトロンソース添え").Customers = {"ラガード陶芸俱楽部", "大道芸人の一座"}
        GetFood("ハイラガコーヒー").Customers = {}
        GetFood("クルミ入りライ麦パン").Customers = {}
        GetFood("シカ肉のステーキ").Customers = {}
        GetFood("麻辣獄火鍋").Customers = {"ラガード陶芸俱楽部", "アナタのためのバード隊 (グリモア)"}
        GetFood("鶏唐揚げの甘酢餡かけ").Customers = {"ひまわり商店街の店員", "ハイラガ酒蔵の蔵人"}
        GetFood("栗月餅").Customers = {"ひまわり商店街の店員", "アナタのためのバード隊 (グリモア)"}
        GetFood("火龍果杏仁").Customers = {"ひまわり商店街の店員"}
        GetFood("怪しい石焼き鍋").Customers = {"ラガード陶芸俱楽部", "ハイラガ酒蔵の蔵人"}
        GetFood("巨猪の豚汁").Customers = {"アナタのためのバード隊 (グリモア)"}
        GetFood("鬼いが栗の茶巾絞り").Customers = {"鉄工鍛冶ギルドの職人", "ひまわり商店街の店員"}
        GetFood("紅葉狩り串団子").Customers = {"鉄工鍛冶ギルドの職人", "ひまわり商店街の店員", "アナタのためのバード隊 (グリモア)"}
        GetFood("かみつきのサンドイッチ").Customers = {"鉄工鍛冶ギルドの職人", "ひまわり商店街の店員", "ハイラガ酒蔵の蔵人"}
        GetFood("旬の秋野菜ポトフ").Customers = {}
        GetFood("パーシモンプディング").Customers = {"鉄工鍛冶ギルドの職人", "ひまわり商店街の店員"}
        GetFood("ジビエカレーライス").Customers = {"アナタのためのバード隊 (グリモア)", "樹海流武士道場の門徒 (グリモア)"}
        GetFood("リンゴ入り愛玉子風ブルーゼリー").Customers = {"ひまわり商店街の店員"}
        GetFood("野牛肉拉麺").Customers = {"アナタのためのバード隊 (グリモア)"}
        GetFood("馬肉中華包子").Customers = {"鉄工鍛冶ギルドの職人"}
        GetFood("雪鳥の蟹玉").Customers = {"鉄工鍛冶ギルドの職人"}
        GetFood("東国の伝統兜焼き").Customers = {}
        GetFood("雪鳥卵の熱々おでん").Customers = {"鉄工鍛冶ギルドの職人", "アナタのためのバード隊 (グリモア)"}
        GetFood("カニの樹海茶漬け").Customers = {"アナタのためのバード隊 (グリモア)", "樹海流武士道場の門徒 (グリモア)"}
        GetFood("林檎と抹茶のかき氷").Customers = {"ひまわり商店街の店員"}
        GetFood("発酵怪魚パニーノ").Customers = {}
        GetFood("野牛ステーキのリンゴソース添え").Customers = {}
        GetFood("カニクリームコロッケ").Customers = {"ひまわり商店街の店員"}
        GetFood("樹海パエリア").Customers = {"樹海流武士道場の門徒 (グリモア)"}
        GetFood("蜘蛛の姿揚げ").Customers = {"大道芸人の一座", "ひまわり商店街の店員", "アナタのためのバード隊 (グリモア)"}
        GetFood("樹海鳥の雷炒飯").Customers = {"鉄工鍛冶ギルドの職人", "ハイラガ酒蔵の蔵人", "樹海流武士道場の門徒 (グリモア)"}
        GetFood("森林サイ角煮").Customers = {}
        GetFood("石化鳥と樹海野菜の細切り炒め").Customers = {"ハイラガ酒蔵の蔵人", "アナタのためのバード隊 (グリモア)"}
        GetFood("亀の甲羅焼き肉").Customers = {"ラガード陶芸俱楽部", "ハイラガ酒蔵の蔵人"}
        GetFood("大苺大福").Customers = {"鉄工鍛冶ギルドの職人", "ひまわり商店街の店員"}
        GetFood("樹海桜茶").Customers = {"ラガード陶芸俱楽部", "訪問中のキャラバン隊員 (イベント)"}
        GetFood("石化鳥の肉じゃが").Customers = {"ハイラガ酒蔵の蔵人", "アナタのためのバード隊 (グリモア)"}
        GetFood("サソリのグリーンパスタ").Customers = {"鉄工鍛冶ギルドの職人", "大道芸人の一座"}
        GetFood("ハチミツジャーマンポテト").Customers = {}
        GetFood("ももステーキの桜苺ジャム添え").Customers = {"ラガード陶芸俱楽部", "ハイラガ酒蔵の蔵人", "訪問中のキャラバン隊員 (イベント)"}
        GetFood("サイ肉のとろとろシチュー").Customers = {"アナタのためのバード隊 (グリモア)"}
        GetFood("ノヅチ丸ごとスープ").Customers = {"大道芸人の一座", "アナタのためのバード隊 (グリモア)"}
        GetFood("宝龍包").Customers = {}
        GetFood("皇帝ツバメの巣と岩茸の幻スープ").Customers = {}
        GetFood("きょうか鶏").Customers = {"ラガード陶芸俱楽部", "鉄工鍛冶ギルドの職人", "ハイラガ酒蔵の蔵人"}
        GetFood("特製大釜の鎧竜炊き込みご飯").Customers = {"ラガード陶芸俱楽部"}
        GetFood("桜肉のしゃぶしゃぶ	").Customers = {"ラガード陶芸俱楽部"}
        GetFood("雲海山鳥の茶碗蒸し").Customers = {"鉄工鍛冶ギルドの職人", "ハイラガ酒蔵の蔵人"}
        GetFood("宝石煮こごりのツバメの巣入り").Customers = {"ラガード陶芸俱楽部", "ひまわり商店街の店員"}
        GetFood("禍々しいアスピック").Customers = {"ひまわり商店街の店員", "ハイラガ酒蔵の蔵人"}
        GetFood("３種食べ比べハンバーグ").Customers = {"ハイラガ酒蔵の蔵人"}
        GetFood("タルタルステーキ").Customers = {"鉄工鍛冶ギルドの職人", "ひまわり商店街の店員"}
        GetFood("ストーンガレット").Customers = {"ラガード陶芸俱楽部", "鉄工鍛冶ギルドの職人"}
        GetFood("危険な花茶").Customers = {"ラガード陶芸俱楽部", "訪問中のキャラバン隊員 (イベント)"}
        GetFood("シュウマイ").Customers = {"鉄工鍛冶ギルドの職人"}
        GetFood("樹海特選の回鍋肉").Customers = {"ラガード陶芸俱楽部", "訪問中のキャラバン隊員 (イベント)"}
        GetFood("蜥蜴肉の蜜汁火方").Customers = {"鉄工鍛冶ギルドの職人", "大道芸人の一座"}
        GetFood("古代ヤドカリの姿造り").Customers = {"ラガード陶芸俱楽部", "大道芸人の一座", "ひまわり商店街の店員"}
        GetFood("黒獣のお吸い物").Customers = {"アナタのためのバード隊 (グリモア)"}
        GetFood("力士秘伝のちゃんこ風鍋").Customers = {}
        GetFood("東国伝来の煮しめ").Customers = {"大道芸人の一座"}
        GetFood("大芋虫のキャセロール").Customers = {"ラガード陶芸俱楽部", "大道芸人の一座", "訪問中のキャラバン隊員 (イベント)"}
        GetFood("オレンジソースの怪獣ステーキ").Customers = {}
        GetFood("タケノコが入ったサルマーレ").Customers = {}
        GetFood("パンプキンパイ").Customers = {"大道芸人の一座", "ひまわり商店街の店員"}
    End Sub

    Private Shared Sub LoadUptownCustomers()
        GetFood("球獣肉とアスパラの中華炒め").Customers = {"城付き近衛兵団員", "黄金樹聖騎士団員 (グリモア)"}
        GetFood("軟骨揚げのシトロン餡かけ丼").Customers = {"貴族子弟の社交クラブ"}
        GetFood("シカ肉のタタキ風").Customers = {"貴族子弟の社交クラブ", "城付き近衛兵団員"}
        GetFood("黒茶").Customers = {"街路の靴磨き職人", "公国メイド組合員", "プリンセス茶会のメンバー (グリモア)", "黄金樹聖騎士団員 (グリモア)"}
        GetFood("アゲハの姿佃煮").Customers = {"街路の靴磨き職人", "公国メイド組合員", "中央区婦人会", "黄金樹聖騎士団員 (グリモア)"}
        GetFood("梟の軟骨からあげ").Customers = {"貴族子弟の社交クラブ"}
        GetFood("くるみ羊羹").Customers = {"貴族子弟の社交クラブ", "街路の靴磨き職人", "公国メイド組合員"}
        GetFood("鹿肉と樹海野菜のすき鍋").Customers = {"街路の靴磨き職人", "中央区婦人会", "城付き近衛兵団員", "プリンセス茶会のメンバー (グリモア)", "黄金樹聖騎士団員 (グリモア)"}
        GetFood("エスカルゴ焼シトロンソース添え").Customers = {"貴族子弟の社交クラブ", "城付き近衛兵団員"}
        GetFood("ハイラガコーヒー").Customers = {"街路の靴磨き職人", "公国メイド組合員", "黄金樹聖騎士団員 (グリモア)"}
        GetFood("クルミ入りライ麦パン").Customers = {"貴族子弟の社交クラブ", "城付き近衛兵団員"}
        GetFood("シカ肉のステーキ").Customers = {"城付き近衛兵団員", "黄金樹聖騎士団員 (グリモア)"}
        GetFood("麻辣獄火鍋").Customers = {"街路の靴磨き職人", "公国メイド組合員", "中央区婦人会", "黄金樹聖騎士団員 (グリモア)"}
        GetFood("鶏唐揚げの甘酢餡かけ").Customers = {"黄金樹聖騎士団員 (グリモア)"}
        GetFood("栗月餅").Customers = {"貴族子弟の社交クラブ", "街路の靴磨き職人", "公国メイド組合員", "城付き近衛兵団員"}
        GetFood("火龍果杏仁").Customers = {"貴族子弟の社交クラブ", "公国メイド組合員"}
        GetFood("怪しい石焼き鍋").Customers = {"中央区婦人会"}
        GetFood("巨猪の豚汁").Customers = {"中央区婦人会", "黄金樹聖騎士団員 (グリモア)"}
        GetFood("鬼いが栗の茶巾絞り").Customers = {"貴族子弟の社交クラブ", "街路の靴磨き職人", "公国メイド組合員"}
        GetFood("紅葉狩り串団子").Customers = {"貴族子弟の社交クラブ", "公国メイド組合員"}
        GetFood("かみつきのサンドイッチ").Customers = {"城付き近衛兵団員", "黄金樹聖騎士団員 (グリモア)"}
        GetFood("旬の秋野菜ポトフ").Customers = {"街路の靴磨き職人", "公国メイド組合員", "中央区婦人会", "黄金樹聖騎士団員 (グリモア)"}
        GetFood("パーシモンプディング").Customers = {"貴族子弟の社交クラブ", "公国メイド組合員"}
        GetFood("ジビエカレーライス").Customers = {"街路の靴磨き職人", "公国メイド組合員", "中央区婦人会", "プリンセス茶会のメンバー (グリモア)", "黄金樹聖騎士団員 (グリモア)"}
        GetFood("リンゴ入り愛玉子風ブルーゼリー").Customers = {"貴族子弟の社交クラブ", "公国メイド組合員", "逗留中の外交使節団 (イベント)"}
        GetFood("野牛肉拉麺").Customers = {"中央区婦人会", "逗留中の外交使節団 (イベント)"}
        GetFood("馬肉中華包子").Customers = {"街路の靴磨き職人", "黄金樹聖騎士団員 (グリモア)"}
        GetFood("雪鳥の蟹玉").Customers = {"城付き近衛兵団員"}
        GetFood("東国の伝統兜焼き").Customers = {"街路の靴磨き職人", "城付き近衛兵団員", "黄金樹聖騎士団員 (グリモア)"}
        GetFood("雪鳥卵の熱々おでん").Customers = {"街路の靴磨き職人", "公国メイド組合員", "中央区婦人会", "黄金樹聖騎士団員 (グリモア)"}
        GetFood("カニの樹海茶漬け").Customers = {"中央区婦人会", "黄金樹聖騎士団員 (グリモア)", "逗留中の外交使節団 (イベント)"}
        GetFood("林檎と抹茶のかき氷").Customers = {"貴族子弟の社交クラブ", "公国メイド組合員", "逗留中の外交使節団 (イベント)"}
        GetFood("発酵怪魚パニーノ").Customers = {"城付き近衛兵団員", "逗留中の外交使節団 (イベント)"}
        GetFood("野牛ステーキのリンゴソース添え").Customers = {"貴族子弟の社交クラブ", "城付き近衛兵団員"}
        GetFood("カニクリームコロッケ").Customers = {"黄金樹聖騎士団員 (グリモア)", "逗留中の外交使節団 (イベント)"}
        GetFood("樹海パエリア").Customers = {"街路の靴磨き職人", "公国メイド組合員", "中央区婦人会", "黄金樹聖騎士団員 (グリモア)"}
        GetFood("蜘蛛の姿揚げ").Customers = {"街路の靴磨き職人", "黄金樹聖騎士団員 (グリモア)"}
        GetFood("樹海鳥の雷炒飯").Customers = {}
        GetFood("森林サイ角煮").Customers = {"街路の靴磨き職人", "中央区婦人会", "黄金樹聖騎士団員 (グリモア)"}
        GetFood("石化鳥と樹海野菜の細切り炒め").Customers = {"街路の靴磨き職人", "公国メイド組合員", "中央区婦人会", "黄金樹聖騎士団員 (グリモア)"}
        GetFood("亀の甲羅焼き肉").Customers = {"城付き近衛兵団員"}
        GetFood("大苺大福").Customers = {"貴族子弟の社交クラブ", "公国メイド組合員"}
        GetFood("樹海桜茶").Customers = {"プリンセス茶会のメンバー (グリモア)"}
        GetFood("石化鳥の肉じゃが").Customers = {"街路の靴磨き職人", "公国メイド組合員", "中央区婦人会", "黄金樹聖騎士団員 (グリモア)"}
        GetFood("サソリのグリーンパスタ").Customers = {"黄金樹聖騎士団員 (グリモア)"}
        GetFood("ハチミツジャーマンポテト").Customers = {"街路の靴磨き職人", "公国メイド組合員", "黄金樹聖騎士団員 (グリモア)"}
        GetFood("ももステーキの桜苺ジャム添え").Customers = {"貴族子弟の社交クラブ", "城付き近衛兵団員", "プリンセス茶会のメンバー (グリモア)"}
        GetFood("サイ肉のとろとろシチュー").Customers = {"街路の靴磨き職人", "公国メイド組合員", "中央区婦人会", "黄金樹聖騎士団員 (グリモア)"}
        GetFood("ノヅチ丸ごとスープ").Customers = {"中央区婦人会", "城付き近衛兵団員", "黄金樹聖騎士団員 (グリモア)", "逗留中の外交使節団 (イベント)"}
        GetFood("宝龍包").Customers = {"街路の靴磨き職人", "城付き近衛兵団員", "黄金樹聖騎士団員 (グリモア)"}
        GetFood("皇帝ツバメの巣と岩茸の幻スープ").Customers = {"街路の靴磨き職人", "中央区婦人会", "黄金樹聖騎士団員 (グリモア)"}
        GetFood("きょうか鶏").Customers = {}
        GetFood("特製大釜の鎧竜炊き込みご飯").Customers = {"城付き近衛兵団員"}
        GetFood("桜肉のしゃぶしゃぶ	").Customers = {"中央区婦人会", "逗留中の外交使節団 (イベント)"}
        GetFood("雲海山鳥の茶碗蒸し").Customers = {"街路の靴磨き職人", "黄金樹聖騎士団員 (グリモア)"}
        GetFood("宝石煮こごりのツバメの巣入り").Customers = {"中央区婦人会", "城付き近衛兵団員"}
        GetFood("禍々しいアスピック").Customers = {"中央区婦人会", "逗留中の外交使節団 (イベント)"}
        GetFood("３種食べ比べハンバーグ").Customers = {"城付き近衛兵団員"}
        GetFood("タルタルステーキ").Customers = {}
        GetFood("ストーンガレット").Customers = {"城付き近衛兵団員", "黄金樹聖騎士団員 (グリモア)"}
        GetFood("危険な花茶").Customers = {"貴族子弟の社交クラブ", "プリンセス茶会のメンバー (グリモア)"}
        GetFood("シュウマイ").Customers = {"街路の靴磨き職人", "黄金樹聖騎士団員 (グリモア)"}
        GetFood("樹海特選の回鍋肉").Customers = {"城付き近衛兵団員", "プリンセス茶会のメンバー (グリモア)", "黄金樹聖騎士団員 (グリモア)"}
        GetFood("蜥蜴肉の蜜汁火方").Customers = {"城付き近衛兵団員", "黄金樹聖騎士団員 (グリモア)"}
        GetFood("古代ヤドカリの姿造り").Customers = {"貴族子弟の社交クラブ"}
        GetFood("黒獣のお吸い物").Customers = {"貴族子弟の社交クラブ", "中央区婦人会"}
        GetFood("力士秘伝のちゃんこ風鍋").Customers = {"中央区婦人会", "城付き近衛兵団員", "黄金樹聖騎士団員 (グリモア)"}
        GetFood("東国伝来の煮しめ").Customers = {"街路の靴磨き職人", "中央区婦人会", "黄金樹聖騎士団員 (グリモア)"}
        GetFood("大芋虫のキャセロール").Customers = {"街路の靴磨き職人", "中央区婦人会", "プリンセス茶会のメンバー (グリモア)", "黄金樹聖騎士団員 (グリモア)"}
        GetFood("オレンジソースの怪獣ステーキ").Customers = {"貴族子弟の社交クラブ", "城付き近衛兵団員"}
        GetFood("タケノコが入ったサルマーレ").Customers = {"中央区婦人会", "城付き近衛兵団員", "黄金樹聖騎士団員 (グリモア)"}
        GetFood("パンプキンパイ").Customers = {"貴族子弟の社交クラブ", "公国メイド組合員"}
    End Sub

    Private Shared Sub LoadSlumCustomers()
        GetFood("球獣肉とアスパラの中華炒め").Customers = {"青空教室の生徒たち"}
        GetFood("軟骨揚げのシトロン餡かけ丼").Customers = {"路上にたむろする若者たち", "見回り中の衛士隊", "ダークハンター束縛連合 (グリモア)"}
        GetFood("シカ肉のタタキ風").Customers = {"アヤしい露店の商人団"}
        GetFood("黒茶").Customers = {"アヤしい露店の商人団"}
        GetFood("アゲハの姿佃煮").Customers = {"昼からのんだくれ隊", "アヤしい露店の商人団"}
        GetFood("梟の軟骨からあげ").Customers = {"アヤしい露店の商人団", "青空教室の生徒たち", "見回り中の衛士隊", "ダークハンター束縛連合 (グリモア)"}
        GetFood("くるみ羊羹").Customers = {"アヤしい露店の商人団"}
        GetFood("鹿肉と樹海野菜のすき鍋").Customers = {"昼からのんだくれ隊", "青空教室の生徒たち"}
        GetFood("エスカルゴ焼シトロンソース添え").Customers = {"昼からのんだくれ隊", "呪言の普及振興を願う会 (グリモア)"}
        GetFood("ハイラガコーヒー").Customers = {"アヤしい露店の商人団"}
        GetFood("クルミ入りライ麦パン").Customers = {"アヤしい露店の商人団"}
        GetFood("シカ肉のステーキ").Customers = {}
        GetFood("麻辣獄火鍋").Customers = {"昼からのんだくれ隊"}
        GetFood("鶏唐揚げの甘酢餡かけ").Customers = {"青空教室の生徒たち", "見回り中の衛士隊", "ダークハンター束縛連合 (グリモア)"}
        GetFood("栗月餅").Customers = {"路上にたむろする若者たち"}
        GetFood("火龍果杏仁").Customers = {}
        GetFood("怪しい石焼き鍋").Customers = {"昼からのんだくれ隊", "見回り中の衛士隊", "ダークハンター束縛連合 (グリモア)"}
        GetFood("巨猪の豚汁").Customers = {"昼からのんだくれ隊"}
        GetFood("鬼いが栗の茶巾絞り").Customers = {}
        GetFood("紅葉狩り串団子").Customers = {"路上にたむろする若者たち", "入国審査待ちの移民団 (イベント)"}
        GetFood("かみつきのサンドイッチ").Customers = {"見回り中の衛士隊"}
        GetFood("旬の秋野菜ポトフ").Customers = {"昼からのんだくれ隊", "青空教室の生徒たち"}
        GetFood("パーシモンプディング").Customers = {"入国審査待ちの移民団 (イベント)"}
        GetFood("ジビエカレーライス").Customers = {"路上にたむろする若者たち", "昼からのんだくれ隊"}
        GetFood("リンゴ入り愛玉子風ブルーゼリー").Customers = {"アヤしい露店の商人団", "入国審査待ちの移民団 (イベント)"}
        GetFood("野牛肉拉麺").Customers = {"アヤしい露店の商人団"}
        GetFood("馬肉中華包子").Customers = {}
        GetFood("雪鳥の蟹玉").Customers = {"昼からのんだくれ隊", "青空教室の生徒たち", "呪言の普及振興を願う会 (グリモア)"}
        GetFood("東国の伝統兜焼き").Customers = {"昼からのんだくれ隊", "呪言の普及振興を願う会 (グリモア)"}
        GetFood("雪鳥卵の熱々おでん").Customers = {"昼からのんだくれ隊"}
        GetFood("カニの樹海茶漬け").Customers = {"路上にたむろする若者たち", "昼からのんだくれ隊", "アヤしい露店の商人団", "呪言の普及振興を願う会 (グリモア)"}
        GetFood("林檎と抹茶のかき氷").Customers = {"アヤしい露店の商人団"}
        GetFood("発酵怪魚パニーノ").Customers = {"昼からのんだくれ隊", "アヤしい露店の商人団", "呪言の普及振興を願う会 (グリモア)"}
        GetFood("野牛ステーキのリンゴソース添え").Customers = {}
        GetFood("カニクリームコロッケ").Customers = {"昼からのんだくれ隊", "アヤしい露店の商人団", "見回り中の衛士隊", "呪言の普及振興を願う会 (グリモア)"}
        GetFood("樹海パエリア").Customers = {"路上にたむろする若者たち", "昼からのんだくれ隊"}
        GetFood("蜘蛛の姿揚げ").Customers = {"路上にたむろする若者たち", "見回り中の衛士隊", "呪言の普及振興を願う会 (グリモア)"}
        GetFood("樹海鳥の雷炒飯").Customers = {"路上にたむろする若者たち", "青空教室の生徒たち", "ダークハンター束縛連合 (グリモア)"}
        GetFood("森林サイ角煮").Customers = {"昼からのんだくれ隊", "アヤしい露店の商人団"}
        GetFood("石化鳥と樹海野菜の細切り炒め").Customers = {"路上にたむろする若者たち", "青空教室の生徒たち", "見回り中の衛士隊", "ダークハンター束縛連合 (グリモア)"}
        GetFood("亀の甲羅焼き肉").Customers = {"見回り中の衛士隊"}
        GetFood("大苺大福").Customers = {"アヤしい露店の商人団"}
        GetFood("樹海桜茶").Customers = {"アヤしい露店の商人団"}
        GetFood("石化鳥の肉じゃが").Customers = {"路上にたむろする若者たち", "昼からのんだくれ隊", "見回り中の衛士隊", "ダークハンター束縛連合 (グリモア)"}
        GetFood("サソリのグリーンパスタ").Customers = {"呪言の普及振興を願う会 (グリモア)"}
        GetFood("ハチミツジャーマンポテト").Customers = {"アヤしい露店の商人団", "青空教室の生徒たち"}
        GetFood("ももステーキの桜苺ジャム添え").Customers = {"見回り中の衛士隊"}
        GetFood("サイ肉のとろとろシチュー").Customers = {"路上にたむろする若者たち", "昼からのんだくれ隊"}
        GetFood("ノヅチ丸ごとスープ").Customers = {"昼からのんだくれ隊", "アヤしい露店の商人団", "ダークハンター束縛連合 (グリモア)"}
        GetFood("宝龍包").Customers = {"青空教室の生徒たち", "ダークハンター束縛連合 (グリモア)"}
        GetFood("皇帝ツバメの巣と岩茸の幻スープ").Customers = {"青空教室の生徒たち"}
        GetFood("きょうか鶏").Customers = {"見回り中の衛士隊", "ダークハンター束縛連合 (グリモア)"}
        GetFood("特製大釜の鎧竜炊き込みご飯").Customers = {"路上にたむろする若者たち", "ダークハンター束縛連合 (グリモア)"}
        GetFood("桜肉のしゃぶしゃぶ	").Customers = {"アヤしい露店の商人団"}
        GetFood("雲海山鳥の茶碗蒸し").Customers = {"青空教室の生徒たち", "見回り中の衛士隊", "ダークハンター束縛連合 (グリモア)"}
        GetFood("宝石煮こごりのツバメの巣入り").Customers = {"昼からのんだくれ隊", "ダークハンター束縛連合 (グリモア)"}
        GetFood("禍々しいアスピック").Customers = {"昼からのんだくれ隊", "アヤしい露店の商人団", "見回り中の衛士隊", "ダークハンター束縛連合 (グリモア)", "入国審査待ちの移民団 (イベント)"}
        GetFood("３種食べ比べハンバーグ").Customers = {"見回り中の衛士隊"}
        GetFood("タルタルステーキ").Customers = {}
        GetFood("ストーンガレット").Customers = {}
        GetFood("危険な花茶").Customers = {}
        GetFood("シュウマイ").Customers = {"青空教室の生徒たち"}
        GetFood("樹海特選の回鍋肉").Customers = {"青空教室の生徒たち", "ダークハンター束縛連合 (グリモア)"}
        GetFood("蜥蜴肉の蜜汁火方").Customers = {"アヤしい露店の商人団", "呪言の普及振興を願う会 (グリモア)", "ダークハンター束縛連合 (グリモア)"}
        GetFood("古代ヤドカリの姿造り").Customers = {"昼からのんだくれ隊", "呪言の普及振興を願う会 (グリモア)"}
        GetFood("黒獣のお吸い物").Customers = {}
        GetFood("力士秘伝のちゃんこ風鍋").Customers = {"昼からのんだくれ隊", "ダークハンター束縛連合 (グリモア)"}
        GetFood("東国伝来の煮しめ").Customers = {"昼からのんだくれ隊", "アヤしい露店の商人団", "青空教室の生徒たち"}
        GetFood("大芋虫のキャセロール").Customers = {"昼からのんだくれ隊", "青空教室の生徒たち"}
        GetFood("オレンジソースの怪獣ステーキ").Customers = {}
        GetFood("タケノコが入ったサルマーレ").Customers = {"昼からのんだくれ隊", "ダークハンター束縛連合 (グリモア)"}
        GetFood("パンプキンパイ").Customers = {"アヤしい露店の商人団", "呪言の普及振興を願う会 (グリモア)"}
    End Sub

    Private Shared Sub LoadSouthWardCustomers()
        GetFood("球獣肉とアスパラの中華炒め").Customers = {"森林マタギ団", "ソードマン闘技研鑽会 (グリモア)"}
        GetFood("軟骨揚げのシトロン餡かけ丼").Customers = {"冒険者支援センター", "ユグドラシル建設作業員"}
        GetFood("シカ肉のタタキ風").Customers = {"森林マタギ団", "ソードマン闘技研鑽会 (グリモア)"}
        GetFood("黒茶").Customers = {"南区の家具工房職人"}
        GetFood("アゲハの姿佃煮").Customers = {"ハイラガ木こりの会", "公国ガンナー管理組合 (グリモア)"}
        GetFood("梟の軟骨からあげ").Customers = {}
        GetFood("くるみ羊羹").Customers = {}
        GetFood("鹿肉と樹海野菜のすき鍋").Customers = {"ハイラガ木こりの会", "森林マタギ団", "公国ガンナー管理組合 (グリモア)"}
        GetFood("エスカルゴ焼シトロンソース添え").Customers = {"ハイラガ木こりの会", "ソードマン闘技研鑽会 (グリモア)"}
        GetFood("ハイラガコーヒー").Customers = {"南区の家具工房職人", "冒険者支援センター"}
        GetFood("クルミ入りライ麦パン").Customers = {"冒険者支援センター"}
        GetFood("シカ肉のステーキ").Customers = {"森林マタギ団", "ソードマン闘技研鑽会 (グリモア)"}
        GetFood("麻辣獄火鍋").Customers = {"南区の家具工房職人", "森林マタギ団", "公国ガンナー管理組合 (グリモア)", "一時雇いの大工集団 (イベント)"}
        GetFood("鶏唐揚げの甘酢餡かけ").Customers = {}
        GetFood("栗月餅").Customers = {}
        GetFood("火龍果杏仁").Customers = {}
        GetFood("怪しい石焼き鍋").Customers = {"公国ガンナー管理組合 (グリモア)", "一時雇いの大工集団 (イベント)"}
        GetFood("巨猪の豚汁").Customers = {"南区の家具工房職人", "森林マタギ団", "公国ガンナー管理組合 (グリモア)"}
        GetFood("鬼いが栗の茶巾絞り").Customers = {}
        GetFood("紅葉狩り串団子").Customers = {}
        GetFood("かみつきのサンドイッチ").Customers = {}
        GetFood("旬の秋野菜ポトフ").Customers = {"南区の家具工房職人", "ハイラガ木こりの会", "森林マタギ団", "公国ガンナー管理組合 (グリモア)"}
        GetFood("パーシモンプディング").Customers = {}
        GetFood("ジビエカレーライス").Customers = {"南区の家具工房職人", "森林マタギ団", "冒険者支援センター", "ユグドラシル建設作業員", "公国ガンナー管理組合 (グリモア)"}
        GetFood("リンゴ入り愛玉子風ブルーゼリー").Customers = {}
        GetFood("野牛肉拉麺").Customers = {"南区の家具工房職人", "森林マタギ団", "冒険者支援センター"}
        GetFood("馬肉中華包子").Customers = {"森林マタギ団", "冒険者支援センター"}
        GetFood("雪鳥の蟹玉").Customers = {"ユグドラシル建設作業員", "ソードマン闘技研鑽会 (グリモア)"}
        GetFood("東国の伝統兜焼き").Customers = {"ソードマン闘技研鑽会 (グリモア)"}
        GetFood("雪鳥卵の熱々おでん").Customers = {"南区の家具工房職人", "ユグドラシル建設作業員"}
        GetFood("カニの樹海茶漬け").Customers = {"南区の家具工房職人", "冒険者支援センター", "ユグドラシル建設作業員"}
        GetFood("林檎と抹茶のかき氷").Customers = {}
        GetFood("発酵怪魚パニーノ").Customers = {"冒険者支援センター", "ソードマン闘技研鑽会 (グリモア)"}
        GetFood("野牛ステーキのリンゴソース添え").Customers = {"森林マタギ団", "ソードマン闘技研鑽会 (グリモア)"}
        GetFood("カニクリームコロッケ").Customers = {"冒険者支援センター"}
        GetFood("樹海パエリア").Customers = {"森林マタギ団", "冒険者支援センター", "ユグドラシル建設作業員"}
        GetFood("蜘蛛の姿揚げ").Customers = {"ハイラガ木こりの会"}
        GetFood("樹海鳥の雷炒飯").Customers = {"冒険者支援センター", "ユグドラシル建設作業員", "ソードマン闘技研鑽会 (グリモア)"}
        GetFood("森林サイ角煮").Customers = {"森林マタギ団", "公国ガンナー管理組合 (グリモア)"}
        GetFood("石化鳥と樹海野菜の細切り炒め").Customers = {"ソードマン闘技研鑽会 (グリモア)"}
        GetFood("亀の甲羅焼き肉").Customers = {"森林マタギ団", "ソードマン闘技研鑽会 (グリモア)", "一時雇いの大工集団 (イベント)"}
        GetFood("大苺大福").Customers = {}
        GetFood("樹海桜茶").Customers = {"南区の家具工房職人"}
        GetFood("石化鳥の肉じゃが").Customers = {"公国ガンナー管理組合 (グリモア)"}
        GetFood("サソリのグリーンパスタ").Customers = {"ハイラガ木こりの会", "冒険者支援センター", "ユグドラシル建設作業員"}
        GetFood("ハチミツジャーマンポテト").Customers = {}
        GetFood("ももステーキの桜苺ジャム添え").Customers = {"ソードマン闘技研鑽会 (グリモア)"}
        GetFood("サイ肉のとろとろシチュー").Customers = {"森林マタギ団", "公国ガンナー管理組合 (グリモア)"}
        GetFood("ノヅチ丸ごとスープ").Customers = {"南区の家具工房職人", "ハイラガ木こりの会", "公国ガンナー管理組合 (グリモア)"}
        GetFood("宝龍包").Customers = {"ハイラガ木こりの会", "冒険者支援センター"}
        GetFood("皇帝ツバメの巣と岩茸の幻スープ").Customers = {"南区の家具工房職人", "ハイラガ木こりの会"}
        GetFood("きょうか鶏").Customers = {"一時雇いの大工集団 (イベント)"}
        GetFood("特製大釜の鎧竜炊き込みご飯").Customers = {"冒険者支援センター", "ユグドラシル建設作業員", "ソードマン闘技研鑽会 (グリモア)", "一時雇いの大工集団 (イベント)"}
        GetFood("桜肉のしゃぶしゃぶ	").Customers = {"森林マタギ団", "公国ガンナー管理組合 (グリモア)", "一時雇いの大工集団 (イベント)"}
        GetFood("雲海山鳥の茶碗蒸し").Customers = {"ハイラガ木こりの会", "ユグドラシル建設作業員"}
        GetFood("宝石煮こごりのツバメの巣入り").Customers = {}
        GetFood("禍々しいアスピック").Customers = {}
        GetFood("３種食べ比べハンバーグ").Customers = {"ソードマン闘技研鑽会 (グリモア)"}
        GetFood("タルタルステーキ").Customers = {"森林マタギ団", "ユグドラシル建設作業員", "ソードマン闘技研鑽会 (グリモア)"}
        GetFood("ストーンガレット").Customers = {"ユグドラシル建設作業員", "一時雇いの大工集団 (イベント)"}
        GetFood("危険な花茶").Customers = {"南区の家具工房職人"}
        GetFood("シュウマイ").Customers = {"ハイラガ木こりの会", "森林マタギ団", "冒険者支援センター"}
        GetFood("樹海特選の回鍋肉").Customers = {"ソードマン闘技研鑽会 (グリモア)"}
        GetFood("蜥蜴肉の蜜汁火方").Customers = {"ハイラガ木こりの会"}
        GetFood("古代ヤドカリの姿造り").Customers = {"ハイラガ木こりの会"}
        GetFood("黒獣のお吸い物").Customers = {"南区の家具工房職人", "森林マタギ団"}
        GetFood("力士秘伝のちゃんこ風鍋").Customers = {"公国ガンナー管理組合 (グリモア)"}
        GetFood("東国伝来の煮しめ").Customers = {"ハイラガ木こりの会", "公国ガンナー管理組合 (グリモア)"}
        GetFood("大芋虫のキャセロール").Customers = {"ハイラガ木こりの会", "公国ガンナー管理組合 (グリモア)"}
        GetFood("オレンジソースの怪獣ステーキ").Customers = {"ソードマン闘技研鑽会 (グリモア)"}
        GetFood("タケノコが入ったサルマーレ").Customers = {"公国ガンナー管理組合 (グリモア)"}
        GetFood("パンプキンパイ").Customers = {"ハイラガ木こりの会"}
    End Sub
#End Region

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DistrictComboBox.SelectedIndexChanged
        GivenWard = DirectCast([Enum].Parse(GetType(Ward), DistrictComboBox.Text), Ward)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles NextButton.Click
        GoDownOneFood()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles PrevButton.Click
        GoUpOneFood()
    End Sub

    Private Sub GroupFlowLayoutPanel_Paint(sender As Object, e As PaintEventArgs) Handles GroupFlowLayoutPanel.Paint

    End Sub
End Class
