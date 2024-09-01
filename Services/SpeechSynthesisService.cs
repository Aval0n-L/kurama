using System.Speech.Synthesis;
using Kurama.Models;
using Kurama.Services.Interfaces;
using NAudio.Wave;

namespace Kurama.Services;

public class SpeechSynthesisService : ISpeechSynthesisService
{
    private readonly IGlossaryService _glossaryService;

    // SpeechSynthesizer - Used to read text out loud.
    private readonly SpeechSynthesizer _synthesizer;

    private readonly Random _random = new();

    public SpeechSynthesisService(IGlossaryService glossaryService)
    {
        _glossaryService = glossaryService;
        _synthesizer = new SpeechSynthesizer();
        _synthesizer.SetOutputToDefaultAudioDevice();
        _synthesizer.SelectVoiceByHints(VoiceGender.Male);
    }

    /*
     * Task.Run
     * Used to perform speech synthesis in a background thread so as not to block the main UI thread.
     */

    public async Task SpeakAsync(string text)
    {
        _synthesizer.SpeakAsyncCancelAll();

        _synthesizer.Speak(text);

        //_synthesizer.SpeakStarted += (sender, e) => StartAmplitudeVisualization();
        //_synthesizer.SpeakCompleted += (sender, e) => StopAmplitudeVisualization();

        //await Task.Run(() =>
        //{
        //    using (var waveOut = new WaveOutEvent())
        //    using (var reader = new WaveFileReader(CreateSpeechWaveStream(text)))
        //    {
        //        waveOut.Init(reader);
        //        waveOut.Play();

        //        byte[] buffer = new byte[reader.WaveFormat.SampleRate];
        //        int bytesRead;

        //        while ((bytesRead = reader.Read(buffer, 0, buffer.Length)) > 0)
        //        {
        //            double[] amplitudes = new double[bytesRead / 2];
        //            for (int i = 0; i < bytesRead; i += 2)
        //            {
        //                short sample = BitConverter.ToInt16(buffer, i);
        //                amplitudes[i / 2] = sample / 32768.0; // [-1.0, 1.0]
        //            }

        //            OnAmplitudeChanged?.Invoke(amplitudes);
        //            Task.Delay(100).Wait();
        //        }
        //    }
        //});
    }

    public void SpeakViaGlossaryAsync(string text, IEnumerable<Glossary> glossary)
    {
        _synthesizer.SpeakAsyncCancelAll();

        var values = _glossaryService.GetGlossaryByKey(glossary, text);
        text = values[_random.Next(0, values.Length)];

        _synthesizer.Speak(text);
    }

    //private bool _isSpeaking;
    //public event Action<double[]>? OnAmplitudeChanged;


    //private void StartAmplitudeVisualization()
    //{
    //    _isSpeaking = true;
    //}

    //private void StopAmplitudeVisualization()
    //{
    //    _isSpeaking = false;
    //    OnAmplitudeChanged?.Invoke(new double[50]);
    //}
}