## Simple Fog Of War
### About
A Unity Extension for terrain occlusion through entity revealed global fog.
Supports permanent revealing and an option to save and load its state (alpha).
#### How to use
It's pretty simple:
Drop the *FogOfWarInfluence* Component onto entities that should reveal fog and configure their radius.  
*FogOfWarSystem* is the main Component, where the basic settings can be configured.
*Size* determines the area of fog (see editor gizmo as an indicator).

###### Notes   
There are two modes of rendering: *DirectSeeThrough* and *Projector*.
*DirectSeeThrough* is a little trick to save performance, *Projector* is the little less performant, but more accurate option. Just play with them to see their visual difference.
Details for the rest of the settings can be found in their tooltips.
