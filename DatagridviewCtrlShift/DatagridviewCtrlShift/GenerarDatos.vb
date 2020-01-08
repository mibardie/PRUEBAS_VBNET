Public Class GenerarDatos
    Public Function GeneraDataTable() As DataTable
        Dim datatable As New DataTable("mTabla")
        Dim columna1 As New DataColumn("chk", GetType(Boolean))
        Dim columna2 As New DataColumn("id")
        Dim columna3 As New DataColumn("valor")

        datatable.Columns.Add(columna1)
        datatable.Columns.Add(columna2)
        datatable.Columns.Add(columna3)

        Dim dr1 As DataRow = datatable.NewRow
        dr1("chk") = False
        dr1("id") = 1
        dr1("valor") = "ejemplo1"

        Dim dr As DataRow

        For k = 0 To 12
            dr = datatable.NewRow
            dr("chk") = False
            dr("id") = k
            dr("valor") = "Valor_" & k
            datatable.Rows.Add(dr)
        Next

        Return datatable

    End Function
End Class
