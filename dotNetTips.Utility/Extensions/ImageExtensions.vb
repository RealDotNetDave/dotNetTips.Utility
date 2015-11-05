'***********************************************************************
' Assembly         : dotNetTips
' Author           : David McCarter
' Created          : 12-10-2008
'
' Last Modified By : David McCarter
' Last Modified On : 06-09-2009
' Description      :
'
' Copyright        : (c) dotNetTips.com. All rights reserved.
'***********************************************************************
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Drawing.Drawing2D
Imports System.Runtime.CompilerServices
Imports dotNetTips.Utility.Extensions
Imports System.Diagnostics.Contracts

Namespace Extensions
    ''' <summary>
    ''' Extensions for Image class
    ''' </summary>
    ''' <remarks></remarks>
    Public Module ImageExtensions
        ''' <summary>
        ''' Converts image to bytes.
        ''' </summary>
        ''' <param name="image">The image.</param>
        ''' <param name="format">The format.</param>
        ''' <returns></returns>
        ''' <remarks>
        ''' Code by: Lucas
        ''' http://code.msdn.microsoft.com/LucasExtensions
        ''' </remarks>
        <System.Runtime.CompilerServices.Extension()> _
        Public Function ToBytes(ByVal image As Image, ByVal format As ImageFormat) As Byte()
            Contracts.Contract.Requires(Of ArgumentNullException)(format IsNot Nothing)
            Contracts.Contract.Requires(Of ArgumentNullException)(image IsNot Nothing)

            Using stream As New MemoryStream()
                image.Save(stream, format)
                Return stream.ToArray()
            End Using

        End Function

        ''' <summary>
        ''' Converts image to byte array.
        ''' </summary>
        ''' <param name="image">The image.</param>
        ''' <returns>System.Byte().</returns>
        <System.Runtime.CompilerServices.Extension()> _
        Public Function ToBytes(ByVal image As Image) As Byte()
            Return image.ToBytes(image.ImageFormat)
        End Function

        ''' <summary>
        ''' Converts image to byte array.
        ''' </summary>
        ''' <param name="image"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <System.Runtime.CompilerServices.Extension()>
        Public Function ToByteArray(image As Image, imageFormat As ImageFormat) As Byte()
            Contract.Requires(Of ArgumentNullException)(image IsNot Nothing)
            Contract.Requires(Of ArgumentNullException)(imageFormat IsNot Nothing)

            Using ms As New MemoryStream
                image.Save(ms, imageFormat)
                Return ms.GetBuffer()
            End Using

        End Function

        ''' <summary>
        ''' Converts Image to a byte array.
        ''' </summary>
        ''' <param name="image">The image.</param>
        ''' <returns>System.Byte().</returns>
        <System.Runtime.CompilerServices.Extension()>
        Public Function ToByteArray(image As Image) As Byte()
            Return image.ToByteArray(image.ImageFormat)
        End Function

        ''' <summary>
        ''' Converts image to base 64 string.
        ''' </summary>
        ''' <param name="image"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <System.Runtime.CompilerServices.Extension()>
        Public Function ToBase64(image As Image, imageFormat As ImageFormat) As String
            Return Convert.ToBase64String(image.ToByteArray(imageFormat))
        End Function

        ''' <summary>
        ''' Gets the image codec info.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>
        ''' Code by: Lucas
        ''' http://code.msdn.microsoft.com/LucasExtensions
        ''' </remarks>
        <System.Runtime.CompilerServices.Extension()> _
        Public Function CodecInfo(ByVal image As Image) As ImageCodecInfo
            Contract.Requires(Of ArgumentNullException)(image IsNot Nothing)

            Dim codec = ImageCodecInfo.GetImageEncoders().FirstOrDefault(Function(i) i.Clsid = image.RawFormat.Guid)

            Return codec
        End Function

        ''' <summary>
        ''' Gets the bounds.
        ''' </summary>
        ''' <param name="image">The image.</param>
        ''' <returns></returns>
        ''' <remarks>
        ''' Code by: Lucas
        ''' http://code.msdn.microsoft.com/LucasExtensions
        ''' </remarks>
        <System.Runtime.CompilerServices.Extension()> _
        Public Function Bounds(ByVal image As Image) As Rectangle
            Contract.Requires(Of ArgumentNullException)(image IsNot Nothing)

            Return New Rectangle(0, 0, image.Width, image.Height)
        End Function

        ''' <summary>
        ''' Surrounds the specified Point.
        ''' </summary>
        ''' <param name="p">The p.</param>
        ''' <param name="distance">The distance.</param>
        ''' <returns></returns>
        ''' <remarks>
        ''' Code by: Lucas
        ''' http://code.msdn.microsoft.com/LucasExtensions
        ''' </remarks>
        <System.Runtime.CompilerServices.Extension()> _
        Public Function Surround(ByVal p As Point, ByVal distance As Integer) As Rectangle
            Return New Rectangle(p.X - distance, p.Y - distance, distance * 2, distance * 2)
        End Function

        ''' <summary>
        ''' Crops an Image.
        ''' </summary>
        ''' <param name="image"></param>
        ''' <param name="r"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()> _
        Public Function Crop(image As Image, ByVal r As Rectangle) As Image
            Contract.Requires(Of ArgumentNullException)(image IsNot Nothing)

            Dim bmp As New Bitmap(r.Width, r.Height, PixelFormat.Format24bppRgb)
            bmp.SetResolution(80, 60)

            Using gfx As Graphics = Graphics.FromImage(bmp)
                gfx.SmoothingMode = SmoothingMode.AntiAlias
                gfx.InterpolationMode = InterpolationMode.HighQualityBicubic
                gfx.PixelOffsetMode = PixelOffsetMode.HighQuality
                gfx.DrawImage(image, New Rectangle(0, 0, r.Width, r.Height), r, GraphicsUnit.Pixel)
            End Using

            Return bmp

        End Function

        ''' <summary>
        ''' Resizes the specified value.
        ''' </summary>
        ''' <param name="value">The value.</param>
        ''' <param name="size">The size.</param>
        ''' <returns>Image.</returns>
        <Extension()>
        Public Function Resize(value As Image, size As Size) As Image
            Contract.Requires(Of ArgumentNullException)(value IsNot Nothing)

            Dim resizedImage = value.GetThumbnailImage(size.Width, size.Height, Nothing, System.IntPtr.Zero)

            Return resizedImage
        End Function

        ''' <summary>
        ''' Finds the format of an image.
        ''' </summary>
        ''' <param name="image"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Public Function ImageFormat(image As Image) As ImageFormat
            Dim format = GetType(System.Drawing.Imaging.ImageFormat).GetProperties(System.Reflection.BindingFlags.[Public] Or System.Reflection.BindingFlags.[Static]).ToList().ConvertAll(Function([property]) [property].GetValue(Nothing, Nothing)).[Single](Function(f) f.Equals(image.RawFormat))

            Return DirectCast(format, ImageFormat)

        End Function

        ''' <summary>
        ''' To the icon.
        ''' </summary>
        ''' <param name="IconFromResources">The icon from resources.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()>
        Public Function ToIcon(ByVal IconFromResources As Bitmap) As Icon
            Dim myIcon As Bitmap = IconFromResources
            Return Icon.FromHandle(myIcon.GetHicon)
        End Function

    End Module
End Namespace