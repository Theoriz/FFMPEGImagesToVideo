using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class FFMPEGImagesToVideo : MonoBehaviour
{
	public string workingDirectory = "";
	public string inputName = "image_%04d.png";
	public string outputName = "output.mp4";
	public bool overwrite = false;
	public int startingFrameNumber = 0;
	public float framerate = 50;
	public int compression = 0;
	public bool convertOnQuit = true;
	public bool useAudio = false;
	public float audioStartTime = 0;
	public string audioPath = "";

	private void OnApplicationQuit() {

		if(convertOnQuit)
			FFMPEGConvertImagesToVideo();

	}

	public void FFMPEGConvertImagesToVideo() {

		if (overwrite && File.Exists(workingDirectory + "/" + outputName))
			File.Delete(workingDirectory + "/" + outputName);

		//Launch ffmpeg conversion
		string command = "ffmpeg -framerate " + framerate + " -i " + inputName;

		if (useAudio) {
			command += " -ss " + audioStartTime + " -i " + audioPath + " -c:a copy -shortest ";
		}

		command += " -crf " + compression + " -pix_fmt yuv420p "+ outputName;

		ShellHelper.ProcessCommand(command, workingDirectory);
	}
}
