TODO LIST
 o Get list of Room Names from ARM Files
 o Get itemnamelist from disk
 o Get itemlist from disk
 o resize / relocate files

 o ProgressBar doesn't work from other threads. (Need to implement own version)


 o Open ZND Files
   1) RamDisk Read ZND
   2) RamDisk Read MPD's
      2.1) Allocate array for MPD list
      2.2) Fill MPD list
   3) RamDisk Read ZUD's
      3.1) Allocate array for ZUD list
      3.2) Fill ZUD list
   4) RamDisk Read NPC's
      4.1) Allocate Array for NPC List
      4.2) Fill NPC list


  1 x ZND File
  1 x MPD File
  N x ZUD Files
  1 x ARM File

ARM Files provide Room Names
ZND Files provide texture packs and NPC packs
MPD Files define rooms
ZUD Files provide NPC models

Each Zone contains a texture pack and a list of NPC's which populate a list of rooms
all rooms within a zone share the same texture pack and contain a subset of the NPC's
    List of Rooms
	List of NPC's
	Texture Pack
	NPC stats
Each room contains
    Room geometry
	Collision Map
	Doors
	Lighting
	Texture Effects
	Audio
	Script
	Door Sections
	Enemy Sections
	Treasure Chest

Level editor
    NPC's
        Insert NPC's into the list
        Remove NPC's from the list
        Modify NPC's in the list
        View NPC's
        List NPC's
        Insert NPC equipment
        Remove NPC equipment
        Modify NPC equipment
        Export NPC 3D models
        Import NPC 3D models
    Rooms
        Insert MPD's into the zone
        Remove MPD's from the zone
        Import MPD 3D models
        Export MPD 3D models
        Insert an NPC from the zone list to this room
        Remove an NPC from the zone list from this room
        Modify Collision Maps
        Modify UV Mapping
        Modify Lighting
        Modify Scripts
        Modify Enemy Triggers
        Modify Door Triggers
        Modify Save points
        Modify containers
        Modify Treasure Chest Contents
    Texture Pack
        Insert textures into the pack
        Remove textures from the pack
        Export textures from the pack
        Import textures into the pack
It would be nice to track RAM and VRAM usage from within the level editor
It would also be nice to customize forging of new equipment.
It would be nice to modify spells and items.
There should be drill down to navigate the level editor
