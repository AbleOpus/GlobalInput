# GlobalInput
This is a .NET 4.5.2 class library that provides system-wide hooking for mouse and keyboard input. GlobalInput wraps _Windows_ hotkey hooking, low-level keyboard hooking, and low-level mouse hooking functionality into a simple .NET abstraction. The low-level hookers can be consumed in a way highly familiar to WinForms developers.

Note: This is a very old project of mine that was eventually refactored. Some of the code and design seems to be from a friend’s project that can be found here:
https://github.com/AlexArchive/Shortcut

## The WinForms Keys Enumeration
Since WinForms key handling is mysterious (especially the Keys enum), it seems like I good idea to describe, in detail, how it all works – feel free to skip this section. The GlobalInput abstraction uses the Windows.Forms.Keys enum to represent keyboard keys. This is a partially flaggable enum that can indicate one non-modifier and the status of three modifiers (Shift, Alt, and Ctrl). The low-order bytes are for the modifiers Ctrl, Alt, and Shift; when these flags are set, the modifiers are down (or depressed). These modifier keys can all be treated as proper flaggable values. The high-order bytes of the key data indicate non-modifiers (called the “key-code” by _Microsoft_). Depending on the context, this can either represent a key that was recently depressed or released. “KeyData” is the name used to describe the entire value. Key-codes are enumerated incrementally – often by one, and therefore, cannot be bitwise combined with each other. Attempts to bitwise combine two or more key-codes will scramble the key data. In WinForms, when a modifier key is down by itself, the high-order value will be set to a key-code version of the modifier, and the modifier flag will also be set. If multiple modifiers are pressed but not any key-code, the most recently pressed modifier will be set as the key-code and all modifiers depressed will have their flags set. The key-code versions of the modifiers are:
``` C#
ShiftKey = 16,
ControlKey = 17,
Menu = 18
```
The left and right _Windows_ keys are recognized by WinForms as key-codes. However, the _Windows_ hotkey API can use both the right and left _Windows_ keys as one modifier (see the “fsModifiers” parameter info at https://msdn.microsoft.com/en-ca/library/windows/desktop/ms646309(v=vs.85).aspx). This is for built-in processes. Judging from the Keys enum, it doesn’t seem like _Microsoft_ wants people to easily use _Windows_ keys as modifiers (which is understandable). I decided to go with the familiar Keys enum and not worry much about the _Windows_ modifier. You can however, set the unnamed modifier flag, which comes after the Alt flag, to bind to _Windows_ modifier. This is not the official indicator of the _Windows_ key modifier, I am simply using an unused flaggable bit. This flag can be accessed through GlobalInput.Keyboard.Extrakeys.WinKeyModifier.
One even seems to be the result of a typo and was kept in the enumeration for compatibility concerns (“IMEAceept” and “IMEAccept”). In any case, use the name that is most intuitive. Perhaps in the future I will write my own Keys enum which elegantly supports all 4 modifiers, also having less redundant values, and more intuitive names and summaries. There are many redundant key-codes in the Keys enum – different names having the same number. Furthermore, one virtual key is specified in the Keys enum as the value “Packet”. This is not a valid key value to use for hotkey hooks.
Keys contains a bitmask value called KeyCode, which can be used to extract the key-code specified in a given value – effectively stripping out any set modifier flags. Example:
``` C#
Keys sequence = Keys.Control | Keys.A;
Keys nonMod = sequence & Keys.KeyCode;
// Result: Keys.A  
```
The same can be done for the modifiers:
``` C#
Keys sequence = Keys.Control | Keys.A;
Keys mods = sequence & Keys.Modifiers;
// Result: Keys.Control
```
Use the GetModifiers() and GetKeyCode() extension methods to acheive the same thing more elegantly.

![](http://i.imgur.com/7s3KM4k.png)

## The LowLevelHooker Class
This class is the base class for the KeyboardHooker and MouseHooker classes. They install hooks into the systems hook chain so that they can listen for input. The hook will catch all input that falls under the hook type implemented. However, some applications may have the same type hook installed and do not allow the input to continue down the hook chain. Whoever installs the hook first can keep input from reaching hooks that are installed later. By default, the low-level hookers in this library will pass input to the next hook in the hook chain. To change this, set the (bindable) “CallNextHook” property to false. To hook and unhook these hookers, call Hook() and Unhook() respectively. The (bindable) “Hooked” property can be used as well. Be sure to explicitly dispose of these hookers immediately after they are no longer needed, as they have a measurable effect on system performance. The low-level hooker is a Component, so any of its derived types can be used in the designer.
## The KeyboardHooker Class
The keyboard hooker provides familiar KeyDown and KeyUp events that fire whenever a key is pressed. The events also use the WinForms class KeyEventArgs for its arguments type. Both the event arguments and events work identically to those of a WinForms control.
``` C#
/// <summary>
/// Occurs when a key been depressed.
/// </summary>
public event EventHandler<KeyEventArgs> KeyDown;

/// <summary>
/// Occurs when a key has been unpressed.
/// </summary>
public event EventHandler<KeyEventArgs> KeyUp;
```
Simply subscribe to these events and call Hook().

## The MouseHooker Class
The mouse hooker class imitates WinForms events and event arguments just like KeyboardHooker. It can track mouse move, button down, button up, and mouse wheel events. It can even track the “xButtons”, which are two buttons commonly found on the left of the mouse – anything beyond that is handled by the mouse’s drivers.
``` C#
/// <summary>
/// Occurs when the mouse wheel scrolls.
/// </summary>
public event MouseEventHandler MouseWheel;

/// <summary>
/// Occurs when the mouse has moved.
/// </summary>
public event MouseEventHandler MouseMoved;

/// <summary>
/// Occurs when a mouse button has been released.
/// </summary>
public event MouseEventHandler MouseUp;

/// <summary>
/// Occurs when a mouse button has been depressed.
/// </summary>
public event MouseEventHandler MouseDown;
```
The MessageFilter property allows one to control what kind of input will raise the events. By default, all input is allowed to raise events. For performance, you may want to unset the “Move” flag.
``` C#
mouseHooker.MessageFilter &= ~MouseMessageTypes.Move;
```
## The HotkeyHooker Class
This class allows system-wide hotkeys to be easily defined. It creates and maintains a list of hotkey bindings. Hotkey bindings bind Actions to Keys. To hook a key and bind an action to it:

``` C#
HotkeyHooker hotKeyHooker = new HotkeyHooker();
hotKeyHooker.Hook(Keys.Control | Keys.P, () => Debug.WriteLine("Print me"));
```
Individual key hooks can be unhooked quite easily:
``` C#
hotKeyHooker.Unhook(Keys.Control | Keys.P);
```
UnhookAll() will unhook all of the key hooks installed by the hooker. The hooker can still be used. However, if Dispose() is called, the hooker can no longer be used.
The InvokeEnabled property can be set to false to temporarily keep the hotkey actions from firing. This is useful when the user opens something like a settings dialog – where hotkey functionality can cause problems.
The static GetOccupiedHotkeys() method can be used to get all of the hotkeys that are currently installed. This can be a long running call, so consider using the task-async version of this method. Individual hotkeys can be tested as well, with the IsHotKeyHooked(Keys) method.

Here are some examples of hotkeys that won’t work out:
``` C#
// ControlKey is a key-code and will scramble the value.
Keys hotkey = Keys.ControlKey | Keys.A;
// Packet is not an actual key value.
Keys hotkey = Keys.Packet;
// This is already bound by a system process.
Keys hotkey = Keys.Alt | Keys.Tab;
// This doesn’t make sense.
Keys hotkey = Keys.None;
```

## Defining Key Names
Both the library and demo project use the static class “KeyNaming” to get user-friendly text representations of keys or key sequences. KeyNaming has a static property called KeyNameBinder which determines how Keys translate to user-friendly strings. By default, the property is set to a culture-neutral English implementation. To provide a custom implementation:

1. derive from KeyNameBinderBase.

2. Implement the culture that best describes the target text output.
    ``` C#
    public override CultureInfo Culture => CultureInfo.GetCultureInfo("en");
    ```
3. Implement how each Key value translates to a string.

   ``` C#
    public override string GetFriendlyName(Keys key)
    {
        switch (key)
        {
            // Mods.
            case Keys.RShiftKey: return "Right Shift";
            case Keys.LShiftKey: return "Left Shift";
            case Keys.LControlKey: return "Left Ctrl";
            case Keys.RControlKey: return "Right Ctrl";
            case Keys.LMenu: return "Left Alt";
            case Keys.RMenu: return "Right Alt";
            // Key-codes.
            case Keys.Back: return "Backspace";
            …
            default: return null;
        }
    }
    ```

    Make sure to return null to indicate that the default text representation should be used. The default representation will be the enum name directly converted to string. Numbers, and key names starting with “Oem”, which do not have a custom text value defined, will be automatically trimmed to be more user friendly. 


4. Implement how to separate the key-code from the modifiers and the modifiers from themselves.
    ``` C# 
    public override string GetSeperator()
    {
        return " + ";
    }
    ```
5. Finally, create an instance of the implementation and assign it to the KeyNaming.KeyNameBinder property.
    
## Goodies
The following __System.Windows.Forms.Keys__ extension methods may come in handy:
``` C#
/// <summary>
/// Gets whether the key represents a letter in the alphabet.
/// </summary>
public static bool IsLetter(Keys)

/// <summary>
/// Gets whether the key represents a number (including number pad keys).
/// </summary>
public static bool IsNumber(Keys)

/// <summary>
/// Gets whether the key is toggleable (Scroll Lock, Caps Lock, or Num Lock).
/// </summary>
public static bool IsToggle(Keys)

/// <summary>
/// Gets whether the key is an arrow key.
/// </summary>
public static bool IsArrow(Keys)

/// <summary>
/// Gets whether the key is a function key.
/// </summary>
public static bool IsFKey(Keys)

/// <summary>
/// Gets whether the specified <see cref="Keys"/> has a toggle key.
/// </summary>
public static bool HasToggleKey(Keys)

/// <summary>
/// Gets whether the specified key contains only modifiers.
/// </summary>
public static bool IsOnlyModifiers(Keys)

/// <summary>
/// Extracts the modifiers from the specified keys.
/// </summary>
public static Keys GetModifiers(Keys)

/// <summary>
/// Extracts the key-code from the specified keys.
/// </summary>
public static Keys GetKeyCode(Keys);

/// <summary>
/// Gets whether the specified hey is depressed (must be a key code).
/// </summary>
public static bool IsPressed(Keys);
```

The __HotkeyTextBox__ is also useful. It allows users to quickly and easily input key sequences. The following are the intrinsic properties defined for this control:

HotkeyKeyCode: Gets or sets the key-code value of the hotkey.

HotkeyModifiers: Gets or sets the hotkey modifier flags.

Hotkey: Gets or sets the user inputted hotkey.

NoKeyString: Gets or sets the string to display when there are no valid keys inputted.

AllowToggleKeys: Gets or sets whether to allow input of toggle keys (Scroll Lock, Caps Lock, and Num Lock).

AllowSoloModifiers: Gets or sets whether to allow submission of only modifiers.
