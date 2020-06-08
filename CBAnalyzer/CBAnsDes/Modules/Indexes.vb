Module Indexes
    Public Const max_width As Integer = 2600
    Public mem As New List(Of Member)
    Public ends As Integer
    Public Zm As Double = 1
    Public selline As Integer = -1
    Public Tselline As Integer = -1
    Public Lselline As Integer = -1
    Public BeamCoords As New List(Of Double)
    Public DX As New List(Of Double)
    Public SF As New List(Of Double)
    Public BM As New List(Of Double)
    Public DE As New List(Of Double)
    Public SL As New List(Of Double)
    Public SFMc As New List(Of Integer)
    Public BMMc As New List(Of Integer)
    Public DEMc As New List(Of Integer)
    Public SLMc As New List(Of Integer)
    Public ShearM, MomentM, DeflectionM, SlopeM As Double
    Public SFpts(SF.Count), SFmaxs(0) As System.Drawing.PointF
    Public BMpts(SF.Count), BMmaxs(0) As System.Drawing.PointF
    Public DEpts(SF.Count), DEmaxs(0) As System.Drawing.PointF
    Public SLpts(SF.Count), SLmaxs(0) As System.Drawing.PointF
End Module
