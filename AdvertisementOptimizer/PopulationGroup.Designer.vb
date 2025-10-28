<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PopulationGroup
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
        Me.PopulationComboBox = New System.Windows.Forms.ComboBox()
        Me.GroupLabel = New System.Windows.Forms.Label()
        Me.CustomerComboBox = New System.Windows.Forms.ComboBox()
        Me.ResetButton = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'PopulationComboBox
        '
        Me.PopulationComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.PopulationComboBox.FormattingEnabled = True
        Me.PopulationComboBox.Location = New System.Drawing.Point(0, 38)
        Me.PopulationComboBox.Margin = New System.Windows.Forms.Padding(2)
        Me.PopulationComboBox.Name = "PopulationComboBox"
        Me.PopulationComboBox.Size = New System.Drawing.Size(183, 20)
        Me.PopulationComboBox.TabIndex = 6
        '
        'GroupLabel
        '
        Me.GroupLabel.AutoSize = True
        Me.GroupLabel.Location = New System.Drawing.Point(0, 1)
        Me.GroupLabel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.GroupLabel.Name = "GroupLabel"
        Me.GroupLabel.Size = New System.Drawing.Size(43, 12)
        Me.GroupLabel.TabIndex = 5
        Me.GroupLabel.Text = "グループ"
        '
        'CustomerComboBox
        '
        Me.CustomerComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CustomerComboBox.FormattingEnabled = True
        Me.CustomerComboBox.Location = New System.Drawing.Point(0, 16)
        Me.CustomerComboBox.Margin = New System.Windows.Forms.Padding(2)
        Me.CustomerComboBox.Name = "CustomerComboBox"
        Me.CustomerComboBox.Size = New System.Drawing.Size(183, 20)
        Me.CustomerComboBox.TabIndex = 4
        '
        'ResetButton
        '
        Me.ResetButton.Location = New System.Drawing.Point(1, 62)
        Me.ResetButton.Margin = New System.Windows.Forms.Padding(2)
        Me.ResetButton.Name = "ResetButton"
        Me.ResetButton.Size = New System.Drawing.Size(182, 34)
        Me.ResetButton.TabIndex = 8
        Me.ResetButton.Text = "リセット"
        Me.ResetButton.UseVisualStyleBackColor = True
        '
        'PopulationGroup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.ResetButton)
        Me.Controls.Add(Me.PopulationComboBox)
        Me.Controls.Add(Me.GroupLabel)
        Me.Controls.Add(Me.CustomerComboBox)
        Me.Margin = New System.Windows.Forms.Padding(0)
        Me.Name = "PopulationGroup"
        Me.Size = New System.Drawing.Size(185, 98)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PopulationComboBox As System.Windows.Forms.ComboBox
   Friend WithEvents GroupLabel As System.Windows.Forms.Label
   Friend WithEvents CustomerComboBox As System.Windows.Forms.ComboBox
   Friend WithEvents ResetButton As System.Windows.Forms.Button

End Class
