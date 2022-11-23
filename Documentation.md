# XMLImportExport

- [1 Introduction](#1-introduction)
- [2 API](#2-api)
  - [2.1 DuplicateNameException (Exception)](#21-duplicatenameexception-exception)
  - [2.2 ExportThread (abstract class)](#22-exportthread-abstract-class)
  - [2.3 ImportThread (abstract class)](#23-importthread-abstract-class)
  - [2.4 ThreadProcessViewer (Window)](#24-threadprocessviewer-window)
  - [2.5 ThreadProcessViewerConfig](#25-threadprocessviewerconfig)
  - [2.6 ThreadProcessViewerStyle](#26-threadprocessviewerstyle)
  - [2.7 ThreadStepper (abstract class)](#27-threadstepper-abstract-class)
  - [2.8 XMLExport (static class)](#28-xmlexport-static-class)

## 1 Introduction

The `XMLImportExport` library offers utility methods for the export of whole database tables into XML files as well as a WPF-Window for viewing the export or import progress.

## 2 API

### 2.1 DuplicateNameException (Exception)

> Indicates, that a name of an entry is duplicated.

Accessible Interface:

```c#
public DuplicateNameException(string message, Exception innerException)
```

### 2.2 ExportThread (abstract class)

> A default `ThreadStepper` for an export of data.

Accessible Interface:

```c#
protected ExportThread(string fileDestination, string exportFailedStep, string exportFailedMessage)
public override void run()
protected abstract void runExport()
```

### 2.3 ImportThread (abstract class)

> A default `ThreadStepper` for an import of data supporting some utility methods.

Accessible Interface:

```c#
protected ImportThread(string fileSource, string importFailedStep, string importFailedMessage, string formatExceptionHeader, string dbConstraintExceptionHeader, Dictionary<string, string> dbConstraintMessages)
protected abstract void runImport()

protected TimeSpan ParseManualTime(string timestring)
protected bool CheckVersionImportable(string maximumVersion, string actualVersion)
protected FormatException AssembleFormatException(string message, Exception innerException)
protected FormatException ParseDBConstraintException(string message, Exception e)
```

### 2.4 ThreadProcessViewer (Window)

> A dialog with a header, progress bar and closing button that guides a `ThreadStepper` through it's process execution.
>
> The dialog shows the current progress in percent including the message broadcasted from the `ThreadStepper` and offers events to catch whenever the process finishes either successful (`ProcessSucceeded`) or not (`ProcessFailed`).
>
> The appearance of the dialog can be configured using the `ThreadProcessViewerConfig`. The dialog should be shown via the `Window.ShowDialog`-method.

Accessible Interface:

```c#
public delegate void ProcessFailedHandler(string message)
public event ProcessFailedHandler ProcessFailed
public delegate void ProcessSucceededHandler()
public event ProcessSucceededHandler ProcessSucceeded

public ThreadProcessViewer(ThreadStepper stepper, ThreadProcessViewerConfig config)
```

### 2.5 ThreadProcessViewerConfig

> Configuration for a `ThreadProcessViewer`

Accessible Interface:

```c#
public string Title
public string FinishButtonCaption
public ThreadProcessViewerStyle Style
```

### 2.6 ThreadProcessViewerStyle

> Configurable style information for a `ThreadProcessViewer`

Accessible Interface:

```c#
public SolidColorBrush HeaderBackground
public SolidColorBrush Background
public FontFamily HeaderFontFamily
public FontFamily FontFamily
public int HeaderFontSize
public int FontSize
public SolidColorBrush HeaderBorderColor
public int HeaderBorderThickness
```

### 2.7 ThreadStepper (abstract class)

> The `ThreadStepper` runs a specific process using `run` that is guided by progress events using `CallStep(float, string)` and `CallFinished(Exception)`.
>
> The events triggered by those methods can be accessed using `StepDone` and `Finished` and can be visualized using a `ThreadProcessViewer`.

Accessible Interface:

```c#
public delegate void StepHandler(float percentage, string message)
public event StepHandler StepDone
public delegate void ResultHandler(Exception result)
public event ResultHandler Finished

protected void CallStep(float percentage, string message)
protected void CallFinished(Exception exc = null)
public abstract void run()
```

### 2.8 XMLExport (static class)

> Provides helper methods for the XML export.

Accessible Interface:

```c#
public static XElement ExportDataFromTable<DB, T>(DB dbConnection, List<string> columns, Dictionary<string, Func<T, object>> additionalComputedProperties = null, Func<T, bool> filter = null, Dictionary<string, Func<object, object>> mappings = null, Func<T, List<XElement>> computeChildren = null)
```
