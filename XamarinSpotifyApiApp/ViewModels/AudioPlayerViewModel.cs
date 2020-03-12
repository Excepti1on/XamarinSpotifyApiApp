using System;
using System.ComponentModel;
using System.Windows.Input;
using XamarinSpotifyApiApp.Services;
using Xamarin.Forms;

namespace XamarinSpotifyApiApp.ViewModels
{
	public class AudioPlayerViewModel : INotifyPropertyChanged
	{
		private IAudioPlayerServices _audioPlayer;
		private bool _isStopped;
		public event PropertyChangedEventHandler PropertyChanged;
		public string _preview_url;

		public AudioPlayerViewModel(IAudioPlayerServices audioPlayer, string preview_url)
		{
			_audioPlayer = audioPlayer;
			_audioPlayer.OnFinishedPlaying = () => {
				_isStopped = true;
				CommandText = "Play";
			};
			CommandText = "Play";
			_isStopped = true;
			_preview_url = preview_url;
		}

		private string _commandText;
		public string CommandText
		{
			get { return _commandText; }
			set
			{
				_commandText = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CommandText"));
			}
		}

		private ICommand _playPauseCommand;
		public ICommand PlayPauseCommand
		{
			get
			{
				return _playPauseCommand ?? (_playPauseCommand = new Command(
					(obj) =>
					{
						if (CommandText == "Play")
						{
							if (_isStopped)
							{
								_isStopped = false;
								_audioPlayer.Play(_preview_url);
							}
							else
							{
								_audioPlayer.Play();
							}
							CommandText = "Pause";
						}
						else
						{
							_audioPlayer.Pause();
							CommandText = "Play";
						}
					}));
			}
		}
	}
}