using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeakingPets
{
    public class ModConfig
    {
        public List<string> DogLines { get; set; }
        public List<string> CatLines { get; set; }

        /// <summary>
        /// Whether the pet can leave the cabin.
        /// If false, will always reside in the cabin.
        /// </summary>
        public bool CanExitCabin { get; set; }

        /// <summary>
        /// Whether the pet can awake in other cabins on Multiplayer.
        /// If false, only awakes in Player 1's cabin
        /// </summary>
        public bool CanVisitOtherCabins { get; set; }
    }
}
