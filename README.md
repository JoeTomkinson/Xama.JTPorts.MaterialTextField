# Xama.JTPorts.MaterialTextField
[![platform](https://img.shields.io/badge/platform-Xamarin.Android-brightgreen.svg)](https://www.xamarin.com/)
[![API](https://img.shields.io/badge/API-10%2B-orange.svg?style=flat)](https://android-arsenal.com/api?level=10s)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT)

### Namespace: Xama.JTPorts.MaterialTextField

Google Material Design - Xamarin.Android Text Field.

This is a ported build, converted from Java to C# for use with the Xamarin MonoFramework. There are only minor additions to the original library, these include the removal of various deprecated dependancies.

<br>

![!gif](https://github.com/DigitalSa1nt/Xama.JTPorts.MaterialTextField/blob/master/images/20190216_225505.gif?raw=true)

<br>

# How to Install

![NugetIcon](https://raw.githubusercontent.com/DigitalSa1nt/Xama.JTPorts.MaterialTextField/master/images/nugetIcon.png)

Simply add the following [Nuget Package](https://www.nuget.org/packages/Xama.JTPorts.MaterialTextField/1.0.2) to your Xamarin.Android App, and ensure you have the latest AndroidX dependancies installed, if you don't the code will tell you which ones are missing.

- Install using Package Manager:
> Install-Package Xama.JTPorts.MaterialTextField -Version 1.0.2

- Install .NET CLI:
> dotnet add package Xama.JTPorts.MaterialTextField --version 1.0.2

# Basic Usage

Create the control inside of your layout and wrap it around a single EditText control.

```cs
<Xama.JTPorts.MaterialTextField.MaterialTextField
        android:id="@+id/mtvInput"
        android:layout_marginTop="15dp"
        android:layout_width="300dp"
        android:layout_height="wrap_content"
        app:mtf_labelColor="@android:color/white"
        app:mtf_image="@drawable/iconemail">

        <EditText
            android:layout_width="match_parent"
            android:layout_height="wrap_content"
            android:hint="Email Address"
            android:inputType="textEmailAddress"
            android:textColor="@android:color/black"
            android:textSize="15sp" />

    </Xama.JTPorts.MaterialTextField.MaterialTextField>  
```

# Available Attributes

You can supply the following attributes:

```cs
    <attr name="mtf_cardCollapsedHeight" format="dimension"/>
    <attr name="mtf_labelColor" format="color"/>
    <attr name="mtf_image" format="reference"/>
    <attr name="mtf_hasFocus" format="boolean"/>
    <attr name="mtf_animationDuration" format="integer"/>
    <attr name="mtf_openKeyboardOnFocus" format="boolean"/>
    <attr name="mtf_backgroundColor" format="color"/>
```

These mostly have default values should you decide not to supply them

# Support 💎
-----

If you want to support the work that I do and you find this library useful? Support it by joining [**stargazers**](https://github.com/DigitalSa1nt/Xama.JTPorts.MaterialTextField/stargazers) for this repository ⭐️
<br/>

or alternatively if you want to you can also buy me a coffee.

<a href="https://www.buymeacoffee.com/JTT" target="_blank"><img src="https://cdn.buymeacoffee.com/buttons/default-red.png" alt="Buy Me A Coffee" tyle="height: 41px !important;width: 174px !important;box-shadow: 0px 3px 2px 0px rgba(190, 190, 190, 0.5) !important;-webkit-box-shadow: 0px 3px 2px 0px rgba(190, 190, 190, 0.5) !important;" ></a>

_You know, only if you want to._
