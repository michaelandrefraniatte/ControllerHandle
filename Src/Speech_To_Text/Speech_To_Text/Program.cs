using System;
using System.Threading;

namespace Speech_To_Text
{
    internal class Program
    {
        private static Ozeki.Media.Microphone microphone;
        private static Ozeki.Media.MediaConnector connector;
        private static Ozeki.Media.SpeechToText speechToText;
        private static string TextFromSpeech;
        private static string[] SpeechToText;
        static void Main(string[] args)
        {
            SpeechToText = new string[] { "Start", "Stop" };
            if (SpeechToText.Length != 0)
            {
                microphone = Ozeki.Media.Microphone.GetDefaultDevice();
                connector = new Ozeki.Media.MediaConnector();
                SetupSpeechToText(SpeechToText);
            }
            Console.ReadLine();
            StopSpeechToText();
        }
        static void SetupSpeechToText(string[] speechwords)
        {
            speechToText = Ozeki.Media.SpeechToText.CreateInstance(speechwords);
            speechToText.WordRecognized += SpeechToText_WordsRecognized;
            connector.Connect(microphone, speechToText);
            microphone.Start();
        }
        private static void SpeechToText_WordsRecognized(object sender, Ozeki.Media.SpeechDetectionEventArgs e)
        {
            TextFromSpeech = e.Word;
            Console.WriteLine(TextFromSpeech);
            Thread.Sleep(100);
            TextFromSpeech = "";
        }
        private static void StopSpeechToText()
        {
            try
            {
                speechToText.WordRecognized -= SpeechToText_WordsRecognized;
            }
            catch { }
            try
            {
                connector.Disconnect(microphone, speechToText);
            }
            catch { }
            try
            {
                speechToText.Dispose();
            }
            catch { }
            try
            {
                microphone.Stop();
            }
            catch { }
        }
    }
}