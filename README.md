# Resource Pack Updater
Resource Pack Updater allows you to automatically change the pack format number for Minecraft resource packs, without having to go through each one and changing them individually.

Note: This only works for resource packs as folders and not zipped archives. If you have any resource packs with the .zip extension, extract them to  a folder first.

## How to Use
1. Download the program and run ResourcePackUpdater.exe.
2. Click "browse" and select the folder that contains your resource packs. If you don't want to update all of your resource packs, create a new folder and move the ones you want to update into the new folder. Select the new folder in ResourcePackUpdater.
3. Enter the new pack format number.
You can find the pack format number for your target version at: https://minecraft.fandom.com/wiki/Tutorials/Creating_a_resource_pack#.22pack_format.22
4. Check "Format pack.mcmeta" if you would like to format the pack.mcmeta files according to JSON format. Leave this unchecked if you want the contents to only be on one line.
5. Click "Update" and the program will update your resource packs. All changes will be logged in the textbox below, you can press the "Clear" button to clear all logs.

- "completed" means that the resource pack has been successfully updated.
- "skipped" means that the resource pack does not contain a valid pack.mcmeta file.
- "already updated" means that the resource pack already has the new pack format number.
- "reformatted" means that the resource pack has already been updated, but reformatted based on your checkbox selection.

## How This Works
1. The program reads all sub-folders inside your selected resource pack folder.
2. For each sub-folder, the program checks to see if there is a pack.mcmeta file inside.
3. If a pack.mcmeta file exists and contains the "pack_format" tag, the file is processed as a JSON and the pack_format value is changed to the inputted new pack format.
4. All changes are written back to the pack.mcmeta file.

Made in C# using Visual Studio.