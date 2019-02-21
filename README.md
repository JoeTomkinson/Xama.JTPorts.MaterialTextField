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

At the moment, until I create the nuget package you would need to download the src code, compile and build it, then simply reference the DLL in your Xamarin.Android Project. Or alternatively you could add the project as a project within your solution if you plan on adjusting any of the code for your own uses.

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
<a href="https://www.buymeacoffee.com/digitalsa1nt" target="_blank"><img src="https://www.buymeacoffee.com/assets/img/custom_images/purple_img.png" alt="Buy Me A Coffee" style="height: auto !important;width: auto !important;" ></a>

 _You know, only if you want to_
