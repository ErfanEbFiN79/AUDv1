using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Net;
using UnityEngine;

public class RunPython : MonoBehaviour
{

    #region Variables

    [SerializeField] private string[] package;
    [TextArea]
    [SerializeField] private string path;
    [SerializeField] private float timeRe;

    private float _timeReSet;

    #endregion
    #region MyRegion
    
    private void Update()
    {
        _timeReSet += Time.deltaTime;
        if (_timeReSet >= timeRe)
        {
            _timeReSet = 0;
            foreach (string name in package)
            {
                CheckPackage(name);
            }
            
            StartPython(path);
        }
    }

    #endregion

    #region Systems
    private void CheckPackage(string package)
    {
        print("we CheckPackege");
        string packageName = package; // Replace with the package you want to check

        ProcessStartInfo start = new ProcessStartInfo();
        start.FileName = "cmd.exe";
        start.Arguments = $"/C python -c \"import {packageName}\"";
        start.UseShellExecute = false;
        start.RedirectStandardOutput = true;
        start.RedirectStandardError = true;
        start.CreateNoWindow = true;

        using (Process process = Process.Start(start))
        {
            using (StreamReader reader = process.StandardError)
            {
                string errors = reader.ReadToEnd();
                if (!string.IsNullOrEmpty(errors))
                {
                    Console.WriteLine($"Package {packageName} is not installed.");
                    print($"Package {packageName} is not installed.");
                    InstallPackageNeed(package);
                }
                else
                {
                    Console.WriteLine($"Package {packageName} is installed.");
                    print($"Package {packageName} is installed.");
                }
            }
        }
    }
    private void InstallPackageNeed(string package)
    {
        string packageName = package; // Replace with the package you want to install

        ProcessStartInfo start = new ProcessStartInfo();
        start.FileName = "cmd.exe";
        start.Arguments = $"/C python -m pip install {packageName}";
        start.UseShellExecute = false;
        start.RedirectStandardOutput = true;
        start.RedirectStandardError = true;

        using (Process process = Process.Start(start))
        {
            using (StreamReader output = process.StandardOutput)
            {
                Console.WriteLine(output.ReadToEnd());
                print(output.ReadToEnd());
            }

            using (StreamReader error = process.StandardError)
            {
                Console.WriteLine(error.ReadToEnd());
                print(error.ReadToEnd());
            }
        }
    }
    
    private static void StartPython(string path)
    {
        // Define the path to the Python interpreter (e.g., "python" or the full path to python.exe)
        string pythonInterpreter = "python";

        // Define the path to your Python script
        string pythonScriptPath = path;

        // Create a new process start info
        ProcessStartInfo start = new ProcessStartInfo
        {
            FileName = pythonInterpreter,
            Arguments = $"\"{pythonScriptPath}\"",
            UseShellExecute = false, // Do not use OS shell
            RedirectStandardOutput = true, // Redirect output so we can read it
            RedirectStandardError = true, // Redirect errors so we can read them
            CreateNoWindow = true // Do not create a window for the process
        };

        // Start the process with the info we specified
        using (Process process = Process.Start(start))
        {
            // Read the output from the process
            using (StreamReader reader = process.StandardOutput)
            {
                string result = reader.ReadToEnd();
                Console.WriteLine("\a");
                Console.WriteLine(result);
                print(result);
            }

            // Read any errors from the process
            using (StreamReader reader = process.StandardError)
            {
                string errors = reader.ReadToEnd();
                if (!string.IsNullOrEmpty(errors))
                {
                    print(errors);
                    Console.WriteLine(errors);
                }
            }
        }
    }

    #endregion
}
