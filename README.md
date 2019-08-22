# VR Pacman

## Fixing Problems

Issues are currently tracked via Bitbucket under the [Issues](https://github.com/iamtomhewitt/bugzilla-live/issues) page for the Bugzilla Live repository. 
Choose an issue, create a new branch for it and code it.
When you are done create a pull request and I will review the code before it can be merged into the develop branch, and then master.

# Code Style

## Files & Folders
Organise scripts and folders into the same category. They can either be grouped by a component, or types, such as: 

```c#
Assets
    Models
        Dragon
            Model.fbx
            Map.png
        Wizard
            Model.fbx
            Map.png
    Scripts
        Dragon
            Fly.cs
            Roar.cs
        Wizard
            Movement.cs
            Spells.cs

// Or the following way can be used

Assets
    Dragon
        Model.fbx
        Map.png
        Fly.cs
        Roar.cs
    Wizard
        Model.fbx
        Map.png
        Movement.cs
        Spells.cs
```
The first way is usually preferred, but it depends on the project.

## Namespaces

Don't use ```com.fpsgame.hud.healthbar```, instead use ```FpsGame.Hud.Healthbar```

## Braces

Braces should follow the One True Brace Style, and always start on a new line, for example: 

```c#
if (something)
{
    // Correct
}

if (something) {
    // Incorrect
}
```
Brace should be used even if there is one statement.

## Comments

Single line or multiline comment: 

```c# 
// Example
```

Documentation above a method or a class:

```c#
/// <summary>
/// Randomly selects a GhostPath to use as the waypoint path from the paths in the scene.
/// Selects a path that is currently not being used by another ghost.
/// </summary>
```

## Documentation

Documentation should only be used when necessary, for example the method ```GetName()``` does not need documenting, but a method like ```SelectNewPath()``` would perhaps need some documentation on where the request is going and what type of request it is.

Each class should also have a relevant documentation block before it:

```c#
/// <summary>
/// Checks if any of the Ghosts are running home. If at least one Ghost is running home this method returns true.
/// </summary>
public bool AllGhostsRunningHome()
```

# *Statements*

## Classes

Written in the ```UpperCamelCase``` form.
See the Camel Case Definition section for classes which have acronyms.

## Methods

Written in the ```UpperCamelCase``` form. Methods should be self describing and only do one thing, for example ```MovePlayer()``` or ```GetHealth()```. If the method does more than one thing, it needs to be refactored.
Other examples:
    
```c#
GetScoresViaHttp(); // NOT: getScoresViaHTTP();
SaveAsJson();       // NOT: saveAsJSON();
```

Methods should have no more than four parameters. If it requires more, refactor your code (for example, use a Builder or an instance variable).
Parameters should remain on the same line unless its absolutely necessary to put them on a new line (e.g. readability):  

```c#
Debug.Log("Failed to process"
        + " request " + request.GetId()
        + " for user " + user.GetId()
        + " query: '" + query.GetText() + "'");
```

## Constants

Constants are written in ```CONSTANT_CASE```: 

```c# 
private int SOME_VARIABLE = 5;
```

## Variables

Written in the ```lowerCamelCase``` form. Variables should be self describing. They should be prefixed with ```public``` or ```private```

```c#
private String username = "";
private String primaryAddress = "";
private String newCustomerId = "";
```

Do not use public variables unless absolutely necessary. Variables should be private with a public ```Get()``` and ```Set()``` method.

If a GameObject is only present once in a scene, then the following instance method should be used

```c#
// GameManager.cs

public static GameManager instance;

void Awake()
{
    if (instance == null)
    {
        instance = this;
    }
    else
    {
        Destroy(this.gameObject);
        return;
    }
}
```
It would be then used in another script:
```c#
GameManager.instance.DoSomething();
```

Variables that need to be private but need to be shown in Inspector should use the ```[SerializeField]``` before a variable. For the other way around, use ```[HideInInspector]```

## Conditions

Where applicable, use a Ternary operator for simple statements:

```c#
int number = 10
bool isLessThanTen = (number < 10) ? true : false; 

// Instead of

int number = 10;
bool isLessThanTen;
if (number < 10)
{
    isLessThanTen = true;
}
else
{
    isLessThanTen = false;
}
```

Switch statements should always have a default statement:

```c#
switch (condition)
{
    case someCase:
        // do something
        break;
    
    case anotherCase:
        // do something else
        break;

    default:
        // do a default, perhaps logging or a default value
        break
}
```

## Camel Case Defined

Camel casing should follow this rule:

|           Form          |      Correct      |     Incorrect     |
|:-----------------------:|:-----------------:|:-----------------:|
| "XML HTTP request"      | XmlHttpRequest    | XMLHTTPRequest    |
| "new customer ID"       | newCustomerId     | newCustomerID     |
| "supports IPv6 on iOS?" | supportsIpv6OnIos | supportsIPv6OnIOS |