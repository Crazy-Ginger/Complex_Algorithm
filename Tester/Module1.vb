﻿Imports System
Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Data
Imports System.Threading
Imports System.Xml

Public Class Node
    Public address As String
    Public original As Integer
End Class

Module Module1

    Sub Main()
        Dim latLngKey As String = "AIzaSyA3A7tDgpFISYEY3B5qYdXm9StRa0pJkcA"
        Dim waypointkey As String = "AIzaSyBqN-1pDwR8taEDQESDP5mnJjiJkIXmv-w"

        Dim node_list As New List(Of String)
        'Dim nodel As New Node
        Dim cont As Boolean = True

        Console.Write("Your first destination: ")
        'nodel.address = Console.ReadLine()
        'nodel.original = node_list.Count
        'node_list.Add(nodel)
        node_list.Add(Console.ReadLine())

        List_print(node_list, True)

        While cont = True Or node_list.Count < 2
            Dim nodeel As New Node
            Console.Write("Next destination: ")
            'nodeel.address = Console.ReadLine()
            'nodeel.original = node_list.Count
            node_list.Add(Console.ReadLine())

            List_print(node_list, True)

            Console.Write("Continue? ")
            cont = Console.ReadLine()
        End While

        Console.Write("Do you have a final destination? ")
        cont = Console.ReadLine()
        If cont = True Then
            Console.Write("Final destination: ")
            'Dim nodeel As New Node
            node_list.Add(Console.ReadLine())
        End If
        Console.Clear()
        List_print(node_list, False)

        Faith_Permute(node_list.Count, node_list, cont)
        Console.ReadLine()
    End Sub

    Function List_print(ByRef list As List(Of String), clear As Boolean)
        If clear = True Then
            Console.Clear()
        End If
        For i As Integer = 0 To list.Count - 1
            Console.WriteLine("Address: " & list.Item(i))
        Next
        Console.WriteLine()
        Return Nothing
    End Function


    Public Function Approximation(ByVal length As Integer, ByVal nodes As List(Of String))
        Dim order(length - 1) As String
        Dim nodesLeft As Integer = length - 1
        Dim currentNode As String = nodes.Item(0)
        For i As Integer = 0 To nodesLeft
            Dim shortestRoute As Integer = 2147483647

        Next
        Return nodes
    End Function


    'Sub Permute(n As Integer, ByRef nodes As List(Of String))
    '    'Generate, count and print (if printem is not false) all permutations of first n integers
    '    'https://rosettacode.org/wiki/Permutations#VBA

    '    Dim P(n) As Integer
    '    Dim t As Integer, i As Integer, j As Integer, k As Integer
    '    Dim count As Long
    '    Dim Last As Boolean

    '    For o As Integer = 0 To n - 1
    '        P(o) = o + 1
    '        Console.Write(P(o) & ", ")
    '    Next
    '    count = 0
    '    Last = False

    '    Do While Not Last
    '        'Print_arrangement(P, nodes)
    '        For l As Integer = 0 To n - 1
    '            Console.Write(P(l) & ", ")
    '        Next
    '        Console.WriteLine()
    '        count = count + 1
    '        Last = True
    '        i = n - 1

    '        Do While i > 0

    '            If P(i) < P(i + 1) Then
    '                Last = False
    '                Exit Do
    '            End If
    '            i = i - 1
    '        Loop

    '        j = i + 1
    '        k = n

    '        While j < k
    '            ' Swap p(j) and p(k)
    '            t = P(j)
    '            P(j) = P(k)
    '            P(k) = t
    '            j = j + 1
    '            k = k - 1
    '        End While

    '        j = n - 1
    '        While P(j) > P(i)
    '            j = j - 1
    '        End While

    '        j = j
    '        'Swap p(i) and p(j)
    '        t = P(i)
    '        P(i) = P(j)
    '        P(j) = t
    '    Loop 'While not last

    '    Console.WriteLine("Number of permutations: " & count)
    'End Sub


    Function Waypointing(ByVal start As String, ByVal finish As String, ByVal key As String)
        Dim output As StringBuilder = New StringBuilder()

        'Dim request As System.Net.HttpWebRequest = System.Net.HttpWebRequest.Create("https://maps.googleapis.com/maps/api/directions/xml?origin=" & start & "&destination=" & finish & "&key=" & key)
        Dim request As System.Net.HttpWebRequest = System.Net.HttpWebRequest.Create("https://maps.googleapis.com/maps/api/directions/xml?origin=HD22EB&destination=Manchester&key=AIzaSyBqN-1pDwR8taEDQESDP5mnJjiJkIXmv-w")

        Using client = New WebClient()

            Dim response As System.Net.HttpWebResponse = request.GetResponse()
            If response.StatusCode = System.Net.HttpStatusCode.OK Then
                Dim stream As System.IO.Stream = response.GetResponseStream()
                Dim IOreader As New System.IO.StreamReader(stream)
                Dim contents As String = IOreader.ReadToEnd()
                Dim xmldoc As New System.Xml.XmlDocument()
                xmldoc.LoadXml(contents)

                Using xmlreader As XmlReader = XmlReader.Create(contents)
                    xmlreader.ReadToFollowing("<duration>")
                    xmlreader.MoveToNextAttribute()
                    Dim time As String = xmlreader.Value
                    output.AppendLine("It will take: " & time & " to get to " & finish)
                    output.AppendLine("This is: " & ((CInt(time)) \ 360) & " hours and " & ((CInt(time)) Mod 360) & "minutes")
                End Using
            End If
        End Using
        Return output
    End Function


    Sub MyHandler(sender As Object, args As UnhandledExceptionEventArgs)
        Dim e As Exception = DirectCast(args.ExceptionObject, Exception)
        Console.WriteLine("MyHandler caught : " + e.Message)
        Console.WriteLine("Runtime terminating: {0}", args.IsTerminating)
    End Sub


    Function Connected(website) As Boolean
        Try
            Return My.Computer.Network.Ping(website)
        Catch
            Return False
        End Try
    End Function


    Function FindLat(ByVal destination As String, ByVal key As String)
        'https://www.aspsnippets.com/Articles/Find-Co-ordinates-Latitude-And-Longitude-of-an-Address-Location-using-Google-Geocoding-API-in-ASPNet-using-C-And-VBNet.aspx
        Dim url2 As String = "https://maps.googleapis.com/maps/api/geocode/json?address=" & destination & "&key=" & key

        'links to google's API stuff (really really useful)  https://developers.google.com/maps/documentation/geocoding/start?hl=en_US
        'https://developers.google.com/maps/documentation/directions/intro#Waypoints
        'https://developers.google.com/maps/documentation/directions/?hl=en_US

        Dim request As WebRequest = WebRequest.Create(url2)
        Using response As WebResponse = DirectCast(request.GetResponse(), HttpWebResponse)
            Using reader As New StreamReader(response.GetResponseStream(), Encoding.UTF8)
                Dim dsResult As New DataSet()
                dsResult.ReadXml(reader)
                Dim dtCoordinates As New DataTable()
                dtCoordinates.Columns.AddRange(New DataColumn(3) {New DataColumn("Id", GetType(Integer)), New DataColumn("Address", GetType(String)), New DataColumn("Latitude", GetType(String)), New DataColumn("Longitude", GetType(String))})
                Dim geometry_id As String = dsResult.Tables("geometry").[Select]("result_id = " + ("result_id").ToString())(0)("geometry_id").ToString()
                Dim location As DataRow = dsResult.Tables("location").[Select](Convert.ToString("geometry_id = ") & geometry_id)(0)
                'dtCoordinates.Rows.Add(row("result_id"), row("formatted_address"), location("lat"), location("lng"))
                Return location("Lat")
            End Using
        End Using
    End Function
End Module
