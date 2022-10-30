// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open first_steps_with_fsharp

[<EntryPoint>]
let main argv =

    printfn "# program ---------------------------------------------------------------------------------"

    loops.main |> ignore

    if_then_else.main |> ignore

    create_and_architect_with_functions.exercise |> ignore

    store_and_apply_operations_on_list_data.list_main |> ignore
    store_and_apply_operations_on_list_data.list_exercise|> ignore

    //
    0
