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

Simply add the following [Nuget Package](https://www.nuget.org/packages/Xama.JTPorts.MaterialTextField/1.0.0) to your Xamarin.Android App, and ensure you have the latest AndroidX dependancies installed, if you don't the code will tell you which ones are missing.

- Install using Package Manager:
> Install-Package Xama.JTPorts.MaterialTextField -Version 1.0.1

- Install .NET CLI:
> dotnet add package Xama.JTPorts.MaterialTextField --version 1.0.1

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

# Useful?
<a href="https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=PFBEH42KW5P84" method="post" target="_top"><img src="https://camo.githubusercontent.com/b8efed595794b7c415163a48f4e4a07771b20abe/68747470733a2f2f7777772e6275796d6561636f666665652e636f6d2f6173736574732f696d672f637573746f6d5f696d616765732f707572706c655f696d672e706e67" alt="Buy Me A Coffee" style="height: auto !important;width: auto !important;" ></a>

 _You know, only if you want to_
