using System.IO;
using System.Threading.Tasks;
using Xabe.FFmpeg;

namespace SlamReborn.Core
{
    public class Converter
    {
        public async Task ConvertToWavAsync(string trackFile, string outputFile)
        {
            //maybe edit paths
            FFmpeg.SetExecutablesPath("..\\..\\..\\ffmpeg");
            //string inputFile = $"..\\..\\..\\Music\\{trackFile}" + "." + "mp3";
            //string outputFile = $"..\\..\\..\\..\\{trackFile}" + "." + "wav";

            string inputFile = trackFile;
            string trackName = Path.GetFileNameWithoutExtension(inputFile);
            var snippet = await FFmpeg.Conversions.FromSnippet.Convert(inputFile, outputFile);
            await snippet.Start();
        }
    }
}
