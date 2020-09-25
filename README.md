# Drowsiness Detector

**Drowsiness Detector** is a simple console application made using .NET Core as a *University Project*. The main idea was to implement a system using .NET Core so that it may detect if a person is drowsy or not by detecting eye blinks.\\
Though similar projects are quite available, most of them are in *Python* and other languages. Even while developing this project, we actually used references from those projects. But to our best of knowledge, this complete set of drowsiness detection using .NET Core was not available. So, we decided to try in implementing it.\\

From here on after I will explain on how to run this project on your PC. There are some requirements that your PC must have. It must have the following components installed, or rather we had it installed when developing the project. Those are:

* Microsoft Visual Studio 2019 Community Version (Free from official website)
* .NET Core

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
    * Go to your NuGet package installation location in your PC. In our case, it was as seen in the screen shot, C:\Users\User\.nuget\packages\
    This location may change (or may not) depending on your installation (not sure though, did not experiment it)
    * Double click dlibdotnet. Then you should see another folder showing the version of the package, double click that also. Then double click on runtimes. Now the directory I am in is: ```C:\Users\User\.nuget\packages\dlibdotnet\19.20.0.20200725\runtimes```. From the screenshot, it can be seen that a win-AnyCPU folder is expected, but in the directory, there is not any. Now to resolve it.
    * Create a new folder, name it *win-AnyCPU*
    * I am using a x64 windows, so from ```C:\Users\User\.nuget\packages\dlibdotnet\19.20.0.20200725\runtimes``` I double clicked on win-x64 and copied the both `native` and `lib` folders. Then I pasted it in ```C:\Users\User\.nuget\packages\dlibdotnet\19.20.0.20200725\runtimes\win-AnyCPU```
    * Try buildin the project again. Problems should not persist.
4. The project is ready to go in your PC.
