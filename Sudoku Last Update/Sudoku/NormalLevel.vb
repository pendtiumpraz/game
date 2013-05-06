Public Class NormalLevel

    Public box As Integer = 9
    Dim Jam, Menit, Detik, mDetik As Integer
    Dim hard As Integer = 0
    Dim hardini As Integer = 0
    Dim normal As Integer = 0
    Dim normalini As Integer = 0
    Dim mulai As Integer = 1
    Dim check As Integer = 0
    Dim backtracking As Boolean = False
    Dim Bold As Font = New Font(Me.Font.FontFamily, Me.FontHeight + 17, FontStyle.Bold)

    '----------------------------------------------------
    '                  FUNGSI RANDOM!
    '----------------------------------------------------

    Dim i As Integer
    Dim j As Integer

    Public Function GetRandom(ByVal Min As Integer, ByVal Max As Integer) As Integer
        Dim Generator As System.Random = New System.Random()
        Return Generator.Next(Min, Max)
    End Function

    Class sudoku_textbox
        Inherits TextBox
        Protected Overrides Sub OnKeyPress(ByVal e As System.Windows.Forms.KeyPressEventArgs)
            If Char.IsDigit(e.KeyChar) Or e.KeyChar = " " Or e.KeyChar = ControlChars.Back Then
                e.Handled = False
            Else
                e.Handled = True
            End If
            If e.KeyChar = " " Or e.KeyChar = "0" Then
                e.KeyChar = ControlChars.Back
            End If

            If HardLevel.box = 4 Or NormalLevel.box = 4 Then
                If e.KeyChar = "5" Or e.KeyChar = "6" Or e.KeyChar = "7" Or e.KeyChar = "8" Or e.KeyChar = "9" Then
                    e.KeyChar = ControlChars.Back
                End If
            End If
        End Sub
    End Class


    Dim cell(0 To box - 1, 0 To box - 1) As sudoku_textbox
    Dim grid(0 To box - 1, 0 To box - 1) As String



    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Button2.Enabled = True
        ButtonSolve.Enabled = True
        If HardLevel.RadioButton2.Checked And mulai = 1 Then
            Label1.Text = "00 : 00 : 00,00"
            Timer1.Enabled = True
            Button1.Visible = True
            Button2.Visible = False
            Button3.Visible = False
            ButtonSolve.Visible = False
            Button4.Visible = True
            RadioButton2.Visible = False
            RadioButton3.Visible = False
            box = 4
            normal = 1
            normalini = 1
            hard = 0
            hardini = 0

            normal_cell()
            random_normal()
            mulai = 0

        ElseIf HardLevel.RadioButton3.Checked And mulai = 1 Then
            Label1.Text = "00 : 00 : 00,00"
            Timer1.Enabled = True
            Button1.Visible = True
            Button2.Visible = True
            Button3.Visible = False
            ButtonSolve.Visible = True
            Button4.Visible = True
            RadioButton2.Visible = False
            RadioButton3.Visible = False
            box = 9
            normal = 0
            normalini = 0
            hard = 1
            hardini = 1

            hard_cell()
            random_hard()
            mulai = 0

        ElseIf hard = 1 And hardini = 1 And box = 9 Then
            'Label1.Text = "00 : 00 : 00,00"
            Me.Hide()
            HardLevel.box = 4
            HardLevel.Show()

        Else
            Label1.Text = "00 : 00 : 00,00"
            Timer1.Enabled = True
            Button1.Visible = True
            Button2.Visible = True
            Button3.Visible = False
            ButtonSolve.Visible = True
            Button4.Visible = True
            RadioButton2.Visible = False
            RadioButton3.Visible = False
            box = 4
            normal = 1
            normalini = 1
            hard = 0
            hardini = 0


            For x As Integer = 0 To box - 1
                For y As Integer = 0 To box - 1
                    cell(x, y).ReadOnly = False
                    cell(x, y).BackColor = Color.White
                    cell(x, y).Text = ""
                    cell(x, y).Show()
                Next
            Next

            random_normal()
        End If

    End Sub

    '-----------------------------------------------------------------------------------------------------------
    '                                         AREA NORMAL LEVEL COMMAND
    '-----------------------------------------------------------------------------------------------------------

    '-----------------------------
    '      Normal Random
    '-----------------------------
    Private Sub random_normal()
        For x As Integer = 0 To 3
            If x = 0 Then
                i = 0
                j = GetRandom(1, 2)
                cell(i, j).Text = 1
            ElseIf x = 1 Then
                i = 1
                j = GetRandom(2, 3)
                cell(i, j).Text = 2
            ElseIf x = 2 Then
                i = 2
                j = GetRandom(3, 4)
                cell(i, j).Text = 3
            ElseIf x = 3 Then
                i = 3
                j = GetRandom(1, 3)
                cell(i, j).Text = 4
            End If
        Next
        HardLevel.score = 0
        Label3.Text = HardLevel.score
    End Sub


    '***************************
    '      Cell Normal 4x4
    '***************************
    Private Sub normal_cell()
        Dim xxtra As Integer
        Dim yxtra As Integer

        For x As Integer = 0 To box - 1
            For y As Integer = 0 To box - 1
                cell(x, y) = New sudoku_textbox
                cell(x, y).AutoSize = False
                cell(x, y).ReadOnly = False
                cell(x, y).Font = Bold
                cell(x, y).Text = ""
                cell(x, y).Width = 50
                cell(x, y).Height = 50
                cell(x, y).MaxLength = 1
                cell(x, y).TextAlign = HorizontalAlignment.Center
                If normal = 0 And normal = 0 Then
                    cell(x, y).Visible = False
                Else
                    cell(x, y).Visible = True
                End If


                xxtra = 0
                yxtra = 0
                If x > 1 Then
                    xxtra = 4
                End If
                If x > 3 Then
                    xxtra = 8
                End If
                If y > 1 Then
                    yxtra = 4
                End If
                If y > 3 Then
                    yxtra = 8
                End If
                cell(x, y).Location = New Point(150 + x * 50 + xxtra, 150 + 50 * y + yxtra)

                'End If
                'cell(x, y).Location = New Point(x * 20, y * 20)
                Me.Controls.Add(cell(x, y))
                AddHandler cell(x, y).TextChanged, AddressOf cell_changed_normal
            Next

        Next

    End Sub


    '***************************
    '   Ganti isi Cell Normal 
    '***************************
    Private Sub cell_changed_normal()
        If backtracking Then Return
        For x As Integer = 0 To box - 1
            For y As Integer = 0 To box - 1
                grid(x, y) = cell(x, y).Text
                cell(x, y).ForeColor = Color.Black
                cell(x, y).BackColor = Color.White
            Next
        Next
        HardLevel.score = HardLevel.score + 2
        Label3.Text = HardLevel.score

        For x = 0 To box - 1
            For y = 0 To box - 1
                If check_rows_normal(x, y) Then
                    If check_columns_normal(x, y) Then
                        If Not check_box_normal(x, y) Then
                            cell(x, y).ForeColor = Color.Red
                            cell(x, y).BackColor = Color.Yellow
                            HardLevel.score = HardLevel.score - 2
                            Label3.Text = HardLevel.score
                        End If
                    Else
                        cell(x, y).ForeColor = Color.Red
                        cell(x, y).BackColor = Color.Yellow
                        HardLevel.score = HardLevel.score - 2
                        Label3.Text = HardLevel.score
                    End If
                Else
                    cell(x, y).ForeColor = Color.Red
                    cell(x, y).BackColor = Color.Yellow
                    HardLevel.score = HardLevel.score - 2
                    Label3.Text = HardLevel.score
                End If
            Next
        Next
    End Sub

    '***************************
    '   Cek Baris Cell Normal 
    '***************************
    Function check_rows_normal(ByVal xsender, ByVal ysender) As Boolean
        Dim noclash As Boolean = True
        For x As Integer = 0 To box - 1
            If grid(x, ysender) <> "" Then
                If x <> xsender Then
                    If grid(x, ysender) = grid(xsender, ysender) Then
                        noclash = False
                    End If
                End If
            End If
        Next
        Return noclash
    End Function

    '***************************
    '  Cek Kolom Cell Normal 
    '***************************
    Function check_columns_normal(ByVal xsender, ByVal ysender) As Boolean
        Dim noclash As Boolean = True
        For y As Integer = 0 To box - 1
            If grid(xsender, y) <> "" Then
                If y <> ysender Then
                    If grid(xsender, y) = grid(xsender, ysender) Then
                        noclash = False
                    End If
                End If
            End If
        Next
        Return noclash
    End Function

    '***************************
    '   Cek Box Cell Normal 
    '***************************
    Function check_box_normal(ByVal xsender, ByVal ysender) As Boolean
        Dim noclash As Boolean = True
        Dim xstart As Integer
        Dim ystart As Integer
        If xsender < 2 Then
            xstart = 0
        ElseIf xsender < 4 Then
            xstart = 2

        End If

        If ysender < 2 Then
            ystart = 0
        ElseIf ysender < 4 Then
            ystart = 2
        End If

        For y As Integer = ystart To (ystart + 1)
            For x As Integer = xstart To (xstart + 1)
                If grid(x, y) <> "" Then
                    If Not (x = xsender And y = ysender) Then
                        If grid(x, y) = grid(xsender, ysender) Then
                            noclash = False
                        End If

                    End If

                End If
            Next
        Next
        Return noclash
    End Function

    '***************************
    '    Problem Solve Normal
    '***************************
    Function backtrack_normal(ByVal x As Integer, ByVal y As Integer) As Boolean

        Dim numbers As Integer = 1
        If grid(x, y) = "" Then
            Do
                grid(x, y) = CStr(numbers)
                If check_rows_normal(x, y) Then
                    If check_columns_normal(x, y) Then
                        If check_box_normal(x, y) Then
                            y = y + 1
                            If y = box Then
                                y = 0
                                x = x + 1
                                If x = box Then Return False

                            End If
                            If backtrack_normal(x, y) Then Return True
                            y = y - 1
                            If y < 0 Then
                                y = box - 1
                                x = x - 1
                            End If
                        End If
                    End If
                End If
                numbers = numbers + 1
            Loop Until numbers = box + 1
            grid(x, y) = ""
            Return False
        Else
            y = y + 1
            If y = box Then
                y = 0
                x = x + 1
                If x = box Then Return True

            End If
            Return backtrack_normal(x, y)
        End If
        ' End If
    End Function



    '-----------------------------------------------------------------------------------------------------------
    '                                         AREA HARD LEVEL COMMAND
    '-----------------------------------------------------------------------------------------------------------

    '-----------------------------
    '      Normal Random
    '-----------------------------
    Private Sub random_hard()
        For x As Integer = 0 To 8
            '----------------------------------------------
            '        Random Baris 1 - 3 | ( 0 - 2 )
            '----------------------------------------------
            If x = 0 Then
                i = GetRandom(0, 2)
                j = GetRandom(0, 2)
                cell(i, j).Text = GetRandom(1, 2)
                cell(i, j).ReadOnly = True
                cell(i, j).BackColor = Color.Blue
                i = GetRandom(0, 2)
                j = GetRandom(0, 2)
                cell(i, j).Text = GetRandom(3, 4)
                cell(i, j).ReadOnly = True
                cell(i, j).BackColor = Color.Blue
            ElseIf x = 1 Then
                i = GetRandom(0, 2)
                j = GetRandom(3, 5)
                cell(i, j).Text = GetRandom(5, 6)
                cell(i, j).ReadOnly = True
                cell(i, j).BackColor = Color.Blue
                i = GetRandom(0, 2)
                j = GetRandom(3, 5)
                cell(i, j).Text = GetRandom(7, 8)
                cell(i, j).ReadOnly = True
                cell(i, j).BackColor = Color.Blue
            ElseIf x = 2 Then
                i = GetRandom(0, 2)
                j = GetRandom(6, 8)
                cell(i, j).Text = GetRandom(2, 3)
                cell(i, j).ReadOnly = True
                cell(i, j).BackColor = Color.Blue
                i = GetRandom(0, 2)
                j = GetRandom(6, 8)
                cell(i, j).Text = GetRandom(4, 5)
                cell(i, j).ReadOnly = True
                cell(i, j).BackColor = Color.Blue

                '----------------------------------------------
                '      Random Baris 4 - 6 | ( 3 - 5 )
                '----------------------------------------------
            ElseIf x = 3 Then
                i = GetRandom(3, 5)
                j = GetRandom(0, 2)
                cell(i, j).Text = GetRandom(5, 6)
                cell(i, j).ReadOnly = True
                cell(i, j).BackColor = Color.Blue
                i = GetRandom(3, 5)
                j = GetRandom(0, 2)
                cell(i, j).Text = GetRandom(7, 8)
                cell(i, j).ReadOnly = True
                cell(i, j).BackColor = Color.Blue
            ElseIf x = 4 Then
                i = GetRandom(3, 5)
                j = GetRandom(3, 5)
                cell(i, j).Text = GetRandom(2, 3)
                cell(i, j).ReadOnly = True
                cell(i, j).BackColor = Color.Blue
                i = GetRandom(3, 5)
                j = GetRandom(3, 5)
                cell(i, j).Text = GetRandom(4, 5)
                cell(i, j).ReadOnly = True
                cell(i, j).BackColor = Color.Blue
            ElseIf x = 5 Then
                i = GetRandom(3, 5)
                j = GetRandom(6, 8)
                cell(i, j).Text = GetRandom(1, 2)
                cell(i, j).ReadOnly = True
                cell(i, j).BackColor = Color.Blue
                i = GetRandom(3, 5)
                j = GetRandom(6, 8)
                cell(i, j).Text = GetRandom(3, 4)
                cell(i, j).ReadOnly = True
                cell(i, j).BackColor = Color.Blue

                '----------------------------------------------
                '      Random Baris 7 - 9 | ( 6 - 8 )
                '----------------------------------------------
            ElseIf x = 6 Then
                i = GetRandom(6, 8)
                j = GetRandom(0, 2)
                cell(i, j).Text = GetRandom(2, 3)
                cell(i, j).ReadOnly = True
                i = GetRandom(6, 8)
                j = GetRandom(0, 2)
                cell(i, j).Text = GetRandom(4, 5)
                cell(i, j).ReadOnly = True
            ElseIf x = 7 Then
                i = GetRandom(6, 8)
                j = GetRandom(3, 5)
                cell(i, j).Text = GetRandom(1, 2)
                cell(i, j).ReadOnly = True
                i = GetRandom(6, 8)
                j = GetRandom(3, 5)
                cell(i, j).Text = GetRandom(3, 4)
                cell(i, j).ReadOnly = True
            ElseIf x = 8 Then
                i = GetRandom(6, 8)
                j = GetRandom(6, 8)
                cell(i, j).Text = GetRandom(6, 7)
                cell(i, j).ReadOnly = True
                i = GetRandom(6, 8)
                j = GetRandom(6, 8)
                cell(i, j).Text = GetRandom(8, 9)
                cell(i, j).ReadOnly = True
            End If
        Next
        HardLevel.score = 0
        Label3.Text = HardLevel.score
    End Sub

    '***************************
    '      Cell Hard 9x9
    '***************************

    Private Sub hard_cell()
        Dim xxtra As Integer
        Dim yxtra As Integer

        For x As Integer = 0 To box - 1
            For y As Integer = 0 To box - 1
                cell(x, y) = New sudoku_textbox
                cell(x, y).AutoSize = False
                cell(x, y).ReadOnly = False
                cell(x, y).Font = Bold
                cell(x, y).Text = ""
                cell(x, y).Width = 50
                cell(x, y).Height = 50
                cell(x, y).MaxLength = 1
                cell(x, y).TextAlign = HorizontalAlignment.Center
                If normal = 0 And hard = 0 Then
                    cell(x, y).Visible = False
                Else
                    cell(x, y).Visible = True
                End If


                xxtra = 0
                yxtra = 0
                If x > 2 Then
                    xxtra = 4
                End If
                If x > 5 Then
                    xxtra = 8
                End If
                If y > 2 Then
                    yxtra = 4
                End If
                If y > 5 Then
                    yxtra = 8
                End If
                cell(x, y).Location = New Point(75 + x * 50 + xxtra, 75 + 50 * y + yxtra)

                'End If
                'cell(x, y).Location = New Point(x * 20, y * 20)
                Me.Controls.Add(cell(x, y))
                AddHandler cell(x, y).TextChanged, AddressOf cell_changed_hard
            Next

        Next

    End Sub


    '***************************
    '   Ganti isi Cell Hard 
    '***************************
    Private Sub cell_changed_hard()
        If backtracking Then Return
        For x As Integer = 0 To box - 1
            For y As Integer = 0 To box - 1
                grid(x, y) = cell(x, y).Text
                cell(x, y).ForeColor = Color.Black
                cell(x, y).BackColor = Color.White
            Next
        Next
        HardLevel.score = HardLevel.score + 2
        Label3.Text = HardLevel.score
        For x = 0 To box - 1
            For y = 0 To box - 1
                If check_rows_hard(x, y) Then
                    If check_columns_hard(x, y) Then
                        If Not check_box_hard(x, y) Then
                            cell(x, y).ForeColor = Color.Red
                            cell(x, y).BackColor = Color.Yellow
                            HardLevel.score = HardLevel.score - 2
                            Label3.Text = HardLevel.score
                        End If
                    Else
                        cell(x, y).ForeColor = Color.Red
                        cell(x, y).BackColor = Color.Yellow
                        HardLevel.score = HardLevel.score - 2
                        Label3.Text = HardLevel.score
                    End If
                Else
                    cell(x, y).ForeColor = Color.Red
                    cell(x, y).BackColor = Color.Yellow
                    HardLevel.score = HardLevel.score - 2
                    Label3.Text = HardLevel.score
                End If
            Next
        Next
    End Sub

    '***************************
    '   Cek Baris Cell Hard 
    '***************************
    Function check_rows_hard(ByVal xsender, ByVal ysender) As Boolean
        Dim noclash As Boolean = True
        For x As Integer = 0 To box - 1
            If grid(x, ysender) <> "" Then
                If x <> xsender Then
                    If grid(x, ysender) = grid(xsender, ysender) Then
                        noclash = False
                    End If
                End If
            End If
        Next
        Return noclash
    End Function

    '***************************
    '  Cek Kolom Cell Hard 
    '***************************
    Function check_columns_hard(ByVal xsender, ByVal ysender) As Boolean
        Dim noclash As Boolean = True
        For y As Integer = 0 To box - 1
            If grid(xsender, y) <> "" Then
                If y <> ysender Then
                    If grid(xsender, y) = grid(xsender, ysender) Then
                        noclash = False
                    End If
                End If
            End If
        Next
        Return noclash
    End Function

    '***************************
    '   Cek Box Cell Hard 
    '***************************
    Function check_box_hard(ByVal xsender, ByVal ysender) As Boolean
        Dim noclash As Boolean = True
        Dim xstart As Integer
        Dim ystart As Integer
        If xsender < 3 Then
            xstart = 0
        ElseIf xsender < 6 Then
            xstart = 3
        Else
            xstart = 6

        End If
        If ysender < 3 Then
            ystart = 0
        ElseIf ysender < 6 Then
            ystart = 3
        Else
            ystart = 6
        End If
        For y As Integer = ystart To (ystart + 2)
            For x As Integer = xstart To (xstart + 2)
                If grid(x, y) <> "" Then
                    If Not (x = xsender And y = ysender) Then
                        If grid(x, y) = grid(xsender, ysender) Then
                            noclash = False
                        End If

                    End If

                End If
            Next
        Next
        Return noclash
    End Function

    '***************************
    '    Problem Solve Hard
    '***************************
    Function backtrack_hard(ByVal x As Integer, ByVal y As Integer) As Boolean

        Dim numbers As Integer = 1

        If grid(x, y) = "" Then
            Do
                grid(x, y) = CStr(numbers)
                If check_rows_hard(x, y) Then
                    If check_columns_hard(x, y) Then
                        If check_box_hard(x, y) Then
                            y = y + 1
                            If y = box Then
                                y = 0
                                x = x + 1
                                If x = box Then Return False

                            End If
                            If backtrack_hard(x, y) Then Return True
                            y = y - 1
                            If y < 0 Then
                                y = box - 1
                                x = x - 1
                            End If
                        End If
                    End If
                End If
                numbers = numbers + 1
            Loop Until numbers = box + 1
            grid(x, y) = ""
            Return False
        Else
            y = y + 1
            If y = box Then
                y = 0
                x = x + 1
                If x = box Then Return True

            End If
            Return backtrack_hard(x, y)
        End If
        ' End If
    End Function

    ''-----------------------------------------------------------------------------------------------------------
    ''                                          BUTTON - BUTTON 
    ''-----------------------------------------------------------------------------------------------------------

    'Private Sub ButtonClear_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim result As MsgBoxResult = MessageBox.Show("Do you want to clear the puzzle?", "Clear", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
    '    If result = vbYes Then
    '        For x As Integer = 0 To box - 1
    '            For y As Integer = 0 To box - 1
    '                cell(x, y).ReadOnly = False
    '                cell(x, y).BackColor = Color.White
    '                cell(x, y).Text = ""
    '            Next
    '        Next
    '        HardLevel.score = 0
    '        Timer1.Enabled = False
    '        Label1.Text = "00 : 00 : 00,00"
    '        Timer1.Enabled = True
    '    End If

    '    If normal = 1 Then
    '        random_normal()
    '    End If
    '    If hard = 1 Then
    '        random_hard()
    '    End If
    'End Sub

    Private Sub ButtonSolve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonSolve.Click
        backtracking = True
        For x As Integer = 0 To box - 1
            For y As Integer = 0 To box - 1
                If cell(x, y).Text = "" Then
                    HardLevel.score = HardLevel.score - 2
                End If
            Next
        Next

        For x As Integer = 0 To box - 1
            For y As Integer = 0 To box - 1
                grid(x, y) = cell(x, y).Text
            Next
        Next
        backtrack_hard(0, 0)
        backtrack_normal(0, 0)
        'If hard = 1 Then
        For x = 0 To box - 1
            For y = 0 To box - 1
                cell(x, y).Text = grid(x, y)
                cell(x, y).BackColor = Color.Green
            Next
        Next
        'End If

        backtracking = False
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub



    '-----------------------------------------------------------------------------------------------------------
    '                                         BUTTON DAN RADIO BUTTON 
    '-----------------------------------------------------------------------------------------------------------
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Label1.Text = "00 : 00 : 00,00"
        Timer1.Enabled = True
        If RadioButton2.Checked Then
            Button1.Visible = True
            Button2.Visible = False
            Button3.Visible = False
            ButtonSolve.Visible = False
            Button4.Visible = True
            RadioButton2.Visible = False
            RadioButton3.Visible = False
            box = 4
            normal = 1
            hard = 0
            normal_cell()
            random_normal()
        End If

        If RadioButton3.Checked Then
            Button1.Visible = True
            Button2.Visible = True
            Button3.Visible = False
            ButtonSolve.Visible = True
            Button4.Visible = True
            RadioButton2.Visible = False
            RadioButton3.Visible = False
            box = 9
            normal = 0
            hard = 1
            hard_cell()
            random_hard()
        End If

    End Sub

    Private Sub NewGameToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewGameToolStripMenuItem.Click
        Dim result As MsgBoxResult = MessageBox.Show("Do you want to clear the puzzle?", "New Game", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = vbYes Then
            For x As Integer = 0 To box - 1
                For y As Integer = 0 To box - 1
                    cell(x, y).ReadOnly = False
                    cell(x, y).BackColor = Color.White
                    cell(x, y).Text = ""
                    cell(x, y).Hide()
                Next
            Next
            Timer1.Enabled = False
            Label1.Text = "00 : 00 : 00,00"
            Jam = 0
            Menit = 0
            Detik = 0
            mDetik = 0
            ButtonSolve.Enabled = True
            Button2.Enabled = True
        End If

        Button1.Visible = False
        Button2.Visible = False
        Button3.Visible = True
        ButtonSolve.Visible = False
        Button4.Visible = False
        RadioButton2.Visible = True
        RadioButton3.Visible = True


        If normal = 1 Then
            random_normal()
        End If
        If hard = 1 Then
            random_hard()
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If check = 0 Then
            backtracking = True
            For x As Integer = 0 To box - 1
                For y As Integer = 0 To box - 1
                    grid(x, y) = cell(x, y).Text
                Next
            Next
            backtrack_hard(0, 0)
            backtrack_normal(0, 0)
            'If hard = 1 Then
            For x = 0 To box - 1
                For y = 0 To box - 1
                    If cell(x, y).Text = grid(x, y) Then
                        cell(x, y).BackColor = Color.Green
                    Else
                        cell(x, y).BackColor = Color.Red
                    End If
                Next
            Next
            'End If

            backtracking = False
            check = 1

        ElseIf check = 1 Then
            For x = 0 To box - 1
                For y = 0 To box - 1
                    cell(x, y).BackColor = Color.White
                Next
            Next
            check = 0
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        mDetik += 1
        If mDetik = 100 Then
            mDetik = 0

            Detik += 1
            If Detik = 60 Then
                Detik = 0

                Menit += 1
                If Menit = 60 Then
                    Menit = 0
                    Jam += 1
                End If

            End If
        End If

        Label1.Text = Format(Jam, "00") & " : " & Format(Menit, "00") & " : " & Format(Detik, "00") & "," & Format(mDetik, "00")
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        End
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        FormScore.Show()
        Timer1.Enabled = False
        ButtonSolve.Enabled = False
        Button2.Enabled = False
    End Sub
End Class

