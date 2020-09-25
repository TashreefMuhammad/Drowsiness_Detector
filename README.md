# Drowsiness Detector

**Drowsiness Detector** is a simple console application made using *.NET Core* as a *University Project*. The main idea was to implement a system using *.NET Core* so that it may detect if a person is drowsy or not by detecting eye blinks.

Though similar projects are quite available, most of them are in *Python* and other languages. Even while developing this project, we actually used references from those projects. But to our best of knowledge, this complete set of drowsiness detection using *.NET Core* was not available. So, we decided to try in implementing it. This file explains the project using following components:
* [Building the Project](#building-the-project)
* [Shallow Overview](#shallow-overview)
    * [Program.cs](#programcs)
    * [CameraCapture.cs](#cameracapturecs)
    * [Detect_Landmarks.cs](#detect_landmarkcs)
    * [ImageShow.cs](#imageshowcs)
    * [Notify.cs](#notifycs)
* [Some Extra Tips](#some-extra-tips)
* [References](#references)

## Building the Project

From here on after I will explain on how to run this project on your PC. There are some requirements that your PC must have. It must have the following components installed, or rather we had it installed when developing the project. Those are:

* Microsoft Visual Studio 2019 Community Version (Free from official website)
* .NET Core 3.1
* Good processor
* Web Camera

To the best of our knowledge, no more components are needed.

A step by step guide on how to run the project is given below:
1. After downloding or cloning the project, you should open the solution file using *Visual Studio*. A little heads up is the .NET Core is relatively new and if you are using an older Visual Studio check before hand if it supports .NET Core or not.
2. Try building the project. In the project there are 3 Nuget packages that were used. Normally by building the project the Nuget packages should get installed. If they do not, follow the steps explained below:
    * Click *Project* from top bar and navigate to *Manage NuGet Packages...* and click it.
    ![img1](https://user-images.githubusercontent.com/43475529/94289308-58da0e80-ff7a-11ea-8acb-98bb8e2bf9ba.png)
    * Go to *Browse*. Then write the following 3 package names and install all of them.
        * DlibDotNet 
        * OpenCvSharp4.Windows
        * Pinvoke.Kernel32
        
        ![image](https://user-images.githubusercontent.com/43475529/94290449-04379300-ff7c-11ea-95b6-dec1b953d821.png)
        
        ![image](https://user-images.githubusercontent.com/43475529/94290594-3cd76c80-ff7c-11ea-98d5-980aca837886.png)
        
3. Now build the project. The DlibDotNet Package currently gives an error, hopefully it will be fixed soon. Normally we have seen that first time running in PC, when trying to build the following errors are seen:
![image](https://user-images.githubusercontent.com/43475529/94290998-cedf7500-ff7c-11ea-9998-65488c1b2e32.png)
It is a minor problem. Below we explain how to resolve it:
    * Go to your NuGet package installation location in your PC. In our case, it was as seen in the screen shot, `C:\Users\User\.nuget\packages\`
    This location may change (or may not) depending on your installation (not sure though, did not experiment it)
    * Double click `dlibdotnet`. Then you should see another folder showing the version of the package, double click that also. Then double click on runtimes. Now the directory I am in is: ```C:\Users\User\.nuget\packages\dlibdotnet\19.20.0.20200725\runtimes```. From the screenshot, it can be seen that a win-AnyCPU folder is expected, but in the directory, there is not any. Now to resolve it.
    * Create a new folder, name it *win-AnyCPU*
    * I am using a `x64` windows, so from ```C:\Users\User\.nuget\packages\dlibdotnet\19.20.0.20200725\runtimes``` I double clicked on *win-x64* and copied the both `native` and `lib` folders. Then I pasted it in ```C:\Users\User\.nuget\packages\dlibdotnet\19.20.0.20200725\runtimes\win-AnyCPU```
    * Try buildin the project again. Problems should not persist.
4. The project is ready to go in your PC.

## Shallow Overview
The whole project consists of 4 classes.
* Program.cs
* CameraCapture.cs
* Detect_Landmarks.cs
* ImageShow.cs
* Notify.cs

Among them, `Program.cs` holds the `main` method. The other classes contain methods that help the whole projects together.

### Program.cs
In this class, there is oly one method, the `main` method. Within the main method, Objects of 3 classes are declared and they are assigned to individual threads while calling a class method to run concurrently. The classe are `CameraCapture`, `Detect_Landmarks` and `ImageShow`. By declaring each of the thread to start each object will start some important method execution.

### CameraCapture.cs
The class content is mainly to capture images from the attached webcams with the device. It also saves snapshots taken from the camera to a certain location. Let us consider that the project solution file is in `X` direcory of your PC. You shall find all the camera taken snapshots in the `X\bin\Debug\netcoreapp3.1\rawImage\` folder.

### Detect_Landmarks.cs
This class detects faces and puts marks on diferent parts of face. It can be manipulated to show different types of pattern over people's face. In this class, also the calculation of `EYE ASPECT RATIO` is calculated to find if the person's eye is closed or not. It also stores the landmarked images in the location `X\bin\Debug\netcoreapp3.1\rawImage\` if `X` is the directory containing the solution file.

### ImageShow.cs
This class shows detected landmarked images on the console. Though it mainly has not much of an effect on the complete project but in preliminary stages to understand the facial landmarks working or not or to understand the landmarking system, it is quite an important feature.

### Notify.cs
The class handles the sound that is supposed to alert the listener. In other words when a person shows drowsiness syndrome this alarm will trigger to make him awake.

# Some Extra Tips
This is by size quite a big project. It also consumes a lot of requirements on the hardware side. Here are some suggestions regarding on how to use this project:
*  Build the project. Then close `Visual Studio 2019`. Run the `EXE` file of the project. The performance will be better as Visual Studio will not be gobbling up some hardware parts. If the solution file is in `X` location, you should find the `EXE` file to execute after building in `X\bin\Debug\netcoreapp3.1\DrowsyDoc.exe`
* A better processor will make the project act more and more smooth
* From `Program.cs` you can commnt out the `i_show.start()` statement. As mentioned [earlier](#imageshowcs) it will only help in understanding the work of facial landmark detection on the go. In complete scaled run, it is not that useful, but resource consuming. So the process could be commented out for better performance.

## References
References from multiple websites and resources were used. Some of them can be enlisted as follows:
* [Detect eyes, nose, lips, and jaw with dlib, OpenCV, and Python](https://www.pyimagesearch.com/2017/04/10/detect-eyes-nose-lips-jaw-dlib-opencv-python/)
* [Drowsiness detection with OpenCV](https://www.pyimagesearch.com/2017/05/08/drowsiness-detection-opencv/)
* [Detect Facial Landmark Points With C# And Dlib In Only 50 Lines Of Code](https://medium.com/machinelearningadvantage/detect-facial-landmark-points-with-c-and-dlib-in-only-50-lines-of-code-71ab59f8873f)
* [How to take snapshots with the web camera with C# using the OpenCVSharp library in WinForms](https://ourcodeworld.com/articles/read/761/how-to-take-snapshots-with-the-web-camera-with-c-sharp-using-the-opencvsharp-library-in-winforms)
* [Console.Beep Method (System) | Microsoft Docs](https://docs.microsoft.com/en-us/dotnet/api/system.console.beep?view=netcore-3.1)
* [Multithreading in C# - C# Tutorial | KnowledgeHut](https://www.knowledgehut.com/tutorials/csharp/csharp-multithreading)
* [How to get points coordinate position in the face landmark detection program of dlib?](https://stackoverflow.com/questions/39793680/how-to-get-points-coordinate-position-in-the-face-landmark-detection-program-of)
* [Display a Image in a console application](https://stackoverflow.com/questions/33538527/display-a-image-in-a-console-application)

Hopefully we will try enhancing this project out and implement the feature in hardware level. Thanks for coming this far.
We hope that you found this project useful.
