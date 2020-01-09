Public Class Form1
    Private _CheckColumnName As String = "CHK"
    Private _ValueColumnName As String = "VALUE"
    Private _CheckColumnIndex As Integer = 0
    Private _ValueColumnIndex As Integer = 2

    Private _EstaPulsandoCtrl As Boolean = False
    Private _EstaPulsandoShift As Boolean = False
    Private _RegistrosSeleccionados As IList(Of String) = Nothing
    Private _SelectionChanged As Boolean = False
    Private _CellClick As Boolean = False
    Private _CellValueChanged As Boolean = False
    Private _UltimoIndiceSeleccionado As Integer = 0


    Private _EnProcesoInterno As Boolean = True



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim gen As New GenerarDatos

        Me.DataGridView1.DataSource = gen.GeneraDataTable
        Me.DataGridView1.Columns(0).ReadOnly = True
        Me.DataGridView1.Columns(0).Name = _CheckColumnName
        Me.DataGridView1.Columns(1).ReadOnly = True
        Me.DataGridView1.Columns(1).Visible = False
        Me.DataGridView1.Columns(2).ReadOnly = True
        Me.DataGridView1.Columns(2).Visible = True
        Me.DataGridView1.Columns(2).Name = _ValueColumnName

        _EnProcesoInterno = False

    End Sub

    

    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyValue = Keys.ControlKey Then
            _EstaPulsandoCtrl = True
            ' DataGridView1.MultiSelect = True
        End If
        If e.KeyValue = Keys.ShiftKey Then
            _EstaPulsandoShift = True
            ' DataGridView1.MultiSelect = True
        End If
    End Sub

    Private Sub DataGridView1_KeyUp(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyUp
        If e.KeyValue = Keys.ControlKey Then
            _EstaPulsandoCtrl = False
            '  DataGridView1.MultiSelect = False
        End If
        If e.KeyValue = Keys.ShiftKey Then
            _EstaPulsandoShift = False
            ' DataGridView1.MultiSelect = False
        End If
    End Sub

    Private Sub DataGridView1_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        ' Sólo se puede cambiar el checkbox

    End Sub
    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If Not _EnProcesoInterno Then
            RemoveHandler DataGridView1.SelectionChanged, AddressOf DataGridView1_SelectionChanged
            RemoveHandler DataGridView1.CellClick, AddressOf DataGridView1_CellClick
            RemoveHandler DataGridView1.CellValueChanged, AddressOf DataGridView1_CellValueChanged
            ' Sólo se puede cambiar el checkbox, AddressOf DataGridView1_CellValueChanged
            If _EstaPulsandoCtrl Then
                Me.DataGridView1.CurrentRow.Cells(_CheckColumnIndex).Value = Me.DataGridView1.CurrentRow.Selected

            Else
                If _EstaPulsandoShift Then
                    '  If Me.DataGridView1.SelectedRows.Count > 1 Then
                    'If MouseButtons.HasFlag(MouseButtons.Left) Then ' Está manteniendo click pulsado
                    '    Me.DataGridView1.CurrentRow.Cells(_CheckColumnIndex).Value = True
                    '    Exit Sub

                    'End If
                    Dim indice As Integer = Me.DataGridView1.CurrentRow.Index
                    Dim ind1, ind2, indiceInicial, indiceFinal As Integer
                    ind1 = Math.Min(_UltimoIndiceSeleccionado, Me.DataGridView1.CurrentRow.Index)
                    ind2 = Math.Max(_UltimoIndiceSeleccionado, Me.DataGridView1.CurrentRow.Index)
                    'ind1 = ObtenerMenorIndiceSeleccionado()
                    'ind2 = ObtenerMayorIndiceSeleccionado()
                    'If indice > ind2 Then
                    '    indiceInicial = ind2
                    '    indiceFinal = indice
                    'Else

                    '    If indice < ind1 Then
                    '        indiceInicial = indice
                    '        indiceFinal = ind1
                    '    Else
                    '        ' índice seleccionado entre el menor y el mayor -> Cojo el último índice seleccionado
                    '        If indice > _UltimoIndiceSeleccionado Then
                    '            indiceInicial = _UltimoIndiceSeleccionado
                    '            indiceFinal = indice
                    '        Else
                    '            indiceInicial = indice
                    '            indiceFinal = _UltimoIndiceSeleccionado
                    '        End If
                    '    End If
                    'End If

                    CheckearTodosONinguno(False)
                    'For k = indiceInicial To indiceFinal
                    '    Me.DataGridView1.Rows(k).Cells(_CheckColumnIndex).Value = True
                    'Next
                    For k = ind1 To ind2

                        Me.DataGridView1.Rows(k).Cells(_CheckColumnIndex).Value = True
                    Next
                    '    If Me.DataGridView1.Rows(k).Selected Then
                    '        Me.DataGridView1.Rows(k).Cells(_CheckColumnIndex).Value = True
                    '    End If


                    'Next

                Else

                    CheckearTodosONinguno(False)
                    Me.DataGridView1.CurrentRow.Cells(_CheckColumnIndex).Value = Me.DataGridView1.CurrentRow.Selected
                End If



            End If
            _UltimoIndiceSeleccionado = Me.DataGridView1.CurrentRow.Index

            SeleccionarChequeados()
            _SelectionChanged = True

            AddHandler DataGridView1.SelectionChanged, AddressOf DataGridView1_SelectionChanged
            AddHandler DataGridView1.CellClick, AddressOf DataGridView1_CellClick
            AddHandler DataGridView1.CellValueChanged, AddressOf DataGridView1_CellValueChanged
        End If
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        ' Vendrá por el click o porque se haya cambiado la selección
        If Not _EnProcesoInterno Then
            If _SelectionChanged = True Then
                _SelectionChanged = False
                Exit Sub
            End If
            Dim check As Boolean
            RemoveHandler DataGridView1.SelectionChanged, AddressOf DataGridView1_SelectionChanged
            RemoveHandler DataGridView1.CellClick, AddressOf DataGridView1_CellClick
            RemoveHandler DataGridView1.CellValueChanged, AddressOf DataGridView1_CellValueChanged
            ' Sólo se puede cambiar el checkbox, AddressOf DataGridView1_CellValueChanged
            check = Not Me.DataGridView1.CurrentRow.Cells(_CheckColumnIndex).Value

            If _EstaPulsandoCtrl Then

            Else

                CheckearTodosONinguno(False)


            End If
            _UltimoIndiceSeleccionado = Me.DataGridView1.CurrentRow.Index

            Me.DataGridView1.CurrentRow.Cells(_CheckColumnIndex).Value = check
            SeleccionarChequeados()
            AddHandler DataGridView1.SelectionChanged, AddressOf DataGridView1_SelectionChanged
            AddHandler DataGridView1.CellClick, AddressOf DataGridView1_CellClick
            AddHandler DataGridView1.CellValueChanged, AddressOf DataGridView1_CellValueChanged
        End If


    End Sub

    Private Sub CheckearTodosONinguno(chequear As Boolean)
        _EnProcesoInterno = True

        If chequear Then
            If _RegistrosSeleccionados Is Nothing Then
                _RegistrosSeleccionados = New List(Of String)
            End If
        Else
            _RegistrosSeleccionados = Nothing
        End If

        For Each row As DataGridViewRow In Me.DataGridView1.Rows
            row.Cells(_CheckColumnName).Value = chequear
            'If row.Selected <> chequear Then
            '    row.Selected = chequear
            'End If
            If chequear Then
                _RegistrosSeleccionados.Add(row.Cells(_ValueColumnName).Value.ToString)
            End If
        Next
        '  AddHandler dgvDatos.CellValueChanged, AddressOf DataGridView1_CellValueChanged

        _EnProcesoInterno = False
    End Sub
    Private Sub SeleccionarChequeados()
        ' RemoveHandler DataGridView1.SelectionChanged, AddressOf DataGridView1_SelectionChanged
        _EnProcesoInterno = True
        For Each dr As DataGridViewRow In Me.DataGridView1.Rows
            dr.Selected = False
            If Not dr.Cells(_CheckColumnName).Value Is Nothing Then
                If Boolean.Parse(dr.Cells(_CheckColumnName).Value.ToString) = True Then
                    dr.Selected = True
                End If
            End If
        Next
        '  AddHandler DataGridView1.SelectionChanged, AddressOf DataGridView1_SelectionChanged
        _EnProcesoInterno = False

    End Sub
    Private Function ObtenerMenorIndiceSeleccionado() As Integer
        Dim n As Integer = DataGridView1.Rows.Count - 1
        For Each elementoseleccionado As DataGridViewRow In Me.DataGridView1.SelectedRows
            If elementoseleccionado.Index < n Then
                n = elementoseleccionado.Index
            End If
        Next
        Return n
    End Function
    Private Function ObtenerMayorIndiceSeleccionado() As Integer
        Dim n As Integer = 0
        For Each elementoseleccionado As DataGridViewRow In Me.DataGridView1.SelectedRows
            If elementoseleccionado.Index > n Then
                n = elementoseleccionado.Index
            End If
        Next
        Return n
    End Function
    Private Function ObtenerIndicesSonContiguos() As Boolean
        Dim n As Integer = 0
        Dim ultimoSeleccionado As Integer = 0
        If Me.DataGridView1.SelectedRows.Count > 0 Then
            ultimoSeleccionado = ObtenerMenorIndiceSeleccionado()
            If ultimoSeleccionado = ObtenerMayorIndiceSeleccionado() Then
                Return True
            End If
            If ultimoSeleccionado = ObtenerMayorIndiceSeleccionado() + 1 Then
                Return True
            End If
            For Each elemento As DataGridViewRow In Me.DataGridView1.Rows
                If elemento.Selected And (elemento.Index - ultimoSeleccionado) > 1 Then
                    Return False
                Else
                    ultimoSeleccionado = elemento.Index
                End If
            Next
        End If

        Return True
    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        MsgBox("Contiguos: " + ObtenerIndicesSonContiguos())
    End Sub
End Class
