using System;
namespace OnlyMusic
{
    public interface IAudio
    {
        bool Play_Pause(string url);
        bool Stop(bool val);
    }
}