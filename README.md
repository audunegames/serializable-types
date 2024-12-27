# Audune Serializable Types

[![openupm](https://img.shields.io/npm/v/com.audune.utils.types?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.audune.utils.types/)

Enables Unity to serialize a reference to a C# type in the editor.

## Features

* A `SerializableType` class to use in place of the C# `Type` class, but with the benefit of the data being serialized in Unity.
* Some type-related extension methods.

## Installation

### Requirements

This package depends on the following packages:

* [Unity IMGUI Editor Utilities](https://openupm.com/packages/com.audune.utils.unityeditor/), version **2.0.5** or higher.

If you're installing the required packages from the [OpenUPM registry](https://openupm.com/), make sure to add a scoped registry with the URL `https://package.openupm.com` and the required scopes before installing the packages.

### Installing from the OpenUPM registry

To install this package as a package from the OpenUPM registry in the Unity Editor, use the following steps:

* In the Unity editor, navigate to **Edit › Project Settings... › Package Manager**.
* Add the following Scoped Registry, or edit the existing OpenUPM entry to include the new Scope:

```
Name:     package.openupm.com
URL:      https://package.openupm.com
Scope(s): com.audune.utils.types
```

* Navigate to **Window › Package Manager**.
* Click the **+** icon and click **Add package by name...**
* Enter the following name in the corresponding field and click **Add**:

```
com.audune.utils.types
```

### Installing as a Git package

To install this package as a Git package in the Unity Editor, use the following steps:

* In the Unity editor, navigate to **Window › Package Manager**.
* Click the **+** icon and click **Add package from git URL...**
* Enter the following URL in the URL field and click **Add**:

```
https://github.com/audunegames/serializable-types.git
```

## Contributing

Contributions to this package are more than welcome! Contributing can be done by making a pull request with your updated code.

## License

This package is licensed under the GNU LGPL 3.0 license. See `LICENSE.txt` for more information.
