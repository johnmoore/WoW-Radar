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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Collections;
using System.Diagnostics;

namespace WoWRadar
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            RadarZoom = 10F;
            ZoomFactor = (int)(TILE_HEIGHT * (float)(RadarZoom / 0.5f));
            if (!System.IO.Directory.Exists("Maps"))
            {
                chkMinimap.Checked = false;
                chkMinimap.Enabled = false;
            }
        }

        MinimapData OldMinimap = new MinimapData(0, 0, 0, new Bitmap(1, 1));
        PointF OldPosition;

        uint ObjectManager = 0;
        uint FirstObject = 0;
        uint TotalWowObjects = 0;
        int ZPosRange = 100;
        string CurrentContinent = "";
        float RadarZoom = 10F;
        int ZoomFactor = 0; //this syncs the radar's zoom with the minimap

        static int RadarHeight = 542;
        static int RadarWidth = 542;

        const float TILE_SCALE_FACTOR = 533.5F;
        const int TILE_HEIGHT = 256;


        Boolean RadarIsReady = false;
        Bitmap RadarBitmap = new Bitmap(RadarWidth, RadarHeight);
        Random RNG = new Random();
        ArrayList waypoints = new ArrayList();

        //color definitions
        Color PlayerColor = Color.Blue; 
        Color PlayerDeadColor = Color.DarkBlue;
        Color NpcColor = Color.Green; 
        Color NpcDeadColor = Color.Purple;
        Color PetColor = Color.LightBlue; 
        Color PetDeadColor = Color.Aquamarine;
        Color LocalColor = Color.Black;
        Color TargetColor = Color.DarkGoldenrod;
        Color NodeColor = Color.Gold;

        WowObject LocalPlayer = new WowObject();
        WowObject LocalTarget = new WowObject();
        WowObject CurrentObject = new WowObject();
        WowObject TempObject = new WowObject();

        MemoryReader.Memory WowReader = new MemoryReader.Memory();

        private float RadianToDegree(float Rotation)
        {
            return (float)(Rotation * (180 / Math.PI));
        }

        public string MobNameFromGuid(ulong Guid)
        {
            uint ObjectBase = GetObjectBaseByGuid(Guid);
            return WowReader.ReadString((IntPtr)(WowReader.ReadUInt32((IntPtr)(WowReader.ReadUInt32((IntPtr)(ObjectBase + NameOffsets.mobName)) + NameOffsets.mobNameEx))));
        }

        // Credits WhatSupMang, SillyBoy72 of MMowned.com
        public string PlayerNameFromGuid(ulong guid)
        {
            return "???";
            ulong mask, base_, offset, current, shortGUID, testGUID;

            mask = WowReader.ReadUInt32((IntPtr)((uint)NameOffsets.nameStore + (uint)NameOffsets.nameMask));
            base_ = WowReader.ReadUInt32((IntPtr)((uint)NameOffsets.nameStore + (uint)NameOffsets.nameBase));

            shortGUID = guid & 0xFFFFFFFF; 
            offset = 12 * (mask & shortGUID);

            current = WowReader.ReadUInt32((IntPtr)(base_ + offset + 8));
            offset = WowReader.ReadUInt32((IntPtr)(base_ + offset));

            if ((current & 0x1) == 0x1) { return ""; }

            testGUID = WowReader.ReadUInt32((IntPtr)(current));

            while (testGUID != shortGUID)
            {
                current = WowReader.ReadUInt32((IntPtr)(current + offset + 4));

                if ((current & 0x1) == 0x1) { return ""; }
                testGUID = WowReader.ReadUInt32((IntPtr)(current));
            }

            return WowReader.ReadString((IntPtr)(current + NameOffsets.nameString));
        }

        private uint GetObjectBaseByGuid(ulong Guid)
        {
            TempObject.BaseAddress = FirstObject;

            while (TempObject.BaseAddress != 0)
            {
                TempObject.Guid = WowReader.ReadUInt64((IntPtr)(TempObject.BaseAddress + ObjectOffsets.Guid));
                if (TempObject.Guid == Guid)
                    return TempObject.BaseAddress;
                TempObject.BaseAddress = WowReader.ReadUInt32((IntPtr)(TempObject.BaseAddress + ClientOffsets.NextObjectOffset));
            }

            return 0;
        }

        private ulong GetObjectGuidByBase(uint Base)
        {
            return WowReader.ReadUInt64((IntPtr)(Base + ObjectOffsets.Guid));
        }

        private Bitmap DrawUnit(Bitmap img, Color UnitColor, float XPos, float YPos, float Rotation, string strName)
        {
            Graphics G = Graphics.FromImage(img);

            G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            SolidBrush RadarBrush = new SolidBrush(Color.White);
            Pen RadarPen = new Pen(UnitColor, 2F);

            Rotation = RadianToDegree(Rotation);

            G.ResetTransform();
            G.TranslateTransform(-XPos, -YPos, System.Drawing.Drawing2D.MatrixOrder.Append);
            try
            {
                G.RotateTransform(-Rotation, System.Drawing.Drawing2D.MatrixOrder.Append);
            }
            catch (ArgumentException)
            {
            }
            G.TranslateTransform(XPos, YPos, System.Drawing.Drawing2D.MatrixOrder.Append);
            try
            {
                G.FillEllipse(RadarBrush, XPos - 5 / 2, YPos - 5 / 2, 5, 5);
                G.DrawEllipse(RadarPen, XPos - 5 / 2, YPos - 5 / 2, 5, 5);
                G.DrawLine(RadarPen, XPos - 5, YPos + 2, XPos, YPos - 8);
                G.DrawLine(RadarPen, XPos + 5, YPos + 2, XPos, YPos - 8);
                G.DrawLine(RadarPen, XPos, YPos - 2, XPos, YPos - 8);
                DrawText(img, strName, Convert.ToInt32(XPos) - Convert.ToInt32((strName.Length) * 2.5), Convert.ToInt32(YPos) + 8);
            }
            catch (Exception ex)
            {

            }

            G.Dispose();
            RadarBrush.Dispose();
            RadarPen.Dispose();

            return img;

        }
        private Bitmap AddTile(Bitmap img, int x, int y, int posx, int posy, int fac)
        {
            Graphics G = Graphics.FromImage(img);

            if (chkMinimap.Checked == true)
            {
                try
                {
                    Image Minimap;
                    try {
                        Minimap = Image.FromFile("Maps\\" + CurrentContinent + "\\map" + x + "_" + y + ".blp.png");
                    } catch (Exception ex) {
                        Minimap = null;
                    }

                    G.DrawImage(Minimap, posx, posy, fac, fac);
                }
                catch (Exception ex)
                {
                    G.FillRectangle(new SolidBrush(Color.Black), new Rectangle(posx, posy, fac, fac));
                }
            }
            if (chkGrid.Checked == true)
            {
                G.DrawRectangle(new Pen(Color.White), new Rectangle(posx, posy, fac, fac));
            }

            G.Dispose();

            return img;
        }

        private Bitmap DrawText(Bitmap img, String TextToDraw, int XPos, int YPos)
        {
            Graphics G = Graphics.FromImage(img);
            G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            Font DrawFont = new Font("Arial", 8);
            
            G.DrawString(TextToDraw, DrawFont, Brushes.Black, new Point(XPos, YPos));

            G.Dispose();
            DrawFont.Dispose();

            return img;
        }

        private Bitmap DrawText(Bitmap img, String TextToDraw, int size)  // will draw on center of image
        {
            Graphics G = Graphics.FromImage(img);
            G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            Font DrawFont = new Font("Arial", size);
            StringFormat SF = new StringFormat();
            SF.LineAlignment = StringAlignment.Center;
            SF.Alignment = StringAlignment.Center;
            RectangleF Rect = new RectangleF(0, 0, RadarWidth, RadarHeight);

            G.DrawString(TextToDraw, DrawFont, Brushes.Black, Rect, SF);

            G.Dispose();
            DrawFont.Dispose();
            SF.Dispose();

            return img;
        }

        private Bitmap GetNearbyMinimap()
        {
            Bitmap Minimap = new Bitmap(TILE_HEIGHT * 3, TILE_HEIGHT * 3);
            Graphics G = Graphics.FromImage(Minimap);

            int a = 32 - (int)Math.Ceiling(LocalPlayer.XPos / TILE_SCALE_FACTOR); //thanks to Arrai
            int b = 32 - (int)Math.Ceiling(LocalPlayer.YPos / TILE_SCALE_FACTOR); //thanks to Arrai

            if (OldMinimap.Equals(new MinimapData(a, b, RadarZoom)))
            {
                return (Bitmap)OldMinimap.img.Clone();
            }

            OldMinimap.a = a;
            OldMinimap.b = b;
            OldMinimap.zoom = RadarZoom;

            int cellBindpointX = (int)((((Math.Abs(LocalPlayer.XPos) % TILE_SCALE_FACTOR) / TILE_SCALE_FACTOR) * TILE_HEIGHT) * 1);
            int cellBindpointY = (int)((((Math.Abs(LocalPlayer.YPos) % TILE_SCALE_FACTOR) / TILE_SCALE_FACTOR) * TILE_HEIGHT) * 1);

            Minimap = AddTile(Minimap, a, b, (int)((RadarWidth) / 2) - (TILE_HEIGHT / 2), (int)((RadarHeight) / 2) - (TILE_HEIGHT / 2), TILE_HEIGHT);


            for (int aa = a - 1; aa <= a + 1; aa++)
            {
                for (int bb = b - 1; bb <= b + 1; bb++)
                {
                    Minimap = AddTile(Minimap, aa, bb, TILE_HEIGHT + (TILE_HEIGHT * (aa - a)), TILE_HEIGHT + (TILE_HEIGHT * (bb - b)), TILE_HEIGHT);
                }
            }

            Bitmap FinalMM = new Bitmap((int)(ZoomFactor * 3), (int)(ZoomFactor * 3));
            Graphics G2 = Graphics.FromImage(FinalMM);

            G2.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
            G2.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
            G2.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;


            RadarBitmap = DrawText(RadarBitmap, "LOADING", 18);
            RadarBox.Image = RadarBitmap;
            RadarBox.Update();
            G2.DrawImage(Minimap, 0, 0, ZoomFactor * 3, ZoomFactor * 3);
            OldMinimap.img = (Bitmap)FinalMM.Clone();

            return FinalMM;
        }

        private Bitmap DrawMinimapOverlay(Bitmap img, Bitmap RadBit)
        {
            Graphics G = Graphics.FromImage(RadBit);

            int a = 32 - (int)Math.Ceiling(LocalPlayer.XPos / TILE_SCALE_FACTOR);
            int b = 32 - (int)Math.Ceiling(LocalPlayer.YPos / TILE_SCALE_FACTOR);

            int cellBindpointX = (int)((((Math.Abs(LocalPlayer.XPos) % TILE_SCALE_FACTOR) / TILE_SCALE_FACTOR) * ZoomFactor) * 1);
            int cellBindpointY = (int)((((Math.Abs(LocalPlayer.YPos) % TILE_SCALE_FACTOR) / TILE_SCALE_FACTOR) * ZoomFactor) * 1);

            if (LocalPlayer.XPos <= 0 && LocalPlayer.YPos <= 0)
            {
                G.DrawImage(img, ((int)((RadarWidth) / 2) - cellBindpointX) - 1 * ZoomFactor, ((int)((RadarHeight) / 2) - cellBindpointY) - 1 * ZoomFactor);
            }
            else if (LocalPlayer.XPos >= 0 && LocalPlayer.YPos <= 0)
            {
                G.DrawImage(img, ((int)((RadarWidth) / 2) + (cellBindpointX - ZoomFactor)) - 1 * ZoomFactor, ((int)((RadarHeight) / 2) - cellBindpointY) - 1 * ZoomFactor);
            }
            else if (LocalPlayer.XPos <= 0 && LocalPlayer.YPos >= 0)
            {
                G.DrawImage(img, ((int)((RadarWidth) / 2) - cellBindpointX) - 1 * ZoomFactor, ((int)((RadarHeight) / 2) + (cellBindpointY - ZoomFactor)) - 1 * ZoomFactor);
            }
            else if (LocalPlayer.XPos >= 0 && LocalPlayer.YPos >= 0)
            {
                G.DrawImage(img, ((int)((RadarWidth) / 2) + (cellBindpointX - ZoomFactor)) - 1 * ZoomFactor, ((int)((RadarHeight) / 2) + (cellBindpointY - ZoomFactor)) - 1 * ZoomFactor);
            }

            return img;
        }

        private void ClearBitmap(ref Bitmap img)
        {
            Graphics G = Graphics.FromImage(img);
            G.Clear(RadarBox.BackColor);
            G.Dispose();
        }

        private void tkbRefreshRate_Scroll(object sender, EventArgs e)
        {
            int tkbValue = tkbRefreshRate.Value;

            if (tkbValue <= 10)
                tkbValue = 10;
            if (tkbValue > 1000)
                tkbValue = 1000;

            tkbRefreshRate.Value = tkbValue;
            this.RadarTimer.Interval = tkbValue;

            this.lblRefreshRate.Text = "Refresh Rate: " + tkbValue + "ms";
        }

        private String ToHexString(uint HexToConvert)
        {
            return "0x" + HexToConvert.ToString("X");
        }

        private Boolean LoadAddresses()
        {
            WowReader.SetProcess("Wow", "Read");
            Process[] Processes = Process.GetProcessesByName("Wow");
            
            uint BAdd = (uint)Processes[0].MainModule.BaseAddress;
            ObjectManager = WowReader.ReadUInt32((IntPtr)(WowReader.ReadUInt32((IntPtr)(BAdd + ClientOffsets.CurrMgr)) + ClientOffsets.CurrMgrEx)); //WowReader.ReadUInt32((IntPtr)(ClientConnection + ClientOffsets.ObjectManagerOffset));
            FirstObject = WowReader.ReadUInt32((IntPtr)(ObjectManager + ClientOffsets.FirstObjectOffset));
            LocalPlayer.Guid = WowReader.ReadUInt64((IntPtr)(ObjectManager + ClientOffsets.LocalGuidOffset));

            this.lblClientConnection.Text = "Client Connection: [Obsolete]";
            this.lblObjectManager.Text = "[Current] Object Manager: " + ToHexString(ObjectManager);
            this.lblFirstObject.Text = "First Object: " + ToHexString(FirstObject);

            if (LocalPlayer.Guid == 0)
                return false;
            else
                return true;
        }

        private void frmMain_Activated(object sender, EventArgs e)
        {
            if (LoadAddresses() == true)
                RadarIsReady = true;

            if (RadarIsReady == false)
            {
                ClearBitmap(ref RadarBitmap);
                RadarBitmap = DrawText(RadarBitmap, "Please enter the game world.", 8);
                RadarBox.Image = RadarBitmap;
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            if (RadarIsReady == true)
                return;

            if (LoadAddresses() == true)
                RadarIsReady = true;

            if (RadarIsReady == false)
            {
                ClearBitmap(ref RadarBitmap);
                RadarBitmap = DrawText(RadarBitmap, "Please enter the game world.", 8);
                RadarBox.Image = RadarBitmap;
            }
            waypoints.Clear();
        }

        private void RadarTimer_Tick(object sender, EventArgs e)
        {
            Process[] Processes = Process.GetProcessesByName("Wow");
            uint BAdd = (uint)Processes[0].MainModule.BaseAddress;
            RadarZoom = (trkZoom.Value / 10);
            this.lblRadarZoom.Text = "Zoom Factor: " + RadarZoom.ToString() + "x";
            ZoomFactor = (int)(TILE_HEIGHT * (float)(RadarZoom / 0.5f));
            CurrentContinent = WowReader.ReadString((IntPtr)(WowReader.ReadUInt32((IntPtr)(BAdd + NameOffsets.continentName)) + NameOffsets.continentNameEx));

            ClearBitmap(ref RadarBitmap);

            if (RadarIsReady == false)
            {
                RadarBitmap = DrawText(RadarBitmap, "Please enter the game world then restart the program.", 8);
                RadarBox.Image = RadarBitmap;
                return;
            }

            if (chkMinimap.Checked == true)
            {
                DrawMinimapOverlay(GetNearbyMinimap(), RadarBitmap);
            }

            TotalWowObjects = 0;
            CurrentObject.BaseAddress = FirstObject;

            LocalPlayer.BaseAddress = GetObjectBaseByGuid(LocalPlayer.Guid);
            LocalPlayer.XPos = WowReader.ReadFloat((IntPtr)(LocalPlayer.BaseAddress + ObjectOffsets.Pos_X));
            LocalPlayer.YPos = WowReader.ReadFloat((IntPtr)(LocalPlayer.BaseAddress + ObjectOffsets.Pos_Y));
            LocalPlayer.ZPos = WowReader.ReadFloat((IntPtr)(LocalPlayer.BaseAddress + ObjectOffsets.Pos_Z));
            LocalPlayer.Rotation = WowReader.ReadFloat((IntPtr)(LocalPlayer.BaseAddress + ObjectOffsets.Rot));
            LocalPlayer.UnitFieldsAddress = WowReader.ReadUInt32((IntPtr)(LocalPlayer.BaseAddress + ObjectOffsets.UnitFields));
            LocalPlayer.CurrentHealth = WowReader.ReadUInt32((IntPtr)(LocalPlayer.UnitFieldsAddress + UnitOffsets.Health));
            LocalPlayer.MaxHealth = WowReader.ReadUInt32((IntPtr)(LocalPlayer.UnitFieldsAddress + UnitOffsets.MaxHealth));
            LocalPlayer.CurrentEnergy = WowReader.ReadUInt32((IntPtr)(LocalPlayer.UnitFieldsAddress + UnitOffsets.Energy));
            LocalPlayer.MaxEnergy = WowReader.ReadUInt32((IntPtr)(LocalPlayer.UnitFieldsAddress + UnitOffsets.MaxEnergy));
            LocalPlayer.Level = WowReader.ReadUInt32((IntPtr)(LocalPlayer.UnitFieldsAddress + UnitOffsets.Level));
            LocalPlayer.Name = WowReader.ReadString((IntPtr)(BAdd + NameOffsets.PlayerName));

            LocalTarget.Guid = WowReader.ReadUInt64((IntPtr)(BAdd + ClientOffsets.LocalTargetGUID));

            Graphics g = Graphics.FromImage(RadarBitmap);

            if (chkWaypoint.Checked == true)
            {
                int c = 0;
                PointF position = new PointF(LocalPlayer.XPos, LocalPlayer.YPos);
                foreach (PointF waypoint in waypoints)
                {
                    g.DrawEllipse(new Pen(new SolidBrush(Color.Cyan)), (((int)(LocalPlayer.XPos - waypoint.X)) * RadarZoom + RadarWidth / 2) - 3, (((int)(LocalPlayer.YPos - waypoint.Y)) * RadarZoom + RadarWidth / 2) - 3, 2, 2);
                    if (chkRadius.Checked == true)
                    {
                        g.FillEllipse(new SolidBrush(Color.FromArgb(50, 127, 127, 127)), (((int)(LocalPlayer.XPos - waypoint.X)) * RadarZoom + RadarWidth / 2) - (150 * (RadarZoom)), (((int)(LocalPlayer.YPos - waypoint.Y)) * RadarZoom + RadarWidth / 2) - (150 * (RadarZoom)), 300 * RadarZoom, 300 * RadarZoom);
                    }

                    c++;
                    if (c > 1 && (Math.Abs(waypoint.X - position.X) <= 100) && (Math.Abs(waypoint.Y - position.Y) <= 100))
                    {
                        g.DrawLine(new Pen(new SolidBrush(Color.Cyan)), new Point((int)((((int)(LocalPlayer.XPos - position.X)) * RadarZoom + RadarWidth / 2) - 3), (int)((((int)(LocalPlayer.YPos - position.Y)) * RadarZoom + RadarWidth / 2) - 3)), new Point((int)((((int)(LocalPlayer.XPos - waypoint.X)) * RadarZoom + RadarWidth / 2) - 3), (int)((((int)(LocalPlayer.YPos - waypoint.Y)) * RadarZoom + RadarWidth / 2) - 3)));
                    }
                    position = waypoint;
                }
            }

            if (chkRadius.Checked == true)
            {
                g.DrawEllipse(new Pen(new SolidBrush(Color.FromArgb(200, 127, 127, 127)), 5), (((int)(1)) * RadarZoom + RadarWidth / 2) - (150 * (RadarZoom)), (((int)(1)) * RadarZoom + RadarWidth / 2) - (150 * RadarZoom), 300 * RadarZoom, 300 * RadarZoom);
            }

            if (LocalTarget.Guid != 0)
            {
                LocalTarget.BaseAddress = GetObjectBaseByGuid(LocalTarget.Guid);
                LocalTarget.XPos = WowReader.ReadFloat((IntPtr)(LocalTarget.BaseAddress + ObjectOffsets.Pos_X));
                LocalTarget.YPos = WowReader.ReadFloat((IntPtr)(LocalTarget.BaseAddress + ObjectOffsets.Pos_Y));
                LocalTarget.ZPos = WowReader.ReadFloat((IntPtr)(LocalTarget.BaseAddress + ObjectOffsets.Pos_Z));
                LocalTarget.Type = (short)WowReader.ReadUInt32((IntPtr)(LocalTarget.BaseAddress + ObjectOffsets.Type));
                LocalTarget.Rotation = WowReader.ReadFloat((IntPtr)(LocalTarget.BaseAddress + ObjectOffsets.Rot));
                LocalTarget.UnitFieldsAddress = WowReader.ReadUInt32((IntPtr)(LocalTarget.BaseAddress + ObjectOffsets.UnitFields));
                LocalTarget.CurrentHealth = WowReader.ReadUInt32((IntPtr)(LocalTarget.UnitFieldsAddress + UnitOffsets.Health));
                LocalTarget.MaxHealth = WowReader.ReadUInt32((IntPtr)(LocalTarget.UnitFieldsAddress + UnitOffsets.MaxHealth));
                LocalTarget.CurrentEnergy = WowReader.ReadUInt32((IntPtr)(LocalTarget.UnitFieldsAddress + UnitOffsets.Energy));
                LocalTarget.MaxEnergy = WowReader.ReadUInt32((IntPtr)(LocalTarget.UnitFieldsAddress + UnitOffsets.MaxEnergy));
                LocalTarget.Level = WowReader.ReadUInt32((IntPtr)(LocalTarget.UnitFieldsAddress + UnitOffsets.Level));
                LocalTarget.SummonedBy = WowReader.ReadUInt64((IntPtr)(LocalTarget.UnitFieldsAddress + UnitOffsets.SummonedBy));

                if (LocalTarget.Type == 3) // not a human player
                    LocalTarget.Name = MobNameFromGuid(LocalTarget.Guid);
                if (LocalTarget.Type == 4) // a human player
                    LocalTarget.Name = PlayerNameFromGuid(LocalTarget.Guid);

                RadarBitmap = DrawUnit(RadarBitmap, TargetColor, (LocalPlayer.XPos - LocalTarget.XPos) * RadarZoom + RadarWidth / 2, (LocalPlayer.YPos - LocalTarget.YPos) * RadarZoom + RadarHeight / 2, LocalTarget.Rotation, LocalTarget.Name);
            }

            while (CurrentObject.BaseAddress != 0 && CurrentObject.BaseAddress % 2 == 0)
            {
                TotalWowObjects = TotalWowObjects + 1;

                CurrentObject.Guid = WowReader.ReadUInt64((IntPtr)(CurrentObject.BaseAddress + ObjectOffsets.Guid));
                CurrentObject.Type = (short)(WowReader.ReadUInt32((IntPtr)(CurrentObject.BaseAddress + ObjectOffsets.Type)));
                CurrentObject.XPos = WowReader.ReadFloat((IntPtr)(CurrentObject.BaseAddress + ObjectOffsets.Pos_X));
                CurrentObject.YPos = WowReader.ReadFloat((IntPtr)(CurrentObject.BaseAddress + ObjectOffsets.Pos_Y));
                CurrentObject.ZPos = WowReader.ReadFloat((IntPtr)(CurrentObject.BaseAddress + ObjectOffsets.Pos_Z));
                CurrentObject.Rotation = WowReader.ReadFloat((IntPtr)(CurrentObject.BaseAddress + ObjectOffsets.Rot));
                CurrentObject.Level = WowReader.ReadUInt32((IntPtr)(CurrentObject.UnitFieldsAddress + UnitOffsets.Level));
                
                if (CurrentObject.Type == 3) // not a human player
                    CurrentObject.Name = MobNameFromGuid(CurrentObject.Guid);
                if (CurrentObject.Type == 4) // a human player
                    CurrentObject.Name = PlayerNameFromGuid(CurrentObject.Guid);

                if ((Math.Abs(LocalPlayer.ZPos - CurrentObject.ZPos) <= ZPosRange) && (CurrentObject.Guid != LocalTarget.Guid))
                {
                    switch (CurrentObject.Type)
                    {
                        case 3: // an npc
                            CurrentObject.UnitFieldsAddress = WowReader.ReadUInt32((IntPtr)(CurrentObject.BaseAddress + ObjectOffsets.UnitFields));
                            CurrentObject.CurrentHealth = WowReader.ReadUInt32((IntPtr)(CurrentObject.UnitFieldsAddress + UnitOffsets.Health));
                            CurrentObject.MaxHealth = WowReader.ReadUInt32((IntPtr)(CurrentObject.UnitFieldsAddress + UnitOffsets.MaxHealth));
                            CurrentObject.SummonedBy = WowReader.ReadUInt64((IntPtr)(CurrentObject.UnitFieldsAddress + UnitOffsets.SummonedBy));
                            CurrentObject.CurrentEnergy = WowReader.ReadUInt32((IntPtr)(CurrentObject.UnitFieldsAddress + UnitOffsets.Energy));
                            CurrentObject.MaxEnergy = WowReader.ReadUInt32((IntPtr)(CurrentObject.UnitFieldsAddress + UnitOffsets.MaxEnergy));

                            if (CurrentObject.SummonedBy > 0)
                            {
                                if (CurrentObject.CurrentHealth <= 0)
                                {
                                    RadarBitmap = DrawUnit(RadarBitmap, PetDeadColor, (LocalPlayer.XPos - CurrentObject.XPos) * RadarZoom + RadarWidth / 2, (LocalPlayer.YPos - CurrentObject.YPos) * RadarZoom + RadarHeight / 2, CurrentObject.Rotation, CurrentObject.Name);
                                }
                                else
                                {
                                    RadarBitmap = DrawUnit(RadarBitmap, PetColor, (LocalPlayer.XPos - CurrentObject.XPos) * RadarZoom + RadarWidth / 2, (LocalPlayer.YPos - CurrentObject.YPos) * RadarZoom + RadarHeight / 2, CurrentObject.Rotation, CurrentObject.Name);
                                }
                            }
                            else
                            {
                                if (CurrentObject.CurrentHealth <= 0)
                                {
                                    RadarBitmap = DrawUnit(RadarBitmap, NpcDeadColor, (LocalPlayer.XPos - CurrentObject.XPos) * RadarZoom + RadarWidth / 2, (LocalPlayer.YPos - CurrentObject.YPos) * RadarZoom + RadarHeight / 2, CurrentObject.Rotation, CurrentObject.Name);
                                }
                                else
                                {
                                    RadarBitmap = DrawUnit(RadarBitmap, NpcColor, (LocalPlayer.XPos - CurrentObject.XPos) * RadarZoom + RadarWidth / 2, (LocalPlayer.YPos - CurrentObject.YPos) * RadarZoom + RadarHeight / 2, CurrentObject.Rotation, CurrentObject.Name);
                                }
                            }
                            break;

                        case 4: // a player
                            CurrentObject.UnitFieldsAddress = WowReader.ReadUInt32((IntPtr)(CurrentObject.BaseAddress + ObjectOffsets.UnitFields));
                            CurrentObject.CurrentHealth = WowReader.ReadUInt32((IntPtr)(CurrentObject.UnitFieldsAddress + UnitOffsets.Health));
                            CurrentObject.MaxHealth = WowReader.ReadUInt32((IntPtr)(CurrentObject.UnitFieldsAddress + UnitOffsets.MaxHealth));
                            CurrentObject.MaxEnergy = WowReader.ReadUInt32((IntPtr)(CurrentObject.UnitFieldsAddress + UnitOffsets.MaxEnergy));
                            CurrentObject.CurrentEnergy = WowReader.ReadUInt32((IntPtr)(CurrentObject.UnitFieldsAddress + UnitOffsets.Energy));
                            CurrentObject.MaxEnergy = WowReader.ReadUInt32((IntPtr)(CurrentObject.UnitFieldsAddress + UnitOffsets.MaxEnergy));

                            if (CurrentObject.CurrentHealth <= 0)
                            {
                                RadarBitmap = DrawUnit(RadarBitmap, PlayerDeadColor, (LocalPlayer.XPos - CurrentObject.XPos) * RadarZoom + RadarWidth / 2, (LocalPlayer.YPos - CurrentObject.YPos) * RadarZoom + RadarHeight / 2, CurrentObject.Rotation, CurrentObject.Name);
                            }
                            else
                            {
                                RadarBitmap = DrawUnit(RadarBitmap, PlayerColor, (LocalPlayer.XPos - CurrentObject.XPos) * RadarZoom + RadarWidth / 2, (LocalPlayer.YPos - CurrentObject.YPos) * RadarZoom + RadarHeight / 2, CurrentObject.Rotation, CurrentObject.Name);
                            }
                            break;

                        case 5:
                            CurrentObject.XPos = WowReader.ReadFloat((IntPtr)(CurrentObject.BaseAddress + ObjectOffsets.Node_Pos_X));
                            CurrentObject.YPos = WowReader.ReadFloat((IntPtr)(CurrentObject.BaseAddress + ObjectOffsets.Node_Pos_Y));
                            CurrentObject.ZPos = WowReader.ReadFloat((IntPtr)(CurrentObject.BaseAddress + ObjectOffsets.Node_Pos_Z));
                            
                            int objectID = WowReader.ReadInt((IntPtr)(WowReader.ReadUInt((IntPtr)(CurrentObject.BaseAddress + 0x08)) + (0x8 * 4)));
                            CurrentObject.Name = "??";
                            
                            foreach (int val in Enum.GetValues(typeof(MineNodes)))
                            {
                                if (objectID == val)
                                {
                                    CurrentObject.Name = Enum.GetName(typeof(MineNodes), val);
                                }
                            }
                            foreach (int val in Enum.GetValues(typeof(HerbNodes)))
                            {
                                if (objectID == val)
                                {
                                    CurrentObject.Name = Enum.GetName(typeof(HerbNodes), val);
                                }
                            }
                            if (CurrentObject.Name != "??")
                            {
                                RadarBitmap = DrawUnit(RadarBitmap, NodeColor, (LocalPlayer.XPos - CurrentObject.XPos) * RadarZoom + RadarWidth / 2, (LocalPlayer.YPos - CurrentObject.YPos) * RadarZoom + RadarHeight / 2, 0, CurrentObject.Name);
                            }

                            break;
                        }
                    }
                    CurrentObject.BaseAddress = WowReader.ReadUInt32((IntPtr)(CurrentObject.BaseAddress + 0x3C));
                }
                RadarBox.Image = RadarBitmap;

                this.lblPlayerName.Text = "Player Name: " + LocalPlayer.Name;
                this.lblPlayerLevel.Text = "Player Level: " + LocalPlayer.Level.ToString();
                this.lblPlayerHealth.Text = "Player Health: " + LocalPlayer.CurrentHealth.ToString() + " / " + LocalPlayer.MaxHealth.ToString();
                this.lblPlayerPower.Text = "Player Power: " + LocalPlayer.CurrentEnergy.ToString() + " / " + LocalPlayer.MaxEnergy.ToString();
                this.lblPlayerXPosition.Text = "Player X-Pos: " + LocalPlayer.XPos.ToString();
                this.lblPlayerYPosition.Text = "Player Y-Pos: " + LocalPlayer.YPos.ToString();
                this.lblPlayerZPosition.Text = "Player Z-Pos: " + LocalPlayer.ZPos.ToString();
                this.lblPlayerGuid.Text = "Player Guid: " + string.Format("0x{0:X}", LocalPlayer.Guid);
                this.lblPlayerRotationR.Text = "Player Rotation (Radians): " + Math.Round(LocalPlayer.Rotation, 2).ToString() + "R";
                this.lblPlayerRotationD.Text = "Player Rotation (Degrees): " + Math.Round(RadianToDegree(LocalPlayer.Rotation), 2).ToString() + "D";

                if (LocalTarget.Guid != 0)
                {
                    this.lblTargetName.Text = "Target Name: " + LocalTarget.Name;
                    this.lblTargetLevel.Text = "Target Level: " + LocalTarget.Level.ToString();
                    this.lblTargetHealth.Text = "Target Health: " + LocalTarget.CurrentHealth.ToString() + " / " + LocalTarget.MaxHealth.ToString();
                    this.lblTargetPower.Text = "Target Power: " + LocalTarget.CurrentEnergy.ToString() + " / " + LocalTarget.MaxEnergy.ToString();
                    this.lblTargetXPosition.Text = "Target X-Pos: " + LocalTarget.XPos.ToString();
                    this.lblTargetYPosition.Text = "Target Y-Pos: " + LocalTarget.YPos.ToString();
                    this.lblTargetZPosition.Text = "Target Z-Pos: " + LocalTarget.ZPos.ToString();
                    this.lblTargetGuid.Text = "Target Guid: " + string.Format("0x{0:X}", LocalTarget.Guid);
                    if (LocalTarget.SummonedBy > 0)
                        this.lblTargetSummonedBy.Text = "Summoned By: " + PlayerNameFromGuid(LocalTarget.SummonedBy);
                }
                else
                {
                    this.lblTargetName.Text = "Target Name: ";
                    this.lblTargetLevel.Text = "Target Level: ";
                    this.lblTargetHealth.Text = "Target Health: ";
                    this.lblTargetXPosition.Text = "Target X-Pos: ";
                    this.lblTargetYPosition.Text = "Target Y-Pos: ";
                    this.lblTargetZPosition.Text = "Target Z-Pos: ";
                    this.lblTargetGuid.Text = "Target Guid: ";
                    this.lblTargetSummonedBy.Text = "";
                }

                lblTotalWowObjects.Text = "Total Objects In Manager: " + TotalWowObjects.ToString();
        }

        private void chkWaypoint_CheckedChanged(object sender, EventArgs e)
        {
            if (chkWaypoint.Checked == true && tmrWaypoint.Enabled == false)
            {
                tmrWaypoint.Enabled = true;
            }
            else
            {
                tmrWaypoint.Enabled = false;
                waypoints.Clear();
            }
        }

        private void tmrWaypoint_Tick(object sender, EventArgs e)
        {
            PointF position = new PointF(LocalPlayer.XPos, LocalPlayer.YPos);
            if (!position.Equals(OldPosition))
            {
                OldPosition = position;
                waypoints.Add(position);
            }
        }
    }
}
