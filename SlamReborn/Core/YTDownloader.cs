using System.Threading.Tasks;
using YoutubeDLSharp;
using YoutubeDLSharp.Options;
using System;
using System.Runtime.Intrinsics.Arm;
using System.IO;

namespace SlamReborn.Core
{
    public class YTDownloader
    {
        public async Task<RunResult<string>> DownloadVideoByMp3(string reference)
        {
            var ytdl = new YoutubeDL();

            ytdl.YoutubeDLPath = "..\\..\\..\\yt-dlp\\yt-dlp.exe";
            var path = Path.GetFullPath("yt-dlp.exe");
            Console.WriteLine(path);
            if (!File.Exists("..\\..\\..\\yt-dlp\\yt-dlp.exe"))
            {
                throw new FileNotFoundException(path);
            }
            ytdl.FFmpegPath = "..\\..\\..\\ffmpeg\\ffmpeg.exe";
            ytdl.OutputFolder = "..\\..\\..\\Music";
            var res = await ytdl.RunAudioDownload(
                reference,
                AudioConversionFormat.Mp3);

            return res;
        }
    }
}
