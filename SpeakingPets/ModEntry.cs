using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using StardewValley.Characters;
using Newtonsoft.Json;
using System.Threading.Tasks;
using xTile;
using xTile.ObjectModel;

namespace SpeakingPets
{
    public class ModEntry : Mod
    {
        private readonly Random _rng;
        private ModConfig _config;

        public ModEntry()
        {
            _rng = new Random();
            
        }

        public override void Entry(IModHelper helper)
        {
            _config = Helper.ReadConfig<ModConfig>();

            helper.Events.GameLoop.DayStarted += GameLoop_DayStarted;
            helper.Events.GameLoop.DayEnding += GameLoop_DayEnding;
            //helper.Events.Player.Warped += Player_Warped;

        }

        // Doesn't work, probably only occurs when player uses warp items.
        private void Player_Warped(object sender, WarpedEventArgs e)
        {
            if (Game1.IsClient) return;

            Monitor.Log($"Debug(warp loc): from {e.OldLocation.Name} to {e.NewLocation.Name}");
            if(e.NewLocation.Name == "Farm" && _config.CanExitCabin)
            {
                var farm = Game1.getFarm();
                if (farm == null) return;

                var pet = FindPet(farm);
                if (pet == null) return;
                if (!(pet is Dog) && !(pet is Cat)) return;

                int x = 0, y = 0;
                Utility.getDefaultWarpLocation("Farm", ref x, ref y);

                Game1.warpCharacter(pet, "Farm", new Point(x, y));
            }
        }

        private Pet FindPet(Farm farm)
        {
            try
            {
                var pet = farm.characters.FirstOrDefault(p => p is Pet);
                if (pet == null) return null;

                return pet as Pet;
            }
            catch
            {
                return null;
            }
        }

        private void GameLoop_DayStarted(object sender, DayStartedEventArgs e)
        {
            try
            {
                if (Game1.IsClient) return;

                var farm = Game1.getFarm();
                if (farm == null) return;

                var pet = FindPet(farm);
                if (pet == null) return;
                if (!(pet is Dog) && !(pet is Cat)) return;

                pet.warpToFarmHouse(Game1.player);
                
                string dialog = (pet is Dog) ?
                     _config.DogLines[_rng.Next(0, _config.DogLines.Count)] :
                     _config.CatLines[_rng.Next(0, _config.CatLines.Count)];

                pet.showTextAboveHead(dialog, 01, 2, 30000);

                // Forced to use this and see pet teleport
                // can't find a way to detect when player leaves cabin
                if (_config.CanExitCabin && _rng.Next(0, 10) < 5) //
                {
                    Task.Run(async() =>
                    {
                        await Task.Delay(45000);

                        int x = 0, y = 0;
                        Utility.getDefaultWarpLocation("Farm", ref x, ref y);

                        Game1.warpCharacter(pet, "Farm", new Point(x, y));
                    });
                }
            }
            catch (Exception ex) { Console.WriteLine($"PET MOD EXCEPTION(DayEnding): {ex}"); }
        }

        private void GameLoop_DayEnding(object sender, DayEndingEventArgs e)
        {
            try
            {
                if (Game1.IsClient) return;

                var farm = Game1.getFarm();
                if (farm == null) return;

                var pet = FindPet(farm);
                if (pet == null) return;

                if (_config.CanVisitOtherCabins && Game1.IsMultiplayer)
                {
                    var farmers = Game1.getOnlineFarmers();
                    var farmer = farmers.ElementAt(_rng.Next(0, farmers.Count));

                    if (farmer != null)
                        pet.warpToFarmHouse(farmer);
                    else
                        pet.warpToFarmHouse(Game1.player);
                }
                else
                    pet.warpToFarmHouse(Game1.player);
            }
            catch(Exception ex) { Console.WriteLine($"PET MOD EXCEPTION(DayEnding): {ex}"); }
        }
    }
}
