<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WardGroup
   Inherits System.Windows.Forms.UserControl

   'UserControl overrides dispose to clean up the component list.
   <System.Diagnostics.DebuggerNonUserCode()> _
   Protected Overrides Sub Dispose(ByVal disposing As Boolean)
      Try
         If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
         End If
      Finally
         MyBase.Dispose(disposing)
      End Try
   End Sub

   'Required by the Windows Form Designer
   Private components As System.ComponentModel.IContainer

   'NOTE: The following procedure is required by the Windows Form Designer
   'It can be modified using the Windows Form Designer.  
   'Do not modify it using the code editor.
   <System.Diagnostics.DebuggerStepThrough()> _
   Private Sub InitializeComponent()
        Me.MainGroupBox = New System.Windows.Forms.GroupBox()

        Me.GroupFlowLayoutPanel = New System.Windows.Forms.FlowLayoutPanel()

        Me.DistrictStarsPanel = New System.Windows.Forms.Panel()

        Me.StarLimitComboBox = New System.Windows.Forms.ComboBox()

        Me.HighestRecipeLabel = New System.Windows.Forms.Label()

        Me.GourmetCheckBox = New System.Windows.Forms.CheckBox()

        Me.DistrictComboBox = New System.Windows.Forms.ComboBox()

        Me.DistrictLabelLabel = New System.Windows.Forms.Label()

        Me.InnerPanel = New System.Windows.Forms.FlowLayoutPanel()

        Me.BottomPanel = New System.Windows.Forms.Panel()

        Me.ButtonPanel = New System.Windows.Forms.Panel()

        Me.PrevButton = New System.Windows.Forms.Button()

        Me.NextButton = New System.Windows.Forms.Button()

        Me.FoodResultLabel = New System.Windows.Forms.Label()

        Me.FoodDisplayLabel = New System.Windows.Forms.Label()

        Me.CalculateButton = New System.Windows.Forms.Button()

        Me.ResetButton = New System.Windows.Forms.Button()

        Me.MainGroupBox.SuspendLayout()

        Me.GroupFlowLayoutPanel.SuspendLayout()

        Me.DistrictStarsPanel.SuspendLayout()

        Me.BottomPanel.SuspendLayout()

        Me.ButtonPanel.SuspendLayout()

        Me.SuspendLayout()

        '

        'MainGroupBox

        '

        Me.MainGroupBox.Controls.Add(Me.GroupFlowLayoutPanel)

        Me.MainGroupBox.Controls.Add(Me.BottomPanel)

        Me.MainGroupBox.Dock = System.Windows.Forms.DockStyle.Fill

        Me.MainGroupBox.Location = New System.Drawing.Point(0, 0)

        Me.MainGroupBox.Margin = New System.Windows.Forms.Padding(0)

        Me.MainGroupBox.Name = "MainGroupBox"

        Me.MainGroupBox.Padding = New System.Windows.Forms.Padding(2)

        Me.MainGroupBox.Size = New System.Drawing.Size(801, 218)

        Me.MainGroupBox.TabIndex = 2

        Me.MainGroupBox.TabStop = False

        '

        'GroupFlowLayoutPanel

        '

        Me.GroupFlowLayoutPanel.Controls.Add(Me.DistrictStarsPanel)

        Me.GroupFlowLayoutPanel.Controls.Add(Me.InnerPanel)

        Me.GroupFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill

        Me.GroupFlowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown

        Me.GroupFlowLayoutPanel.Location = New System.Drawing.Point(2, 14)

        Me.GroupFlowLayoutPanel.Margin = New System.Windows.Forms.Padding(0)

        Me.GroupFlowLayoutPanel.Name = "GroupFlowLayoutPanel"

        Me.GroupFlowLayoutPanel.Size = New System.Drawing.Size(797, 100)

        Me.GroupFlowLayoutPanel.TabIndex = 4

        '

        'DistrictStarsPanel

        '

        Me.DistrictStarsPanel.AutoSize = True

        Me.DistrictStarsPanel.Controls.Add(Me.StarLimitComboBox)

        Me.DistrictStarsPanel.Controls.Add(Me.HighestRecipeLabel)

        Me.DistrictStarsPanel.Controls.Add(Me.GourmetCheckBox)

        Me.DistrictStarsPanel.Controls.Add(Me.DistrictComboBox)

        Me.DistrictStarsPanel.Controls.Add(Me.DistrictLabelLabel)

        Me.DistrictStarsPanel.Location = New System.Drawing.Point(0, 0)

        Me.DistrictStarsPanel.Margin = New System.Windows.Forms.Padding(0)

        Me.DistrictStarsPanel.Name = "DistrictStarsPanel"

        Me.DistrictStarsPanel.Size = New System.Drawing.Size(496, 24)

        Me.DistrictStarsPanel.TabIndex = 3

        '

        'StarLimitComboBox

        '

        Me.StarLimitComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList

        Me.StarLimitComboBox.FormattingEnabled = True

        Me.StarLimitComboBox.Items.AddRange(New Object() {"★☆☆☆☆☆ (1)", "★★☆☆☆☆ (2)", "★★★☆☆☆ (3)", "★★★★☆☆ (4)", "★★★★★☆ (5)", "★★★★★★ (6)"})

        Me.StarLimitComboBox.Location = New System.Drawing.Point(235, 2)

        Me.StarLimitComboBox.Margin = New System.Windows.Forms.Padding(2)

        Me.StarLimitComboBox.Name = "StarLimitComboBox"

        Me.StarLimitComboBox.Size = New System.Drawing.Size(195, 20)

        Me.StarLimitComboBox.TabIndex = 5

        '

        'HighestRecipeLabel

        '

        Me.HighestRecipeLabel.AutoSize = True

        Me.HighestRecipeLabel.Location = New System.Drawing.Point(161, 5)

        Me.HighestRecipeLabel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)

        Me.HighestRecipeLabel.Name = "HighestRecipeLabel"

        Me.HighestRecipeLabel.Size = New System.Drawing.Size(70, 12)

        Me.HighestRecipeLabel.TabIndex = 6

        Me.HighestRecipeLabel.Text = "レシピのランク:"

        '

        'GourmetCheckBox

        '

        Me.GourmetCheckBox.AutoSize = True

        Me.GourmetCheckBox.Location = New System.Drawing.Point(434, 6)

        Me.GourmetCheckBox.Margin = New System.Windows.Forms.Padding(2)

        Me.GourmetCheckBox.Name = "GourmetCheckBox"

        Me.GourmetCheckBox.Size = New System.Drawing.Size(60, 16)

        Me.GourmetCheckBox.TabIndex = 4

        Me.GourmetCheckBox.Text = "美食王"

        Me.GourmetCheckBox.UseVisualStyleBackColor = True

        '

        'DistrictComboBox

        '

        Me.DistrictComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList

        Me.DistrictComboBox.FormattingEnabled = True

        Me.DistrictComboBox.Items.AddRange(New Object() {"南区", "東区", "西区", "北区", "中央区", "街外れ"})

        Me.DistrictComboBox.Location = New System.Drawing.Point(50, 2)

        Me.DistrictComboBox.Margin = New System.Windows.Forms.Padding(2)

        Me.DistrictComboBox.Name = "DistrictComboBox"

        Me.DistrictComboBox.Size = New System.Drawing.Size(92, 20)

        Me.DistrictComboBox.TabIndex = 1

        '

        'DistrictLabelLabel

        '

        Me.DistrictLabelLabel.AutoSize = True

        Me.DistrictLabelLabel.Location = New System.Drawing.Point(2, 5)

        Me.DistrictLabelLabel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)

        Me.DistrictLabelLabel.Name = "DistrictLabelLabel"

        Me.DistrictLabelLabel.Size = New System.Drawing.Size(31, 12)

        Me.DistrictLabelLabel.TabIndex = 2

        Me.DistrictLabelLabel.Text = "地区:"

        '

        'InnerPanel

        '

        Me.InnerPanel.AutoSize = True

        Me.InnerPanel.Dock = System.Windows.Forms.DockStyle.Top

        Me.InnerPanel.Location = New System.Drawing.Point(0, 24)

        Me.InnerPanel.Margin = New System.Windows.Forms.Padding(0)

        Me.InnerPanel.Name = "InnerPanel"

        Me.InnerPanel.Size = New System.Drawing.Size(496, 0)

        Me.InnerPanel.TabIndex = 0

        '

        'BottomPanel

        '

        Me.BottomPanel.Controls.Add(Me.ButtonPanel)

        Me.BottomPanel.Controls.Add(Me.FoodResultLabel)

        Me.BottomPanel.Controls.Add(Me.FoodDisplayLabel)

        Me.BottomPanel.Controls.Add(Me.CalculateButton)

        Me.BottomPanel.Controls.Add(Me.ResetButton)

        Me.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom

        Me.BottomPanel.Location = New System.Drawing.Point(2, 114)

        Me.BottomPanel.Margin = New System.Windows.Forms.Padding(0)

        Me.BottomPanel.Name = "BottomPanel"

        Me.BottomPanel.Size = New System.Drawing.Size(797, 102)

        Me.BottomPanel.TabIndex = 3

        '

        'ButtonPanel

        '

        Me.ButtonPanel.Controls.Add(Me.PrevButton)

        Me.ButtonPanel.Controls.Add(Me.NextButton)

        Me.ButtonPanel.Dock = System.Windows.Forms.DockStyle.Right

        Me.ButtonPanel.Location = New System.Drawing.Point(744, 0)

        Me.ButtonPanel.Margin = New System.Windows.Forms.Padding(2)

        Me.ButtonPanel.Name = "ButtonPanel"

        Me.ButtonPanel.Size = New System.Drawing.Size(53, 102)

        Me.ButtonPanel.TabIndex = 7

        '

        'PrevButton

        '

        Me.PrevButton.Dock = System.Windows.Forms.DockStyle.Top

        Me.PrevButton.Location = New System.Drawing.Point(0, 0)

        Me.PrevButton.Margin = New System.Windows.Forms.Padding(2)

        Me.PrevButton.Name = "PrevButton"

        Me.PrevButton.Size = New System.Drawing.Size(53, 38)

        Me.PrevButton.TabIndex = 6

        Me.PrevButton.Text = "前へ"

        Me.PrevButton.UseVisualStyleBackColor = True

        '

        'NextButton

        '

        Me.NextButton.Dock = System.Windows.Forms.DockStyle.Bottom

        Me.NextButton.Location = New System.Drawing.Point(0, 67)

        Me.NextButton.Margin = New System.Windows.Forms.Padding(2)

        Me.NextButton.Name = "NextButton"

        Me.NextButton.Size = New System.Drawing.Size(53, 35)

        Me.NextButton.TabIndex = 5

        Me.NextButton.Text = "次へ"

        Me.NextButton.UseVisualStyleBackColor = True

        '

        'FoodResultLabel

        '

        Me.FoodResultLabel.AutoSize = True

        Me.FoodResultLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

        Me.FoodResultLabel.ForeColor = System.Drawing.Color.DarkGreen

        Me.FoodResultLabel.Location = New System.Drawing.Point(201, 16)

        Me.FoodResultLabel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)

        Me.FoodResultLabel.Name = "FoodResultLabel"

        Me.FoodResultLabel.Size = New System.Drawing.Size(104, 40)

        Me.FoodResultLabel.TabIndex = 4

        Me.FoodResultLabel.Text = "Example Text" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Line 2" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)

        '

        'FoodDisplayLabel

        '

        Me.FoodDisplayLabel.AutoSize = True

        Me.FoodDisplayLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

        Me.FoodDisplayLabel.Location = New System.Drawing.Point(144, 16)

        Me.FoodDisplayLabel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)

        Me.FoodDisplayLabel.Name = "FoodDisplayLabel"

        Me.FoodDisplayLabel.Size = New System.Drawing.Size(53, 24)

        Me.FoodDisplayLabel.TabIndex = 3

        Me.FoodDisplayLabel.Text = "料理:"

        '

        'CalculateButton

        '

        Me.CalculateButton.Location = New System.Drawing.Point(2, 3)

        Me.CalculateButton.Margin = New System.Windows.Forms.Padding(2)

        Me.CalculateButton.Name = "CalculateButton"

        Me.CalculateButton.Size = New System.Drawing.Size(124, 32)

        Me.CalculateButton.TabIndex = 2

        Me.CalculateButton.Text = "計算する"

        Me.CalculateButton.UseVisualStyleBackColor = True

        '

        'ResetButton

        '

        Me.ResetButton.Location = New System.Drawing.Point(2, 67)

        Me.ResetButton.Margin = New System.Windows.Forms.Padding(2)

        Me.ResetButton.Name = "ResetButton"

        Me.ResetButton.Size = New System.Drawing.Size(124, 32)

        Me.ResetButton.TabIndex = 1

        Me.ResetButton.Text = "リセット"

        Me.ResetButton.UseVisualStyleBackColor = True

        '

        'WardGroup

        '

        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)

        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font

        Me.Controls.Add(Me.MainGroupBox)

        Me.Margin = New System.Windows.Forms.Padding(0)

        Me.Name = "WardGroup"

        Me.Size = New System.Drawing.Size(801, 218)

        Me.MainGroupBox.ResumeLayout(False)

        Me.GroupFlowLayoutPanel.ResumeLayout(False)

        Me.GroupFlowLayoutPanel.PerformLayout()

        Me.DistrictStarsPanel.ResumeLayout(False)

        Me.DistrictStarsPanel.PerformLayout()

        Me.BottomPanel.ResumeLayout(False)

        Me.BottomPanel.PerformLayout()

        Me.ButtonPanel.ResumeLayout(False)

        Me.ResumeLayout(False)



    End Sub

    Friend WithEvents MainGroupBox As System.Windows.Forms.GroupBox
   Friend WithEvents InnerPanel As System.Windows.Forms.FlowLayoutPanel
   Friend WithEvents ResetButton As System.Windows.Forms.Button
   Friend WithEvents CalculateButton As System.Windows.Forms.Button
   Friend WithEvents BottomPanel As System.Windows.Forms.Panel
   Friend WithEvents FoodDisplayLabel As System.Windows.Forms.Label
   Friend WithEvents FoodResultLabel As System.Windows.Forms.Label
   Friend WithEvents GroupFlowLayoutPanel As System.Windows.Forms.FlowLayoutPanel
   Friend WithEvents DistrictComboBox As System.Windows.Forms.ComboBox
   Friend WithEvents DistrictStarsPanel As System.Windows.Forms.Panel
   Friend WithEvents DistrictLabelLabel As System.Windows.Forms.Label
   Friend WithEvents GourmetCheckBox As System.Windows.Forms.CheckBox
   Friend WithEvents StarLimitComboBox As System.Windows.Forms.ComboBox
   Friend WithEvents HighestRecipeLabel As System.Windows.Forms.Label
   Friend WithEvents PrevButton As System.Windows.Forms.Button
   Friend WithEvents NextButton As System.Windows.Forms.Button
   Friend WithEvents ButtonPanel As System.Windows.Forms.Panel

End Class
