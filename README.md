# Speaking Pets
This mod spawns your cat or dog in your cabin at the start of the day and displays a random dialog line above their head.
Configurable dialog lines and ability for pet to leave your cabin and visit other players on multiplayer saves.

# Configuration
To configure the dialog lines and mod functionality, edit the values in config.json.
The included config provides multiple lines out of the box!

Options:

 * DogLines: List of dialog lines for the dog
 * CatLines: List of dialog lines for the dog
 * CanExitCabin: Whether the pet has a random chance of leaving the cabin and roaming outside after the dialog message disappeares.
 * CanVisitOtherCabins: Whether the pet can wake up in other players cabins at the start of the day(player will be picked at random).

# Installing
Install https://smapi.io/ if you don't have it.

Download the latest release from https://github.com/bizzycola/SpeakingPets/releases

Place the SpeakingPets folder from inside the zip file into your smapi Mods folder and play the game!

# Developing
If you decide to pull the project and work on it, only to find it doesn't load the packages, you need to update `SpeakingPets/SpeakingPets.csproj`.

Specifically, you need to update this:
```
<GamePath>E:\SteamLibrary\steamapps\common\Stardew Valley</GamePath>
```

and change it to the path you have your game located in.

# Screenshot
![preview image](https://i.imgur.com/OHhpIMA.png)