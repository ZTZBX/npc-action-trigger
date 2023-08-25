using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using System.Collections.Generic;

namespace npc_action_trigger.Client
{
    public class ModelsLoader : BaseScript
    {
        private static List<string> modelLoaded = new List<string>();

        public static async Task GetNPCModel(string modelName)
        {

            if (modelLoaded.Contains(modelName)) {  ModelsToLoad.data.Remove(modelName); return; }

            uint uid = (uint)GetHashKey(modelName);
            RequestModel(uid);
            while (HasModelLoaded(uid) == false)
            {
                RequestModel(uid);
                await Delay(100);
            }

            ModelsToLoad.data.Remove(modelName);
        }


    }
}