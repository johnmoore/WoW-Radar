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
using System.Windows.Forms;

namespace WoWRadar
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
    }
}
