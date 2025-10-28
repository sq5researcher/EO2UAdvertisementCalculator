<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainWindow
   Inherits System.Windows.Forms.Form

   'Form overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainWindow))
        Me.AboutButton = New System.Windows.Forms.Button()
        Me.WardGroup1 = New AdvertisementOptimizer.WardGroup()
        Me.SuspendLayout()
        '
        'AboutButton
        '
        Me.AboutButton.Location = New System.Drawing.Point(879, 8)
        Me.AboutButton.Margin = New System.Windows.Forms.Padding(2)
        Me.AboutButton.Name = "AboutButton"
        Me.AboutButton.Size = New System.Drawing.Size(62, 19)
        Me.AboutButton.TabIndex = 1
        Me.AboutButton.Text = "About"
        Me.AboutButton.UseVisualStyleBackColor = True
        '
        'WardGroup1
        '
        Me.WardGroup1.GivenWard = AdvertisementOptimizer.WardGroup.Ward.南区
        Me.WardGroup1.Location = New System.Drawing.Point(8, 8)
        Me.WardGroup1.Margin = New System.Windows.Forms.Padding(0)
        Me.WardGroup1.Name = "WardGroup1"
        Me.WardGroup1.Size = New System.Drawing.Size(933, 488)
        Me.WardGroup1.TabIndex = 0
        '
        'MainWindow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(949, 504)
        Me.Controls.Add(Me.AboutButton)
        Me.Controls.Add(Me.WardGroup1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "MainWindow"
        Me.Padding = New System.Windows.Forms.Padding(8)
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.Text = "EO2U Advertisement Calculator"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents WardGroup1 As AdvertisementOptimizer.WardGroup
   Friend WithEvents AboutButton As System.Windows.Forms.Button

End Class
