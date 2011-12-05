This directory contains the original 3DS Max 9 scenes that were used to create the OgreMax Scene Viewer sample scenes

When you load these scenes in 3DS Max for the first time, you will receive a standard 3DS Max dialog informing 
you that various bitmap files cannot be found. To resolve this issue, just add the OgreMaxViewer\Media\materials\textures 
directory to the list of directories.

You will also receive an OgreMax error informing you that the resource locations are invalid. This is 
because your machine most likely does not have the same directory structure as the machine on which the scenes were created.
To resolve this issue, set the "Base" resource location in the dialog that appears to point at the OgreMaxViewer\Media
directory, wherever it is on your system.