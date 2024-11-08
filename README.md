# MsDyn365BC.Custom.Analyzer Template

This is a Template that helps you to get started with developing your own custom code Analyzer for the AL Lanuage.

## Setup after creating a new Repo from this Template

The Template is intended to be plug and play, so you should already see your first Pipeline run being started and (hopefully) succeed. It should also already have a new draft release for you created.

### Rename the Create Analyzer

The default name for the analyzer will be `CustomCodeCop.dll`. If you want to change this name I would highly recommend you to do this using vs code since you can search & replace across all files. You can use vs code in the web for this. Wen you are at you github repository just press the `.` (dot) key on your keyboard.
Just search for `CustomCodeCop` and replace it with your inteded name. This name should be best be PascalCase as well. 
Its also important to rename the `CustomCodeCop.csproj` in the same way.
You might want to delete the Draft release created since it will contain dlls from the original name, but this is not mandatory as it wont break anything.

## Starting the Codespace

After that, the Repo should be ready for getting started with development.

I recommend you use GitHub Codespaces for getting started, as the ContainerPrep script will take care about everything that needs to be set up.
If you prefer to run it locally, just know that you will need to have dotnet installed. The Prep Script might need a few tweaks as well.

Once the Codespace is ready, use `F1` to open the command pallete and search for `Run Task`, choose the `Prep Codespace` task from the list.

At the end of this file, it will try to open new tab of vs code within the codespace, and there might be a popup asking you if you want to continue, confirm that.
This will open the test AL project for you so you can debug your analyzer.

Also note, that the app create does not have any dependencies, also not against Mircosoft Application, that way we do not need any symbols.

## Debugging

For debugging you will first need to rebuild the project to have it reflect the latest changes, if you did not do so already, clone the second tab with the AL Project. Otherwise the AL Lanauage server, which is responsible for the diagnosics/warnings in vs code, might block the .dll and prevent the Compiler from replacing it.

Now once you have the changes in the Analyzer code itsself, run the tasks again, this time choose `Build` it will run build dotnet and open vs code at once. So you will get a new compiled version and it will ask you again to open the AL Project after it finished compiling.

I recommend you wait a few moments until the first diagnostics are coming up, that way you can be sure that everything is loaded and up and running.

Now go back to the Analyzer project, set your breakpoint and press `F5`. This will open up a menu where you need to select the process to attach to. For this project this will be the `Microsoft.Dynamics.Nav.EditorServices.Host`, but you should find it if you just start to type `Dynamics`. Sometime there are multiple process running, just select the one with the highest process id.

Keep an eye on the break point you have set, before debugging it should have been red, right during attaching it will turn into a grey circle. If it does not turn red again after a few seconds, it means that the process you attached to, does not run the same code you are looking it. The reason could be that the compile did not work or that you did not attach to the right process. If that happens just repeat the build and attach.

After th debugger is attached correclty, you can switch to the AL Project again and resave the file you want to debug in, that will retrigger the analyzer.

And thats it, that should get you started.

If you need any inspiration, head over to the https://github.com/StefanMaron/BusinessCentral.LinterCop and have a look at the existing rules there.