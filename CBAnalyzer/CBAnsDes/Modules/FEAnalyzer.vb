Module FEAnalyzer
    Dim Tmem As List(Of Member)

    Public Sub TESLA(Optional ByVal c As Boolean = False)
        Dim _NoofDivision As Integer = 2
        Tmem = New List(Of Member)
        FixTempMem(Tmem, _NoofDivision)

        '------Main Variables
        Dim str As String = ""
        Dim b, i As String
        Dim GSM(Tmem.Count * 2 + 1, Tmem.Count * 2 + 1) As Double
        Dim DOFM(Tmem.Count * 2 + 1) As Integer
        Dim FERM(Tmem.Count * 2 + 1) As Double
        Dim Cbound As Integer
        Dim totlength As Double
        Dim ScaTotLength As Integer = beamcreate.coverpic.Width - 200
        For Each itm In Tmem
            totlength = totlength + itm.spanlength
        Next

        For Each itm In Tmem
            Call FixFER(itm)
            Call GStiffness(itm, GSM)
        Next
        Call FixDM(DOFM)
        Call FixFM(FERM)


        '-------------For Check-----------------
        str = str & vbNewLine
        str = str & "----> Degree Of Freedom <----" & vbNewLine & vbNewLine

        For Each itm In Tmem
            i = (Tmem.IndexOf(itm) + 1)
            b = itm.DOF(0) * -1
            str = str & "Member " & i & ":   " & b & vbTab
            b = itm.DOF(1) * -1
            str = str & b & vbTab
            b = itm.DOF(2) * -1
            str = str & b & vbTab
            b = itm.DOF(3) * -1
            str = str & b & vbTab & vbNewLine
        Next
        str = str & vbNewLine & vbNewLine
        str = str & "----> Fixed End Moment <----" & vbNewLine & vbNewLine

        For Each itm In Tmem
            i = (Tmem.IndexOf(itm) + 1)
            b = Math.Round(itm.FER(0), 2)
            str = str & "Member " & i & ":   " & b & vbTab
            b = Math.Round(itm.FER(1), 2)
            str = str & b & vbTab
            b = Math.Round(itm.FER(2), 2)
            str = str & b & vbTab
            b = Math.Round(itm.FER(3), 2)
            str = str & b & vbTab & vbNewLine
        Next
        str = str & vbNewLine & vbNewLine
        str = str & "----> Global Stiffnessmatrix <----" & vbNewLine & vbNewLine

        For p = 0 To ((Tmem.Count * 2) + 1)
            str = str & "|"
            For t = 0 To ((Tmem.Count * 2) + 1)
                b = Math.Round(GSM(p, t), 2)
                str = str & vbTab & b
            Next
            str = str & vbTab & "|" & vbNewLine
        Next
        str = str & vbNewLine & vbNewLine
        str = str & "----> Load Matrix <----" & vbTab & vbTab & "----> DOF Matrix <----" & vbNewLine & vbNewLine
        For p = 0 To ((Tmem.Count * 2) + 1)
            str = str & "|"
            b = Math.Round(FERM(p), 2)
            str = str & vbTab & b & vbTab & "|"
            str = str & vbTab & vbTab & "|"
            b = DOFM(p) * -1
            str = str & vbTab & b
            str = str & vbTab & "|" & vbNewLine
        Next

        '-------Curtailment Zone --------------
        '--------------------------------------
        Call Curtailment(GSM, DOFM, FERM, Cbound)
        '--------------------------------------
        '--------------------------------------

        str = str & vbNewLine & vbNewLine
        str = str & "----> Global Stiffnessmatrix After Curtailment<----" & vbNewLine & vbNewLine
        For p = 0 To (Cbound) - 1
            str = str & "|"
            For t = 0 To (Cbound) - 1
                b = Math.Round(GSM(p, t), 2)
                str = str & vbTab & b
            Next
            str = str & vbTab & "|" & vbNewLine
        Next
        str = str & vbNewLine & vbNewLine
        str = str & "----> Load Matrix After Curtailment <----" & vbNewLine & vbNewLine
        For p = 0 To (Cbound) - 1
            str = str & "|"
            b = Math.Round(FERM(p), 2)
            str = str & vbTab & b
            str = str & vbTab & "|" & vbNewLine
        Next

        '----------- Gauss Elimination Zone -------------
        '------------------------------------------------
        Dim RESM(Cbound - 1) As Double
        Call Gauss(GSM, FERM, RESM, Cbound - 1)
        '------------------------------------------------
        '------------------------------------------------

        str = str & vbNewLine & vbNewLine
        str = str & "----> Result Matrix <----" & vbNewLine & vbNewLine
        For p = 0 To (Cbound) - 1
            str = str & "|"
            b = Math.Round((RESM(p)), 4)
            str = str & vbTab & b
            str = str & vbTab & "|" & vbNewLine
        Next

        '----------------- Welding Zone ----------------
        '-----------------------------------------------
        ReDim GSM((Tmem.Count * 2) + 1, (Tmem.Count * 2) + 1)
        ReDim DOFM((Tmem.Count * 2) + 1)
        ReDim FERM((Tmem.Count * 2) + 1)
        For Each itm In Tmem
            Call FixFER(itm)
            Call GStiffness(itm, GSM)
        Next
        Call FixDM(DOFM)
        Call FixFM(FERM)
        '-----------------Deflection & Rotation at Nodes - Result matrix RESM 
        Dim theta_delta_matrix((Tmem.Count * 2) + 1) As Double

        Call Welding(RESM, FERM, DOFM, theta_delta_matrix)
        Call GMultiplier(GSM, RESM)
        Call loadMINU(RESM, FERM, theta_delta_matrix)
        '-----------------------------------------------
        '-----------------------------------------------

        str = str & vbNewLine & vbNewLine
        str = str & "----> Result Matrix after welding <----" & vbNewLine & vbNewLine
        For p = 0 To ((Tmem.Count * 2) + 1)
            str = str & "|"
            b = Math.Round((RESM(p)), 2)
            str = str & vbTab & b
            str = str & vbTab & "|" & vbNewLine
        Next
        str = str & vbNewLine
        str = str & "----> Reaction in Members <----" & vbNewLine & vbNewLine
        str = str & "                     RA" & vbTab & "MA" & vbTab & "RB" & vbTab & "MB" & vbNewLine
        For Each itm In Tmem
            i = (Tmem.IndexOf(itm) + 1)
            b = Math.Round(itm.RES(0), 2)
            str = str & "Member " & i & ":   " & b & vbTab
            b = Math.Round(itm.RES(1), 2)
            str = str & b & vbTab
            b = Math.Round(itm.RES(2), 2)
            str = str & b & vbTab
            b = Math.Round(itm.RES(3), 2)
            str = str & b & vbTab & vbNewLine
        Next

        Dim _tempI As Integer = 0
        For Each E In mem
            mem(mem.IndexOf(E)).RES(0) = Tmem(_tempI).RES(0)
            mem(mem.IndexOf(E)).RES(1) = Tmem(_tempI).RES(1)
            mem(mem.IndexOf(E)).RES(2) = Tmem((_tempI + _NoofDivision) - 1).RES(2)
            mem(mem.IndexOf(E)).RES(3) = Tmem((_tempI + _NoofDivision) - 1).RES(3)

            mem(mem.IndexOf(E)).DISP(0) = Tmem(_tempI).DISP(0)
            mem(mem.IndexOf(E)).DISP(1) = Tmem(_tempI).DISP(1)
            mem(mem.IndexOf(E)).DISP(2) = Tmem((_tempI + _NoofDivision) - 1).DISP(2)
            mem(mem.IndexOf(E)).DISP(3) = Tmem((_tempI + _NoofDivision) - 1).DISP(3)
            _tempI = _tempI + _NoofDivision
        Next

        str = str & vbNewLine
        str = str & "----> Reaction in Compiled Members <----" & vbNewLine & vbNewLine
        str = str & "                     RA" & vbTab & "MA" & vbTab & "RB" & vbTab & "MB" & vbNewLine
        For Each itm In mem
            i = (mem.IndexOf(itm) + 1)
            b = Math.Round(itm.RES(0), 2)
            str = str & "Member " & i & ":   " & b & vbTab
            b = Math.Round(itm.RES(1), 2)
            str = str & b & vbTab
            b = Math.Round(itm.RES(2), 2)
            str = str & b & vbTab
            b = Math.Round(itm.RES(3), 2)
            str = str & b & vbTab & vbNewLine
        Next


        Dim EquilibriumMember As New Member
        FixEquilibriumMember(EquilibriumMember)

        str = str & vbNewLine
        str = str & "----> Equilibrium Member <----" & vbNewLine & vbNewLine
        str = str & "Location" & vbTab & "Point Load" & vbNewLine
        For Each P In EquilibriumMember.Pload
            str = str & P.pdist & vbTab & P.pload & vbNewLine
        Next
        str = str & vbNewLine
        str = str & "Location" & vbTab & "Moment" & vbNewLine
        For Each M In EquilibriumMember.Mload
            str = str & M.mdist & vbTab & M.mload & vbNewLine
        Next
        str = str & vbNewLine
        str = str & "Location" & vbTab & "UVL" & vbNewLine
        For Each U In EquilibriumMember.Uload
            str = str & U.udist1 & vbTab & U.uload1 & vbNewLine
            str = str & U.udist2 & vbTab & U.uload2 & vbNewLine
        Next
        str = str & vbNewLine
        str = str & "Location" & vbTab & "UVL" & vbNewLine


        '-------------- Member Details --------------
        '--------------------------------------------
        If c = True Then
            Dim a As New memDetails
            a.text1 = str
            a.ShowDialog()
            Exit Sub
        End If

        Call CoordinateCalculator()
    End Sub

#Region "FEA Analysis"
    Private Sub FixTempMem(ByRef Tmem As List(Of Member), ByVal _NoofDivision As Integer)
        '----Temporary member disintegration for better results and non singular solutions for determinate beams
        For Each itm In mem
            Call FixDOF(itm)
        Next
        For Each itm In mem
            'If itm.Pload.Count = 0 And itm.Uload.Count = 0 And itm.Mload.Count = 0 Then
            '    Tmem.Add(itm)
            '    Continue For
            'End If
            Dim _Division As Double = itm.spanlength / _NoofDivision
            For K = 1 To _NoofDivision
                Dim _disintegratedmember As New Member
                _disintegratedmember.spanlength = _Division
                _disintegratedmember.Emodulus = itm.Emodulus
                _disintegratedmember.Inertia = itm.Inertia
                '-----Adding Point Load comes under the disintegrated member
                For Each Pl In itm.Pload
                    If Pl.pdist <= (K * _Division) And Pl.pdist > ((K - 1) * _Division) Then
                        Dim T_Pl As New Member.P
                        T_Pl.pload = Pl.pload
                        T_Pl.pdist = Pl.pdist - ((K - 1) * _Division)
                        _disintegratedmember.Pload.Add(T_Pl)
                    End If
                Next
                '-----Adding UVL comes under the disintegrated member
                For Each Ul In itm.Uload
                    If Ul.udist2 <= (K * _Division) And Ul.udist1 > ((K - 1) * _Division) Then
                        '----Case 1: If the whole load lies inside the disintegration
                        Dim T_Ul As New Member.U
                        T_Ul.uload1 = Ul.uload1
                        T_Ul.uload2 = Ul.uload2
                        T_Ul.udist1 = Ul.udist1 - ((K - 1) * _Division)
                        T_Ul.udist2 = Ul.udist2 - ((K - 1) * _Division)
                        _disintegratedmember.Uload.Add(T_Ul)
                    ElseIf Ul.udist1 < (K * _Division) And Ul.udist1 > ((K - 1) * _Division) Then
                        '----Case 2: location 1 lies inside the disintegration
                        Dim T_Ul As New Member.U
                        T_Ul.uload1 = Ul.uload1
                        If Ul.uload1 <= Ul.uload2 Then
                            T_Ul.uload2 = Ul.uload1 + _
                                            (((Ul.uload2 - Ul.uload1) / (Ul.udist2 - Ul.udist1)) * ((K * _Division) - Ul.udist1))
                        Else
                            T_Ul.uload2 = Ul.uload2 + _
                                            (((Ul.uload1 - Ul.uload2) / (Ul.udist2 - Ul.udist1)) * (Ul.udist2 - (K * _Division)))
                        End If
                        T_Ul.udist1 = Ul.udist1 - ((K - 1) * _Division)
                        T_Ul.udist2 = _Division
                        _disintegratedmember.Uload.Add(T_Ul)
                    ElseIf Ul.udist2 < (K * _Division) And Ul.udist2 > ((K - 1) * _Division) Then
                        '----Case 3: location 2 lies inside the disintegration
                        Dim T_Ul As New Member.U
                        If Ul.uload1 <= Ul.uload2 Then
                            T_Ul.uload1 = Ul.uload1 + _
                                            (((Ul.uload2 - Ul.uload1) / (Ul.udist2 - Ul.udist1)) * (((K - 1) * _Division) - Ul.udist1))
                        Else
                            T_Ul.uload1 = Ul.uload2 + _
                                            (((Ul.uload1 - Ul.uload2) / (Ul.udist2 - Ul.udist1)) * (Ul.udist2 - ((K - 1) * _Division)))
                        End If
                        T_Ul.uload2 = Ul.uload2
                        T_Ul.udist1 = 0
                        T_Ul.udist2 = Ul.udist2 - ((K - 1) * _Division)
                        _disintegratedmember.Uload.Add(T_Ul)
                    ElseIf Ul.udist2 >= (K * _Division) And Ul.udist1 <= ((K - 1) * _Division) Then
                        '----Case 4: disintegration lies inside the load
                        Dim T_Ul As New Member.U
                        If Ul.uload1 <= Ul.uload2 Then
                            T_Ul.uload1 = Ul.uload1 + _
                                            (((Ul.uload2 - Ul.uload1) / (Ul.udist2 - Ul.udist1)) * (((K - 1) * _Division) - Ul.udist1))
                            T_Ul.uload2 = Ul.uload1 + _
                                            (((Ul.uload2 - Ul.uload1) / (Ul.udist2 - Ul.udist1)) * ((K * _Division) - Ul.udist1))
                        Else
                            T_Ul.uload1 = Ul.uload2 + _
                                            (((Ul.uload1 - Ul.uload2) / (Ul.udist2 - Ul.udist1)) * (Ul.udist2 - ((K - 1) * _Division)))
                            T_Ul.uload2 = Ul.uload2 + _
                                            (((Ul.uload1 - Ul.uload2) / (Ul.udist2 - Ul.udist1)) * (Ul.udist2 - (K * _Division)))
                        End If
                        T_Ul.udist1 = 0
                        T_Ul.udist2 = _Division
                        _disintegratedmember.Uload.Add(T_Ul)
                    End If
                Next
                '-----Adding moment comes under the disintegrated member
                For Each Ml In itm.Mload
                    If Ml.mdist <= (K * _Division) And Ml.mdist > ((K - 1) * _Division) Then
                        Dim T_Ml As New Member.M
                        T_Ml.mload = Ml.mload
                        T_Ml.mdist = Ml.mdist - ((K - 1) * _Division)
                        _disintegratedmember.Mload.Add(T_Ml)
                    End If
                Next
                '---Fixing Degree Of Freedom
                If K = 1 Then
                    '----Left end
                    _disintegratedmember.DOF(0) = itm.DOF(0)
                    _disintegratedmember.DOF(1) = itm.DOF(1)
                    _disintegratedmember.DOF(2) = 1
                    _disintegratedmember.DOF(3) = 1
                ElseIf K = _NoofDivision Then
                    '----Right end
                    _disintegratedmember.DOF(0) = 1
                    _disintegratedmember.DOF(1) = 1
                    _disintegratedmember.DOF(2) = itm.DOF(2)
                    _disintegratedmember.DOF(3) = itm.DOF(3)
                Else
                    '-----Inbetween disintegrated members are free to translate and rotate
                    _disintegratedmember.DOF(0) = 1
                    _disintegratedmember.DOF(1) = 1
                    _disintegratedmember.DOF(2) = 1
                    _disintegratedmember.DOF(3) = 1
                End If

                Tmem.Add(_disintegratedmember)
            Next
        Next
    End Sub

    Private Sub FixDOF(ByVal M As Member)
        If mem.IndexOf(M) = 0 And mem.IndexOf(M) = mem.Count - 1 Then
            Select Case ends
                Case 1         'Fixed-Fixed
                    mem(mem.IndexOf(M)).DOF(0) = 0
                    mem(mem.IndexOf(M)).DOF(1) = 0
                    mem(mem.IndexOf(M)).DOF(2) = 0
                    mem(mem.IndexOf(M)).DOF(3) = 0
                    Exit Sub
                Case 2         'Fixed-Free
                    mem(mem.IndexOf(M)).DOF(0) = 0
                    mem(mem.IndexOf(M)).DOF(1) = 0
                    mem(mem.IndexOf(M)).DOF(2) = 1
                    mem(mem.IndexOf(M)).DOF(3) = 1
                    Exit Sub
                Case 3         'Pinned-Pinned
                    mem(mem.IndexOf(M)).DOF(0) = 0
                    mem(mem.IndexOf(M)).DOF(1) = 1
                    mem(mem.IndexOf(M)).DOF(2) = 0
                    mem(mem.IndexOf(M)).DOF(3) = 1
                    Exit Sub
                Case 4         'Free-Free
                    Exit Sub
                Case 5         'Fixed-Pinned
                    mem(mem.IndexOf(M)).DOF(0) = 0
                    mem(mem.IndexOf(M)).DOF(1) = 0
                    mem(mem.IndexOf(M)).DOF(2) = 0
                    mem(mem.IndexOf(M)).DOF(3) = 1
                    Exit Sub
                Case 6         'pinned-free
                    Exit Sub
            End Select
        ElseIf mem.IndexOf(M) = 0 Then
            Select Case ends
                Case 1         'Fixed-Fixed
                    mem(mem.IndexOf(M)).DOF(0) = 0
                    mem(mem.IndexOf(M)).DOF(1) = 0
                    mem(mem.IndexOf(M)).DOF(2) = 0
                    mem(mem.IndexOf(M)).DOF(3) = 1
                    Exit Sub
                Case 2         'Fixed-Free
                    mem(mem.IndexOf(M)).DOF(0) = 0
                    mem(mem.IndexOf(M)).DOF(1) = 0
                    mem(mem.IndexOf(M)).DOF(2) = 0
                    mem(mem.IndexOf(M)).DOF(3) = 1
                    Exit Sub
                Case 3         'Pinned-Pinned
                    mem(mem.IndexOf(M)).DOF(0) = 0
                    mem(mem.IndexOf(M)).DOF(1) = 1
                    mem(mem.IndexOf(M)).DOF(2) = 0
                    mem(mem.IndexOf(M)).DOF(3) = 1
                    Exit Sub
                Case 4         'Free-Free
                    mem(mem.IndexOf(M)).DOF(0) = 1
                    mem(mem.IndexOf(M)).DOF(1) = 1
                    mem(mem.IndexOf(M)).DOF(2) = 0
                    mem(mem.IndexOf(M)).DOF(3) = 1
                    Exit Sub
                Case 5         'Fixed-Pinned
                    mem(mem.IndexOf(M)).DOF(0) = 0
                    mem(mem.IndexOf(M)).DOF(1) = 0
                    mem(mem.IndexOf(M)).DOF(2) = 0
                    mem(mem.IndexOf(M)).DOF(3) = 1
                    Exit Sub
                Case 6         'pinned-free
                    mem(mem.IndexOf(M)).DOF(0) = 0
                    mem(mem.IndexOf(M)).DOF(1) = 1
                    mem(mem.IndexOf(M)).DOF(2) = 0
                    mem(mem.IndexOf(M)).DOF(3) = 1
                    Exit Sub
            End Select
        ElseIf mem.IndexOf(M) = mem.Count - 1 Then
            Select Case ends
                Case 1         'Fixed-Fixed
                    mem(mem.IndexOf(M)).DOF(0) = 0
                    mem(mem.IndexOf(M)).DOF(1) = 1
                    mem(mem.IndexOf(M)).DOF(2) = 0
                    mem(mem.IndexOf(M)).DOF(3) = 0
                    Exit Sub
                Case 2         'Fixed-Free
                    mem(mem.IndexOf(M)).DOF(0) = 0
                    mem(mem.IndexOf(M)).DOF(1) = 1
                    mem(mem.IndexOf(M)).DOF(2) = 1
                    mem(mem.IndexOf(M)).DOF(3) = 1
                    Exit Sub
                Case 3         'Pinned-Pinned
                    mem(mem.IndexOf(M)).DOF(0) = 0
                    mem(mem.IndexOf(M)).DOF(1) = 1
                    mem(mem.IndexOf(M)).DOF(2) = 0
                    mem(mem.IndexOf(M)).DOF(3) = 1
                    Exit Sub
                Case 4         'Free-Free
                    mem(mem.IndexOf(M)).DOF(0) = 0
                    mem(mem.IndexOf(M)).DOF(1) = 1
                    mem(mem.IndexOf(M)).DOF(2) = 1
                    mem(mem.IndexOf(M)).DOF(3) = 1
                    Exit Sub
                Case 5         'Fixed-Pinned
                    mem(mem.IndexOf(M)).DOF(0) = 0
                    mem(mem.IndexOf(M)).DOF(1) = 1
                    mem(mem.IndexOf(M)).DOF(2) = 0
                    mem(mem.IndexOf(M)).DOF(3) = 1
                    Exit Sub
                Case 6         'pinned-free
                    mem(mem.IndexOf(M)).DOF(0) = 0
                    mem(mem.IndexOf(M)).DOF(1) = 1
                    mem(mem.IndexOf(M)).DOF(2) = 1
                    mem(mem.IndexOf(M)).DOF(3) = 1
                    Exit Sub
            End Select
        Else
            mem(mem.IndexOf(M)).DOF(0) = 0
            mem(mem.IndexOf(M)).DOF(1) = 1
            mem(mem.IndexOf(M)).DOF(2) = 0
            mem(mem.IndexOf(M)).DOF(3) = 1
            Exit Sub
        End If

    End Sub

#Region " Fixed End Reaction - Force & Moment"
    Private Sub FixFER(ByVal E As Member)
        '<------Finding reaction--------->
        Dim Fi, Mi, Fj, Mj As Double
        FER_Total(Fi, Mi, Fj, Mj, E)

        Tmem(Tmem.IndexOf(E)).FER(0) = Fi 'Upward - ive
        Tmem(Tmem.IndexOf(E)).FER(1) = Mi 'Anti clockwise - ive
        Tmem(Tmem.IndexOf(E)).FER(2) = Fj 'downward + ive
        Tmem(Tmem.IndexOf(E)).FER(3) = Mj ' clockwise + ive
    End Sub

    Private Sub FER_Total(ByRef Fi As Double, ByRef Mi As Double, ByRef Fj As Double, ByRef Mj As Double, ByVal Fmem As Member)
        Fi = 0
        Mi = 0
        Fj = 0
        Mj = 0
        Dim Tmi, Tmj, Tfi, Tfj As Double
        Dim Length As Double = Fmem.spanlength
        Dim K, a, b, c As Double
        For Each PL In Fmem.Pload
            K = PL.pload
            a = PL.pdist
            b = Length - PL.pdist

            Tmi = ((4 * deltaI_PointLoad(PL.pload, b, Length) - 2 * deltaJ_PointLoad(PL.pload, a, Length)) / Length)
            Tmj = ((2 * deltaI_PointLoad(PL.pload, b, Length) - 4 * deltaJ_PointLoad(PL.pload, a, Length)) / Length)
            Tfi = (((K * b) + Tmi + Tmj) / Length)
            Tfj = (((K * a) - Tmi - Tmj) / Length)

            Mi = Mi + Tmi
            Mj = Mj + Tmj
            Fi = Fi + Tfi
            Fj = Fj + Tfj
        Next
        For Each ML In Fmem.Mload
            K = ML.mload
            a = ML.mdist
            b = Length - ML.mdist

            Tmi = ((4 * deltaI_Momentoad(ML.mload, b, Length) - 2 * deltaJ_Momentoad(ML.mload, a, Length)) / Length)
            Tmj = ((2 * deltaI_Momentoad(ML.mload, b, Length) - 4 * deltaJ_Momentoad(ML.mload, a, Length)) / Length)
            Tfi = (((K * b) + Tmi + Tmj) / Length)
            Tfj = (((K * a) - Tmi - Tmj) / Length)

            Mi = Mi + ((((ML.mload) * b * ((3 * a) - Length)) / (Length ^ 2)))  ' + Tmi
            Mj = Mj + ((((ML.mload) * a * ((3 * b) - Length)) / (Length ^ 2))) ' + Tmj
            Fi = Fi + (((6 * (ML.mload) * a * b) / (Length ^ 3))) ' + Tfi
            Fj = Fj - (((6 * (ML.mload) * a * b) / (Length ^ 3))) '+ Tfj
        Next
        For Each UL In Fmem.Uload
            If UL.uload1 = UL.uload2 Then
                c = UL.udist2 - UL.udist1
                K = UL.uload1 * c
                a = UL.udist1 + (c / 2)
                b = Length - (UL.udist2 - (c / 2))

                Tmi = ((4 * deltaI_UVLoadCase1(UL.uload1, a, b, c, Length) - 2 * deltaJ_UVLoadCase1(UL.uload1, a, b, c, Length)) / Length)
                Tmj = ((2 * deltaI_UVLoadCase1(UL.uload1, a, b, c, Length) - 4 * deltaJ_UVLoadCase1(UL.uload1, a, b, c, Length)) / Length)
                Tfi = (((K * b) + Tmi + Tmj) / Length)
                Tfj = (((K * a) - Tmi - Tmj) / Length)

                Mi = Mi + Tmi
                Mj = Mj + Tmj
                Fi = Fi + Tfi
                Fj = Fj + Tfj
            ElseIf UL.uload2 > UL.uload1 Then
                '---------For the smallest of load - ie., uniformly distributed
                c = UL.udist2 - UL.udist1
                K = UL.uload1 * c
                a = UL.udist1 + (c / 2)
                b = Length - (UL.udist2 - (c / 2))

                Tmi = ((4 * deltaI_UVLoadCase1(UL.uload1, a, b, c, Length) - 2 * deltaJ_UVLoadCase1(UL.uload1, a, b, c, Length)) / Length)
                Tmj = ((2 * deltaI_UVLoadCase1(UL.uload1, a, b, c, Length) - 4 * deltaJ_UVLoadCase1(UL.uload1, a, b, c, Length)) / Length)
                Tfi = (((K * b) + Tmi + Tmj) / Length)
                Tfj = (((K * a) - Tmi - Tmj) / Length)

                Mi = Mi + Tmi
                Mj = Mj + Tmj
                Fi = Fi + Tfi
                Fj = Fj + Tfj

                '---------For the varrying load
                c = UL.udist2 - UL.udist1
                K = (UL.uload2 - UL.uload1) * (c / 2)
                a = UL.udist1 + ((2 * c) / 3)
                b = Length - (UL.udist2 - (c / 3))

                Tmi = ((4 * deltaI_UVLoadCase2((UL.uload2 - UL.uload1), a, c, Length) - 2 * deltaJ_UVLoadCase2((UL.uload2 - UL.uload1), b, c, Length)) / Length)
                Tmj = ((2 * deltaI_UVLoadCase2((UL.uload2 - UL.uload1), a, c, Length) - 4 * deltaJ_UVLoadCase2((UL.uload2 - UL.uload1), b, c, Length)) / Length)
                Tfi = (((K * b) + Tmi + Tmj) / Length)
                Tfj = (((K * a) - Tmi - Tmj) / Length)

                Mi = Mi + Tmi
                Mj = Mj + Tmj
                Fi = Fi + Tfi
                Fj = Fj + Tfj
            ElseIf UL.uload2 < UL.uload1 Then
                '---------For the smallest of load - ie., uniformly distributed
                c = UL.udist2 - UL.udist1
                K = UL.uload2 * c
                a = UL.udist1 + (c / 2)
                b = Length - (UL.udist2 - (c / 2))

                Tmi = ((4 * deltaI_UVLoadCase1(UL.uload2, a, b, c, Length) - 2 * deltaJ_UVLoadCase1(UL.uload2, a, b, c, Length)) / Length)
                Tmj = ((2 * deltaI_UVLoadCase1(UL.uload2, a, b, c, Length) - 4 * deltaJ_UVLoadCase1(UL.uload2, a, b, c, Length)) / Length)
                Tfi = (((K * b) + Tmi + Tmj) / Length)
                Tfj = (((K * a) - Tmi - Tmj) / Length)

                Mi = Mi + Tmi
                Mj = Mj + Tmj
                Fi = Fi + Tfi
                Fj = Fj + Tfj

                '---------For the varrying load
                c = UL.udist2 - UL.udist1
                K = (UL.uload1 - UL.uload2) * (c / 2)
                a = UL.udist1 + (c / 3)
                b = Length - (UL.udist2 - ((2 * c) / 3))

                Tmi = ((4 * deltaI_UVLoadCase3((UL.uload1 - UL.uload2), a, c, Length) - 2 * deltaJ_UVLoadCase3((UL.uload1 - UL.uload2), b, c, Length)) / Length)
                Tmj = ((2 * deltaI_UVLoadCase3((UL.uload1 - UL.uload2), a, c, Length) - 4 * deltaJ_UVLoadCase3((UL.uload1 - UL.uload2), b, c, Length)) / Length)
                Tfi = (((K * b) + Tmi + Tmj) / Length)
                Tfj = (((K * a) - Tmi - Tmj) / Length)

                Mi = Mi + Tmi
                Mj = Mj + Tmj
                Fi = Fi + Tfi
                Fj = Fj + Tfj
            End If
        Next
    End Sub

    Private Function deltaI_PointLoad(ByVal W As Double, ByVal b As Double, ByVal L As Double) As Double
        Dim Di As Double
        Di = (W * b * ((L ^ 2) - (b ^ 2))) / (6 * L)
        Return Di
    End Function

    Private Function deltaJ_PointLoad(ByVal W As Double, ByVal a As Double, ByVal L As Double) As Double
        Dim Dj As Double
        Dj = (W * a * ((L ^ 2) - (a ^ 2))) / (6 * L)
        Return Dj
    End Function

    Private Function deltaI_UVLoadCase1(ByVal W As Double, ByVal a As Double, ByVal b As Double, ByVal c As Double, ByVal L As Double) As Double
        '-------- Uload1 = uload2
        Dim Di As Double
        Di = (W * b * c * ((4 * a * (b + L)) - (c ^ 2))) / (24 * L)
        Return Di
    End Function

    Private Function deltaJ_UVLoadCase1(ByVal W As Double, ByVal a As Double, ByVal b As Double, ByVal c As Double, ByVal L As Double) As Double
        '-------- Uload1 = uload2
        Dim Dj As Double
        Dj = (W * a * c * ((4 * b * (a + L)) - (c ^ 2))) / (24 * L)
        Return Dj
    End Function

    Private Function deltaI_UVLoadCase2(ByVal W As Double, ByVal a As Double, ByVal c As Double, ByVal L As Double) As Double
        '-------- Uload1 < uload2
        Dim Di, alpha, gamma As Double
        alpha = a / L
        gamma = c / L
        Di = ((W * (L ^ 3) * gamma) * _
             ((270 * (alpha - (alpha ^ 3))) - ((gamma ^ 2) * ((45 * alpha) - (2 * gamma))))) / _
             (3240)
        Return Di
    End Function

    Private Function deltaJ_UVLoadCase2(ByVal W As Double, ByVal b As Double, ByVal c As Double, ByVal L As Double) As Double
        '-------- Uload1 < uload2
        Dim Dj, beta, gamma As Double
        beta = b / L
        gamma = c / L
        Dj = ((W * (L ^ 3) * gamma) * _
             ((270 * (beta - (beta ^ 3))) - ((gamma ^ 2) * ((45 * beta) + (2 * gamma))))) / _
             (3240)
        Return Dj
    End Function

    Private Function deltaI_UVLoadCase3(ByVal W As Double, ByVal a As Double, ByVal c As Double, ByVal L As Double) As Double
        '-------- Uload1 > uload2
        Dim Di, alpha, gamma As Double
        alpha = a / L
        gamma = c / L
        Di = ((W * (L ^ 3) * gamma) * _
             ((270 * (alpha - (alpha ^ 3))) - ((gamma ^ 2) * ((45 * alpha) + (2 * gamma))))) / _
             (3240)
        Return Di
    End Function

    Private Function deltaJ_UVLoadCase3(ByVal W As Double, ByVal b As Double, ByVal c As Double, ByVal L As Double) As Double
        '-------- Uload1 > uload2
        Dim Dj, beta, gamma As Double
        beta = b / L
        gamma = c / L
        Dj = ((W * (L ^ 3) * gamma) * _
             ((270 * (beta - (beta ^ 3))) - ((gamma ^ 2) * ((45 * beta) - (2 * gamma))))) / _
             (3240)
        Return Dj
    End Function

    Private Function deltaI_Momentoad(ByVal W As Double, ByVal b As Double, ByVal L As Double) As Double
        Dim Di As Double
        Di = ((W) * ((3 * (b ^ 2)) - (L ^ 2))) / (6 * L)
        Return Di
    End Function

    Private Function deltaJ_Momentoad(ByVal W As Double, ByVal a As Double, ByVal L As Double) As Double
        Dim Dj As Double
        Dj = ((W) * ((L ^ 2) - (3 * (a ^ 2)))) / (6 * L)
        Return Dj
    End Function
#End Region

    Private Sub GStiffness(ByVal M As Member, ByRef gm(,) As Double)
        '-----------Stiffness matrix for Members --------------
        Dim c1 As Double = (M.Emodulus * M.Inertia) _
                            / (M.spanlength * ((M.spanlength ^ 2) + (12 * M.g)))

        Tmem(Tmem.IndexOf(M)).stiff(0, 0) = 12 * c1
        Tmem(Tmem.IndexOf(M)).stiff(0, 1) = 6 * M.spanlength * c1
        Tmem(Tmem.IndexOf(M)).stiff(0, 2) = -12 * c1
        Tmem(Tmem.IndexOf(M)).stiff(0, 3) = 6 * M.spanlength * c1
        Tmem(Tmem.IndexOf(M)).stiff(1, 0) = 6 * M.spanlength * c1
        Tmem(Tmem.IndexOf(M)).stiff(1, 1) = ((4 * (M.spanlength ^ 2)) + (12 * M.g)) * c1
        Tmem(Tmem.IndexOf(M)).stiff(1, 2) = -6 * M.spanlength * c1
        Tmem(Tmem.IndexOf(M)).stiff(1, 3) = ((2 * (M.spanlength ^ 2)) - (12 * M.g)) * c1
        Tmem(Tmem.IndexOf(M)).stiff(2, 0) = -12 * c1
        Tmem(Tmem.IndexOf(M)).stiff(2, 1) = -6 * M.spanlength * c1
        Tmem(Tmem.IndexOf(M)).stiff(2, 2) = 12 * c1
        Tmem(Tmem.IndexOf(M)).stiff(2, 3) = -6 * M.spanlength * c1
        Tmem(Tmem.IndexOf(M)).stiff(3, 0) = 6 * M.spanlength * c1
        Tmem(Tmem.IndexOf(M)).stiff(3, 1) = ((2 * (M.spanlength ^ 2)) - (12 * M.g)) * c1
        Tmem(Tmem.IndexOf(M)).stiff(3, 2) = -6 * M.spanlength * c1
        Tmem(Tmem.IndexOf(M)).stiff(3, 3) = ((4 * (M.spanlength ^ 2)) + (12 * M.g)) * c1
        Dim d As Integer = Tmem.IndexOf(M) * 2
        For l = 0 To 3
            For k = 0 To 3
                gm((l + d), (k + d)) = (gm((l + d), (k + d)) + (Tmem(Tmem.IndexOf(M)).stiff(l, k)))
            Next
        Next
    End Sub

    Private Sub FixDM(ByRef dm() As Integer)
        For Each E In Tmem
            dm(Tmem.IndexOf(E) * 2) = E.DOF(0)
            dm((Tmem.IndexOf(E) * 2) + 1) = E.DOF(1)
            dm((Tmem.IndexOf(E) * 2) + 2) = E.DOF(2)
            dm((Tmem.IndexOf(E) * 2) + 3) = E.DOF(3)
        Next
    End Sub

    Private Sub FixFM(ByRef fm() As Double)
        For Each E In Tmem
            If Tmem.IndexOf(E) = 0 Then
                fm(Tmem.IndexOf(E) * 2) = E.FER(0)
                fm((Tmem.IndexOf(E) * 2) + 1) = E.FER(1)
                fm((Tmem.IndexOf(E) * 2) + 2) = E.FER(2)
                fm((Tmem.IndexOf(E) * 2) + 3) = E.FER(3)
                Continue For
            End If
            fm(Tmem.IndexOf(E) * 2) = fm(Tmem.IndexOf(E) * 2) + E.FER(0)
            fm((Tmem.IndexOf(E) * 2) + 1) = fm((Tmem.IndexOf(E) * 2) + 1) + E.FER(1)
            fm((Tmem.IndexOf(E) * 2) + 2) = E.FER(2)
            fm((Tmem.IndexOf(E) * 2) + 3) = E.FER(3)
        Next
    End Sub

    Private Sub Curtailment(ByRef gm(,) As Double, ByRef dofm() As Integer, ByRef ferm() As Double, ByRef cb As Integer)
        Dim tgm(Tmem.Count * 2 + 1, Tmem.Count * 2 + 1) As Double
        Dim tdofm(Tmem.Count * 2 + 1) As Integer
        Dim tferm(Tmem.Count * 2 + 1) As Double

        Dim r, s As Integer
        For p = 0 To (Tmem.Count * 2 + 1)
            If dofm(p) = 0 Then
                Continue For
            Else
                s = 0
                For t = 0 To (Tmem.Count * 2 + 1)
                    If dofm(t) = 0 Then
                        Continue For
                    Else
                        tferm(s) = ferm(t)
                        tdofm(s) = dofm(t)
                        tgm(r, s) = gm(p, t)
                        s = s + 1
                    End If
                Next
                r = r + 1
            End If
        Next

        ReDim gm(r - 1, r - 1)
        ReDim dofm(r - 1)
        ReDim ferm(r - 1)

        For p = 0 To r - 1
            dofm(p) = tdofm(p)
            ferm(p) = tferm(p)
            For t = 0 To r - 1
                gm(p, t) = tgm(p, t)
            Next
        Next
        cb = r
    End Sub

    Private Sub GElimination(ByRef A(,) As Double, ByRef B() As Double, ByRef re() As Double, ByVal cb As Integer)
        '----Check For Double Span
        If Tmem.Count - 1 <= 0 Then
            Exit Sub
        End If

        Dim Triangular_A(cb, cb + 1), line_1, temporary_1, multiplier_1, sum_1 As Double
        Dim soln(cb) As Double 'Solution matrix
        For n = 0 To cb
            For m = 0 To cb
                Triangular_A(m, n) = A(m, n)
            Next
        Next

        '.... substituting the force to triangularmatrics....
        For n = 0 To cb
            Triangular_A(n, cb + 1) = B(n)
        Next

        '...............soving the triangular matrics.............
        For k = 0 To cb
            '......Bring a non-zero element first by changes lines if necessary
            If Triangular_A(k, k) = 0 Then
                For n = k To cb
                    If Triangular_A(n, k) <> 0 Then line_1 = n : Exit For 'Finds line_1 with non-zero element
                Next n
                '..........Change line k with line_1
                For m = k To cb
                    temporary_1 = Triangular_A(k, m)
                    Triangular_A(k, m) = Triangular_A(line_1, m)
                    Triangular_A(line_1, m) = temporary_1
                Next m
            End If
            '....For other lines, make a zero element by using:
            '.........Ai1=Aij-A11*(Aij/A11)
            '.....and change all the line using the same formula for other elements
            For n = k + 1 To cb
                If Triangular_A(n, k) <> 0 Then 'if it is zero, stays as it is
                    multiplier_1 = Triangular_A(n, k) / Triangular_A(k, k)
                    For m = k To cb + 1
                        Triangular_A(n, m) = Triangular_A(n, m) - Triangular_A(k, m) * multiplier_1
                    Next m
                End If
            Next n
        Next k


        '..... calculating the dof value..........

        'First, calculate last xi (for i = System_DIM)
        soln(cb) = Triangular_A(cb, cb + 1) / Triangular_A(cb, cb)

        '................
        For n = 0 To cb
            sum_1 = 0
            For m = 0 To n
                sum_1 = sum_1 + soln(cb + 1 - m) * Triangular_A(cb - n, cb - m)
            Next m
            soln(cb - n) = (Triangular_A(cb - n, cb + 1) - sum_1) / Triangular_A(cb - n, cb - n)

        Next n

        For n = 0 To cb
            re(n) = soln(n + 1)
        Next
    End Sub

#Region "Gauss Elimination Method"
    '-----Redefined Gauss Elimination Procedure
    Private Sub Gauss(ByVal A(,) As Double, ByVal B() As Double, ByRef X() As Double, ByVal Bound As Integer)
        Dim Triangular_A(Bound, Bound + 1) As Double
        Dim soln(Bound) As Double 'Solution matrix
        For m = 0 To Bound
            For n = 0 To Bound
                Triangular_A(m, n) = A(m, n)
            Next
        Next
        '.... substituting the force to triangularmatrics....
        For n = 0 To Bound
            Triangular_A(n, Bound + 1) = B(n)
        Next
        ForwardSubstitution(Triangular_A, Bound)
        ReverseElimination(Triangular_A, X, Bound)
    End Sub


    Private Sub ForwardSubstitution(ByRef _triang(,) As Double, ByVal bound As Integer)
        'Forward Elimination
        'Dim _fraction As Double
        For k = 0 To bound - 1
            For i = k + 1 To bound
                If _triang(k, k) = 0 Then
                    Continue For
                End If
                _triang(i, k) = _triang(i, k) / _triang(k, k)
                For j = k + 1 To bound + 1
                    _triang(i, j) = _triang(i, j) - (_triang(i, k) * _triang(k, j))
                Next
            Next
        Next
    End Sub

    Private Sub ReverseElimination(ByRef _triang(,) As Double, ByRef X() As Double, ByVal bound As Integer)
        'Back Substitution
        For i = 0 To bound
            X(i) = _triang(i, bound + 1)
        Next

        For i = bound To 0 Step -1
            For j = i + 1 To bound
                X(i) = X(i) - (_triang(i, j) * X(j))
            Next
            X(i) = X(i) / _triang(i, i)
        Next
    End Sub
#End Region

    Private Sub Welding(ByRef re() As Double, ByRef fer() As Double, ByRef dm() As Integer, ByRef Theta_Delta_matrix() As Double)
        Dim tres(Tmem.Count * 2 + 1) As Double
        Dim j As Integer
        For i = 0 To (Tmem.Count * 2 + 1)
            If dm(i) = 0 Then
                Continue For
            End If
            tres(i) = tres(i) + re(j)
            j = j + 1
        Next
        ReDim re(Tmem.Count * 2 + 1)
        ReDim Theta_Delta_matrix(Tmem.Count * 2 + 1)
        For i = 0 To (Tmem.Count * 2 + 1)
            re(i) = tres(i)
            Theta_Delta_matrix(i) = tres(i)
        Next
    End Sub

    Private Sub GMultiplier(ByRef gm(,) As Double, ByRef re() As Double)
        Dim teR(Tmem.Count * 2 + 1) As Double
        For i = 0 To ((Tmem.Count * 2) + 1)
            teR(i) = 0
            For j = 0 To ((Tmem.Count * 2) + 1)
                teR(i) = teR(i) + (gm(i, j) * re(j))
            Next
        Next
        For i = 0 To (Tmem.Count * 2 + 1)
            re(i) = teR(i)
        Next
    End Sub

    Private Sub loadMINU(ByRef re() As Double, ByRef lo() As Double, ByRef ThetaDelta() As Double)
        For i = 0 To (Tmem.Count * 2 + 1)
            re(i) = re(i) - lo(i)
        Next
        For Each E In Tmem
            Tmem(Tmem.IndexOf(E)).RES(0) = re(Tmem.IndexOf(E) * 2)
            Tmem(Tmem.IndexOf(E)).RES(1) = re((Tmem.IndexOf(E) * 2) + 1)
            Tmem(Tmem.IndexOf(E)).RES(2) = re((Tmem.IndexOf(E) * 2) + 2)
            Tmem(Tmem.IndexOf(E)).RES(3) = re((Tmem.IndexOf(E) * 2) + 3)

            Tmem(Tmem.IndexOf(E)).DISP(0) = ThetaDelta(Tmem.IndexOf(E) * 2)
            Tmem(Tmem.IndexOf(E)).DISP(1) = ThetaDelta((Tmem.IndexOf(E) * 2) + 1)
            Tmem(Tmem.IndexOf(E)).DISP(2) = ThetaDelta((Tmem.IndexOf(E) * 2) + 2)
            Tmem(Tmem.IndexOf(E)).DISP(3) = ThetaDelta((Tmem.IndexOf(E) * 2) + 3)
        Next
    End Sub
#End Region

#Region "Co-ordinate calculation"
    Public Sub CoordinateCalculator()
        '------- Normalized Equilibrium Member
        Dim FRmem As New Member

        FixEquilibriumMember(FRmem)

        BeamCoords.Clear()
        DX.Clear()
        SF.Clear()
        BM.Clear()
        SL.Clear()
        DE.Clear()

        '------- Shear Force & Bending moment Coordinate Fixing
        FixDisintegration(FRmem)
        For Each X In DX
            BeamCoords.Add(((max_width / 2 - beamcreate.coverpic.Width / 2) + 100) + (((beamcreate.coverpic.Width - 200) / FRmem.spanlength) * X))
            SF.Add(Total_ShearForce_L(Math.Round(X, 2), FRmem))
            BM.Add(Total_BendingMoment_L(Math.Round(X, 2), FRmem))
        Next
        FixShearForce_Coords(FRmem)
        FixDisplacement_Slope_Deflection(FRmem)
        FixBendingMoment_Coords()
        FixSlopeDeflection_Coords()
    End Sub

    Private Sub FixDisintegration(ByVal FRmem As Member)
        Dim _dx As Double = FRmem.spanlength / (beamcreate.coverpic.Width - 200)
        Dim _LoopV As Double = 0
        Dim TempDX As New List(Of Double)
        For _LoopV = 0 To FRmem.spanlength Step _dx
            TempDX.Add(_LoopV)
        Next
        For Each Pl In FRmem.Pload
            If TempDX.Contains(Pl.pdist) = False Then
                TempDX.Add(Pl.pdist)
            End If
        Next
        For Each Ul In FRmem.Uload
            If TempDX.Contains(Ul.udist1) = False Then
                TempDX.Add(Ul.udist1)
            End If
            If TempDX.Contains(Ul.udist2) = False Then
                TempDX.Add(Ul.udist2)
            End If
        Next
        For Each Ml In FRmem.Mload
            If TempDX.Contains(Ml.mdist) = False Then
                TempDX.Add(Ml.mdist)
            End If
        Next
        TempDX.Sort()
        DX = TempDX
    End Sub

    Private Sub FixEquilibriumMember(ByRef FRmem As Member)
        Dim TempLength As Double = 0
        For Each E In mem
            '-------Left End reaction
            If Math.Round(E.RES(0), 2) <> 0 Then
                Dim Reaction_PL_L As Member.P
                Reaction_PL_L.pload = E.RES(0)
                Reaction_PL_L.pdist = TempLength
                FRmem.Pload.Add(Reaction_PL_L)
            End If
            '-------Left End moment
            If Math.Round(E.RES(1), 2) <> 0 Then
                Dim Reaction_ML_L As Member.M
                Reaction_ML_L.mload = E.RES(1)
                Reaction_ML_L.mdist = TempLength
                FRmem.Mload.Add(Reaction_ML_L)
            End If
            For Each Pl In E.Pload
                Dim TPl As New Member.P
                TPl.pdist = TempLength + Pl.pdist
                TPl.pload = Pl.pload
                FRmem.Pload.Add(TPl)
            Next
            For Each Ul In E.Uload
                Dim TUl As New Member.U
                TUl.udist1 = TempLength + Ul.udist1
                TUl.udist2 = TempLength + Ul.udist2
                TUl.uload1 = Ul.uload1
                TUl.uload2 = Ul.uload2
                FRmem.Uload.Add(TUl)
            Next
            For Each Ml In E.Mload
                Dim TMl As New Member.M
                TMl.mdist = TempLength + Ml.mdist
                TMl.mload = -Ml.mload  '--Clockwise +ive
                FRmem.Mload.Add(TMl)
            Next

            If mem.IndexOf(E) = mem.Count - 1 Then
                '-------Right End reaction
                If Math.Round(E.RES(2), 2) <> 0 Then
                    Dim Reaction_PL_R As Member.P
                    Reaction_PL_R.pload = E.RES(2)
                    Reaction_PL_R.pdist = TempLength + E.spanlength
                    FRmem.Pload.Add(Reaction_PL_R)
                End If
                '-------Right End moment
                If Math.Round(E.RES(3), 2) <> 0 Then
                    Dim Reaction_ML_R As Member.M
                    Reaction_ML_R.mload = E.RES(3)
                    Reaction_ML_R.mdist = TempLength + E.spanlength
                    FRmem.Mload.Add(Reaction_ML_R)
                End If
            End If
            TempLength = TempLength + E.spanlength
        Next
        FRmem.spanlength = TempLength
        beamcreate.Tlength = TempLength
    End Sub

#Region "Shear Force Calculator"
    Private Function Total_ShearForce_L(ByVal _curDx As Double, ByVal Fmem As Member) As Double
        '-----Function Returns Total Shear Force in a point from left to right
        Dim SF_L As Double
        SF_L = SF_PointLoad_L(_curDx, Fmem) + SF_UVL_L(_curDx, Fmem)
        Return SF_L
    End Function

    Private Function SF_PointLoad_L(ByVal _curDx As Double, ByVal Fmem As Member) As Double
        '----Shear Force due to point load from left to right
        Dim SF, T_SF As Double
        For Each PL In Fmem.Pload
            SF = 0
            If _curDx >= PL.pdist Then
                SF = PL.pload
            End If
            T_SF = T_SF + SF
        Next
        Return T_SF
    End Function

    Private Function SF_UVL_L(ByVal _curDx As Double, ByVal Fmem As Member) As Double
        '----Shear force due to UVL from left to right
        Dim _RectF, _TriF, _SecF, SF, T_SF As Double
        For Each UL In Fmem.Uload
            SF = 0
            If UL.uload1 <= UL.uload2 Then
                If (_curDx >= UL.udist2) Then
                    _RectF = UL.uload1 * (UL.udist2 - UL.udist1)
                    _TriF = 0.5 * (UL.uload2 - UL.uload1) * (UL.udist2 - UL.udist1)
                    SF = _RectF + _TriF
                ElseIf ((_curDx >= UL.udist1) And (_curDx < UL.udist2)) Then
                    _RectF = UL.uload1 * (_curDx - UL.udist1)
                    _SecF = ((UL.uload2 - UL.uload1) / (UL.udist2 - UL.udist1)) * _
                            (_curDx - UL.udist1)
                    _TriF = 0.5 * _SecF * (_curDx - UL.udist1)
                    SF = _RectF + _TriF
                End If
                T_SF = T_SF + SF
            Else
                If (_curDx >= UL.udist2) Then
                    _RectF = UL.uload2 * (UL.udist2 - UL.udist1)
                    _TriF = 0.5 * (UL.uload1 - UL.uload2) * (UL.udist2 - UL.udist1)
                    SF = _RectF + _TriF
                ElseIf ((_curDx >= UL.udist1) And (_curDx < UL.udist2)) Then
                    _SecF = UL.uload2 + _
                                (((UL.uload1 - UL.uload2) / (UL.udist2 - UL.udist1)) * (UL.udist2 - _curDx))
                    _RectF = _SecF * (_curDx - UL.udist1)
                    _TriF = 0.5 * (UL.uload1 - _SecF) * (_curDx - UL.udist1)
                    SF = _RectF + _TriF
                End If
                T_SF = T_SF + SF
            End If
        Next
        Return T_SF
    End Function

    Private Sub FixShearForce_Coords(ByVal Fmem As Member)
        Call FixShearForce_CurveCoords(Fmem)
        Call FixShearForce_ShowCoords(Fmem)
    End Sub

    Private Sub FixShearForce_CurveCoords(ByVal Fmem As Member)
        '-------Finding maximum shear force for scaling the shear curve
        Dim maxV As Double = 0
        For Each SForce In SF
            If Math.Abs(SForce) > maxV Then
                maxV = Math.Abs(SForce)
            End If
        Next
        ShearM = maxV
        maxV = 180 / maxV

        '--------Fixing the curve coordinates
        ReDim SFpts(SF.Count - 1)
        Dim f As Integer = 0

        For Each S In SF
            SFpts(f).X = BeamCoords(f)
            SFpts(f).Y = (S * maxV) + (beamcreate.MEheight / 2)
            f = f + 1
        Next
    End Sub

    Private Sub FixShearForce_ShowCoords(ByVal Fmem As Member)
        Dim CDSindex As New List(Of Integer)
        CaptureLoadLocations(CDSindex, Fmem)
        SFMc.Clear()
        ReDim SFmaxs(CDSindex.Count - 1)
        Dim f As Integer = 0
        For Each CD In CDSindex
            SFMc.Add(CD)
            SFmaxs(f).X = SFpts(CD).X
            SFmaxs(f).Y = SFpts(CD).Y + If(SFpts(CD).Y > (beamcreate.MEheight / 2), 15, -30)
            f = f + 1
        Next
    End Sub

    Private Sub CaptureLoadLocations(ByRef _CDIndex As List(Of Integer), ByVal Fmem As Member)
        '------Capturing the shear force indexes for displaying
        Dim _CDLoc As List(Of Double)
        Dim _TemCDindex As New List(Of Integer)
        Dim LoadLocation() As Double
        Dim i As Integer = 0

        If Fmem.Pload.Count <> 0 Then
            i = i + (Fmem.Pload.Count)
        End If
        ReDim LoadLocation(i - 1)
        '_______________________________________________________________________
        '------Capturing Point Load
        i = 0
        For Each PL In Fmem.Pload
            LoadLocation(i) = PL.pdist
            i = i + 1
        Next
        '------Checking for zero values and identical values and adding to list
        '------Only for point loads --> coz need to capture left and right
        _CDLoc = New List(Of Double)
        For j = 0 To i - 1
            If _CDLoc.Contains(LoadLocation(j)) = False Then
                _CDLoc.Add(LoadLocation(j))
            End If
        Next
        '------Capturing left and right index except for start and end location
        For Each CDL In _CDLoc
            If CDL = 0 Then
                _TemCDindex.Add(FirstfromLeft_LoadLocation(CDL, Fmem))
            ElseIf CDL = Fmem.spanlength Then
                _TemCDindex.Add(FirstfromRight_LoadLocation(CDL, Fmem))
            Else
                _TemCDindex.Add(FirstfromLeft_LoadLocation(CDL, Fmem))
                _TemCDindex.Add(FirstfromRight_LoadLocation(CDL, Fmem))
            End If
        Next
        '_______________________________________________________________________
        '------Capturing UVL and Moment 
        i = 0
        If Fmem.Uload.Count <> 0 Then
            i = i + ((Fmem.Uload.Count * 2))
        End If
        If Fmem.Mload.Count <> 0 Then
            i = i + (Fmem.Mload.Count)
        End If
        ReDim LoadLocation(i - 1)
        i = 0
        For Each Ul In Fmem.Uload
            LoadLocation(i) = Ul.udist1
            LoadLocation(i + 1) = Ul.udist2
            i = i + 2
        Next
        For Each ML In Fmem.Mload
            LoadLocation(i) = ML.mdist
            i = i + 1
        Next
        '------Checking for zero values and identical values and adding to list
        '------for UVL & Moment --> need to either left or right
        _CDLoc.Clear()
        For j = 0 To i - 1
            If _CDLoc.Contains(LoadLocation(j)) = False Then
                _CDLoc.Add(LoadLocation(j))
            End If
        Next
        '------Capturing left and right index except for start and end location
        For Each CDL In _CDLoc
            If CDL = 0 Then
                _TemCDindex.Add(FirstfromLeft_LoadLocation(CDL, Fmem))
            ElseIf CDL = Fmem.spanlength Then
                _TemCDindex.Add(FirstfromRight_LoadLocation(CDL, Fmem))
            Else
                _TemCDindex.Add(FirstfromLeft_LoadLocation(CDL, Fmem))
                '_TemCDindex.Add(FirstRight_LoadLocation(CDL, Fmem))
            End If
        Next

        '------Checking for identical values and adding to list
        For Each _ind In _TemCDindex
            If _CDIndex.Contains(_ind) = False Then
                _CDIndex.Add(_ind)
            End If
        Next
    End Sub

    Private Function FirstfromLeft_LoadLocation(ByVal Loc As Double, ByVal FRmem As Member) As Integer
        Dim _dx As Double = FRmem.spanlength / (beamcreate.coverpic.Width - 200)
        Dim J As Integer = 0
        For Each X In DX
            If X = Loc Then
                Return J
                Exit Function
            End If
            J = J + 1
        Next
    End Function

    Private Function FirstfromRight_LoadLocation(ByVal Loc As Double, ByVal FRmem As Member) As Integer
        Dim _dx As Double = FRmem.spanlength / (beamcreate.coverpic.Width - 200)
        Dim J As Integer = 0
        For Each X In DX
            If X = Loc Then
                Return J - 1
                Exit Function
            End If
            J = J + 1
        Next
    End Function
#End Region

#Region "Bending Moment Calculator"
    Private Function Total_BendingMoment_L(ByVal _curDx As Double, ByVal Fmem As Member) As Double
        '-----Function Returns Total Bending Moment in a point from left to right
        Dim BM As Double
        BM = BM_PointLoad_L(_curDx, Fmem) + _
             BM_UVL_L(_curDx, Fmem) + _
             BM_moment_L(_curDx, Fmem)
        Return BM
    End Function

    Private Function BM_PointLoad_L(ByVal _curDx As Double, ByVal Fmem As Member) As Double
        '----Bending moment due to point load from left to right
        Dim BM, T_BM As Double
        For Each PL In Fmem.Pload
            BM = 0
            If _curDx >= PL.pdist Then
                BM = PL.pload * (_curDx - PL.pdist)
            End If
            T_BM = T_BM + BM
        Next
        Return T_BM
    End Function

    Private Function BM_UVL_L(ByVal _curDx As Double, ByVal Fmem As Member) As Double
        '----Bending moment due to UVL from left to right
        Dim _RectF, _TriF, _SecF, BM, T_BM As Double
        Dim _RectCentroid, _TriCentroid As Double
        For Each UL In Fmem.Uload
            BM = 0
            If UL.uload1 <= UL.uload2 Then
                If (_curDx >= UL.udist2) Then
                    _RectF = UL.uload1 * (UL.udist2 - UL.udist1)
                    _TriF = 0.5 * (UL.uload2 - UL.uload1) * (UL.udist2 - UL.udist1)
                    _RectCentroid = _curDx - (UL.udist1 + ((UL.udist2 - UL.udist1) * (1 / 2)))
                    _TriCentroid = _curDx - (UL.udist1 + ((UL.udist2 - UL.udist1) * (2 / 3)))
                    BM = (_RectF * _RectCentroid) + _
                         (_TriF * _TriCentroid)
                ElseIf ((_curDx >= UL.udist1) And (_curDx < UL.udist2)) Then
                    _RectF = UL.uload1 * (_curDx - UL.udist1)
                    _SecF = ((UL.uload2 - UL.uload1) / (UL.udist2 - UL.udist1)) * _
                            (_curDx - UL.udist1)
                    _TriF = 0.5 * _SecF * (_curDx - UL.udist1)
                    _RectCentroid = _curDx - (UL.udist1 + ((_curDx - UL.udist1) * (1 / 2)))
                    _TriCentroid = _curDx - (UL.udist1 + ((_curDx - UL.udist1) * (2 / 3)))
                    BM = (_RectF * _RectCentroid) + _
                         (_TriF * _TriCentroid)
                End If
                T_BM = T_BM + BM
            Else
                If (_curDx >= UL.udist2) Then
                    _RectF = UL.uload2 * (UL.udist2 - UL.udist1)
                    _TriF = 0.5 * (UL.uload1 - UL.uload2) * (UL.udist2 - UL.udist1)
                    _RectCentroid = _curDx - (UL.udist1 + ((UL.udist2 - UL.udist1) * (1 / 2)))
                    _TriCentroid = _curDx - (UL.udist1 + ((UL.udist2 - UL.udist1) * (1 / 3)))
                    BM = (_RectF * _RectCentroid) + _
                         (_TriF * _TriCentroid)
                ElseIf ((_curDx >= UL.udist1) And (_curDx < UL.udist2)) Then
                    _SecF = UL.uload2 + _
                            (((UL.uload1 - UL.uload2) / (UL.udist2 - UL.udist1)) * (UL.udist2 - _curDx))
                    _RectF = _SecF * (_curDx - UL.udist1)
                    _TriF = 0.5 * (UL.uload1 - _SecF) * (_curDx - UL.udist1)
                    _RectCentroid = _curDx - (UL.udist1 + ((_curDx - UL.udist1) * (1 / 2)))
                    _TriCentroid = _curDx - (UL.udist1 + ((_curDx - UL.udist1) * (1 / 3)))
                    BM = (_RectF * _RectCentroid) + _
                         (_TriF * _TriCentroid)
                End If
                T_BM = T_BM + BM
            End If
        Next
        Return T_BM
    End Function

    Private Function BM_moment_L(ByVal _curDx As Double, ByVal Fmem As Member) As Double
        '----- Bending moment due to moment from left to right
        Dim BM, T_BM As Double
        For Each ML In Fmem.Mload
            BM = 0
            If _curDx >= ML.mdist Then
                BM = -ML.mload
            End If
            T_BM = T_BM + BM
        Next
        Return T_BM
    End Function

    Private Sub FixBendingMoment_Coords()
        FixBendingMoment_CurveCoords()
        FixBendingMoment_ShowCoords()
    End Sub

    Private Sub FixBendingMoment_CurveCoords()
        '-------Finding maximum Bending Moment for scaling the shear curve
        Dim maxV As Double = 0
        For Each Bmoment In BM
            If Math.Abs(Bmoment) > maxV Then
                maxV = Math.Abs(Bmoment)
            End If
        Next
        MomentM = maxV
        maxV = 180 / maxV

        '--------Fixing the curve coordinates
        ReDim BMpts(BM.Count - 1)
        Dim f As Integer = 0
        For Each B In BM
            BMpts(f).X = BeamCoords(f)
            BMpts(f).Y = (B * maxV) + (beamcreate.MEheight / 2)
            f = f + 1
        Next
    End Sub

    Private Sub FixBendingMoment_ShowCoords()
        '------Capturing the Bending Moment indexes for displaying
        Dim CDSindex As New List(Of Integer)
        Dim S_sor As Double = 0
        For Each S In SF
            If (S > 0 And S_sor < 0) Or (S < 0 And S_sor > 0) Then
                If Math.Round(BM(SF.IndexOf(S)), 2) <> 0 Then
                    CDSindex.Add(SF.IndexOf(S))
                End If
            End If
            S_sor = S
        Next
        '------Capturing Moment Location
        Dim L As Double = 0
        For Each E In mem
            For Each M In E.Mload
                Dim I As Integer = DX.IndexOf(L + M.mdist)
                CDSindex.Add(I - 1)
                CDSindex.Add(I + 1)
            Next
            L = L + E.spanlength
        Next

        If mem(0).DOF(1) = 0 Then
            CDSindex.Add(0)
        End If
        If mem(mem.Count - 1).DOF(3) = 0 Then
            If Math.Round(BM(SF.Count - 1), 2) <> 0 Then
                CDSindex.Add(SF.Count - 1)
            ElseIf Math.Round(BM(SF.Count - 2), 2) <> 0 Then
                CDSindex.Add(SF.Count - 2)
            ElseIf Math.Round(BM(SF.Count - 3), 2) <> 0 Then
                CDSindex.Add(SF.Count - 3)
            ElseIf Math.Round(BM(SF.Count - 4), 2) <> 0 Then
                CDSindex.Add(SF.Count - 4)
            End If
        End If

        BMMc.Clear()
        ReDim BMmaxs(CDSindex.Count - 1)
        Dim f As Integer = 0
        For Each CD In CDSindex
            BMMc.Add(CD)
            BMmaxs(f).X = BMpts(CD).X
            BMmaxs(f).Y = BMpts(CD).Y + If(BMpts(CD).Y > (beamcreate.MEheight / 2), 15, -30)
            f = f + 1
        Next
    End Sub
#End Region

#Region "Slope and deflection Calculator"
    Private Function GaussQuadrature_3Point(ByVal b As Double, ByVal a As Double, ByVal FRmem As Member, ByVal _Curx As Double)
        Dim delta As Double = (b - a) / 2
        Const C1 = 0.555555556
        Const C2 = 0.888888889
        Const C3 = 0.555555556


        Const X1 = -0.774596669
        Const X2 = 0.0
        Const X3 = 0.774596669


        Dim FX1 As Double = Total_BendingMoment_L(((delta * X1) + ((b + a) / 2)), FRmem)
        Dim FX2 As Double = Total_BendingMoment_L(((delta * X2) + ((b + a) / 2)), FRmem)
        Dim FX3 As Double = Total_BendingMoment_L(((delta * X3) + ((b + a) / 2)), FRmem)

        Dim Integration As Double
        Integration = delta * ((C1 * FX1) + (C2 * FX2) + (C3 * FX3))
        Return Integration
    End Function

    Private Sub FixDisplacement_Slope_Deflection(ByVal FRmem As Member)
        DE.Clear()
        SL.Clear()

        '------- Fixing the disintegration
        Dim J As Integer = 0
        Dim _TDx(mem.Count - 1) As List(Of Double)
        Dim TotLength As Double = 0
        For Each E In mem
            J = 0
            _TDx(mem.IndexOf(E)) = New List(Of Double)
            For Each X In DX
                If X >= TotLength And X < (TotLength + E.spanlength) Then
                    _TDx(mem.IndexOf(E)).Add(X - TotLength)
                    J = J + 1
                End If
            Next
            TotLength = TotLength + E.spanlength
        Next


        '--------First Integration
        Dim _FirstIntegration As New List(Of Double)
        Dim LowerLimit As Double = 0
        Dim UpperLimit As Double = 0
        Dim CummulativeBM As Double = 0
        LowerLimit = DX(0)
        UpperLimit = (DX(0) + DX(1)) / 2
        CummulativeBM = (GaussQuadrature_3Point(UpperLimit, LowerLimit, FRmem, DX(0)))
        _FirstIntegration.Add(CummulativeBM)
        For i = 1 To DX.Count - 2
            LowerLimit = (DX(i - 1) + DX(i)) / 2
            UpperLimit = (DX(i) + DX(i + 1)) / 2
            CummulativeBM = CummulativeBM + (GaussQuadrature_3Point(UpperLimit, LowerLimit, FRmem, DX(i)))
            _FirstIntegration.Add(CummulativeBM)
        Next
        LowerLimit = (DX(DX.Count - 2) + DX(DX.Count - 1)) / 2
        UpperLimit = DX(DX.Count - 1)
        CummulativeBM = (CummulativeBM + GaussQuadrature_3Point(UpperLimit, LowerLimit, FRmem, DX(DX.Count - 1)))
        _FirstIntegration.Add(CummulativeBM)


        '--------Second Integration
        Dim _SecondIntegration As New List(Of Double)
        LowerLimit = 0
        UpperLimit = 0
        Dim CummulativeSL As Double = 0
        LowerLimit = DX(0)
        UpperLimit = (DX(0) + DX(1)) / 2
        CummulativeSL = (((UpperLimit - LowerLimit) / 2) * (_FirstIntegration(0)))
        _SecondIntegration.Add(CummulativeSL)
        For i = 1 To DX.Count - 2
            LowerLimit = (DX(i - 1) + DX(i)) / 2
            UpperLimit = (DX(i) + DX(i + 1)) / 2
            CummulativeSL = CummulativeSL + (((UpperLimit - LowerLimit) / 2) * (_FirstIntegration(i - 1) + _FirstIntegration(i + 1)))
            _SecondIntegration.Add(CummulativeSL)
        Next
        LowerLimit = (DX(DX.Count - 2) + DX(DX.Count - 1)) / 2
        UpperLimit = DX(DX.Count - 1)
        CummulativeSL = CummulativeSL + (((UpperLimit - LowerLimit) / 2) * (_FirstIntegration(DX.Count - 1)))
        _SecondIntegration.Add(CummulativeSL)


        '--------Finding Slope and deflection incorporating the integration constants
        J = 0
        Dim c1 As Double
        TotLength = 0
        Dim c3 As Double
        For Each E In mem
            c1 = c1 + (E.DISP(1))
            c3 = c3 + (E.DISP(0))
            For Each X In _TDx(mem.IndexOf(E))
                SL.Add(((_FirstIntegration(J) / (E.Inertia * E.Emodulus)) + c1))
                DE.Add(((_SecondIntegration(J) / (E.Inertia * E.Emodulus)) + (c1 * (TotLength + X)) + c3))
                J = J + 1
            Next
            c1 = c1 - (E.DISP(3))
            c3 = c3 - (E.DISP(2))
            TotLength = TotLength + E.spanlength
        Next
    End Sub

    Private Function EI_member(ByVal X As Double) As Double
        Dim XI As Double = 0
        Dim EI As Double
        For Each E In mem
            If X >= XI And X <= XI + E.spanlength Then
                EI = E.spanlength * E.Inertia
                Exit For
            End If
            XI = XI + E.spanlength
        Next
        Return EI
    End Function

    Private Sub FixSlopeDeflection_Coords()
        FixSlope_CurveCoords()
        FixDeflection_CurveCoords()
        FixSlope_ShowCoords()
        FixDeflection_ShowCoords()
    End Sub

    Private Sub FixSlope_CurveCoords()
        '-------Finding maximum shear force for scaling the shear curve
        Dim maxV As Double = 0
        For Each Slope In SL
            If Math.Abs(Slope) > maxV Then
                maxV = Math.Abs(Slope)
            End If
        Next
        SlopeM = maxV
        maxV = 180 / maxV

        '--------Fixing the curve coordinates
        ReDim SLpts(SL.Count - 1)
        Dim f As Integer = 0

        For Each S In SL
            SLpts(f).X = BeamCoords(f)
            SLpts(f).Y = (S * maxV) + (beamcreate.MEheight / 2)
            f = f + 1
        Next
    End Sub

    Private Sub FixSlope_ShowCoords()
        '------Capturing the Slope indexes for displaying
        Dim CDSindex As New List(Of Integer)
        Dim S_sor As Double = 0
        For Each B In BM
            If (Math.Round(B, 3) > 0 And S_sor < 0) Or (Math.Round(B, 3) < 0 And S_sor > 0) Then
                If Math.Round(SL(BM.IndexOf(B)), 8) <> 0 Then
                    CDSindex.Add(BM.IndexOf(B))
                End If
            End If
            S_sor = Math.Round(B, 3)
        Next
        'If mem(0).DOF(1) = 0 Then
        '    CDSindex.Add(0)
        'End If
        'If mem(mem.Count - 1).DOF(3) = 0 Then
        '    If Math.Round(BM(SF.Count - 1), 2) <> 0 Then
        '        CDSindex.Add(SF.Count - 1)
        '    ElseIf Math.Round(BM(SF.Count - 2), 2) <> 0 Then
        '        CDSindex.Add(SF.Count - 2)
        '    ElseIf Math.Round(BM(SF.Count - 3), 2) <> 0 Then
        '        CDSindex.Add(SF.Count - 3)
        '    ElseIf Math.Round(BM(SF.Count - 4), 2) <> 0 Then
        '        CDSindex.Add(SF.Count - 4)
        '    End If
        'End If

        SLMc.Clear()
        ReDim SLmaxs(CDSindex.Count - 1)
        Dim f As Integer = 0
        For Each CD In CDSindex
            SLMc.Add(CD)
            SLmaxs(f).X = SLpts(CD).X
            SLmaxs(f).Y = SLpts(CD).Y + If(SLpts(CD).Y > (beamcreate.MEheight / 2), 15, -30)
            f = f + 1
        Next
    End Sub

    Private Sub FixDeflection_CurveCoords()
        '-------Finding maximum shear force for scaling the shear curve
        Dim maxV As Double = 0
        For Each Deflection In DE
            If Math.Abs(Deflection) > maxV Then
                maxV = Math.Abs(Deflection)
            End If
        Next
        DeflectionM = maxV
        maxV = 180 / maxV

        '--------Fixing the curve coordinates
        ReDim DEpts(DE.Count - 1)
        Dim f As Integer = 0

        For Each D In DE
            DEpts(f).X = BeamCoords(f)
            DEpts(f).Y = (D * maxV) + (beamcreate.MEheight / 2)
            f = f + 1
        Next
    End Sub

    Private Sub FixDeflection_ShowCoords()
        '------Capturing the Slope indexes for displaying
        Dim CDSindex As New List(Of Integer)
        Dim S_sor As Double = 0
        For Each S In SL
            If (Math.Round(S, 8) > 0 And S_sor < 0) Or (Math.Round(S, 8) < 0 And S_sor > 0) Then
                If Math.Round(DE(SL.IndexOf(S)), 8) <> 0 Then
                    CDSindex.Add(SL.IndexOf(S))
                End If
            End If
            S_sor = Math.Round(S, 8)
        Next
        'If mem(0).DOF(1) = 0 Then
        '    CDSindex.Add(0)
        'End If
        'If mem(mem.Count - 1).DOF(3) = 0 Then
        '    If Math.Round(BM(SF.Count - 1), 2) <> 0 Then
        '        CDSindex.Add(SF.Count - 1)
        '    ElseIf Math.Round(BM(SF.Count - 2), 2) <> 0 Then
        '        CDSindex.Add(SF.Count - 2)
        '    ElseIf Math.Round(BM(SF.Count - 3), 2) <> 0 Then
        '        CDSindex.Add(SF.Count - 3)
        '    ElseIf Math.Round(BM(SF.Count - 4), 2) <> 0 Then
        '        CDSindex.Add(SF.Count - 4)
        '    End If
        'End If

        DEMc.Clear()
        ReDim DEmaxs(CDSindex.Count - 1)
        Dim f As Integer = 0
        For Each CD In CDSindex
            DEMc.Add(CD)
            DEmaxs(f).X = DEpts(CD).X
            DEmaxs(f).Y = DEpts(CD).Y + If(DEpts(CD).Y > (beamcreate.MEheight / 2), 15, -30)
            f = f + 1
        Next
    End Sub
#End Region
#End Region
End Module
