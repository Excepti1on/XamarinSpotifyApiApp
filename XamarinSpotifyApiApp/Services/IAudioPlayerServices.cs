using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinSpotifyApiApp.Services
{
    public interface IAudioPlayerServices
    {
        void Play(string pathToAudioFile);
        void Play();
        void Pause();
        Action OnFinishedPlaying { get; set; }
    }
}
