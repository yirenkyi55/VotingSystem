<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmResetSystem
    Inherits DevComponents.DotNetBar.Office2007Form

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
        Me.components = New System.ComponentModel.Container()
        Me.StyleManager1 = New DevComponents.DotNetBar.StyleManager(Me.components)
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonX1 = New DevComponents.DotNetBar.ButtonX()
        Me.btnVoters = New DevComponents.DotNetBar.ButtonX()
        Me.btnCandidate = New DevComponents.DotNetBar.ButtonX()
        Me.btnSettings = New DevComponents.DotNetBar.ButtonX()
        Me.btnPositions = New DevComponents.DotNetBar.ButtonX()
        Me.btnResetAll = New DevComponents.DotNetBar.ButtonX()
        Me.btnBallots = New DevComponents.DotNetBar.ButtonX()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'StyleManager1
        '
        Me.StyleManager1.ManagerColorTint = System.Drawing.Color.White
        Me.StyleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.VisualStudio2012Light
        Me.StyleManager1.MetroColorParameters = New DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(242, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer)))
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel5.Location = New System.Drawing.Point(506, 30)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(8)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(9, 389)
        Me.Panel5.TabIndex = 38
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel4.Location = New System.Drawing.Point(0, 30)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(8)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(9, 389)
        Me.Panel4.TabIndex = 39
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 419)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(8)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(515, 10)
        Me.Panel2.TabIndex = 40
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.Panel1.Controls.Add(Me.ButtonX1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(8)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(515, 30)
        Me.Panel1.TabIndex = 41
        '
        'ButtonX1
        '
        Me.ButtonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.ButtonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueOrb
        Me.ButtonX1.Location = New System.Drawing.Point(470, 4)
        Me.ButtonX1.Name = "ButtonX1"
        Me.ButtonX1.Size = New System.Drawing.Size(37, 22)
        Me.ButtonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.ButtonX1.SymbolSet = DevComponents.DotNetBar.eSymbolSet.Material
        Me.ButtonX1.TabIndex = 0
        Me.ButtonX1.Text = "X"
        '
        'btnVoters
        '
        Me.btnVoters.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnVoters.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueOrb
        Me.btnVoters.Location = New System.Drawing.Point(144, 33)
        Me.btnVoters.Name = "btnVoters"
        Me.btnVoters.Size = New System.Drawing.Size(266, 46)
        Me.btnVoters.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnVoters.Symbol = "57384"
        Me.btnVoters.SymbolSet = DevComponents.DotNetBar.eSymbolSet.Material
        Me.btnVoters.TabIndex = 0
        Me.btnVoters.Text = "Reset Voters"
        '
        'btnCandidate
        '
        Me.btnCandidate.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnCandidate.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueOrb
        Me.btnCandidate.Location = New System.Drawing.Point(144, 103)
        Me.btnCandidate.Name = "btnCandidate"
        Me.btnCandidate.Size = New System.Drawing.Size(266, 46)
        Me.btnCandidate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnCandidate.Symbol = "57384"
        Me.btnCandidate.SymbolSet = DevComponents.DotNetBar.eSymbolSet.Material
        Me.btnCandidate.TabIndex = 1
        Me.btnCandidate.Text = "Reset Candidates"
        '
        'btnSettings
        '
        Me.btnSettings.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnSettings.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueOrb
        Me.btnSettings.Location = New System.Drawing.Point(144, 173)
        Me.btnSettings.Name = "btnSettings"
        Me.btnSettings.Size = New System.Drawing.Size(266, 46)
        Me.btnSettings.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnSettings.Symbol = "57384"
        Me.btnSettings.SymbolSet = DevComponents.DotNetBar.eSymbolSet.Material
        Me.btnSettings.TabIndex = 2
        Me.btnSettings.Text = "Reset Settings"
        '
        'btnPositions
        '
        Me.btnPositions.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnPositions.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueOrb
        Me.btnPositions.Location = New System.Drawing.Point(144, 243)
        Me.btnPositions.Name = "btnPositions"
        Me.btnPositions.Size = New System.Drawing.Size(266, 46)
        Me.btnPositions.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnPositions.Symbol = "57384"
        Me.btnPositions.SymbolSet = DevComponents.DotNetBar.eSymbolSet.Material
        Me.btnPositions.TabIndex = 3
        Me.btnPositions.Text = "Reset Positions"
        '
        'btnResetAll
        '
        Me.btnResetAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnResetAll.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueOrb
        Me.btnResetAll.Location = New System.Drawing.Point(12, 366)
        Me.btnResetAll.Name = "btnResetAll"
        Me.btnResetAll.Size = New System.Drawing.Size(491, 46)
        Me.btnResetAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnResetAll.Symbol = "57384"
        Me.btnResetAll.SymbolSet = DevComponents.DotNetBar.eSymbolSet.Material
        Me.btnResetAll.TabIndex = 5
        Me.btnResetAll.Text = "Reset ALL"
        '
        'btnBallots
        '
        Me.btnBallots.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnBallots.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueOrb
        Me.btnBallots.Location = New System.Drawing.Point(144, 308)
        Me.btnBallots.Name = "btnBallots"
        Me.btnBallots.Size = New System.Drawing.Size(266, 46)
        Me.btnBallots.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnBallots.Symbol = "57384"
        Me.btnBallots.SymbolSet = DevComponents.DotNetBar.eSymbolSet.Material
        Me.btnBallots.TabIndex = 4
        Me.btnBallots.Text = "Reset Ballots"
        '
        'frmResetSystem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(515, 429)
        Me.Controls.Add(Me.btnResetAll)
        Me.Controls.Add(Me.btnBallots)
        Me.Controls.Add(Me.btnPositions)
        Me.Controls.Add(Me.btnSettings)
        Me.Controls.Add(Me.btnCandidate)
        Me.Controls.Add(Me.btnVoters)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Consolas", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmResetSystem"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmResetSystem"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents StyleManager1 As DevComponents.DotNetBar.StyleManager
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel1 As Panel
    Private WithEvents btnVoters As DevComponents.DotNetBar.ButtonX
    Private WithEvents btnCandidate As DevComponents.DotNetBar.ButtonX
    Private WithEvents btnSettings As DevComponents.DotNetBar.ButtonX
    Private WithEvents btnPositions As DevComponents.DotNetBar.ButtonX
    Private WithEvents btnResetAll As DevComponents.DotNetBar.ButtonX
    Private WithEvents btnBallots As DevComponents.DotNetBar.ButtonX
    Private WithEvents ButtonX1 As DevComponents.DotNetBar.ButtonX
End Class
