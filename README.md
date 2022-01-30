# Unity-Simple-Controller-Vibration
A package for creating custom vibrations for gamepads using Scriptable Objects.

The tools are reliant on the New Input System and Editor Coroutines (These are imported automatically when adding the package)


## Table of Contents
1. [Getting Started](#getting-started)
2. [Samples](#samples-optional)
3. [Implementation](#implementation)
4. [Creating Vibration Parts](#creating-vibration-parts)
5. [Creating Vibration Sequences](#creating-vibration-sequences)
6. [Testing Vibration Sequences](#testing-vibration-sequences)
7. [Limitations](#limitations)



## Getting Started
To use the package, add the project using Unity's package manager.

Window > Package Manager > Add Package From Git URL

![Package Manager](https://user-images.githubusercontent.com/34044928/144337933-f6b1ef4a-d179-42ce-b717-7ea30f53a5ee.png)

If you are prompted with a warning about the new input system, select Yes

![New-input-system](https://user-images.githubusercontent.com/34044928/144338031-2c8a2d36-c317-41aa-9270-b310f3d21bfc.png)

The assets are imported into the packages section.

 
## Samples (optional)
To help get you started, samples are provided.
 
These samples can be added into your assets folder by clicking import after adding the package in the package manager.
 ![image](https://user-images.githubusercontent.com/34044928/151714608-53c3a49e-5241-4897-ab89-80b1a6dd2cc6.png)

 
 
## Implementation
To use the package, add the vibration controller Component to a Game Object.

![image](https://user-images.githubusercontent.com/34044928/151714652-a18f8b1c-8552-4e15-b952-566ec0396823.png)

 - Adding it to the player would be a good idea but not required
 - The vibration controller asset has the method Vibrate:
 - Supplying a vibration sequence to this method will play the vibration
 ```csharp
  public void Vibrate(List<IVibrationPart> newList)
```


### Example Implementation
This example is only to show how to get started and I would recommend something cleaner.

 - Create a new C# Script ActivateVibration and attach to a UI button.
 ```csharp
using UnityEngine;
using ControllerVibration;
public class ActivateVibration : MonoBehaviour
{
    public VibrateController vibrateController;
    public VibrationSequence vibSequence;

    public void Activate()
    { vibrateController.Vibrate(vibSequence.sequence); }
}
 ```
 - Set the On Click event to call the method Activate()
 - Assign the public fields of ActivateVibration in the inspector
 - If you have not downloaded the [Samples](#samples-optional), you will need to [create vibration parts](#creating-vibration-assets) and then [the sequence](#testing-vibration-sequences)
![image](https://user-images.githubusercontent.com/34044928/151714960-865d4831-49bd-4a8d-a366-2d9e464e869a.png)

 - With the game running, pressing the UI Button with a controller plugged will play the vibration sequence.
 - If you have problems, try [testing your Vibration Sequences](#testing-vibration-sequences) using the sample assets
 
## Creating Vibration Parts
Vibration parts are the building blocks for vibration sequences.

Simple and Curve vibration assets can be created by:
 - Assets > Create > Scriptable Objects > Vibration
 - (either through the main menu or the Assets folder right click menu)

### Simple Parts
Simple parts have 2 settings.

![image](https://user-images.githubusercontent.com/34044928/151715077-b1f65bc6-91ce-474e-93e4-70d995e4fccd.png)

 - Duration is the length of the vibration before stopping. 0 - inf
 - Strength is for the strength of the left(x) / right(y) motors. 0 - 1
   - On an Xbox One controller, the left motor has the larger rumble


### Curve Part
![image](https://user-images.githubusercontent.com/34044928/151715094-242ffa23-ba74-483b-a712-7f319dbe086d.png)

Curve parts are created from 2 graphs.

![image](https://user-images.githubusercontent.com/34044928/151715120-f0dfce83-2c10-46e9-9638-033f678b2dfa.png)

 - The y axis indicates the strength of the rumble over time. (0 - 1)
 - The x axis indicates the duration of the rumble (0 - inf)
 - If one graph is longer than the other, the longer graph will be used as the duration of the whole vibration part


## Creating Vibration Sequences
Sequences are used to play vibrations parts.

![image](https://user-images.githubusercontent.com/34044928/151715201-eb3aefc2-19e2-4172-9003-623c1af11278.png)

 - The VibrationController.cs will expect a sequence to be passed to it
 - Sequences can contain single or multiple vibration parts and are played in order.
 - Vibration sequences can be created by:
   - Assets > Create > Scriptable Objects > Vibration > Sequence
   - (either through the main menu or the Assets folder context menu)

## Testing Vibration Sequences
You can test vibration sequences in the inspector without needing to go into runtime
 - Bring up the vibration sequence in the inspector
 - Press the Test Sequence Button
 - The Controller should perform the vibration sequence
 - If your controller does not vibrate
   - check it is plugged in and turned on
   - it might not have vibration support
   - your sequence might be empty
   - your parts in the sequence might have a duration of 0

## Limitations
As of version 1.1:

 - It is not possible to interrupt or stop a vibration sequence
 - A vibration sequence must finish before taking the next request
 - Any request given whilst a vibration is in process will be ignored
