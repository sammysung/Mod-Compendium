﻿using System.Collections.Generic;
using System.Linq;
using ModCompendiumLibrary.Logging;
using ModCompendiumLibrary.VirtualFileSystem;

namespace ModCompendiumLibrary.ModSystem.Mergers
{
    public class BottomToTopModMerger : IModMerger
    {
        public VirtualDirectory Merge( IEnumerable<Mod> mods )
        {
            Log.Merger.Info( "Merging mods from bottom to top" );

            var fileDirectory = new VirtualDirectory();
            var entryHistory = new HashSet<string>();

            foreach ( var mod in mods.Reverse() )
            {
                var dataDirectory = VirtualDirectory.FromHostDirectory( mod.DataDirectory );
                dataDirectory.Name = string.Empty;

                foreach ( var entry in dataDirectory )
                {
                    if ( !entryHistory.Contains( entry.FullName ) )
                    {
                        entry.CopyTo( fileDirectory );
                        entryHistory.Add( entry.FullName );
                    }
                }
            }

            return fileDirectory;
        }
    }
}