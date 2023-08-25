using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using System.Collections.Generic;


namespace npc_action_trigger.Client
{
    public class ClientMain : BaseScript
    {

        public ClientMain()
        {
            // firt wee make a simple npc ho can pass a function and execut it
            EventHandlers["loadNPCModel"] += new Action<string>(LoadNPCModel);
            EventHandlers["modelsAreLoading"] += new Func<bool>(ModelsAreLoading);
            EventHandlers["createNPC"] += new Func<string, string, float, float, float, float, dynamic>(CreateNPC);
        }

        private void LoadNPCModel(string model)
        {
            ModelsLoader.GetNPCModel(model);
            ModelsToLoad.data.Add(model);
        }

        private bool ModelsAreLoading()
        {
            return ModelsToLoad.data.Count == 0;
        }

        private dynamic CreateNPC(string name, string model, float x, float y, float z, float r)
        {
            NPC currentNpc = new NPC(name, model, x, y, z, r);
            return currentNpc;
        }

    }
}