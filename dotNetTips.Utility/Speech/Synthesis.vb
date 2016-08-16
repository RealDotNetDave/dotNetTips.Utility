' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : David McCarter
' Created          : 05-31-2016
'
' Last Modified By : David McCarter
' Last Modified On : 07-22-2016
' ***********************************************************************
' <copyright file="Synthesis.vb" company="NicheWare - David McCarter">
'     '     '     NicheWare - David McCarter
'
'
' </copyright>
' <summary></summary>
' *************************************************************************
Imports System.Threading.Tasks
Namespace Speech
    ''' <summary>
    ''' Synthesis helper methods.
    ''' </summary>
    Public Module Synthesis
        ''' <summary>
        ''' _speech
        ''' </summary>
        Private ReadOnly _speech As New System.Speech.Synthesis.SpeechSynthesizer()

        ''' <summary>
        ''' Speaks the asynchronous.
        ''' </summary>
        ''' <param name="textToSpeak">The text to speak.</param>
        ''' <returns>Task(Of System.Speech.Synthesis.Prompt).</returns>
        Public Function SpeakAsync(ByVal textToSpeak As String) As Task(Of System.Speech.Synthesis.Prompt)
            If _speech.GetInstalledVoices.Count > 0 Then
                _speech.SpeakAsyncCancelAll()
                Return Task(Of System.Speech.Synthesis.Prompt).Factory.StartNew(Function() _speech.SpeakAsync(textToSpeak))
            Else
                Return Nothing
            End If
        End Function

        ''' <summary>
        ''' Speaks the specified textxt to speak.
        ''' </summary>
        ''' <param name="textToSpeak">The text to speak.</param>
        Public Sub Speak(ByVal textToSpeak As String)
            If _speech.GetInstalledVoices.Count > 0 Then
                _speech.SpeakAsyncCancelAll()
                _speech.Speak(textToSpeak)
            End If
        End Sub
    End Module
End Namespace