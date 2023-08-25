using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using System.Collections.Generic;

namespace npc_action_trigger.Client
{
    public class NPC : BaseScript
    {
        private int ID = -1;
        private string name = null;
        private bool messageInfoNeeded = true;
        private Action<string, int> action;

        public NPC(string name, string model, float x, float y, float z, float r)
        {
            uint uidModel = (uint)GetHashKey(model);

            this.ID = CreatePed(
                        4,
                        uidModel,
                        x,
                        y,
                        z,
                        r,
                        false,
                        true
                      );

            this.name = name;

            ExecuteActions();
        }

        private async Task ExecuteActions()
        {
            Vector3 currectCoords;
            Vector3 coordsOfNPC;

            while (true)
            {
                await Delay(0);

                if (action == null) {continue;}

                currectCoords = GetEntityCoords(PlayerPedId(), false);
                coordsOfNPC = GetEntityCoords(this.ID, false);

                bool around = MathU.CheckIfCoordsAreInRadeo(
                    currectCoords.X,
                    currectCoords.Y,
                    currectCoords.Z,
                    coordsOfNPC.X,
                    coordsOfNPC.Y,
                    coordsOfNPC.Z,
                    1.25f
                );

                if (around)
                {
                    if (this.messageInfoNeeded)
                    {
                        Exports["notification"].send($"{this.name}", $"SERVER INFO", $"Salut, ma numesc {this.name} apasa [E] sa interactionezi cu mine!");
                        this.messageInfoNeeded = false;
                    }

                    if (IsControlJustReleased(0, 38))
                    {
                        this.action(this.name, this.ID);
                    }
                } 
                else 
                {
                    this.messageInfoNeeded = true;
                }

            }

        }

        public void SetAction(Action<string, int> action)
        {
            this.action = action;
        }

        public int GetID() { return this.ID; }
        public string GetName() { return this.name; }
    }
}