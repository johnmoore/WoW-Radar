// WoW Radar
// Copyright (C) 2010 John Moore
// 
// Original code by jbrauman of MMOwned.com (http://www.mmowned.com/forums/world-of-warcraft/bots-programs/203457-source-code-wow-radar-application.html)
// MemoryReader.dll is not an original work of John Moore
// 
// This program is not associated with or endorsed by Blizzard Entertainment in any way. 
// World of Warcraft is copyright of Blizzard Entertainment.
//
//
// http://www.programiscellaneous.com/programming-projects/world-of-warcraft/wow-radar/what-is-it/
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WoWRadar
{
    //Offsets for 4.0.1 build 13164
    //Thanks to MMOwned.com users

    public static class ClientOffsets
    {
        public static uint
            CurrMgr = 0x8BF1A8, //0x8B3F78,
            CurrMgrEx = 0x462C, //0x462C,
            FirstObjectOffset = 0xB4, //good
            LocalGuidOffset = 0xB8, //good
            NextObjectOffset = 0x3C, //good
            LocalPlayerGUID = 0x008BD838,
            LocalTargetGUID = 0x99C6B8, //0x98F6B8,
            CurrentContinent = 0x7B76A0; //0xBFF84C;
    }

    public static class NameOffsets
    {
        public static uint
            nameStore = 0x89ACC0 + 0x8, //0x88EA90 + 0x8,
            nameMask = 0x024, //good
            nameBase = 0x01C, //good
            nameString = 0x020, //good
            PlayerName = 0x8BF1E0, //0x8B2FB0,
            mobName = 0xA24, //good
            mobNameEx = 0x60, //good
            continentName = 0x9162A0,
            continentNameEx = 0x28DC;
    }

    public enum ObjectOffsets : uint
    {
        Type = 0x14,
        Pos_X = 0x898 + 0x4,
        Pos_Y = 0x898,
        Pos_Z = 0x8A0,
        Rot = 0x8A8,
        Guid = 0x30,
        UnitFields = 0x8,
        Node_Pos_X = 0x110,
        Node_Pos_Y = ObjectOffsets.Node_Pos_X + 0x4,
        Node_Pos_Z = ObjectOffsets.Node_Pos_X + 0x8
    }

    public enum UnitOffsets : uint
    {
        Level = (0x8 + 0x40) * 4,
        Health = (0x8 + 0x12) * 4,
        Energy = (0x8 + 0x13) * 4,
        MaxHealth = (0x8 + 0x1E) * 4,
        SummonedBy = (0x8 + 0x8) * 4,
        MaxEnergy = (0x8 + 0x1F) * 4
    }

    public enum MineNodes : int
    {
        Copper = 310,
        Tin = 315,
        Incendicite = 384,
        Silver = 314,
        Iron = 312,
        Indurium = 384,
        Gold = 311,
        LesserBloodstone = 48,
        Mithril = 313,
        Truesilver = 314,
        DarkIron = 2571,
        SmallThorium = 3951,
        RichThorium = 3952,
        ObsidianChunk = 6650,
        FelIron = 6799,
        Adamantite = 6798,
        Cobalt = 7881,
        Nethercite = 6650,
        Khorium = 6800,
        Saronite = 7804,
        Titanium = 6798
    }

    public enum HerbNodes : int
    {
        Peacebloom = 269,
        Silverleaf = 270,
        Earthroot = 414,
        Mageroyal = 268,
        Briarthorn = 271,
        Stranglekelp = 700,
        Bruiseweed = 358,
        WildSteelbloom = 371,
        GraveMoss = 357,
        Kingsblood = 320,
        Liferoot = 677,
        Fadeleaf = 697,
        Goldthorn = 698,
        KhadgarsWhisker = 701,
        Wintersbite = 699,
        Firebloom = 2312,
        PurpleLotus = 2314,
        ArthasTears = 2310,
        Sungrass = 2315,
        Blindweed = 2311,
        GhostMushroom = 389,
        Gromsblood = 2313,
        GoldenSansam = 4652,
        Dreamfoil = 4635,
        MountainSilversage = 4633,
        Plaguebloom = 4632,
        Icecap = 4634,
        BlackLotus = 4636,
        Felweed = 6968,
        DreamingGlory = 6948,
        Terocone = 6969,
        Ragveil = 6949,
        FlameCap = 6966,
        AncientLichen = 6967,
        Netherbloom = 6947,
        NightmareVine = 6946,
        ManaThistle = 6945,
        TalandrasRose = 7865,
        Goldclover = 7844,
        AddersTongue = 8084
    }
    public enum WoWGameObjectType : uint
    {
        Door = 0,
        Button = 1,
        QuestGiver = 2,
        Chest = 3,
        Binder = 4,
        Generic = 5,
        Trap = 6,
        Chair = 7,
        SpellFocus = 8,
        Text = 9,
        Goober = 0xa,
        Transport = 0xb,
        AreaDamage = 0xc,
        Camera = 0xd,
        WorldObj = 0xe,
        MapObjTransport = 0xf,
        DuelArbiter = 0x10,
        FishingNode = 0x11,
        Ritual = 0x12,
        Mailbox = 0x13,
        AuctionHouse = 0x14,
        SpellCaster = 0x16,
        MeetingStone = 0x17,
        Unkown18 = 0x18,
        FishingPool = 0x19,
        FORCEDWORD = 0xFFFFFFFF,
    }
}
