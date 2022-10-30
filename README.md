# Erste Schritte mir F# [Link](https://learn.microsoft.com/de-de/training/modules/fsharp-first-steps/2-what-is-fsharp)


```
dotnet new console -lang F# -o Erste-Schritte-mit-F# -f net6.0
```


## F# Interactive

F# Interactive ist in das .NET SDK integriert und kann mit dem .NET CLI-Befehl dotnet fsi gestartet werden.

```Shell
dotnet fsi
```

```fsharp
> #help;;
> #q;;
```


```fsharp
> printfn "Hello World!";;
Hello World!
val it: unit = ()
```
