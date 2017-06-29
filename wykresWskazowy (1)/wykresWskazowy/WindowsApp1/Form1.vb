Public Class Form1

    Dim graf As Graphics
    Dim freq, R, L, U, cap, XL, XC, Z As Double

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        inductance.Enabled = True
        capityTB.Enabled = True
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click

    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        capityTB.Enabled = False
        inductance.Enabled = True
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        inductance.Enabled = False
        capityTB.Enabled = True
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        graf = Me.CreateGraphics()
    End Sub

    Dim I, skala, cosfi, sinfi, deltax, deltay As Double
    Dim UR, UL, UC As Double
    Dim p As New Pen(Color.Blue, 2)
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        graf = Me.CreateGraphics()
        Dim x, y, x1, y1, x0, y0, xk, yk As Single
        Try
            freq = Double.Parse(freqTB.Text)
            U = Double.Parse(tensityTB.Text)
            R = Double.Parse(resistanceTB.Text)
            If (RadioButton1.Checked) Then
                L = 0
            Else
                L = Double.Parse(inductance.Text)
            End If
            If (RadioButton2.Checked) Then
                cap = 0
            Else
                cap = Double.Parse(capityTB.Text)
            End If
        Catch
            MsgBox("Nie podano przynajmniej jedenej z wartości bądż podano warość nieprawidłową np. literę. Pamiętaj, że separatorem jest przecinek.", MsgBoxStyle.OkOnly, "Błąd")
            Return
        End Try
        If (U = 0) Then
            MsgBox("Napiecie nie może wynosić 0 ", MsgBoxStyle.OkOnly, "Uwaga")
            Return
        End If

        graf.Clear(BackColor)
        XL = 2 * Math.PI * freq * L
        If (cap = 0) Then
            XC = 0
        Else
            If (freq = 0) Then
                MsgBox("Wpisana częśtotliwość to 0 a obwód zawiera kondensator, prąd nie popłynie a całe napięcie odłoży się na kondenatorze ", MsgBoxStyle.OkOnly, "Uwaga")
                R = 0
                XC = 1
            Else
                XC = -1 / (2 * Math.PI * freq * cap)
            End If

        End If
        x0 = 250
        y0 = Me.Height - 100
        xk = 250
        yk = 200
        skala = (y0 - yk) / U
        Z = Math.Sqrt((R * R) + ((XL + XC) * (XL + XC)))
        I = U / Z
        UR = I * R
        UL = I * XL
        UC = I * XC
        p.Color = Color.Blue
        graf.DrawLine(p, x0, y0, xk, yk)
        cosfi = R / Z
        sinfi = (XC + XL) / Z
        Label6.Text = "Cos fi wynosi " & Math.Round(cosfi, 2) & " a sin fi " & Math.Round(sinfi, 2) & ", XL wynosi " & Math.Round(XL, 2) & ", XC wynosi " & Math.Round(XC, 2)
        deltay = cosfi * UR * skala
        deltax = sinfi * UR * skala
        x = x0 + deltax
        y = y0 - deltay
        p.Color = Color.Red
        graf.DrawLine(p, x0, y0, x, y)
        deltay = sinfi * UC * skala
        deltax = cosfi * UC * skala
        x1 = x - deltax
        y1 = y - deltay
        p.Color = Color.Green
        graf.DrawLine(p, x, y, x1, y1)
        deltay = sinfi * UL * skala
        deltax = cosfi * UL * skala
        x1 = x - deltax
        y1 = y - deltay
        p.Color = Color.Purple
        graf.DrawLine(p, x, y, x1, y1)

        x0 = 750
        y0 = Me.Height - 100
        xk = 750
        yk = 100
        skala = (y0 - yk) / U
        p.Color = Color.Blue
        graf.DrawLine(p, x0, y0, xk, yk)
        cosfi = R / Z
        sinfi = (XC + XL) / Z
        Label6.Text = "Cos fi wynosi " & Math.Round(cosfi, 2) & " a sin fi " & Math.Round(sinfi, 2) & ", XL wynosi " & Math.Round(XL, 2) & ", XC wynosi " & Math.Round(XC, 2)
        deltay = cosfi * UR * skala
        deltax = sinfi * UR * skala
        x = x0 + deltax
        y = y0 - deltay
        p.Color = Color.Red
        graf.DrawLine(p, x0, y0, x, y)
        p.Color = Color.Brown

        graf.DrawLine(p, x, y, xk, yk)

    End Sub

End Class
