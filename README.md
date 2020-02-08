# GodHands
Tool for Vagrant Story Modding. This is intended to be a low level editor for enemy stats, equipment, and a means of exporting / importing game assets (such as 3D models and texture maps) to/from standard file formats.

## Features (Version 0.2.1)
1. Runs under Windows through DotNet 3.5.
2. Runs under Linux through Mono.
3. Undo/Redo
4. Disk Tool can export files (import is todo)
5. Disk Tool can relocate and resize files (the file data is moved to the new location)
6. Edit ZND enemy stats and equipment
7. Edit MPD treasure chests
8. Export ZND texture maps to TIM/PNG/BMP
9. Export ZND enemy/equipment/bodypart/skills
10. Export MPD enemy
11. Export MPD treasure chest equipment

## To do
1. Insert new data (requires resizing files)
2. Auto update internal LBA tables.
3. MPD doors
4. Export/Import sections from ZUD files
5. MPD script section editor
6. Export/Import MPD/SHP/WEP geometry and textures
7. OpenGL based 3D MPD/SHP/WEP viewer

## Images
![V0-2-0](https://github.com/collinsmichael/GodHands/blob/master/Releases/Images/GodHands-0-2-0.png?raw=true)

## Contributions and acknowledgments
Many thanks to Morris for all your hard work, [fantastic tools](https://github.com/morris/vstools) and reverse engineering. It's very much appreciated.

Mercurial for putting together [this beatiful port](https://github.com/MercurialForge/VSViewer).


Korobetski for your work on the [game assets](https://github.com/korobetski/Vagrant-Story-Unity-Parser) (AKAO and the 3D stuff)

Jaythreee, for [the inspiration](https://github.com/jaythreee/VSStatsEditor)

Rosto for finding bugs in the reverse engineering, and for making [this tool](https://github.com/Rosto75/vstrack)

To all the folks over at [DataCrystal](https://datacrystal.romhacking.net/wiki/Vagrant_Story) for maintaining this reference and keeping things alive.

And last but not least, huge thanks to [The_Eyes_o_O](https://www.youtube.com/channel/UCJFE6qILY4xBhQk78yMBIog). Whos working on a [Rebalance Mod](https://vszenith.wordpress.com/). He's been hacking VS for a [Long](http://ngplus.net/index.php?/forums/topic/175-vagrant-story-rebalance-mod/&), [Long](https://gamefaqs.gamespot.com/boards/914326-vagrant-story/76794893) time.
