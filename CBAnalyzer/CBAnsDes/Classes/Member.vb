
<Serializable()> _
Public Class Member
    Private _E As Single
    Private _I As Single
    Private _g As Single
    Private _spanlength As Single
    Private _rect As System.Drawing.Rectangle
    <Serializable()> _
    Public Structure P
        Private _pload As Single
        Private _pdist As Single
        Private _rect As System.Drawing.Rectangle
        Public Property pload() As Single
            Get
                Return _pload
            End Get
            Set(ByVal value As Single)
                _pload = value
            End Set
        End Property
        Public Property pdist() As Single
            Get
                Return _pdist
            End Get
            Set(ByVal value As Single)
                _pdist = value
            End Set
        End Property
        Public Property rect() As System.Drawing.Rectangle
            Get
                Return _rect
            End Get
            Set(ByVal value As System.Drawing.Rectangle)
                _rect = value
            End Set
        End Property
    End Structure
    <Serializable()> _
    Public Structure U
        Private _uload1 As Single
        Private _uload2 As Single
        Private _udist1 As Single
        Private _udist2 As Single
        Private _rect As System.Drawing.Rectangle
        Public Property uload1() As Single
            Get
                Return _uload1
            End Get
            Set(ByVal value As Single)
                _uload1 = value
            End Set
        End Property
        Public Property uload2() As Single
            Get
                Return _uload2
            End Get
            Set(ByVal value As Single)
                _uload2 = value
            End Set
        End Property
        Public Property udist1() As Single
            Get
                Return _udist1
            End Get
            Set(ByVal value As Single)
                _udist1 = value
            End Set
        End Property
        Public Property udist2() As Single
            Get
                Return _udist2
            End Get
            Set(ByVal value As Single)
                _udist2 = value
            End Set
        End Property
        Public Property rect() As System.Drawing.Rectangle
            Get
                Return _rect
            End Get
            Set(ByVal value As System.Drawing.Rectangle)
                _rect = value
            End Set
        End Property
    End Structure
    <Serializable()> _
    Public Structure M
        Private _mload As Single
        Private _mdist As Single
        Private _rect As System.Drawing.Rectangle
        Public Property mload() As Single
            Get
                Return _mload
            End Get
            Set(ByVal value As Single)
                _mload = value
            End Set
        End Property
        Public Property mdist() As Single
            Get
                Return _mdist
            End Get
            Set(ByVal value As Single)
                _mdist = value
            End Set
        End Property
        Public Property rect() As System.Drawing.Rectangle
            Get
                Return _rect
            End Get
            Set(ByVal value As System.Drawing.Rectangle)
                _rect = value
            End Set
        End Property
    End Structure

    Private _pload As New List(Of P)
    Private _uload As New List(Of U)
    Private _mload As New List(Of M)

    Public DOF(0 To 3) As Boolean
    Public FER(0 To 3) As Single
    Public stiff(0 To 3, 0 To 3) As Single
    Public RES(0 To 3) As Single
    Public DISP(0 To 3) As Single

    Private _Fshear As List(Of Single)
    Private _Bmoment As List(Of Single)
    Private _VDeflection As List(Of Single)
    Private _slope As List(Of Single)

    Public Property spanlength() As Single
        Get
            Return _spanlength
        End Get
        Set(ByVal value As Single)
            _spanlength = value
        End Set
    End Property

    Public Property Emodulus() As Single
        Get
            Return _E
        End Get
        Set(ByVal value As Single)
            _E = value
        End Set
    End Property

    Public Property Inertia() As Single
        Get
            Return _I
        End Get
        Set(ByVal value As Single)
            _I = value
        End Set
    End Property

    Public Property g() As Single
        Get
            Return _g
        End Get
        Set(ByVal value As Single)
            _g = value
        End Set
    End Property

    Public Property rect() As System.Drawing.Rectangle
        Get
            Return _rect
        End Get
        Set(ByVal value As System.Drawing.Rectangle)
            _rect = value
        End Set
    End Property

    Public Property Pload() As List(Of P)
        Get
            Return _pload
        End Get
        Set(ByVal value As List(Of P))
            _pload = value
        End Set
    End Property

    Public Property Uload() As List(Of U)
        Get
            Return _uload
        End Get
        Set(ByVal value As List(Of U))
            _uload = value
        End Set
    End Property

    Public Property Mload() As List(Of M)
        Get
            Return _mload
        End Get
        Set(ByVal value As List(Of M))
            _mload = value
        End Set
    End Property

    Public Property Fshear() As List(Of Single)
        Get
            Return _Fshear
        End Get
        Set(ByVal value As List(Of Single))
            _Fshear = value
        End Set
    End Property

    Public Property Bmoment() As List(Of Single)
        Get
            Return _Bmoment
        End Get
        Set(ByVal value As List(Of Single))
            _Bmoment = value
        End Set
    End Property

    Public Property VDeflection() As List(Of Single)
        Get
            Return _VDeflection
        End Get
        Set(ByVal value As List(Of Single))
            _VDeflection = value
        End Set
    End Property

    Public Property Slope() As List(Of Single)
        Get
            Return _slope
        End Get
        Set(ByVal value As List(Of Single))
            _slope = value
        End Set
    End Property
End Class
