# 🎮 Narexa's Modular Interaction System

A clean, flexible interaction system for Unity games. Makes it easy to add player interactions with objects in your game world. This system serves as a solid foundation that you can build upon and customize to fit your specific game needs - it's not meant to be a one-size-fits-all solution, but rather a starting point that you can adapt and expand.

![Unity Version](https://img.shields.io/badge/Unity-2022.3%2B-blue)
![License](https://img.shields.io/badge/License-MIT-green)
![Author](https://img.shields.io/badge/Author-Narexa-orange)

## 💬 Get in Touch
Got questions, suggestions, or just want to chat about game development? Feel free to reach out to me on Discord: **narexa**

## ⚡ Quick Install

### Option 1: Unity Package Manager (Recommended)
1. Open Unity's Package Manager (Window > Package Manager)
2. Click the + button > Add package from git URL
3. Paste: `https://github.com/narexa/ModularInteractionSystem.git`

### Option 2: Manual Install
1. Download this repo
2. Drop the `ModularInteractionSystem` folder into your Unity project's `Assets` folder

## 🚀 Quick Start

1. Set up your player:
```csharp
// Add these components to your player:
- PlayerInput (Unity Input System)
- PlayerInteractor
```

2. Create an interactable object:
```csharp
public class Chest : InteractableBase
{
    protected override void HandleInteractionBegin(IInteractor interactor)
    {
        // Open chest
    }

    protected override void HandleInteractionEnd(IInteractor interactor)
    {
        // Maybe close chest
    }
}
```

3. Setup in Unity Editor:
- Add your interactable script to an object
- Set the interaction range
- Make sure it's on the right layer
- Done! 🎉

## 🛠️ Features

- Clean, modular design
- Easy to extend and customize
- Works with Unity's new Input System
- Built-in detection system
- UI prompt support
- Editor visualization tools
- Designed to be built upon and modified

## 📝 Example

```csharp
// Making a simple button
public class PressableButton : InteractableBase
{
    public UnityEvent onPressed;

    protected override void HandleInteractionBegin(IInteractor interactor)
    {
        onPressed?.Invoke();
    }

    protected override void HandleInteractionEnd(IInteractor interactor)
    {
        // Button released
    }
}
```

## 🤔 Common Issues

- **No interaction prompt?** Make sure you've set up the UI references in PlayerInteractor
- **Can't interact?** Check your layer masks and interaction ranges
- **Input not working?** Verify your Input Actions asset has an "Interact" action

## Requirements
- Unity 2022.3 or newer
- Input System Package
- TextMeshPro Package

## 🤝 Contributing

I'd love to see what you build with this system! Feel free to:
- Open issues (I'm happy to help troubleshoot)
- Submit PRs with your improvements
- Fork and modify to suit your needs
- Share your custom implementations

Just keep the code clean and well-commented 😉 And don't hesitate to reach out on Discord if you need any help!

## 📜 License

MIT License - Use it in whatever you want, credit appreciated! I'd love to see what you create with it.

---
Made by Narexa - A fellow game dev who loves building tools to make development easier, whilst improving my personal skills!  
[www.narexa.dev](https://www.narexa.dev) 